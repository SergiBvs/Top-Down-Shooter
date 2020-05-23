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
        m_ButtonImage = this.GetComponent<Image>();
        ChangeColors();

    }

    void Update()
    {
       
    }


    public void BuyWeapons()
    {
        int l_randSound = Random.Range(1, 3);
       
        if(this.gameObject.CompareTag("AR"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[1] == false)
            {
                Player.GunBoughtArray[1] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = 1;
                PlayerPrefs.SetInt(this.tag + "Bought", m_Bought);
                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);
        }
        else if (this.gameObject.CompareTag("Shotgun"))
        {
            if (GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[2] == false)
            {
                Player.GunBoughtArray[2] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = 1;
                PlayerPrefs.SetInt(this.tag + "Bought", m_Bought);
                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);
        }
        else if(this.gameObject.CompareTag("LaserGun"))
        {
            if (GameManager.instance.m_Currency >= m_Cost && Player.GunBoughtArray[3] == false)
            {
                Player.GunBoughtArray[3] = true;
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = 1;
                PlayerPrefs.SetInt(this.tag + "Bought", m_Bought);
                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);
        }

        ChangeColors();

    }

    public void BuyUpgrades()
    {
        int l_randSound = Random.Range(1, 3);

        if (this.gameObject.CompareTag("Health"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && GameManager.instance.m_Health <200)
            {
                GameManager.instance.m_Health += 25;
                GameManager.instance.SetMaxHealth(GameManager.instance.m_Health);
                m_PlayerScript.m_CurrentHealth = GameManager.instance.m_Health;

                GameManager.instance.m_Currency -= m_Cost;

                m_Cost += 125;
                PlayerPrefs.SetInt(this.tag + "Cost", m_Cost);

                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);

            if (GameManager.instance.m_Health>=200)
            {
                m_MaxUpgraded = 1;
                PlayerPrefs.SetInt(this.tag + "MaxUpgraded", m_MaxUpgraded);
            }

            //max health +25 max 200 4 upgrades
        }
        else if (this.gameObject.CompareTag("Luck"))
        {
            if(GameManager.instance.m_Currency >= m_Cost /* && que este debajo del maximo*/)
            {
                //augmento de luck
                GameManager.instance.m_Luck += 5;

                GameManager.instance.m_Currency -= m_Cost;
                m_Cost += 125; //lo puedes cambiar si quieres
                PlayerPrefs.SetInt(this.tag + "Cost", m_Cost);

                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);

            /*if(si ha llegado al maximo)
            {
                m_MaxUpgraded = 1;
                PlayerPrefs.SetInt(this.tag + "MaxUpgraded", m_MaxUpgraded);
            }*/
        }
        else if(this.gameObject.CompareTag("Speed"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && Player.m_PlayerSpeed<20)
            {
                Player.m_PlayerSpeed += 2;

                GameManager.instance.m_Currency -= m_Cost;

                m_Cost += 125;
                PlayerPrefs.SetInt(this.tag + "Cost", m_Cost);
                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);

            if (Player.m_PlayerSpeed>=20)
            {
                m_MaxUpgraded = 1;
                PlayerPrefs.SetInt(this.tag + "MaxUpgraded", m_MaxUpgraded);
            }
            
        }
        else if (this.gameObject.CompareTag("Magazine"))
        {
            if(GameManager.instance.m_Currency >= m_Cost && GameManager.instance.m_CurrentMagazineUpgrade <5)
            {
                GameManager.instance.UpgradeMagazine();

                GameManager.instance.m_Currency -= m_Cost;

                m_Cost += 125;
                PlayerPrefs.SetInt(this.tag + "Cost", m_Cost);

                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);

            if (GameManager.instance.m_CurrentMagazineUpgrade == 5)
            {
                m_MaxUpgraded = 1;
                PlayerPrefs.SetInt(this.tag + "MaxUpgraded", m_MaxUpgraded);
            }
        }
        else if(this.gameObject.CompareTag("Ammo"))
        {
            if (GameManager.instance.m_Currency >= m_Cost && GameManager.instance.m_CanRefillAmmo)
            {
                GameManager.instance.RefillAmmo();
                GameManager.instance.m_Currency -= m_Cost;
                m_Bought = 1;
                PlayerPrefs.SetInt(this.tag + "Bought", m_Bought);
                SoundManager.instance.PlaySound("BuySound" + l_randSound, 1, 1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);
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
