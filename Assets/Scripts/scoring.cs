using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class scoring : NetworkBehaviour
{
    [SerializeField]
    private GameObject m_ScoreManager;

    [SerializeField]
    private string side;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            if (IsServer)
            {
                if (side == "Right")
                {
                    m_ScoreManager.GetComponent<ScoreManager>().OnWallRightHit();
                }
                else if (side == "Left")
                {
                    m_ScoreManager.GetComponent<ScoreManager>().OnWallLeftHit();
                }

                //Destroy ball  
                collision.collider.GetComponent<NetworkObject>().Despawn();
                Destroy(collision.collider);
            }
        }
    }
}
