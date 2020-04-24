using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : MonoBehaviour
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

        //NOT WORKING XD
        if (Physics2D.Raycast(transform.position, m_Player.transform.position, 5))
        {
            Debug.DrawRay(transform.position, m_Player.transform.position, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, m_Player.transform.position, Color.gray);
        }
    }
}
