using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Basic : Gun
{
    public override void Update()
    {
        base.Update();
    }

    public override void Shoot(float rotationZ)
    {
        float l_rand;

        l_rand = Random.Range(0.9f, 1.4f);
        SoundManager.instance.PlaySound("PistolShot", 0.8f, l_rand);
      
        base.Shoot(rotationZ);
    }
}
