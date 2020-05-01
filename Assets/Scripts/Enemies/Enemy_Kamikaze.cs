using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Kamikaze : Enemy
{
    
    private bool m_OnRange = false;
    public override void Update()
    {
        if(!m_OnRange)
            base.Update();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_OnRange && collision.collider.CompareTag("Player"))
        {
            print("OnRange");
            m_OnRange = true;
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1f);
        GameObject explosion = Instantiate((GameObject)Resources.Load("Enemy/Explosion"));
        explosion.transform.position = transform.position;
        yield return new WaitForSeconds(1f);
        Destroy(explosion);
        Destroy(this.gameObject);
    }
}
