using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{

    public int m_DamagePerAmmoUnit;
    public LineRenderer m_LR;
    
    public override void  Update()
    {
        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        if (Input.GetButton("Fire1"))
        {
            if (m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused)
                Shoot(player.localRotation.eulerAngles.z);
        }

        if ((Input.GetKeyDown(KeyCode.R)) && (m_CurrentAmmo < m_Magazine) && (m_CurrentMaxAmmo > 0))
        {
            StartCoroutine(Reload());
        }

        if (m_CurrentAmmo <= 0)
        {
            GUIHelp.m_ReloadPanel.SetActive(true);
            GUIHelp.m_AmmoText.color = new Color(255, 0, 0);
        }
    }

    public override void Shoot(float rotationZ)
    {

        RaycastHit2D hit = Physics2D.Raycast(GunTip.position, transform.right , 30);
        Debug.DrawRay(GunTip.position, transform.right, Color.red, hit.distance);


        if(m_canShoot)
        {
            StartCoroutine(GunCooldown());

            m_CurrentAmmo--;

            m_canShoot = false;
       

            //aixo hauria de funcionar no?

            /* if (m_CurrentAmmo <= (m_Magazine / 2))
             {
                 GUIHelp.m_AmmoText.color = new Color(255, 129, 129);
             }

             if (m_CurrentAmmo <= (m_Magazine / 4))
             {
                 GUIHelp.m_AmmoText.color = new Color(255, 57, 57);
             }*/

            //aixo si funciona


            if (m_CurrentAmmo <= 0)
            {
                GUIHelp.m_ReloadPanel.SetActive(true);
                GUIHelp.m_AmmoText.color = new Color(255, 0, 0);
                m_HasBullets = false;
            }

            if(hit)
            {

                //particulas de colision con el laser (podemos usar las del enemigo de laser maybe)

                if (hit.collider.CompareTag("Enemy"))
                {
                    Enemy l_Enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    l_Enemy.TakeDamage(m_DamagePerAmmoUnit);
                }
                else if (hit.collider.CompareTag("Wall"))
                {
                    //particles
                }
                else if(hit.collider.CompareTag("Window"))
                {
                    //particles
                }

                m_LR.SetPosition(0, GunTip.position);
                m_LR.SetPosition(1, hit.point);
            }
            else
            {
                m_LR.SetPosition(0, GunTip.position);
                m_LR.SetPosition(1, GunTip.position+GunTip.right *100);
            }

            m_LR.enabled = true;

            StartCoroutine(LineCD());
        }
    }

    IEnumerator LineCD()
    {
        yield return new WaitForSeconds(0.02f);
        m_LR.enabled = false;
    }
}
