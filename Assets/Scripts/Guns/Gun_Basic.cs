using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Basic : Gun
{
    public override void Update()
    {
        base.Update();

        m_Comprada = true;
    }
}
