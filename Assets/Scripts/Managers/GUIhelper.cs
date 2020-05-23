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
        //GameManager.instance.m_Telon.SetTrigger("Telon");
        //StartCoroutine(GameManager.instance.TelonWait(SceneManager.GetActiveScene().buildIndex));

        //hay que reiniciar municion y dinero a los valores del principio del nivel!!!!

        m_PausePanel.SetActive(false);
        GameManager.instance.m_GameIsPaused = false;
        Time.timeScale = 1f;
        GameManager.instance.m_IsGameOverPanelOn = false;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
        GameManager.instance.SetMaxHealth(100);
    }

    public void MainMenu()
    {
        //reiniciar todo a 0????

        //telon o lo que sea
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        //no reiniciar ni dinero ni municion pero si vida (hay que ser justos)
        GameManager.instance.ChangeMusic(MusicManager.instance.m_GameMusic[0]);
        MusicManager.instance.gameObject.GetComponent<AudioHighPassFilter>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadBase()
    {
        GameManager.instance.m_NeedsSpawnPosition = true;
        GameManager.instance.m_AlreadyInElevator = true;
        GameManager.instance.m_SpawnPosition = new Vector2(45, -3);
        SceneManager.LoadScene(4);
    }
}
