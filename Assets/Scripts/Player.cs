using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector2 m_movement;
    [HideInInspector] public Rigidbody2D m_PlayerRB2D;

    //Variables de player
    [Header("Stats")]
    public int m_CurrentHealth;
    public static float m_PlayerSpeed = 10;

    [Header("Guns")]
    public static bool GunBoughtArrayActivator = false;
    public GameObject[] GunArray;
    public GameObject[] GunImages;
    public static bool[] GunBoughtArray;
    [HideInInspector] public Gun m_CurrentGun;

    private GUIhelper GUIHelp;
    private Animator m_Anim;

    public static int m_lastWeapon = 0;

   

    void Start()
    {
        m_PlayerRB2D = this.GetComponent<Rigidbody2D>();
        m_CurrentHealth = GameManager.instance.m_Health;
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
        m_Anim = GetComponent<Animator>();

        if (GameManager.instance.m_NeedsSpawnPosition)
        {
            transform.position = GameManager.instance.m_SpawnPosition;
            GameManager.instance.m_NeedsSpawnPosition = false;
        }
        //quitar comentarios cuando se deje de testear

        if(!GunBoughtArrayActivator) 
        {
            GunBoughtArray = new bool[GunArray.Length];
            GunBoughtArrayActivator = true;
        }

        GunBoughtArray[0] = true;
        m_CurrentGun = GunArray[0].GetComponent<Gun>();

        int i = 0;
        foreach (GameObject item in GunArray)
        {
            GameManager.instance.m_WeaponsArray[i] = item.GetComponent<Gun>();
            i++;
        }

        StartCoroutine(WaitForEndOfAssignment());
        
    }

    IEnumerator WaitForEndOfAssignment()
    {
        yield return new WaitForSeconds(0.05f);
        ChangeWeapon(m_lastWeapon);
    }

   

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) //test 
        {
            TakeDamage(50);
        }


        //MOVIMIENTO
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_PlayerRB2D.MovePosition(m_PlayerRB2D.position + m_movement * m_PlayerSpeed * Time.deltaTime);
        if(m_movement.x != 0 || m_movement.y != 0)
        {
            m_Anim.SetTrigger("WALK");
        }
        else
        {
            m_Anim.SetTrigger("IDLE");
        }

        //APUNTADO

        if(!GameManager.instance.m_GameIsPaused )
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (mousePosition - transform.position).normalized);
        }

        //CAMBIO DE ARMAS  

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_lastWeapon = 0;
            ChangeWeapon(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GunBoughtArray[1])
            {
                m_lastWeapon = 1;
                ChangeWeapon(1);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (GunBoughtArray[2])
            {
                m_lastWeapon = 2;
                ChangeWeapon(2);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);

        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (GunBoughtArray[3])
            {
                m_lastWeapon = 3;
                ChangeWeapon(3);
            }
            else
                SoundManager.instance.PlaySound("ErrorSound1", 1, 1);
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
        GUIHelp.m_AmmoText.color = new Color(255, 255, 255);
        GUIHelp.m_ReloadPanel.SetActive(false);
        /*m_CurrentGun = GunArray[weaponNumber].GetComponent<Gun>();
        m_CurrentGun.UpdateGUI();
        m_CurrentGun.m_canShoot = true;*/
        GameManager.instance.GetCurrentWeapon();
        StartCoroutine(UpdateGUIAfterTime());
        GameManager.instance.m_CurrentWeapon.m_canShoot = true;
    }

    IEnumerator UpdateGUIAfterTime()
    {
        yield return new WaitForSeconds(0.05f);
        GameManager.instance.m_CurrentWeapon.UpdateGUI();
        GameManager.instance.m_CurrentWeapon.m_canShoot = true;
    }

    public void TakeDamage(int amount)
    {
        m_CurrentHealth -= amount;
        GameManager.instance.SetHealth(m_CurrentHealth);
        Instantiate((GameObject)Resources.Load("DamagedParticles"), transform);

        int l_soundRand = Random.Range(1, 3);
        SoundManager.instance.PlaySound("Hurt" +l_soundRand, 1 ,1);

        if(m_CurrentHealth <= 0)
        {
            SoundManager.instance.PlaySound("GameOver", 1, 1);
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


    public void PlayFootsteps()
    {
        int l_randomSound = Random.Range(1, 5);

        SoundManager.instance.PlaySound("FootStep" + l_randomSound, 1, 1);
    }
}
