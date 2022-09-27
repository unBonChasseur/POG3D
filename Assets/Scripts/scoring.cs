using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class scoring : MonoBehaviour
{
    [SerializeField]
    private string side;

    [SerializeField]
    private TextMeshProUGUI txt_scorePlayer1;

    private int scorePlayer1;
    [SerializeField]
    private TextMeshProUGUI txt_scorePlayer2;

    private int scorePlayer2;

    private void Start()
    {
        scorePlayer1 = scorePlayer2 = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            if (side == "Right")
            {
                scorePlayer1++;
                txt_scorePlayer1.text = scorePlayer1.ToString();
            }
            else if (side == "Left")
            {
                scorePlayer2++;
                txt_scorePlayer2.text = scorePlayer2.ToString();
            }
            //Destroy ball 
            collision.collider.GetComponent<NetworkObject>().Despawn();
            Destroy(collision.collider);
        }
    }
}
