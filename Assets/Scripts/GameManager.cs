using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    private bool m_hasBegin;

    [SerializeField]
    GameObject m_ball;

    Camera m_mainCamera;

    [SerializeField]
    TextMeshProUGUI m_countdownText;

    [SerializeField]
    GameObject m_scoreManager;
    private ScoreManager scriptScore;

    [SerializeField]
    GameObject m_UIManager;
    private UIManager scriptUI;

    private NetworkVariable<int> m_secondes = new NetworkVariable<int>();

    void Start()
    {
        m_hasBegin = false;
        m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        scriptScore = m_scoreManager.GetComponent<ScoreManager>();
        scriptUI = m_UIManager.GetComponent<UIManager>();
        m_secondes.Value = 10;
    }

    void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (!ball && HasTwoPlayers())
        {
            if (scriptScore.getJ2Score() < scriptScore.getMaxValue() && scriptScore.getJ1Score() < scriptScore.getMaxValue())
            {
                Debug.Log(scriptScore.getGameOverValue());
                ball = Instantiate(m_ball, Vector3.zero, Quaternion.identity);
                if(IsServer)
                    ball.GetComponent<NetworkObject>().Spawn();
                if (!m_hasBegin)
                {
                    m_hasBegin = true;
                }
                else
                {
                    m_secondes.Value = 3;
                }
                ball.GetComponent<Balle>().InvokeBall(m_secondes.Value);
                StartCoroutine(Countdown());
            }
            else if (scriptScore.getGameOverValue() == 3) 
            {
                if (IsClient)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else if (!scriptUI.IsAfterGameDisplayed())
            {
                scriptUI.ShowAfterGameUI();
                m_hasBegin = false;
            }
        }

        if (IsClient && (scriptScore.getJ2Score() >= scriptScore.getMaxValue() || scriptScore.getJ1Score() >= scriptScore.getMaxValue()))
        {
            Debug.Log("ici");
            scriptUI.ShowAfterGameUI();
            m_hasBegin = false;
        }

        

        if (m_secondes.Value >= 1)
            m_countdownText.text = m_secondes.Value.ToString();
        else
            m_countdownText.text = "";
    }
    
    private bool HasTwoPlayers()
    {
        if (IsHost)
        {
            return NetworkManager.Singleton.ConnectedClientsList.Count == 2;
        }
        else if(IsClient)
        {
            return true;
        }
        return false;
    }

    private IEnumerator Countdown()
    {
        while (m_secondes.Value >= 1)
        {
            if (m_secondes.Value > 1)
            {
                m_secondes.Value--;
                FindObjectOfType<AudioManager>().ChangeVolume("PongStart", 0.3f);
                FindObjectOfType<AudioManager>().Play("PongStart");
            }
            else if (m_secondes.Value == 1)
            {
                m_secondes.Value--;
                FindObjectOfType<AudioManager>().ChangeVolume("PongStart", .5f);
                FindObjectOfType<AudioManager>().Play("PongStart");
            }



            yield return new WaitForSeconds(1);
        }
    }

    public void quit()
    {
        Application.Quit();
    }
}
