using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Netcode;
using UnityEngine;

public class Balle : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float m_movementSpeed;

    [SerializeField]
    private float m_accelOnHit;

    [SerializeField]
    private float m_maxAngle_z;
    [SerializeField]
    private float m_minAngle_z;

    private GameObject m_objCamera;
    private Camera m_mainCamera;

    private Vector3 m_direction;

    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();


    void Start()
    {
        m_objCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_mainCamera = m_objCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            transform.Translate(m_direction.normalized * m_movementSpeed * Time.deltaTime);

            if (IsServer)
            {
                //Vector3 posOnScreen = m_mainCamera.WorldToScreenPoint(transform.position);
                //if(posOnScreen.x > Screen.width)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Pong1");
            ContactPoint contact = collision.contacts[0];
            Vector3 reflectedDirection = Vector3.Reflect(m_direction, contact.normal);
            int sens = collision.collider.GetComponent<PlayerMovement>().getVelocity();
            if (sens == 1)
            {
                if (m_direction.z < 0)
                {
                    int straight = Random.Range(1, 5);
                    m_direction = reflectedDirection;
                    if (straight == 1)
                        m_direction.z = 0;
                    else
                    {
                        m_direction.z = m_direction.z / Random.Range(1.0f, 2.0f);
                        m_direction.z = Mathf.Clamp(m_direction.z, m_minAngle_z, m_maxAngle_z);
                    }
                }
                else if (m_direction.z > 0)
                {
                    m_direction = reflectedDirection;
                    m_direction.z = m_direction.z * Random.Range(1.0f, 10.0f);
                    m_direction.z = Mathf.Clamp(m_direction.z, m_minAngle_z, m_maxAngle_z);
                }
                else if (m_direction.z == 0)
                {
                    m_direction = new Vector3(sign(reflectedDirection.x)* 1 , 0, randomSign() * Random.Range(0.0f,1.0f));
                }
                else
                {
                    m_direction = reflectedDirection;
                }
            }
            else if (sens == -1)
            {
                if(m_direction.z > 0)
                {
                    //Debug.Log("Mult");
                    m_direction = reflectedDirection;
                    m_direction.z = m_direction.z * Random.Range(1.0f, 10.0f);
                    m_direction.z = Mathf.Clamp(m_direction.z, m_minAngle_z, m_maxAngle_z);
                }
                else if(m_direction.z < 0)
                {
                    int straight = Random.Range(1, 5);
                    m_direction = reflectedDirection;
                    if (straight == 1)
                        m_direction.z = 0;
                    else
                    {
                        m_direction.z = m_direction.z / Random.Range(1.0f, 2.0f);
                        m_direction.z = Mathf.Clamp(m_direction.z, m_minAngle_z, m_maxAngle_z);
                    }
                    
                }
                else if(m_direction.z == 0)
                {
                    m_direction = new Vector3(sign(reflectedDirection.x) * 1, 0, randomSign() *  Random.Range(0.0f, 1.0f));
                }
                else
                {
                    m_direction = reflectedDirection;
                }
            }
            else
            {

                Debug.Log($"1: {m_direction}");
                if (m_direction.z != 0.0f)
                    m_direction = reflectedDirection;
                else
                {
                    m_direction = new Vector3(sign(reflectedDirection.x) * 1, 0, randomSign() * Random.Range(0.0f, 1.0f));
                }
                  

                Debug.Log($"2: {m_direction}");
            }
        }

        if (collision.collider.CompareTag("Wall"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 reflectedDirection = Vector3.Reflect(m_direction, contact.normal);
            m_direction = reflectedDirection;
        }
 
    }

    public void InvokeBall(float p_time)
    {
        Invoke("LaunchBall", p_time);
    }

    public void LaunchBall()
    {
        m_direction = new Vector3(randomSign(), 0, randomSign());
    }

    private int sign(float value)
    {
        if (value < 0)
            return -1;
        else if (value > 0)
            return 1;
        return 0;
    }

    private int randomSign()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            return -1;
        return 1;
    }

}
