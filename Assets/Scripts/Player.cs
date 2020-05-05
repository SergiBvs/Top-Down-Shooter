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

    int l_WeaponNumber = 1;
    int l_LastWeaponNumber = 0;



    void Start()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        m_CurrentHealth = m_Health;
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

        // no funciona, y muy complicado, debe haber otra forma de hacer esto segurisimo

        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            l_WeaponNumber = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            l_WeaponNumber = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            l_WeaponNumber = 3;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            l_WeaponNumber = 4;
        }

        switch (l_WeaponNumber)
        {
            case 1:
                m_BasicGun.SetActive(true);
                ChangeWeapon();
                l_LastWeaponNumber = 1;
                break;
            case 2:
                m_AutoRifle.SetActive(true);
                ChangeWeapon();
                l_LastWeaponNumber = 2;
                break;
            case 3:
                m_Shotgun.SetActive(true);
                ChangeWeapon();
                l_LastWeaponNumber = 3;
                break;
            case 4:
                m_SomethingElse.SetActive(true);
                ChangeWeapon();
                l_LastWeaponNumber = 4;
                break;
            default:

                break;
        }*/

        

    }

    /*public void ChangeWeapon()
    {
        switch (l_LastWeaponNumber)
        {
            case 1:
                m_BasicGun.SetActive(false);
                break;
            case 2:
                m_AutoRifle.SetActive(false);
                break;
            case 3:
                m_Shotgun.SetActive(false);
                break;
            case 4:
                m_SomethingElse.SetActive(false);
                break;
            default:
                break;
        }
    }*/

    public void TakeDamage(int amount)
    {
        m_CurrentHealth -= amount;

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
