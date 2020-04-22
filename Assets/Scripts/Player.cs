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

        //RotatePlayerAlongMousePosition();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, (mousePosition - transform.position).normalized);
    }

    Vector3 lookPos;
    void RotatePlayerAlongMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit == true)
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.x = 0;
        lookDir.y = 0;
        transform.LookAt( lookPos, Vector3.forward);
    }
}
