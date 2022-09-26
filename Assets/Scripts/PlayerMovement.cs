using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float m_movementSpeed;

    private GameObject m_objCamera;
    private Camera m_mainCamera = null;
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    private Rigidbody m_rigidbody;

    private int sens;

    void Start()
    {
        m_objCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_mainCamera = m_objCamera.GetComponent<Camera>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();


        if (IsHost)
        {
            transform.position = m_mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * .1f, Screen.height / 2, m_mainCamera.transform.position.y));
        }
        else
        {
            transform.position = m_mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width * .1f, Screen.height / 2, m_mainCamera.transform.position.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            PlayerControl();

            if (IsServer)
            {
                Position.Value = transform.position;
            }
            else
            {
                SubmitPositionRequestServerRpc(transform.position);
            }

        }
        else
        {
            transform.position = Position.Value;
        }
        
        
    }
    
    [ServerRpc]
    void SubmitPositionRequestServerRpc(Vector3 p_vector)
    {
        Position.Value = p_vector;
    }

    public void PlayerControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 top = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + gameObject.transform.lossyScale.z / 2);
            Vector3 top_screen = m_mainCamera.WorldToScreenPoint(top);
            if (top_screen.y < Screen.height - Screen.height*.03f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + (m_movementSpeed * Time.deltaTime));
                //velocity.z = m_movementSpeed;
                sens = 1;
            }
            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 bottom = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - gameObject.transform.lossyScale.z / 2);
            Vector3 bottom_screen = m_mainCamera.WorldToScreenPoint(bottom);
            if (bottom_screen.y > Screen.height * .03f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - (m_movementSpeed * Time.deltaTime));
                //velocity.z = - m_movementSpeed;
                sens = -1; 
            }
            
        }
        else
        {
            sens = 0;
        }

    }


    public int getVelocity()
    {
        return sens;
    }

}
