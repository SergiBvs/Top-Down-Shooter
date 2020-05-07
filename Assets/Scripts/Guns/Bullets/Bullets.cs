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
    private Transform gunpoint;


    public virtual void Start()
    {
        gunpoint = gameObject.transform;
        m_BulletRB2D = gameObject.GetComponent<Rigidbody2D>();
    }

    
    public virtual void Update()
    {
        if (!shot)
        {
            m_BulletRB2D.AddForce(gunpoint.right * m_BulletForce, ForceMode2D.Impulse);
            shot = true;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {


            if (collision.gameObject.CompareTag("Wall"))
            {
                //particulas
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy l_Enemy = collision.gameObject.GetComponent<Enemy>();
                l_Enemy.TakeDamage(m_Damage); //particulas en esa funcion como en la de window?
            }
            else if(collision.gameObject.CompareTag("Window"))
            {
                collision.gameObject.GetComponent<Window>().WindowDamage(m_Damage, this.transform.position);
            }
        
        
        Destroy(this.gameObject);
    }

}
