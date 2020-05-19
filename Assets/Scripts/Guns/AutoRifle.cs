using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Gun
{


    
    public override void Update()
    {
        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;
        m_ReloadBar.value = CalculateSliderValue();

        if (Input.GetButton("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused && !GameManager.instance.m_IsInShop)
                Shoot(player.localRotation.eulerAngles.z);
        }

        if ((Input.GetKeyDown(KeyCode.R)) && (m_CurrentAmmo < m_Magazine) && (m_CurrentMaxAmmo > 0) && (!m_IsReloading))
        {
            m_CurrentReloadSpeed = m_ReloadSpeed;
            m_IsReloading = true;
        }

        if (m_IsReloading)
            Reload();

        if (m_CurrentAmmo <= 0)
        {
            GUIHelp.m_ReloadPanel.SetActive(true);
            GUIHelp.m_AmmoText.color = new Color(255, 0, 0);
        }
    }
}
