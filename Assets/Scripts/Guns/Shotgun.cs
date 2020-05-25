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

       
        m_ReloadBar.value = CalculateSliderValue();

        if (Input.GetButtonDown("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused && !GameManager.instance.m_IsInShop)
            {
                SoundManager.instance.PlaySound("ShotgunShot", 0.5f, 1);

                Shoot(player.localRotation.eulerAngles.z);
            }
            else if (!m_HasBullets)
                SoundManager.instance.PlaySound("EmptyGun", 0.5f, 1);
        }

        if ((Input.GetKeyDown(KeyCode.R)) && (m_CurrentAmmo < m_Magazine) && (m_CurrentMaxAmmo > 0) && (!m_IsReloading))
        {
            m_CurrentReloadSpeed = m_ReloadSpeed;
            m_IsReloading = true;
        }

        if(m_IsReloading)
            Reload();

        if (m_CurrentAmmo <= 0)
        {
            GUIHelp.m_ReloadPanel.SetActive(true);
            GUIHelp.m_AmmoText.color = new Color(255, 0, 0);
        }
    }

    public override void Shoot(float rotationZ)
    {
        m_canShoot = false;

        StartCoroutine(CreateBullets(rotationZ));

        StartCoroutine(GunCooldown());

        m_CurrentAmmo-= 5;

        if (m_CurrentAmmo <= (m_Magazine / 2))
        {
            GUIHelp.m_AmmoText.color = new Color(236 / 255f, 128 / 255f, 48 / 255f);

            if (m_CurrentAmmo <= (m_Magazine / 4))
            {
                GUIHelp.m_AmmoText.color = new Color(236 / 255f, 94 / 255f, 52 / 255f);
            }

        }

        if (m_CurrentAmmo <= 0)
        {
            GUIHelp.m_ReloadPanel.SetActive(true);
            GUIHelp.m_AmmoText.color = new Color(1, 0, 0);
            m_HasBullets = false;
        }

        UpdateGUI();
        SaveValues();
    }

    IEnumerator CreateBullets(float rotationZ)
    {
        yield return new WaitForSeconds(0.001f);
        float l_angle = Random.Range(0, m_MinAngle);
        Instantiate(Resources.Load("Bullets/" + m_BulletName), GunTip.position, Quaternion.Euler(0, 0, rotationZ + l_angle));

        yield return new WaitForSeconds(0.001f);
        l_angle = Random.Range(0, m_MinAngle);
        Instantiate(Resources.Load("Bullets/" + m_BulletName), GunTip.position, Quaternion.Euler(0, 0, rotationZ + l_angle));

        yield return new WaitForSeconds(0.001f);
        Instantiate(Resources.Load("Bullets/" + m_BulletName), GunTip.position, Quaternion.Euler(0, 0, rotationZ));

        yield return new WaitForSeconds(0.001f);
        l_angle = Random.Range(0, m_MaxAngle);
        Instantiate(Resources.Load("Bullets/" + m_BulletName), GunTip.position, Quaternion.Euler(0, 0, rotationZ + l_angle));

        yield return new WaitForSeconds(0.001f);
        l_angle = Random.Range(0, m_MaxAngle);
        Instantiate(Resources.Load("Bullets/" + m_BulletName), GunTip.position, Quaternion.Euler(0, 0, rotationZ + l_angle));
    }
}

