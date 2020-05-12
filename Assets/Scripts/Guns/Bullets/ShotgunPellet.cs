using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPellet : Bullets
{

    public int FallOffDamageDistance;
    public int DamageAfterFallOff;
    
    public override void Update()
    {
        base.Update();

        float distance = Vector3.Distance(this.transform.position, gunpoint.position);

        if (distance >=FallOffDamageDistance)
        {
            m_Damage = DamageAfterFallOff;
        }
    }
}
