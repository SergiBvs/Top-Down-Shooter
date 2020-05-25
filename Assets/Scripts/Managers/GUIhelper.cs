using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIhelper : MonoBehaviour
{
    //GUI
    [Header("Health GUI")]
    public TextMeshProUGUI m_HealthText;
    public ParticleSystem m_healthParticles;
    public Slider m_Slider;
    public Image fill;
    public ParticleSystem.MainModule m_psmain;
    public Gradient m_Gradient;

    [Header("Other GUI")]
    public TextMeshProUGUI m_AmmoText;
    public TMP_Text CoinText;

    [Header("PickUp GUI")]
    public GameObject m_CoinPickupText;
    public GameObject m_AmmoPickupText;
    public GameObject m_PlayerPickupCanvas;
    

    //PANELS
    [Header("Panels")]
    public GameObject m_GameOverPanel;
    public GameObject m_PausePanel;
    public GameObject m_GUIPanel;
    public GameObject m_ReloadPanel;

    [Header("Audio")]
    public AudioSource m_AS;

    public int m_NumberOfEnemies;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        GameManager.instance.ReassignObjs();
        GameManager.instance.m_GameIsPaused = false;
        SoundManager.instance.ReassignObjs();
        m_psmain = m_healthParticles.main;
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

        if(Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(8);
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
        GameManager.instance.m_HasRestarted = true;
        GameManager.instance.m_Currency = GameManager.instance.m_InitialCurrency;
        m_PausePanel.SetActive(false);
        GameManager.instance.m_GameIsPaused = false;
        Time.timeScale = 1f;
        GameManager.instance.m_IsGameOverPanelOn = false;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }

    public void RestartArena()
    {
        GameManager.instance.m_HasRestarted = true;
        m_PausePanel.SetActive(false);
        GameManager.instance.m_GameIsPaused = false;
        Time.timeScale = 1f;
        GameManager.instance.m_IsGameOverPanelOn = false;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }

    public void MainMenu()
    {
        //reiniciar todo a 0????
        GameManager.instance.m_GameIsPaused = false;
        Time.timeScale = 1f;
        GameManager.instance.m_IsGameOverPanelOn = false;
        //telon o lo que sea
        SceneManager.LoadScene(1);
    }
}
