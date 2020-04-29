﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Gun
{
    
    public override void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (m_canShoot && m_HasBullets && !m_IsReloading)
                Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
}
