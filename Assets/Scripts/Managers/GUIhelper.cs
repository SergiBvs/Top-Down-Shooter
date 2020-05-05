using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIhelper : MonoBehaviour
{
    //GUI

    public TextMeshProUGUI m_AmmoText;
    public Slider m_Slider;
    public Gradient m_Gradient;
    public Image fill;

    //PANELS

    public GameObject m_GameOverPanel;
    public GameObject m_PausePanel;
    public bool m_GameIsPaused;

    void Start()
    {
        GameManager.instance.ReassignObjs();
    }


    void Update()
    {
        if (GameManager.instance.m_IsGameOverPanelOn == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (m_GameIsPaused)
                {
                    ResumeGame();
                }
                else if (!m_GameIsPaused)
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        m_PausePanel.SetActive(false);
        Time.timeScale = 1f;
        m_GameIsPaused = false;
    }

    public void PauseGame()
    {
        m_PausePanel.SetActive(true);
        Time.timeScale = 0f;
        m_GameIsPaused = true;

    }

    public void RestartLevel()
    {
        //GameManager.instance.m_Telon.SetTrigger("Telon");
        //StartCoroutine(GameManager.instance.TelonWait(SceneManager.GetActiveScene().buildIndex));
        m_PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.m_IsGameOverPanelOn = false;
    }
}
