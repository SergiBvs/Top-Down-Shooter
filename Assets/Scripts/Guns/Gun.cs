using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun variables

    public int m_Magazine;
    public int m_CurrentAmmo;
    public int m_MaxAmmo;
    public int m_CurrentMaxAmmo;
    public float m_ShootCD;
    public float m_ReloadSpeed;
    public string m_BulletName;

    //others

    public Transform GunTip;
    public bool m_canShoot = true;
    public bool m_HasBullets = true;
    public bool m_IsReloading = false;

    protected GUIhelper GUIHelp;
    protected Transform player;

    void Start()
    {
        m_CurrentAmmo = m_Magazine;
        m_CurrentMaxAmmo = m_MaxAmmo;
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

    }


    
    public virtual void Update()
    {

        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        if (Input.GetButtonDown("Fire1"))
        {
            if(m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused)
            {
                Shoot(player.localRotation.eulerAngles.z);
            }
        }

        if((Input.GetKeyDown(KeyCode.R)) && (m_CurrentAmmo < m_Magazine) && (m_CurrentMaxAmmo > 0))
        {
            StartCoroutine(Reload());
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

        GameObject bullet = Instantiate((GameObject)Resources.Load("Bullets/" + m_BulletName), GunTip.position, Quaternion.Euler(0, 0, rotationZ));

        StartCoroutine(GunCooldown());

        m_CurrentAmmo--;

        //aixo hauria de funcionar no?

        if (m_CurrentAmmo <= (m_Magazine/2))
        {
            GUIHelp.m_AmmoText.color = new Color(255, 129, 129);
        }

        if(m_CurrentAmmo <=(m_Magazine/4))
        {
            GUIHelp.m_AmmoText.color = new Color(255, 57, 57);
        }

        //aixo si funciona

        if (m_CurrentAmmo <= 0)
        {
            GUIHelp.m_ReloadPanel.SetActive(true);
            GUIHelp.m_AmmoText.color = new Color(255, 0, 0);
            m_HasBullets = false;
        }
       
    }

    protected IEnumerator Reload()
    {
        m_IsReloading = true;
        yield return new WaitForSeconds(m_ReloadSpeed);

        if(m_MaxAmmo > 0)
        {

            if (m_CurrentMaxAmmo >= m_Magazine)
            {
                m_CurrentMaxAmmo -= (m_Magazine - m_CurrentAmmo);
                m_CurrentAmmo += (m_Magazine - m_CurrentAmmo);
            }
            else
            {   
                if(m_CurrentAmmo == 0)
                {
                    m_CurrentAmmo = m_CurrentMaxAmmo;
                    m_CurrentMaxAmmo = 0;
                }
                else
                {
                    m_CurrentMaxAmmo -= (m_Magazine - m_CurrentAmmo);
                    m_CurrentAmmo += (m_Magazine - m_CurrentAmmo);

                    if((m_Magazine - m_CurrentAmmo) > m_CurrentMaxAmmo)
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
    }

    protected IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(m_ShootCD);
        m_canShoot = true;
    }
}

