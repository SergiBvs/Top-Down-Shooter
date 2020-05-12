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

                //las 4 formas diferentes segun lo que ha dicho el profe

                Shoot(/*GunTip.localRotation.z*/ /*GunTip.rotation.z*/ player.localRotation.z /*player.rotation.z*/);
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

    public void Shoot(float rotationZ)
    {
        m_canShoot = false;

        //las 4 formas diferentes segun lo que me ha dicho el profe
        //en el script de bullet hay el addforce que las mueve adelante para el gunpoint tiene que ser .right i para el player tiene que ser .up (aunque con el euler no funcione igualmente) , no preguntes el porque porque no lo se

        Instantiate(Resources.Load("Bullets/" + m_BulletName),  GunTip.position,  /*Quaternion.Euler(GunTip.localRotation.x , GunTip.localRotation.y, rotationZ)*/ /*Quaternion.Euler(GunTip.rotation.x , GunTip.rotation.y, rotationZ)*/ Quaternion.Euler(player.localRotation.x , player.localRotation.y, rotationZ) /*Quaternion.Euler(player.rotation.x , player.rotation.y, rotationZ)*/);


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
                m_CurrentAmmo = m_Magazine;
            }
            else
            {
                m_CurrentAmmo = m_CurrentMaxAmmo;
                m_CurrentMaxAmmo = 0;
            }

            m_HasBullets = true;
        }

        GUIHelp.m_AmmoText.color = new Color(255, 255, 255);
        GUIHelp.m_ReloadPanel.SetActive(false);
        m_IsReloading = false;
    }

    IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(m_ShootCD);
        m_canShoot = true;
    }
}

