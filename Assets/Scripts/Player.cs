using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector2 m_movement;
    [HideInInspector] public Rigidbody2D m_PlayerRB2D;

    //Variables de player
    
    public int m_CurrentHealth;
    public int m_PlayerSpeed;

    //public static bool GunBoughtArrayActivator = false;

    public GameObject[] GunArray;
    public GameObject[] GunImages;
    public /*static*/ bool[] GunBoughtArray;

    [HideInInspector] public Gun m_CurrentGun;

    private GUIhelper GUIHelp;


    void Start()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        m_CurrentHealth = GameManager.instance.m_Health;
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();

        //quitar comentarios cuando se deje de testear

        //if(!GunBoughtArrayActivator) 
        //{
            GunBoughtArray = new bool[GunArray.Length];
            //GunBoughtArrayActivator = true;
        //}

        GunBoughtArray[0] = true;
        m_CurrentGun = GunArray[0].GetComponent<Gun>();
        ChangeWeapon(0);
    }


    void Update()
    {
        //MOVIMIENTO
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_PlayerRB2D.MovePosition(m_PlayerRB2D.position + m_movement * m_PlayerSpeed * Time.deltaTime);


        //APUNTADO

        if(!GameManager.instance.m_GameIsPaused )
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

        foreach(GameObject image in GunImages)
        {
            image.SetActive(false);
        }

        GunArray[weaponNumber].SetActive(true);
        GunImages[weaponNumber].SetActive(true);
        m_CurrentGun = GunArray[weaponNumber].GetComponent<Gun>();
        GUIHelp.m_AmmoText.color = new Color(255, 255, 255);
        GUIHelp.m_ReloadPanel.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        m_CurrentHealth -= amount;
        GameManager.instance.SetHealth(m_CurrentHealth);

        if(m_CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
            GameManager.instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            TakeDamage(50);
        }
    }

    public void Fall(Transform DZpivot)
    {
        //Fall Animation
        TakeDamage(20);
        transform.position = DZpivot.position;
    }
}
