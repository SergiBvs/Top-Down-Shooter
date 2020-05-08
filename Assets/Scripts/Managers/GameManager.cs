using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private GUIhelper GUIHelp;

    public bool m_IsGameOverPanelOn = false;
    public bool m_GameIsPaused;
    public int m_Health;

    public int m_Currency;

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
    }

    public void ReassignObjs()
    {
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
        SetMaxHealth(m_Health);
    }

    public void SetMaxHealth(int health)
    {
        GUIHelp.m_Slider.maxValue = health;
        GUIHelp.m_Slider.value = health;
        GUIHelp.fill.color = GUIHelp.m_Gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        GUIHelp.m_Slider.value = health;
        GUIHelp.fill.color = GUIHelp.m_Gradient.Evaluate(GUIHelp.m_Slider.normalizedValue);
    }

    public void NextLevel()
    {
        //telon o lo que sea
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }

    public void SetCoins(int l_quantity)
    {
        m_Currency += l_quantity;
    }
}



