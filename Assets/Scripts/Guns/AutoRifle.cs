using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Gun
{


    
    public override void Update()
    {
        m_ReloadBar.value = CalculateSliderValue();

        if (Input.GetButton("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused && !GameManager.instance.m_IsInShop)
            {
                float l_rand;
                l_rand = Random.Range(0.9f, 1.4f);

                SoundManager.instance.PlaySound("ARShot", 0.8f, l_rand);
                Shoot(player.localRotation.eulerAngles.z);
            }
        }

        if(Input.GetButtonDown("Fire1"))
            if(!m_HasBullets && !GameManager.instance.m_GameIsPaused && !GameManager.instance.m_IsInShop)
                SoundManager.instance.PlaySound("EmptyGun", 0.5f, 1);

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

    public override void Shoot(float rotationZ)
    {
        SoundManager.instance.PlaySound("ARShot", 0.01f, 1);
        base.Shoot(rotationZ);
    }
}
