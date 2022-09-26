using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float m_movementSpeed;

    private GameObject m_objCamera;
    private Camera m_mainCamera = null;

    private Rigidbody m_rigidbody;

    private int sens;

    void Start()
    {
        m_objCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_mainCamera = m_objCamera.GetComponent<Camera>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerControl();
        
    }

    public void PressKeyUp()
    {
        Vector3 top = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + gameObject.transform.lossyScale.z / 2);
        Vector3 top_screen = m_mainCamera.WorldToScreenPoint(top);
        if (top_screen.y < Screen.height)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + (m_movementSpeed * Time.deltaTime));
            //velocity.z = m_movementSpeed;
            sens = 1;
        }
    }

    public void PressKeyDown()
    {
        Vector3 bottom = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - gameObject.transform.lossyScale.z / 2);
        Vector3 bottom_screen = m_mainCamera.WorldToScreenPoint(bottom);
        if (bottom_screen.y > 0)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - (m_movementSpeed * Time.deltaTime));
            //velocity.z = - m_movementSpeed;
            sens = -1;
        }
    }

    public void ResetSens()
    {
        sens = 0;
    }
    public void PlayerControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 top = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + gameObject.transform.lossyScale.z / 2);
            Vector3 top_screen = m_mainCamera.WorldToScreenPoint(top);
            if (top_screen.y < Screen.height)
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
            if (bottom_screen.y > 0)
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
