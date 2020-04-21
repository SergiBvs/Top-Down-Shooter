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

    }

    Vector3 lookPos;
    void RotatePlayerAlongMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }
        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir, Vector3.up);
    }
}
