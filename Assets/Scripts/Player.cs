using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float m_directionX;
    float m_directionY;

    public Rigidbody2D m_PlayerRB2D;
    public int m_PlayerSpeed;

    
    void Start()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        m_directionX = Input.GetAxisRaw("Horizontal");
        m_directionY = Input.GetAxisRaw("Vertical");

        if(m_directionX >0) //go left
        {
            m_PlayerRB2D.AddForce(new Vector2(1, 0) * m_PlayerSpeed, ForceMode2D.Impulse);
        }
        else if(m_directionX <0) //go right
        {
            m_PlayerRB2D.AddForce(new Vector2(-1, 0) * m_PlayerSpeed, ForceMode2D.Impulse);
        }

        if(m_directionY < 0) //go down
        {
            m_PlayerRB2D.AddForce(new Vector2(0, -1) * m_PlayerSpeed, ForceMode2D.Impulse);
        }
        else if(m_directionY > 0) //go up
        {
            m_PlayerRB2D.AddForce(new Vector2(0, 1) * m_PlayerSpeed, ForceMode2D.Impulse);
        }

        if (Mathf.Abs(m_PlayerRB2D.velocity.x) >= 0 && m_directionX != 0)
        {
            m_PlayerRB2D.velocity = new Vector2(m_PlayerSpeed * m_directionX, m_PlayerRB2D.velocity.y);
        }

        if (Mathf.Abs(m_PlayerRB2D.velocity.y) >= 0 && m_directionY != 0)
        {
            m_PlayerRB2D.velocity = new Vector2(m_PlayerRB2D.velocity.x, m_PlayerSpeed * m_directionY);
        }

        if (m_directionX == 0 && m_directionY == 0)
        {
            m_PlayerRB2D.velocity = new Vector2(0, 0);
        }

        //i dont know what the fuck am i doing 
    }
}
