using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    public override void Update()
    {
        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused)
            {
                //las 4 formas diferentes segun lo que me ha dicho el profe

                Shoot(/*GunTip.localRotation.z + 45*/ /*GunTip.rotation.z + 45*/ player.localRotation.z + 45 /*player.rotation.z + 45*/);
                Shoot(/*GunTip.localRotation.z*/ /*GunTip.rotation.z*/ player.localRotation.z /*player.rotation.z*/);
                Shoot(/*GunTip.localRotation.z - 45*/ /*GunTip.rotation.z - 45*/ player.localRotation.z - 45 /*player.rotation.z - 45*/);
            }
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
}
