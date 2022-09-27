using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ScoreManager : NetworkBehaviour
{

    private NetworkVariable<int> scoreJ1 = new NetworkVariable<int>();
    private NetworkVariable<int> scoreJ2 = new NetworkVariable<int>();
    private NetworkVariable<int> gameOver = new NetworkVariable<int>();

    private bool ended; 

    private int maxValue = 11;

    [SerializeField]
    private TextMeshProUGUI txt_scorePlayer1;
    [SerializeField]
    private TextMeshProUGUI txt_scorePlayer2;

    [SerializeField]
    private TextMeshProUGUI txt_finalScorePlayer1;
    [SerializeField]            
    private TextMeshProUGUI txt_finalScorePlayer2;

    public override void OnNetworkSpawn()
    {
        gameOver.Value = 0;
        ended = false;
    }

    private void Update()
    {
        txt_scorePlayer1.text = scoreJ1.Value.ToString();
        txt_scorePlayer2.text = scoreJ2.Value.ToString();
        txt_finalScorePlayer1.text = scoreJ1.Value.ToString();
        txt_finalScorePlayer2.text = scoreJ2.Value.ToString();

    }

    public int getGameOverValue()
    {
        return gameOver.Value;
    }

    public void OnWallRightHit()
    {
        if(scoreJ1.Value < maxValue)
            scoreJ1.Value++;
        if (scoreJ1.Value >= maxValue)
        {
            txt_finalScorePlayer1.text = scoreJ1.Value.ToString();
            txt_finalScorePlayer2.text = scoreJ2.Value.ToString();
            if (IsServer) gameOver.Value = 1;
            ended = true;
        }
    }

    public void OnWallLeftHit()
    {
        if(scoreJ2.Value < maxValue)
            scoreJ2.Value++;
        if(scoreJ2.Value >= maxValue)
        {
            txt_finalScorePlayer1.text = scoreJ1.Value.ToString();
            txt_finalScorePlayer2.text = scoreJ2.Value.ToString();
            if (IsServer)gameOver.Value = 2;
            ended = true;
            
        }
    }

    public void ResetValues()
    {
        if (IsServer)
        {
            scoreJ1.Value = 0;
            scoreJ2.Value = 0;
            gameOver.Value = 0;
        }
        else if(IsOwner)
            SubmitValueResetServerRpc();
    }


    [ServerRpc]
    void SubmitValueResetServerRpc()
    {
        scoreJ1.Value = 0;
        scoreJ2.Value = 0;
        gameOver.Value = 0;
    }

    public void Reset()
    {
        if (IsServer)
        {
            gameOver.Value = 3; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    public bool getEnded()
    {
        return ended;
    }

    public int getJ1Score()
    {
        return scoreJ1.Value;
    }

    public int getJ2Score()
    {
        return scoreJ2.Value;
    }

    public int getMaxValue()
    {
        return maxValue;
    }
}
