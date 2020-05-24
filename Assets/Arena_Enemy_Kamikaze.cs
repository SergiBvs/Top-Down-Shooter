using System.Collections;
using UnityEngine;

public class Arena_Enemy_Kamikaze : Arena_Enemy
{
    private bool m_TouchedPlayer = false;
    private bool m_GonnaExplode = false;
    protected override void Update()
    {
        if (!m_GonnaExplode)
            base.Update();
    }

    public override void Shoot()
    {
        //Do nothing.
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_TouchedPlayer && collision.collider.CompareTag("Player"))
        {
            m_TouchedPlayer = true;
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1f);
        m_GonnaExplode = true;
        GameObject explosion = Instantiate((GameObject)Resources.Load("Enemy/Explosion"));
        explosion.transform.position = transform.position;
        yield return new WaitForSeconds(1f);
        Destroy(explosion);
        wManager.m_EnemiesDefeated++;
        wManager.m_WaveEnemiesDefeated++;
        Destroy(this.gameObject);
    }

    public override void TakeDamage(int amount)
    {
        if(!m_GonnaExplode)
            base.TakeDamage(amount);
    }
}
