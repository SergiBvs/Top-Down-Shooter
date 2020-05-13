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

    [Header("For Ammo Box")]
    public int m_HowMuchAmmo = 10;

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
            m_PickupChain++;
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

        GameManager.instance.SetCoins(1);
    }
    private void PickAmmo()
    {
        m_Player.m_CurrentGun.m_CurrentMaxAmmo += m_HowMuchAmmo;

        guiHelp.m_AmmoPickupText.SetActive(true);
        guiHelp.m_AmmoPickupText.GetComponent<TMP_Text>().text = "+ " + m_HowMuchAmmo;
        guiHelp.m_AmmoPickupText.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
