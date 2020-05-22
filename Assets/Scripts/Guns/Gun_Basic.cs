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
        SoundManager.instance.PlaySound("PistolShot", 0.8f, 1);
        base.Shoot(rotationZ);
    }
}
