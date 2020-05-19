using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{

    public int m_Cost;

    private Player m_PlayerScript;
    private Gun m_GunScript;
    public TextMeshProUGUI m_CostText;
    private Image m_ButtonImage;

    public int m_MaxUpgraded = 0; //0 = false 1 = true
    public int m_Bought = 0; //0 = false 1 = true

    void Start()
    {
        if (PlayerPrefs.HasKey(this.tag + "Bought"))
            m_Bought = PlayerPrefs.GetInt(this.tag + "Bought");
        if (PlayerPrefs.HasKey(this.tag + "MaxUpgraded"))
            m_MaxUpgraded = PlayerPrefs.GetInt(this.tag + "MaxUpgraded");
        if (PlayerPrefs.HasKey(this.tag + "Cost"))
            m_Cost = PlayerPrefs.GetInt(this.tag + "Cost");

        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //m_GunScript = GameObject.FindGameObjectWithTag("Gun").Get

        m_ButtonImage = this.GetComponent<Image>();
        ChangeColors();

        /*m_MaxUpgraded = PlayerPrefs.GetInt(this.tag);
        m_Bought = PlayerPrefs.GetInt(this.tag);*/
    }

    void Update()
    {
       
    }


    public void BuyWeapons()
    {
        if(this.gameObject.CompareTag("AR"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[1] == false)
            {
                Player.GunBoughtArray[1] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = 1;
                PlayerPrefs.SetInt(this.tag + "Bought", m_Bought);
            }
        }
        else if (this.gameObject.CompareTag("Shotgun"))
        {
            if (GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[2] == false)
            {
                Player.GunBoughtArray[2] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = 1;
                PlayerPrefs.SetInt(this.tag + "Bought", m_Bought);
            }
        }
        else if(this.gameObject.CompareTag("LaserGun"))
        {
            if (GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[3] == false)
            {
                Player.GunBoughtArray[3] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = 1;
                PlayerPrefs.SetInt(this.tag + "Bought", m_Bought);
            }
        }

        ChangeColors();

    }

    public void BuyUpgrades()
    {
        if (this.gameObject.CompareTag("Health"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && GameManager.instance.m_Health <200)
            {
                GameManager.instance.m_Health += 25;
                GameManager.instance.SetMaxHealth(GameManager.instance.m_Health);
                m_PlayerScript.m_CurrentHealth = GameManager.instance.m_Health;

                m_Cost += 125;
                PlayerPrefs.SetInt(this.tag + "Cost", m_Cost);

                //mostrar +25 de vida y poner una barra mas de las 4 en total

                GameManager.instance.m_Currency -= m_Cost;
            }

            if(GameManager.instance.m_Health>=200)
            {
                m_MaxUpgraded = 1;
                PlayerPrefs.SetInt(this.tag + "MaxUpgraded", m_MaxUpgraded);
            }

            //max health +25 max 200 4 upgrades
        }
        else if (this.gameObject.CompareTag("Luck"))
        {
            //no se como funciona lo de luck hazlo tu alex
        }
        else if(this.gameObject.CompareTag("Speed"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && Player.m_PlayerSpeed<20)
            {
                Player.m_PlayerSpeed += 2;

                m_Cost += 125;
                PlayerPrefs.SetInt(this.tag + "Cost", m_Cost);

                //mostrar +1 de speed y poner una barra mas de las 5 en total

                GameManager.instance.m_Currency -= m_Cost;
            }

            if(Player.m_PlayerSpeed>=20)
            {
                m_MaxUpgraded = 1;
                PlayerPrefs.SetInt(this.tag + "MaxUpgraded", m_MaxUpgraded);
            }
            
        }
        else if (this.gameObject.CompareTag("Magazine"))
        {
            
        }
        else if(this.gameObject.CompareTag("Ammo"))
        {
            m_GunScript.RefillAmmo();
        }

        ChangeColors();
        //lo que sea que se quiera añadir 
    }

    public void ChangeColors()
    {
        if (m_MaxUpgraded == 1)
        {
            m_CostText.text = "MAX";
            m_ButtonImage.color = new Color(255 / 255f, 150 / 255f, 0);
        }
        else if (m_Bought == 1)
        {
            m_CostText.text = "BOUGHT";
            m_ButtonImage.color = new Color(255 / 255f, 150 / 255f, 0);
        }
        else if (GameManager.instance.m_Currency >= m_Cost && m_MaxUpgraded == 0)
        {
            m_CostText.text = m_Cost + "$";
            m_ButtonImage.color = new Color(0, 255 / 255f, 0);
        }
        else
        {
            m_CostText.text = m_Cost + "$";
            m_ButtonImage.color = new Color(255 / 255f, 0, 0);
        }
    }
}
