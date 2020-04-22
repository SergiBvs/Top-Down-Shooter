using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 m_movement;
    public Rigidbody2D m_PlayerRB2D;
    public int m_PlayerSpeed;

    
    void Start()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //MOVIMIENTO

        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_PlayerRB2D.MovePosition(m_PlayerRB2D.position + m_movement * m_PlayerSpeed * Time.deltaTime);

        //APUNTADO

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, (mousePosition - transform.position).normalized); 
    }

}
