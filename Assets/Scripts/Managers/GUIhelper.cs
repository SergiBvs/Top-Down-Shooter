using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIhelper : MonoBehaviour
{
    //GUI

    public TextMeshProUGUI m_AmmoText;
    public TextMeshProUGUI m_HealthText;
    public Slider m_Slider;
    public Gradient m_Gradient;
    public Image fill;

    //PANELS

    public GameObject m_GameOverPanel;
    public GameObject m_PausePanel;
    public GameObject m_GUIpanel;
    public GameObject m_ReloadPanel;

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
                if (GameManager.instance.m_GameIsPaused)
                {
                    ResumeGame();
                }
                else if (!GameManager.instance.m_GameIsPaused)
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
        GameManager.instance.m_GameIsPaused = false;
    }

    public void PauseGame()
    {
        m_PausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.m_GameIsPaused = true;
    }

    public void RestartLevel()
    {
        //GameManager.instance.m_Telon.SetTrigger("Telon");
        //StartCoroutine(GameManager.instance.TelonWait(SceneManager.GetActiveScene().buildIndex));

        m_PausePanel.SetActive(false);
        GameManager.instance.m_GameIsPaused = false;
        Time.timeScale = 1f;
        GameManager.instance.m_IsGameOverPanelOn = false;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
        GameManager.instance.SetMaxHealth(100);
    }

    public void MainMenu()
    {
        //telon o lo que sea
        SceneManager.LoadScene(0);
    }
}
