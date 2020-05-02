using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Turret : Enemy
{
    public override void MoveToPlayer()
    {
        //EN VEZ DE MOVER HACER UNA LINEA QUE APUNTE AL PLAYER EN PLAN ESTAS EN MI PUNTO DE MIRA Y EN CUALQUIER MOMENTO DISPARO.
        //Cambiar esto a Line renderer.
        //Debug.DrawRay(transform.position, m_Player.transform.position - transform.position, Color.white);
    }


}
