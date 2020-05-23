using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    //bullet variables

    public float m_BulletForce;
    public int m_Damage;

    //others

    public Rigidbody2D m_BulletRB2D;
    private bool shot;
    protected Transform gunpoint;
    


    public virtual void Start()
    {
        gunpoint = gameObject.transform;
        m_BulletRB2D = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(DeleteBulletAfterTime());
    }

    
    public virtual void Update()
    {
        if (!shot)
        {
            m_BulletRB2D.AddForce(gunpoint.up * m_BulletForce, ForceMode2D.Impulse);
            shot = true;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            int l_hitSound = Random.Range(1, 3);
            SoundManager.instance.PlaySound("WallHit" + l_hitSound, 0.2f, 1);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy l_Enemy = collision.gameObject.GetComponent<Enemy>();
            l_Enemy.TakeDamage(m_Damage); //particulas en esa funcion como en la de window?
            SoundManager.instance.PlaySound("HitSound", 0.7f, 1);
        }
        else if (collision.collider.CompareTag("ArenaEnemy"))
        {
            Arena_Enemy l_aEnemy = collision.collider.GetComponent<Arena_Enemy>();
            l_aEnemy.TakeDamage(m_Damage);
            SoundManager.instance.PlaySound("HitSound", 0.7f, 1);
        }
        else if(collision.gameObject.CompareTag("Window"))
        {
            int l_hitWindowSound = Random.Range(1,3);
            SoundManager.instance.PlaySound("WindowHit" + l_hitWindowSound, 0.6f, 1);
            collision.gameObject.GetComponent<Window>().WindowDamage(m_Damage, this.transform.position);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeDamage(m_Damage);
        }
        
        Destroy(this.gameObject);
    }

    public IEnumerator DeleteBulletAfterTime()
    {
        yield return new WaitForSeconds(8f);
        Destroy(this.gameObject);
    }

}
