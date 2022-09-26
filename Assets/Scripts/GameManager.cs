using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private bool m_hasBegin;

    [SerializeField]
    GameObject m_ball;

    Camera m_mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        m_hasBegin = false;
        m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (!ball && HasTwoPlayers())
        {
            ball = Instantiate(m_ball, Vector3.zero, Quaternion.identity);
            if (!m_hasBegin)
            {
                m_hasBegin = true;
                ball.GetComponent<Balle>().InvokeBall(2);
                Debug.Log("begin");
            }
            else
            {
                ball.GetComponent<Balle>().InvokeBall(3);
                Debug.Log("next");
            }
        }
    }
    
    private bool HasTwoPlayers()
    {
        if (IsHost)
        {
            return NetworkManager.Singleton.ConnectedClientsList.Count == 2;
        }
        else if(IsClient && !IsServer)
        {
            return true;
        }
        return false;
    }

}
