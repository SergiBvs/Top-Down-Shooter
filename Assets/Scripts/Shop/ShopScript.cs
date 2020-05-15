using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{

    public int m_Cost;
    private Player m_PlayerScript;
    public TextMeshProUGUI m_CostText;
    private Image m_ButtonImage;
    public bool m_MaxUpgraded;
    public bool m_Bought;

    void Start()
    {
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_ButtonImage = this.GetComponent<Image>();
        ChangeColors();
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
                m_Bought = true;
            }
        }
        else if (this.gameObject.CompareTag("Shotgun"))
        {
            if (GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[2] == false)
            {
                Player.GunBoughtArray[2] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = true;
            }
        }
        else if(this.gameObject.CompareTag("LaserGun"))
        {
            if (GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[3] == false)
            {
                Player.GunBoughtArray[3] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = true;
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

                //mostrar +25 de vida y poner una barra mas de las 4 en total

                GameManager.instance.m_Currency -= m_Cost;
            }

            if(GameManager.instance.m_Health>=200)
            {
                m_MaxUpgraded = true;
            }

            //max health +25 max 200 4 upgrades
        }
        else if (this.gameObject.CompareTag("Luck"))
        {
            //no se como funciona lo de luck hazlo tu alex
        }
        else if(this.gameObject.CompareTag("Speed"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && Player.m_PlayerSpeed<15)
            {
                Player.m_PlayerSpeed += 1;
                m_Cost += 125;

                //mostrar +1 de speed y poner una barra mas de las 5 en total

                GameManager.instance.m_Currency -= m_Cost;
            }

            if(Player.m_PlayerSpeed>=15)
            {
                m_MaxUpgraded = true;
            }
            
        }
        else if (this.gameObject.CompareTag("Magazine"))
        {
            
        }
        else if(this.gameObject.CompareTag("Ammo"))
        {
            
        }

        ChangeColors();
        //lo que sea que se quiera añadir 
    }

    public void ChangeColors()
    {
        if (m_MaxUpgraded)
        {
            m_CostText.text = "MAX";
            m_ButtonImage.color = new Color(255 / 255f, 150 / 255f, 0);
        }
        else if (m_Bought)
        {
            m_CostText.text = "BOUGHT";
            m_ButtonImage.color = new Color(255 / 255f, 150 / 255f, 0);
        }
        else if (GameManager.instance.m_Currency >= m_Cost && !m_MaxUpgraded)
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
