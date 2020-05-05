using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Gun
{


    
    public override void Update()
    {
        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        if (Input.GetButton("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading)
                Shoot();
        }

        if ((Input.GetKeyDown(KeyCode.R)) && (m_CurrentAmmo < m_Magazine) && (m_CurrentMaxAmmo > 0))
        {
            StartCoroutine(Reload());
        }
    }
}
