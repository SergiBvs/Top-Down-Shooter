using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun variables

    public int m_Damage;
    public int m_Magazine;
    public float m_ShootCD;

    //others

    public Transform GunTip;
    public bool m_canShoot = true;
    public LineRenderer m_LR;
    
    void Start()
    {
        
    }

    
    public virtual void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(m_canShoot)
            Shoot();
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

    }

    public void Reload()
    {

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

