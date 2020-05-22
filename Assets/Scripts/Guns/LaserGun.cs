using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun
{

    public int m_DamagePerAmmoUnit;
    public LineRenderer m_LR;
    
    public override void  Update()
    {
        
        m_ReloadBar.value = CalculateSliderValue();

        if (Input.GetButton("Fire1"))
        {
            if (m_HasBullets && !m_IsReloading && !GameManager.instance.m_GameIsPaused && !GameManager.instance.m_IsInShop)
                Shoot(player.localRotation.eulerAngles.z);
            else if (!m_HasBullets)
                SoundManager.instance.PlaySound("EmptyGun", 0.5f, 1);
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

    public override void Shoot(float rotationZ)
    {
        SoundManager.instance.PlaySound("LaserBeamSound1", 1, 1);

        RaycastHit2D hit = Physics2D.Raycast(GunTip.position, transform.right , 30);
        Debug.DrawRay(GunTip.position, hit.point - (Vector2)transform.position, Color.red, 0.5f);


        if(m_canShoot)
        {
            StartCoroutine(GunCooldown());

            m_CurrentAmmo--;

            m_canShoot = false;


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
                    //Fem que no pugui trencar finestres
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
            UpdateGUI();
            SaveValues();
        }
    }

    IEnumerator LineCD()
    {
        yield return new WaitForSeconds(0.02f);
        m_LR.enabled = false;
    }
}
