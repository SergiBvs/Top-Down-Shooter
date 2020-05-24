using UnityEngine;

public class Arena_Enemy : MonoBehaviour
{
    private Transform m_Player;
    protected Animator m_Anim;

    protected WaveManager wManager;
    private float w_difficutly;

    [Header("Enemy Stats")]
    public int m_Health;
    public int m_Damage;
    protected float m_AttackCooldown;
    public float m_MaxAttackCooldown;

    [Header("Shooting")]
    public Transform m_GunTip;
    public string m_BulletName;


    //MOVEMENT AND PATROL
    [Header("Movement")]
    [Range(0, 10)]
    public float m_MovementSpeed = 5f;

    [Header("Loot Chances (%)")]
    [SerializeField] private int m_MaxCoin = 0;
    [SerializeField] private int m_minCoin = 0;
    [Range(0, 100)]
    [SerializeField] private int m_AmmoChance = 0;
    [Range(0, 100)]
    [SerializeField] private int m_HealthChance = 0;
    private int m_Luck;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Anim = GetComponent<Animator>();
        wManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        w_difficutly = wManager.g_Difficulty;


        float l_randomScale = Random.Range(-0.3f, 0.25f);
        transform.localScale += new Vector3(l_randomScale, l_randomScale, 0);

        m_Health += (int)w_difficutly * 5 + (int)l_randomScale*50;
        m_MovementSpeed += w_difficutly * 0.5f;


        m_Luck = GameManager.instance.m_Luck;

        m_MaxCoin += m_Luck;
        m_minCoin += m_Luck;
        m_AmmoChance += m_Luck;
        m_HealthChance += m_Luck;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        MoveToPlayer();
        if (m_AttackCooldown <= 0)
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

    public virtual void TakeDamage(int amount)
    {
        m_Health -= amount;
        Instantiate((GameObject)Resources.Load("DamagedParticles"), transform.position, transform.rotation);

        if (m_Health <= 0)
        {
            Loot();
            Instantiate((GameObject)Resources.Load("EnemyDeathParticles"), transform.position, Quaternion.Euler(90,0,0));
            wManager.m_EnemiesDefeated++;
            wManager.m_WaveEnemiesDefeated++;
            Destroy(this.gameObject);
        }
    }

    public virtual void Loot()
    {
        int l_RandomNumber = Random.Range(m_minCoin, m_MaxCoin + 1);
        for (int i = 0; i <= l_RandomNumber; i++)
        {
            GameObject coin = Instantiate((GameObject)Resources.Load("Loot/Coin"));
            coin.transform.position = transform.position;
            coin.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(1f, 2f), Random.Range(-2f, 2f)) * 30, ForceMode2D.Impulse);
        }


        int l_AmmoBoxChance = Random.Range(0, 100);
        if (l_AmmoBoxChance <= m_AmmoChance)
        {
            Instantiate(Resources.Load("Loot/Ammo"), transform.position, Quaternion.identity);
        }

        int l_HealthChance = Random.Range(0, 100);
        if (l_HealthChance <= m_HealthChance)
        {
            Instantiate(Resources.Load("Loot/Health"), transform.position, Quaternion.identity);
        }
    }
    public void MoveToPlayer()
    {

        transform.rotation = Quaternion.LookRotation(Vector3.forward, (m_Player.transform.position - transform.position).normalized);

        Vector2 l_position = transform.position;
        Vector3 dir = (Vector2)m_Player.position - l_position;
        transform.position += dir.normalized * Time.deltaTime * m_MovementSpeed;
        m_Anim.SetTrigger("WALK");
    }

}

