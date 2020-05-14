using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{

    public int m_WeaponCost;
    private Player m_PlayerScript;

    public TextMeshProUGUI m_WeaponCostText;
    
    void Start()
    {
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //m_WeaponCosts = new int[3];
    }

    void Update()
    {
        m_WeaponCostText.text = m_WeaponCost + "$";
    }


    public void BuyWeapons()
    {
        if(this.gameObject.CompareTag("AR"))
        {
            if(GameManager.instance.m_Currency >= m_WeaponCost && m_PlayerScript.GunBoughtArray[1] == false)
            {
                m_PlayerScript.GunBoughtArray[1] = true;
                GameManager.instance.m_Currency -= m_WeaponCost;
            }
            else
            {
                //mostrar que no puedes comprar el arma o que ya la tienes
            }
        }
        else if (this.gameObject.CompareTag("Shotgun"))
        {
            if (GameManager.instance.m_Currency >= m_WeaponCost && m_PlayerScript.GunBoughtArray[2] == false)
            {
                m_PlayerScript.GunBoughtArray[2] = true;
                GameManager.instance.m_Currency -= m_WeaponCost;
            }
            else
            {
                //mostrar que no puedes comprar el arma o que ya la tienes
            }
        }
        else if(this.gameObject.CompareTag("LaserGun"))
        {
            if (GameManager.instance.m_Currency >= m_WeaponCost && m_PlayerScript.GunBoughtArray[3] == false)
            {
                m_PlayerScript.GunBoughtArray[3] = true;
                GameManager.instance.m_Currency -= m_WeaponCost;
            }
            else
            {
                //mostrar que no puedes comprar el arma o que ya la tienes
            }
        }
    }

    public void BuyUpgrades()
    {
        if (this.gameObject.CompareTag("HealthUpgrade"))
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
            if (this.gameObject.name == "AmmoSmallRefill")
            {
                //pequeño incremento, si tienes poco dinero
            }
            else if (this.gameObject.name == "AmmoFullRefill")
            {
                //toda la municion restaurada
            }
        }

        //lo que sea que se quiera añadir 
    }


}
