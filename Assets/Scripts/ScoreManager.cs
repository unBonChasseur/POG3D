using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ScoreManager : NetworkBehaviour
{

    private NetworkVariable<int> scoreJ1 = new NetworkVariable<int>();
    private NetworkVariable<int> scoreJ2 = new NetworkVariable<int>();

    [SerializeField]
    private TextMeshProUGUI txt_scorePlayer1;
    [SerializeField]
    private TextMeshProUGUI txt_scorePlayer2;

    private void Update()
    {
        txt_scorePlayer1.text = scoreJ1.Value.ToString();
        txt_scorePlayer2.text = scoreJ2.Value.ToString();
    }

    public void OnWallRightHit()
    {
        scoreJ1.Value++;
    }

    public void OnWallLeftHit()
    {
        scoreJ2.Value++;
    }
}
