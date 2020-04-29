using System.Collections;
using System.Collections.Generic;
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
        //APUNTANDO
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (m_Player.transform.position - transform.position).normalized);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 20, LayerMask.GetMask("Player", "Default"));
        
        if (hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
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

    public void TakeDamage(int amount)
    {
        m_Health -= amount; 

        if(m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
