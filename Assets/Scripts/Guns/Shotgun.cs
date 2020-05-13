using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    public int m_NumberOfPellets;
    public float m_MaxAngle;
    public float m_MinAngle;

    public override void Update()
    {
        if (m_CurrentAmmo < 0)
            m_CurrentAmmo = 0;

        GUIHelp.m_AmmoText.text = m_CurrentAmmo + " / " + m_CurrentMaxAmmo;

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused)
            {
                
                Shoot(player.localRotation.eulerAngles.z);

                for (int i = 0; i < m_NumberOfPellets/2; i++)
                {
                    float l_angle = Random.Range(0, m_MaxAngle);
                    Shoot(player.localRotation.eulerAngles.z + l_angle);
                }

                for (int i2 = 0; i2 < m_NumberOfPellets/2; i2++)
                {
                    float l_angle = Random.Range(0, m_MinAngle);

                    Shoot(player.localRotation.eulerAngles.z + l_angle);
                }
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
