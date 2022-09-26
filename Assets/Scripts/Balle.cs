using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
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

    //private Rigidbody m_rigidbody;
    private bool hit;
    private float accelTimer;

    private Vector3 direction;



    void Start()
    {
        m_objCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_mainCamera = m_objCamera.GetComponent<Camera>();
       // m_rigidbody = gameObject.GetComponent<Rigidbody>();

        Invoke("LaunchBall", 2);
        accelTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(hit)
        {
            accelTimer += Time.deltaTime;
            //Debug.Log(accelTimer);
            gameObject.transform.Translate(direction * Mathf.Lerp(m_movementSpeed * 2, m_movementSpeed, accelTimer ) * Time.deltaTime);
        }
        if (hit && accelTimer >= 2)
        {
            //Debug.LogError("ok");
            hit = false;
            accelTimer = 0;
        }
        else
        {
           
        }*/
        gameObject.transform.Translate(direction * (m_movementSpeed) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.CompareTag("Player"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 reflectedDirection = Vector3.Reflect(direction, contact.normal);
            int sens = collision.collider.GetComponent<Player>().getVelocity();
            if (sens == 1)
            {
                if (direction.z < 0)
                {
                    int straight = Random.Range(1, 5);
                    direction = reflectedDirection;
                    if (straight == 1)
                        direction.z = 0;
                    else
                    {
                        direction.z = direction.z / Random.Range(1.0f, 2.0f);
                        direction.z = Mathf.Clamp(direction.z, m_minAngle_z, m_maxAngle_z);
                    }
                }
                else if (direction.z > 0)
                {
                    direction = reflectedDirection;
                    direction.z = direction.z * Random.Range(1.0f, 10.0f);
                    direction.z = Mathf.Clamp(direction.z, m_minAngle_z, m_maxAngle_z);
                }
                else if (direction.z == 0)
                {
                    direction = new Vector3(sign(reflectedDirection.x)* 1 , 0, randomSign() * Random.Range(0.0f,1.0f));
                }
                else
                {
                    direction = reflectedDirection;
                }
            }
            else if (sens == -1)
            {
                if(direction.z > 0)
                {
                    //Debug.Log("Mult");
                    direction = reflectedDirection;
                    direction.z = direction.z * Random.Range(1.0f, 10.0f);
                    direction.z = Mathf.Clamp(direction.z, m_minAngle_z, m_maxAngle_z);
                }
                else if(direction.z < 0)
                {
                    int straight = Random.Range(1, 5);
                    direction = reflectedDirection;
                    if (straight == 1)
                        direction.z = 0;
                    else
                    {
                        direction.z = direction.z / Random.Range(1.0f, 2.0f);
                        direction.z = Mathf.Clamp(direction.z, m_minAngle_z, m_maxAngle_z);
                    }
                    
                }
                else if(direction.z == 0)
                {
                    direction = new Vector3(sign(reflectedDirection.x) * 1, 0, randomSign() *  Random.Range(0.0f, 1.0f));
                }
                else
                {
                    direction = reflectedDirection;
                }
            }
            else
            {

                Debug.Log($"1: {direction}");
                if (direction.z != 0.0f)
                    direction = reflectedDirection;
                else
                {
                    direction = new Vector3(sign(reflectedDirection.x) * 1, 0, randomSign() * Random.Range(0.0f, 1.0f));
                }
                  

                Debug.Log($"2: {direction}");
            }
            
            hit = true; 
        }

        if (collision.collider.CompareTag("Wall"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 reflectedDirection = Vector3.Reflect(direction, contact.normal);
            direction = reflectedDirection;
        }
 
    }

    public void LaunchBall()
    {
        direction = new Vector3(-1,0,-1); 
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
        int rand = Random.Range(0, 1);
        if (rand == 0)
            return -1;
        return 1;
    }

}
