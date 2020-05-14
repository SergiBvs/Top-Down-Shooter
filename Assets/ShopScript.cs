using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{

    public GameObject m_PressBtoBuyText;
    public GameObject m_GUICanvas;
    public GameObject m_ReloadBarCanvas;
    public GameObject m_ShopPanel;


    public int[] m_WeaponCosts;

    private Player m_PlayerScript;
    
    void Start()
    {
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //m_WeaponCosts = new int[3];
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            EnterShop();
        if(Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.m_IsInShop)
            ExitShop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            m_PressBtoBuyText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_PressBtoBuyText.SetActive(false);
        }
    }


    public void EnterShop()
    {
        m_PressBtoBuyText.SetActive(false);
        m_GUICanvas.SetActive(false);
        m_ReloadBarCanvas.SetActive(false);
        Time.timeScale = 0;
        GameManager.instance.m_IsInShop = true;
        m_ShopPanel.SetActive(true);
    }

    public void ExitShop()
    {
        m_PressBtoBuyText.SetActive(true);
        m_GUICanvas.SetActive(true);
        Time.timeScale = 1;
        GameManager.instance.m_IsInShop = false;
        m_ShopPanel.SetActive(false);
    }

    public void Buy()
    {

        //nada de esto funciona n o se como hacerlo

        /*if(this.gameObject.CompareTag("AR"))
        {
            print("test");
            if(m_WeaponCosts[0] >= GameManager.instance.m_Currency && m_PlayerScript.GunBoughtArray[1] == false)
            {
                print("test");
                m_PlayerScript.GunBoughtArray[1] = true;
            }
            else
            {
                //mostrar que no puedes comprar el arma o que ya la tienes
            }
        }
        /*else if (this.gameObject.CompareTag("Shotgun"))
        {
            if (m_WeaponCosts[1] >= GameManager.instance.m_Currency && m_PlayerScript.GunBoughtArray[2] == false)
                m_PlayerScript.GunBoughtArray[2] = true;
            else
            {
                //mostrar que no puedes comprar el arma o que ya la tienes
            }
        }
        else if(this.gameObject.CompareTag("LaserGun"))
        {
            if (m_WeaponCosts[2] >= GameManager.instance.m_Currency && m_PlayerScript.GunBoughtArray[3] == false)
                m_PlayerScript.GunBoughtArray[3] = true;
            else
            {
                //mostrar que no puedes comprar el arma o que ya la tienes
            }
        }
        else if(this.gameObject.CompareTag("HealthUpgrade"))
        {
            //max health +25 max 175
        }
        else if (this.gameObject.CompareTag("LuckUpgrade"))
        {
            //mas luck
        }
        else if (this.gameObject.CompareTag("AmmoUpgrade"))
        {
            //mas ammo total
        }
        else if (this.gameObject.CompareTag("AmmoRefill"))
        {
            if(this.gameObject.name == "AmmoSmallRefill")
            {
                //pequeño incremento, si tienes poco dinero
            }
            else if (this.gameObject.name == "AmmoFullRefill")
            {
                //toda la municion restaurada
            }
        }*/
    
        //lo que sea que se quiera añadir 
    }

   
}
