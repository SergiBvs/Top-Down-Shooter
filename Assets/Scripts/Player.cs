using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 m_movement;
    [HideInInspector] public Rigidbody2D m_PlayerRB2D;

    //Variables de player

    public int m_Health = 100;
    public int m_CurrentHealth;
    public int m_PlayerSpeed;

    public GameObject m_BasicGun;
    public GameObject m_AutoRifle;
    public GameObject m_Shotgun;
    public GameObject m_SomethingElse;



    void Start()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        m_CurrentHealth = m_Health;
        GameManager.instance.SetMaxHealth(m_Health);
        ChangeWeapon(1);
       
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

        //CAMBIO DE ARMAS

        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(4);
        }
    }

    public void ChangeWeapon(int weaponNumber)
    {
        switch (weaponNumber)
        {
            case 1:

                m_BasicGun.SetActive(true);
                m_AutoRifle.SetActive(false);
                m_Shotgun.SetActive(false);
                m_SomethingElse.SetActive(false);

                break;
            case 2:

                m_BasicGun.SetActive(false);
                m_AutoRifle.SetActive(true);
                m_Shotgun.SetActive(false);
                m_SomethingElse.SetActive(false);

                break;
            case 3:

                m_BasicGun.SetActive(false);
                m_AutoRifle.SetActive(false);
                m_Shotgun.SetActive(true);
                m_SomethingElse.SetActive(false);

                break;
            case 4:

                m_BasicGun.SetActive(false);
                m_AutoRifle.SetActive(false);
                m_Shotgun.SetActive(false);
                m_SomethingElse.SetActive(true);

                break;
            default:
                break;
        }
    }

    public void TakeDamage(int amount)
    {
        print("test");
        m_CurrentHealth -= amount;
        GameManager.instance.SetHealth(m_CurrentHealth);

        if(m_CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            TakeDamage(50);
        }
    }
}
