using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject m_Player;
    private Player m_PlayerScript;

    private Vector2 m_LastSeenPosition;
    private Rigidbody2D m_rb2d;

    public int m_Health;
    public float m_Damage;

    //Movement and patrol variables
    public float m_MovementSpeed = 5f;
    private float m_PatrolCooldown = 0f;
    private bool m_HasHitWall = false;
    Vector2 m_nextPatrolPosition = Vector2.zero;
    private bool m_HasReachedLastSeen = false;

    // Start is called before the first frame update
    void Start()
    {
        m_nextPatrolPosition = transform.position;
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
            //Si esta viendo al player
            if (hit.collider.CompareTag("Player"))
            {
                m_LastSeenPosition = hit.transform.position;
                Debug.DrawRay(transform.position, transform.up, Color.red, 20);
                MoveToPlayer();
            }
            else //Si no esta viendo al player
            {
                Debug.DrawRay(transform.position, transform.up, Color.blue, 20);

                //Comprobar si ya ha llegado al ultimo punto en el que vió al player antes de compenzar su patrulla.
                if (m_HasReachedLastSeen)
                    Patrol();
                else
                    MoveToPlayer();
                
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up, Color.gray, 20);
            Patrol();
        }

    }

    public void MoveToPlayer()
    {
        Vector2 l_position = transform.position;
        print("MoveToPlayer");

        //Ir hacia donde ha visto al player por ultima vez (Mas "natural"?)
        //No me salia con rigidbody xD
        Vector3 dir = m_LastSeenPosition - l_position;
        if (Vector2.Distance(l_position, m_LastSeenPosition) >= 0.1f)
        {
            transform.position += dir.normalized * Time.deltaTime * m_MovementSpeed;
            m_HasReachedLastSeen = false;
        }

        else
        {
            m_HasReachedLastSeen = true;
        }

    }


    //Moverse aleatoriamente cuando no esta viendo al player
    public void Patrol()
    {
        print("Patrol");
        //Si ya ha llegado pararse, esperar un tiempo y volver a empezar
        if (m_PatrolCooldown <= 0 || m_HasHitWall)
        {
            m_HasHitWall = false;
            m_nextPatrolPosition = transform.position;
            m_PatrolCooldown = 0;
            m_nextPatrolPosition = new Vector2(transform.position.x + Random.Range(-7f, 7f), transform.position.y + Random.Range(-7f, 7f));
            m_PatrolCooldown = Random.Range(4f, 8f);
            print("New patrol Position");
        }
        else m_PatrolCooldown -= Time.deltaTime;

        Vector2 l_position = transform.position;
        Vector3 dir = m_nextPatrolPosition - l_position;
        if (Vector2.Distance(l_position, m_nextPatrolPosition) >= 0.1f)
            transform.position += dir.normalized * Time.deltaTime * m_MovementSpeed * 0.5f;
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
       else if (collision.gameObject.CompareTag("Wall"))
       {
            m_HasHitWall = true;
            print("Touching wall");
       }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            m_HasHitWall = true;
            print("Touching wall");
        }
    }
}
