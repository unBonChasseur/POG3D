using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_menuUI;

    [SerializeField]
    private GameObject m_inGameUI;

    [SerializeField]
    private GameObject m_afterGameUI;

    void Start()
    {
        m_menuUI = GameObject.FindGameObjectWithTag("MenuUI");
        m_inGameUI = GameObject.FindGameObjectWithTag("InGameUI");
        m_afterGameUI = GameObject.FindGameObjectWithTag("AfterGameUI");

        ShowMenuUI();
    }

    public void ShowMenuUI()
    {
        m_menuUI.SetActive(true);
        m_inGameUI.SetActive(false);
        m_afterGameUI.SetActive(false);
    }

    public void ShowInGameUI()
    {
        m_menuUI.SetActive(false);
        m_inGameUI.SetActive(true);
        m_afterGameUI.SetActive(false);
    }


    public void ShowAfterGameUI()
    {
        m_menuUI.SetActive(false);
        m_inGameUI.SetActive(false);
        m_afterGameUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
