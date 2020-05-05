using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun variables

    public int m_Damage;
    public int m_Magazine;
    public int m_CurrentAmmo;
    public int m_MaxAmmo;
    public int m_CurrentMaxAmmo;
    public float m_ShootCD;
    public float m_ReloadSpeed;
    public bool m_Comprada = false;

    //others

    public Transform GunTip;
    public bool m_canShoot = true;
    public bool m_HasBullets = true;
    public bool m_IsReloading = false;
    public LineRenderer m_LR;

    protected GUIhelper GUIHelp;

    void Start()
    {
        m_CurrentAmmo = m_Magazine;
        m_CurrentMaxAmmo = m_MaxAmmo;
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();

        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;
    }

    
    public virtual void Update()
    {

        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        if (Input.GetButtonDown("Fire1"))
        {
            if(m_canShoot && m_HasBullets && !m_IsReloading)
            {
                Shoot();
            }
        }

        if((Input.GetKeyDown(KeyCode.R)) && (m_CurrentAmmo < m_Magazine) && (m_CurrentMaxAmmo > 0))
        {
            StartCoroutine(Reload());
        }
    }

    public void Shoot()
    {
        print(m_CurrentAmmo);
        m_canShoot = false;
   
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(GunTip.position, transform.right, 30);
        Debug.DrawRay(GunTip.position, transform.right, Color.red, hit.distance);

        StartCoroutine(GunCooldown());

        if(hit)
        {
            m_LR.SetPosition(0, GunTip.position);
            m_LR.SetPosition(1, hit.point);


            if(hit.collider.CompareTag("Enemy"))
            {
                Enemy l_Enemy = hit.transform.GetComponent<Enemy>();
                l_Enemy.TakeDamage(m_Damage);
            }
            else if(hit.collider.CompareTag("Window"))
            {
                hit.transform.GetComponent<Window>().WindowDamage(m_Damage, hit.point);
            }
        }
        else
        {
            m_LR.SetPosition(0, GunTip.position);
            m_LR.SetPosition(1, GunTip.position + GunTip.right * 100);
        }

        m_LR.enabled = true;
        StartCoroutine(LineCooldown());


        m_CurrentAmmo--;
        

        if (m_CurrentAmmo <= 0)
        {
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

        m_IsReloading = false;
    }

    IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(m_ShootCD);
        m_canShoot = true;
    }

    IEnumerator LineCooldown()
    {
        yield return new WaitForSeconds(0.02f);
        m_LR.enabled = false;
    }
}

