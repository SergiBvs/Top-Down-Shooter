using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameObject m_Player;
    private Player m_PlayerScript;

    public int m_Health;
    public float m_Damage;
    protected float m_AttackCooldown;
    public float m_MaxAttackCooldown;

    //MOVEMENT AND PATROL
    public float m_MovementSpeed = 5f;
    protected float m_PatrolCooldown = 0f;
    private bool m_HasHitWall = false;
    private bool m_HasReachedLastSeen = false;
    protected Vector2 m_nextPatrolPosition = Vector2.zero;
    protected Vector2 m_LastSeenPosition;
    public float m_PatrolDistance = 0;

    public virtual void Start()
    {
        m_AttackCooldown = m_MaxAttackCooldown;
        m_nextPatrolPosition = transform.position;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_LastSeenPosition = transform.position; 
    }

    public virtual void Update()
    {
        //APUNTANDO
        //Raycast que sigue constantemente al Player, mirando si hay paredes de por medio.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_Player.transform.position - transform.position, 25, LayerMask.GetMask("Player", "Default"));
        Debug.DrawRay(transform.position, hit.point - (Vector2)transform.position, Color.red,0.1f);
        if (hit.collider.CompareTag("Player"))
        {
            //Si encuentra al player mirarle constantemente.
            FaceToPlayer();
        }
        else
        {
            //Mirar donde sea
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (m_nextPatrolPosition - (Vector2)transform.position).normalized);
        }
                
        
        if (hit)
        {
            //Si esta viendo al player ir hacia el
            if (hit.collider.CompareTag("Player"))
            {
                m_LastSeenPosition = hit.transform.position;
                MoveToPlayer();
                AttackState();
            }
            else //Si no esta viendo al player
            {

                //Comprobar si ya ha llegado al ultimo punto en el que vió al player antes de compenzar su patrulla.
                if (m_HasReachedLastSeen)
                    Patrol();
                else
                    MoveToPlayer();
                
            }
        }
        else
        {
            Patrol();
        }
    }

    public virtual void FaceToPlayer()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (m_Player.transform.position - transform.position).normalized);
    }

    public virtual void MoveToPlayer()
    {
        Vector2 l_position = transform.position;

        //Ir hacia donde ha visto al player por ultima vez (Mas "natural"?) No me salia con rigidbody xD
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

    public virtual void AttackState()
    {
        if(m_AttackCooldown <= 0)
        {
            Shoot();
        }
        else
        {
            m_AttackCooldown -= Time.deltaTime;
        }
    }

    public virtual void Shoot()
    {
        //HACER UNA PEQUEÑA ALEATORIEDAD EN LA DESVIACION PARA QUE NO SEA TAN PRECISO.
        RaycastHit2D l_ShotHit = Physics2D.Raycast(transform.position, transform.up, 30, LayerMask.GetMask("Player", "Default"));
        if (l_ShotHit.collider.CompareTag("Player"))
        {
            //CAMBIAR ESTA LINEA POR LAS GUAYS
            Debug.DrawLine(transform.position, m_Player.transform.position, Color.red, 0.1f);
            m_PlayerScript.TakeDamage(m_Damage);
            
        }
        m_AttackCooldown = m_MaxAttackCooldown;
    }

    //Moverse aleatoriamente cuando no esta viendo al player
    protected virtual void Patrol()
    {
        //Si ya ha llegado o ha tocado una pared pararse, esperar un tiempo y volver a empezar
        if (m_PatrolCooldown <= 0 || m_HasHitWall)
        {
            m_HasHitWall = false;
            m_nextPatrolPosition = transform.position;
            m_PatrolCooldown = 0;
            m_nextPatrolPosition = new Vector2(transform.position.x 
                + Random.Range(-m_PatrolDistance, m_PatrolDistance), transform.position.y + Random.Range(-m_PatrolDistance, m_PatrolDistance));
            m_PatrolCooldown = Random.Range(4f, 8f);
        }
        else m_PatrolCooldown -= Time.deltaTime;

        Vector2 l_position = transform.position;
        Vector3 dir = m_nextPatrolPosition - l_position;
        if (Vector2.Distance(l_position, m_nextPatrolPosition) >= 0.1f)
            transform.position += dir.normalized * Time.deltaTime * m_MovementSpeed * 0.5f;
    }

    public virtual void TakeDamage(int amount)
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
       }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            m_HasHitWall = true;
        }
    }
}
