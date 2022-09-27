using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private bool m_hasBegin;

    [SerializeField]
    GameObject m_ball;

    Camera m_mainCamera;

    [SerializeField]
    TextMeshProUGUI TxtCountdown;


    void Start()
    {
        m_hasBegin = false;
        m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (!ball && HasTwoPlayers())
        {
            ball = Instantiate(m_ball, Vector3.zero, Quaternion.identity);
            ball.GetComponent<NetworkObject>().Spawn();
            if (!m_hasBegin)
            {
                m_hasBegin = true;
                ball.GetComponent<Balle>().InvokeBall(10);
                StartCoroutine(Countdown(10));
            }
            else
            {
                ball.GetComponent<Balle>().InvokeBall(3);
                StartCoroutine(Countdown(3));
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

    IEnumerator Countdown(int seconds)
    {
        while (seconds >= 0)
        {
            if (seconds > 0)
            {
                seconds--;
                TxtCountdown.text = seconds.ToString();
                
            }
            else if (seconds == 0)
            {
                TxtCountdown.text = "";
            }

            yield return new WaitForSeconds(1);
        }
    }
}
