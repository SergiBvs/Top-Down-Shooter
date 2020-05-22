using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //Gun variables

    public int m_InitialMagazine;
    public int m_Magazine;
    public int m_CurrentAmmo;
    public int m_InitialMaxAmmo;
    public int m_MaxAmmo;
    public int m_CurrentMaxAmmo;
    public float m_ShootCD;
    public float m_ReloadSpeed;
    public float m_CurrentReloadSpeed;
    public string m_BulletName;

    //others

    public Transform GunTip;
    public GameObject m_ReloadBarCanvas;
    public Slider m_ReloadBar;
    public bool m_canShoot = true;
    public bool m_HasBullets = true;
    public bool m_IsReloading = false;
    public static bool m_InitialAmmoActivator = false;

    protected GUIhelper GUIHelp;
    protected Transform player;

    int i = 0;


    void Start()
    {
        

        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        //StartCoroutine(test());
    }

   IEnumerator test()
   {
        print("test");
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.UpdateUpgrades();
        LoadValues();
        UpdateGUI();
   }


    public virtual void Update()
    {
        m_ReloadBar.value = CalculateSliderValue();

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused && !GameManager.instance.m_IsInShop)
            {
                Shoot(player.localRotation.eulerAngles.z);
            }
        }

        if ((Input.GetKeyDown(KeyCode.R)) && (m_CurrentAmmo < m_Magazine) && (m_CurrentMaxAmmo > 0) && (!m_IsReloading))
        {
            m_CurrentReloadSpeed = m_ReloadSpeed;
            m_IsReloading = true;
        }

        if (m_IsReloading)
        {
            Reload();
        }

        if (m_CurrentAmmo <= 0)
        {
            GUIHelp.m_ReloadPanel.SetActive(true);
            GUIHelp.m_AmmoText.color = new Color(255, 0, 0);
        }
    }

    public virtual void Shoot(float rotationZ)
    {
        m_canShoot = false;

        Instantiate(Resources.Load("Bullets/" + m_BulletName), GunTip.position, Quaternion.Euler(0, 0, rotationZ));
        Instantiate(Resources.Load("Shoot_Particles"), GunTip.transform);

        StartCoroutine(GunCooldown());

        m_CurrentAmmo--;

        //aixo hauria de funcionar no?

        if (m_CurrentAmmo <= (m_Magazine / 2))
        {
            GUIHelp.m_AmmoText.color = new Color(236 / 255f, 128 / 255f, 48 / 255f);

            if (m_CurrentAmmo <= (m_Magazine / 4))
            {
                GUIHelp.m_AmmoText.color = new Color(236 / 255f, 94 / 255f, 52 / 255f);
            }

        }
        //aixo si funciona

        if (m_CurrentAmmo <= 0)
        {
            GUIHelp.m_ReloadPanel.SetActive(true);
            GUIHelp.m_AmmoText.color = new Color(1, 0, 0);
            m_HasBullets = false;
        }

        UpdateGUI();
        SaveValues();

    }

    protected void Reload()
    {
        m_ReloadBarCanvas.SetActive(true);

        if (m_CurrentReloadSpeed <= 0)
        {
            m_CurrentReloadSpeed = 0;
            m_ReloadBarCanvas.SetActive(false);

            if (m_MaxAmmo > 0)
            {
                if (m_CurrentMaxAmmo >= m_Magazine)
                {
                    m_CurrentMaxAmmo -= (m_Magazine - m_CurrentAmmo);
                    m_CurrentAmmo += (m_Magazine - m_CurrentAmmo);
                }
                else
                {
                    if (m_CurrentAmmo == 0)
                    {
                        m_CurrentAmmo = m_CurrentMaxAmmo;
                        m_CurrentMaxAmmo = 0;
                    }
                    else
                    {
                        m_CurrentMaxAmmo -= (m_Magazine - m_CurrentAmmo);
                        m_CurrentAmmo += (m_Magazine - m_CurrentAmmo);

                        if ((m_Magazine - m_CurrentAmmo) > m_CurrentMaxAmmo)
                        {
                            m_CurrentAmmo += m_CurrentMaxAmmo;
                            m_CurrentMaxAmmo = 0;
                        }

                    }
                }

                m_HasBullets = true;
            }

            GUIHelp.m_AmmoText.color = new Color(255, 255, 255);
            GUIHelp.m_ReloadPanel.SetActive(false);
            m_IsReloading = false;

            SaveValues();
        }
        else if (m_CurrentReloadSpeed > 0)
        {
            m_CurrentReloadSpeed -= Time.deltaTime;
        }

        UpdateGUI();
    }

    public void RefillAmmo()
    {
        m_CurrentMaxAmmo = (int)(m_MaxAmmo * 0.2f);
    }

    public void UpdateGUI()
    {
        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;
    }

    protected IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(m_ShootCD);
        m_canShoot = true;
    }

    public float CalculateSliderValue()
    {
        return (m_CurrentReloadSpeed / m_ReloadSpeed);
    }

    public void SaveValues()
    {
        i = 0;

        foreach(Gun item in GameManager.instance.m_WeaponsArray)
        {
            PlayerPrefs.SetInt("CurrentAmmoValues" + i, item.m_CurrentAmmo);
            PlayerPrefs.SetInt("CurrentMaxAmmoValues" + i, item.m_CurrentMaxAmmo);
            i++;
        }
    }

    public void LoadValues()
    {
        i = 0;

        foreach (Gun item in GameManager.instance.m_WeaponsArray)
        {
            if (PlayerPrefs.HasKey("CurrentAmmoValues" + i))
                item.m_CurrentAmmo = PlayerPrefs.GetInt("CurrentAmmoValues" + i);
            else
                item.m_CurrentAmmo = item.m_Magazine;
            if (PlayerPrefs.HasKey("CurrentMaxAmmoValues" + i))
                item.m_CurrentMaxAmmo = PlayerPrefs.GetInt("CurrentMaxAmmoValues" + item.name);
            else
                item.m_CurrentMaxAmmo = item.m_MaxAmmo;
         
            i++;
        }
    }
}

