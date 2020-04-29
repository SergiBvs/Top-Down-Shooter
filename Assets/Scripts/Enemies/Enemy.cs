using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject m_Player;
    private Player m_PlayerScript;

    private Vector2 m_LastSeenPosition;
    private Rigidbody2D m_rb2d;

    public int m_Health;
    public float m_Damage;

    public float m_MovementSpeed = 5f;
    private float m_PatrolCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_rb2d = GetComponent<Rigidbody2D>();
        m_LastSeenPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        //APUNTANDO
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (m_Player.transform.position - transform.position).normalized);

        //Raycast siguiendo constantemente al player.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 20, LayerMask.GetMask("Player", "Default"));
        
        
        if (hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                m_LastSeenPosition = hit.transform.position;
                print(m_LastSeenPosition);
                Debug.DrawRay(transform.position, transform.up, Color.red, 20);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.up, Color.blue, 20);
                //Patrol();
            }
            Debug.Log("hit " + hit.collider.tag);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up, Color.gray, 20);
        }

        Movement();
    }

    public void Movement()
    {
        Vector2 l_position = transform.position;

        //Ir hacia donde ha visto al player por ultima vez (Mas "natural"?)
        //No me salia con rigidbody xD
        Vector3 dir = m_LastSeenPosition - l_position;
        if(Vector2.Distance(l_position, m_LastSeenPosition) >= 0.1f)
            transform.position += dir.normalized * Time.deltaTime * m_MovementSpeed;

    }

    //NO FUNCIONA.
    //Moverse aleatoriamente cuando no esta viendo al player
    public void Patrol()
    {
        Vector2 l_position = transform.position;
        m_LastSeenPosition = new Vector2(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(5f,5f));

        //Si aun no ha llegado moverse hacia alli
        if (Vector2.Distance(l_position, m_LastSeenPosition) >= 0.1f)
            Debug.Log(".");
        // transform.position += (Vector3)m_LastSeenPosition.normalized * Time.deltaTime * m_MovementSpeed;

        //HACER QUE TAMBIEN PARE SI TOCA UNA PARED PARA EVITAR BUGS

        //Si ya ha llegado pararse, esperar un tiempo y volver a empezar
        else
        {
            if (m_PatrolCooldown <= 0)
            {
                m_PatrolCooldown = Random.Range(2f, 5f);
                Patrol();
            }
            else m_PatrolCooldown -= Time.deltaTime;
        }
    }

    public void TakeDamage(int amount)
    {
        m_Health -= amount; 

        if(m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
       {
            m_PlayerScript.TakeDamage(m_Damage);
       }
    }
}
