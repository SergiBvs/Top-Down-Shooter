using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject m_Player;
    public int m_Health;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(m_Player.transform.position), 10);
        
        if (hit)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(m_Player.transform.position), Color.red, hit.distance);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(m_Player.transform.position), Color.gray, hit.distance);
        }
        
    }

    public void TakeDamage(int amount)
    {
        m_Health -= amount; 

        if(m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
