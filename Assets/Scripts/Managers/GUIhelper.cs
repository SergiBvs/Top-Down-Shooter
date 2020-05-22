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


    void Start()
    {
        GameManager.instance.ReassignObjs();
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
            GameManager.instance.LoadScene(7);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.instance.LoadScene(2);
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

    public void NextLevel()
    {
        GameManager.instance.ChangeMusic(GameManager.instance.MManager.m_GameMusic[0]);
        GameManager.instance.gameObject.GetComponent<AudioHighPassFilter>().enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadBase()
    {
        GameManager.instance.m_NeedsSpawnPosition = true;
        GameManager.instance.m_AlreadyInElevator = true;
        GameManager.instance.m_SpawnPosition = new Vector2(45, -3);
        SceneManager.LoadScene(3);
    }
}
