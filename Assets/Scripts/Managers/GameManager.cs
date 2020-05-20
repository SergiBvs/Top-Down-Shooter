using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private GUIhelper GUIHelp;

    public bool m_IsGameOverPanelOn = false;
    public bool m_GameIsPaused;
    public bool m_IsInShop;
    public int m_Health;
    public int m_Currency;


    //for upgrades

    public Gun[] m_WeaponsArray;
    public int m_CurrentMagazineUpgrade = 0;
    public bool m_CanRefillAmmo = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        ReassignObjs();
        PlayerPrefs.DeleteAll();
    }

    public void ReassignObjs()
    {
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
        SetMaxHealth(m_Health);
    }

    public void GetAllWeapons()
    {
        m_WeaponsArray = FindObjectsOfType<Gun>();
    }

    public void UpgradeMagazine()
    {
        //GetAllWeapons();
        m_CurrentMagazineUpgrade++;

        foreach(Gun item in m_WeaponsArray)
        {
            item.m_Magazine = item.m_InitialMagazine + (m_CurrentMagazineUpgrade * (int)(item.m_InitialMagazine * 0.2f));
            item.m_CurrentAmmo = item.m_Magazine;
            item.m_MaxAmmo = item.m_InitialMaxAmmo + (m_CurrentMagazineUpgrade * (int)(item.m_InitialMaxAmmo * 0.2f));
            item.m_CurrentMaxAmmo = item.m_MaxAmmo;
            item.UpdateGUI();
        }
    }

    public void RefillAmmo()
    {
        //GetAllWeapons();
        m_CanRefillAmmo = false; //poner en true cuando se entre en el ascensor

        foreach(Gun item in m_WeaponsArray)
        {
            item.m_CurrentMaxAmmo = item.m_CurrentMaxAmmo + (int)(item.m_InitialMaxAmmo * 0.3f);
            item.UpdateGUI();
        }

    }

    public void SetMaxHealth(int health)
    {
        GUIHelp.m_HealthText.text = "" + health;
        GUIHelp.m_Slider.maxValue = health;
        GUIHelp.m_Slider.value = health;
        GUIHelp.fill.color = GUIHelp.m_Gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        GUIHelp.m_HealthText.text = health.ToString();
        GUIHelp.m_Slider.value = health;
        GUIHelp.fill.color = GUIHelp.m_Gradient.Evaluate(GUIHelp.m_Slider.normalizedValue);
        GUIHelp.m_psmain.startColor = GUIHelp.fill.color;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetCoins(int l_quantity)
    {
        m_Currency += l_quantity;
        GUIHelp.CoinText.text = m_Currency.ToString();
    }

    public void GameOver()
    {
        GUIHelp.m_GUIPanel.SetActive(false); 
        GUIHelp.m_GameOverPanel.SetActive(true);
        m_IsGameOverPanelOn = true;
    }

}



