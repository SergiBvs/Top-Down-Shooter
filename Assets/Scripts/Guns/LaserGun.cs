using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{

    public int m_DamagePerAmmoUnit;
    
    public override void  Update()
    {
        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        if (Input.GetButton("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused)
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
        m_canShoot = false;

        RaycastHit2D hit = Physics2D.Raycast(GunTip.position, transform.right , 30);
        Debug.DrawRay(GunTip.position, transform.right, Color.red, hit.distance);

        StartCoroutine(GunCooldown());

        m_CurrentAmmo--;

        //aixo hauria de funcionar no?

        if (m_CurrentAmmo <= (m_Magazine / 2))
        {
            GUIHelp.m_AmmoText.color = new Color(255, 129, 129);
        }

        if (m_CurrentAmmo <= (m_Magazine / 4))
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

        if(hit)
        {
            
        }
    }
}
