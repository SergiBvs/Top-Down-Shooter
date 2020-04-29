﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject m_Player;
    private Player m_PlayerScript;

    public int m_Health;
    public int m_Damage;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //APUNTANDO
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (m_Player.transform.position - transform.position).normalized);

        //Raycast siguiendo constantemente al player.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 20, LayerMask.GetMask("Player", "Default"));
        
        
        if (hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Movement();
                Debug.DrawRay(transform.position, transform.up, Color.red, 20);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.up, Color.blue, 20);
            }
            Debug.Log("hit " + hit.collider.tag);
        }
        else
        {
            Debug.Log("not hitting");
            Debug.DrawRay(transform.position, transform.up, Color.gray, 20);
        }
        
    }

    public void Movement()
    {

    }

    public void TakeDamage(int amount)
    {
        m_Health -= amount; 

        if(m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
       {
            m_PlayerScript.TakeDamage(m_Damage);
       }
    }
}
