using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class DropPicker : MonoBehaviour
{

    private static DropPicker instance;
    bool isThisMainInstance;

    private Player m_Player;

    private string thisTag = "";
    private static int m_PickupChain = 0;
    private static float m_ChainTime = 0;
    private static bool startedChain = false;

    private bool m_picked = false;

    private GUIhelper guiHelp;
    [Header("For Coins")]
    public int m_CoinValue = 1;

    [Header("For Ammo Box")]
    public int m_HowMuchAmmo = 10;

    [Header("For Health")]
    public int m_HowMuchHealth = 40;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            isThisMainInstance = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        thisTag = this.tag;
        guiHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
        guiHelp.m_CoinPickupText.SetActive(false);
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (isThisMainInstance)
        {
            if (startedChain)
            {
                if (m_ChainTime > 0)
                {
                    m_ChainTime -= Time.deltaTime;
                }
                else
                {
                    m_PickupChain = 0;
                    instance = null;
                    startedChain = false;
                    if (m_picked)
                        Destroy(this.gameObject);
                }
            }
        }

        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (thisTag)
            {
                case "Coin":
                    PickCoin();
                    break;
                case "Ammo":
                    PickAmmo();
                    break;
                case "Health":
                    PickHealth();
                    break;
            }
        }
    }

    private void PickCoin()
    {
        //Una especie de "cadena" de monedas para mostrar la contidad total ganada a la vez. 
        startedChain = true;
        m_picked = true;
        m_ChainTime = 1f;

        if(m_ChainTime > 0)
        {
            m_PickupChain += m_CoinValue;
            guiHelp.m_CoinPickupText.SetActive(false);
            guiHelp.m_CoinPickupText.SetActive(true);
            guiHelp.m_PlayerPickupCanvas.transform.position = transform.position;
            guiHelp.m_CoinPickupText.GetComponent<TMP_Text>().text = "+ " + m_PickupChain.ToString();

            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        if(instance != this)
        {
            Destroy(this.gameObject);
        }

        GameManager.instance.SetCoins(m_CoinValue);
        SoundManager.instance.PlaySound("Money", 0.3f, 1);
    }
    private void PickAmmo()
    {
        GameManager.instance.GetCurrentWeapon();

        foreach (Gun item in GameManager.instance.m_WeaponsArray)
        {
            item.m_CurrentMaxAmmo += m_HowMuchAmmo;
        }

        GameManager.instance.m_CurrentWeapon.SaveValues();
        GameManager.instance.m_CurrentWeapon.UpdateGUI();

        guiHelp.m_AmmoPickupText.SetActive(true);
        guiHelp.m_AmmoPickupText.GetComponent<TMP_Text>().text = "+ " + m_HowMuchAmmo;
        guiHelp.m_PlayerPickupCanvas.transform.position = transform.position;
        SoundManager.instance.PlaySound("Ammo", 1, 1);
        Destroy(this.gameObject);
    }

    private void PickHealth()
    {
        m_Player.m_CurrentHealth += m_HowMuchHealth;
        if (m_Player.m_CurrentHealth > GameManager.instance.m_Health)
            m_Player.m_CurrentHealth = GameManager.instance.m_Health;

        GameManager.instance.SetHealth(m_Player.m_CurrentHealth);
        SoundManager.instance.PlaySound("Health", 1, 1);
        Destroy(this.gameObject);

    }
}
