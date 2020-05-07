using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 m_movement;
    [HideInInspector] public Rigidbody2D m_PlayerRB2D;

    //Variables de player

    
    public int m_CurrentHealth;
    public int m_PlayerSpeed;

    //public static bool GunBoughtArrayActivator = false;

    public GameObject[] GunArray;
    public /*static*/ bool[] GunBoughtArray;

    void Start()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        m_CurrentHealth = GameManager.instance.m_Health;

        //quitar comentarios cuando se deje de testear

        //if(!GunBoughtArrayActivator) 
        //{
            GunBoughtArray = new bool[GunArray.Length];
            //GunBoughtArrayActivator = true;
        //}

        GunBoughtArray[0] = true;

        ChangeWeapon(0);
    }


    void Update()
    {
        //MOVIMIENTO
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_PlayerRB2D.MovePosition(m_PlayerRB2D.position + m_movement * m_PlayerSpeed * Time.deltaTime);


        //APUNTADO

        if(!GameManager.instance.m_GameIsPaused)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (mousePosition - transform.position).normalized);
        }

        //CAMBIO DE ARMAS  

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GunBoughtArray[1])
                ChangeWeapon(1);
            else { }
            //mostrar de alguna forma que no se puede cambiar de arma hasta que se compre
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (GunBoughtArray[2])
                ChangeWeapon(2);
            else { }
            //mostrar de alguna forma que no se puede cambiar de arma hasta que se compre
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (GunBoughtArray[3])
                ChangeWeapon(3);
            else { }
            //mostrar de alguna forma que no se puede cambiar de arma hasta que se compre
        }
    }

    public void ChangeWeapon(int weaponNumber)
    {

        foreach (GameObject gun in GunArray)
        {
            gun.SetActive(false);
        }

        GunArray[weaponNumber].SetActive(true);
    }

    public void TakeDamage(int amount)
    {
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
