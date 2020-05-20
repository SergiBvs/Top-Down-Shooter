using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private enum PatrolType { Cyclic, PingPong, Static, None }

    protected GameObject m_Player;
    private Player m_PlayerScript;
    protected Animator m_Anim;

    [Header("Enemy Stats")]
    public int m_Health;
    public int m_Damage;
    protected float m_AttackCooldown;
    public float m_MaxAttackCooldown;

    [Header("Shooting")]
    public Transform m_GunTip;
    public string m_BulletName;


    //MOVEMENT AND PATROL
    [Header("Movement & Patrol")]
    [SerializeField] PatrolType patrolType = PatrolType.Cyclic;
    [Range(0, 10)]
    public float m_PatrolSpeed = 4f;
    [Range(0, 10)]
    public float m_MovementSpeed = 5f;
    protected float m_PatrolCooldown = 0f;
    private bool m_HasReachedLastSeen = false;
    protected Vector2 m_LastSeenPosition;

    bool ComesFromChasing = false;

    public Transform[] pivots;
    private int m_PivotIndex = 0;
    private int dir = 1;
    private bool HasReachedPivot = false;
    private Transform m_NextPatrolPosition;

    [Header("Loot Chances (%)")]
    [SerializeField] private int m_MaxCoin = 0;
    [SerializeField] private int m_minCoin = 0;
    [Range(0,100)]
    [SerializeField] private int m_AmmoChance = 0;
    [Range(0,100)]
    [SerializeField] private int m_HealthChance = 0;

    public virtual void Start()
    {
        m_AttackCooldown = m_MaxAttackCooldown;
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_LastSeenPosition = transform.position;
        m_Anim = GetComponent<Animator>();

        if(patrolType != PatrolType.None)
            m_NextPatrolPosition = pivots[0];
        if(patrolType == PatrolType.Static)
        {
            m_HasReachedLastSeen = true;
        }
    }

    public virtual void Update()
    {
        //APUNTANDO
        //Raycast que sigue constantemente al Player, mirando si hay paredes de por medio.
        if (!GameManager.instance.m_IsGameOverPanelOn)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, m_Player.transform.position - transform.position, 50, LayerMask.GetMask("Player", "Obstacle"));
            Debug.DrawRay(transform.position, hit.point - (Vector2)transform.position, Color.gray, 0.1f);

            if (hit)
            {
                //Si esta viendo al player ir hacia el
                if (hit.collider.CompareTag("Player"))
                {
                    //Si encuentra al player mirarle constantemente.
                    if (Vector2.Distance(hit.point, transform.position) <= 40)
                    {
                        m_LastSeenPosition = hit.transform.position;
                        FaceToPlayer();
                        MoveToPlayer();
                        if (Vector2.Distance(hit.point, transform.position) <= 30)
                            AttackState();
                    }
                }
                else //Si no esta viendo al player
                {
                    //Comprobar si ya ha llegado al ultimo punto en el que vió al player antes de compenzar su patrulla.
                    if ((m_HasReachedLastSeen) && patrolType != PatrolType.None)
                    {
                        transform.rotation = Quaternion.LookRotation(Vector3.forward, ((Vector2)m_NextPatrolPosition.position - (Vector2)transform.position).normalized);
                        if (patrolType != PatrolType.Static)
                            Patrol();
                        else
                        {
                            HasReachedPivot = true;
                            m_Anim.SetTrigger("IDLE");
                        }
                    }
                    else
                    {
                        if (patrolType != PatrolType.None)
                        {
                            MoveToPlayer();
                            transform.rotation = Quaternion.LookRotation(Vector3.forward, (m_LastSeenPosition - (Vector2)transform.position).normalized);
                        }
                        
                    }
                }
            }
            else
            {
                if (patrolType != PatrolType.None && patrolType != PatrolType.Static)
                    Patrol();
                else if(patrolType == PatrolType.Static)
                {
                    m_Anim.SetTrigger("IDLE");
                }
            }
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
        ComesFromChasing = true;
        m_Anim.SetTrigger("WALK");
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
        Instantiate((GameObject)Resources.Load("Bullets/Enemy/" + m_BulletName), m_GunTip.position, Quaternion.Euler(transform.rotation.eulerAngles));
        Instantiate(Resources.Load("Shoot_Particles"), m_GunTip.transform);
        m_AttackCooldown = m_MaxAttackCooldown;
    }

    //Moverse de pivote en pivote cuando no esta viendo al player
    protected virtual void Patrol()
    {
        if ((m_PatrolCooldown <= 0) || ComesFromChasing)
        {
            
            HasReachedPivot = false;
            ChoosePatrolDirection();
            m_PatrolCooldown = Random.Range(3f, 5f);
        }
        else if (HasReachedPivot)
        {
            m_Anim.SetTrigger("IDLE");
            m_PatrolCooldown -= Time.deltaTime;
        }

        Vector2 l_position = transform.position;
        Vector3 dir = (Vector2)m_NextPatrolPosition.position - l_position;
        if (Vector2.Distance(l_position, m_NextPatrolPosition.position) >= 0.2f)
        {
            m_Anim.SetTrigger("WALK");
            transform.position += dir.normalized * Time.deltaTime * m_PatrolSpeed;
        }
        else
        {
            HasReachedPivot = true;
            m_Anim.SetTrigger("IDLE");
        }
    }

    void ChoosePatrolDirection()
    {
        if (!ComesFromChasing)
        {
            if (m_PivotIndex == 0)
                dir = 1;
            else if (m_PivotIndex == pivots.Length - 1)
            {
                if (patrolType == PatrolType.PingPong)
                    dir = -1;
                else if (patrolType == PatrolType.Cyclic)
                    m_PivotIndex = -1;
            }

            m_PivotIndex += dir;
            m_NextPatrolPosition = pivots[m_PivotIndex];
        }
        else
        {
            ComesFromChasing = false;
            int closerOne = 0;
            float Lowestdist = 0;
            //Si viene de perseguir al player cambiar el siguiente pivote al pivote mas cercano para no quedarse atrapado con paredes.
            for (int i = 0; i < pivots.Length; i++)
            {
                float CurrentDist = Vector2.Distance(transform.position, pivots[i].position);
                if (i == 0) {
                    Lowestdist = CurrentDist;
                    closerOne = i;
                }
                else
                {
                    if(CurrentDist < Lowestdist)
                    {
                        Lowestdist = CurrentDist;
                        closerOne = i;
                    }
                }
            }
            m_PivotIndex = closerOne;
            m_NextPatrolPosition = pivots[closerOne];
        }
        
    }

    public virtual void TakeDamage(int amount)
    {
        m_Health -= amount; 

        if(m_Health <= 0)
        {
            Loot();
            Destroy(this.gameObject);
        }
    }

    public virtual void Loot()
    {
        int l_RandomNumber = Random.Range(m_minCoin, m_MaxCoin+1);
        for(int i = 0; i <= l_RandomNumber; i++)
        {
            GameObject coin = Instantiate((GameObject)Resources.Load("Loot/Coin"));
            coin.transform.position = transform.position;
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 20, ForceMode2D.Impulse);
        }
    

        int l_AmmoBoxChance = Random.Range(0, 100);
        if(l_AmmoBoxChance <= m_AmmoChance)
        {
            Instantiate(Resources.Load("Loot/Ammo"), transform.position, Quaternion.identity);
        }

        int l_HealthChance = Random.Range(0, 100);
        if (l_HealthChance <= m_HealthChance)
        {
            Instantiate(Resources.Load("Loot/Health"), transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
       {
            m_PlayerScript.TakeDamage(m_Damage);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            StartCoroutine(OpenDoor(collision.GetComponent<Door>()));
        }
    }

    IEnumerator OpenDoor(Door door)
    {
        if(!door.m_Open)
            door.OpenDoor(transform);
        yield return new WaitForSeconds(2.5f);
        door.CloseDoor();
    }

}
