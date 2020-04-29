using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun variables

    public int m_Damage;
    public int m_Magazine;
    public float m_ShootCD;
    public int m_CurrentAmmo;
    //others

    public Transform GunTip;
    public bool m_canShoot = true;
    public bool m_HasBullets = true;
    public LineRenderer m_LR;
    
    void Start()
    {
        m_CurrentAmmo = m_Magazine;
    }

    
    public virtual void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if((m_canShoot) && (m_HasBullets))
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        m_canShoot = false;
   
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(GunTip.position, mousePosition);

        StartCoroutine(GunCooldown());

        if(hit)
        {
            m_LR.SetPosition(0, GunTip.position);
            m_LR.SetPosition(1, hit.point);

            Enemy m_Enemy = hit.transform.GetComponent<Enemy>();

            if(hit.collider.CompareTag("Enemy"))
            {
                m_Enemy.TakeDamage(m_Damage);
            }

            if(hit.collider.CompareTag("Wall"))
            {
                Debug.Log("i hit a wall");
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
            StartCoroutine(Reload());
        }
       

    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2.5f); //reload time
        m_CurrentAmmo = m_Magazine;
        m_HasBullets = true;
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

