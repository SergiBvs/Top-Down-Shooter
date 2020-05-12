using UnityEngine;

public class Lasers : MonoBehaviour
{

    private enum LaserType { Static, Moving, OnOff, MovingOnOff}
    [SerializeField] private LaserType laserType = LaserType.Static;

    private GameObject laserBeam;
    private bool m_Active = true;

    //For Moving
    [Header("For Moving Lasers")]
    [SerializeField] private Vector2 MovingPosition_1 = Vector2.zero;
    [SerializeField] private Vector2 MovingPosition_2 = Vector2.zero;
    [SerializeField] private float MovingSpeed = 0.125f;
    private bool m_isGoingTo2 = false;
    private bool m_MovingActive = true;

    //For OnOff
    [Header("For ONOFF Lasers")]
    [SerializeField] private float m_ActiveTime = 1f;
    [SerializeField] private float m_DisabledTime = 1f;
    private float m_ActiveCopy;
    private float m_DisabledCopy;


    void Start()
    {
        m_DisabledCopy = m_DisabledTime;
        m_ActiveCopy = m_ActiveTime;
        laserBeam = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        switch (laserType)
        {
            case LaserType.Static:
                break;
            case LaserType.Moving:
                if(m_MovingActive)
                    Laser_Moving();
                break;
            case LaserType.OnOff:
                Laser_OnOff();
                break;
            case LaserType.MovingOnOff:
                if (m_MovingActive)
                    Laser_Moving();
                Laser_OnOff();
                break;
        }

        if (m_Active)
        {
            LaserActive();
        }
        else
        {
            laserBeam.SetActive(false);
        }
    }

    void LaserActive()
    {
        laserBeam.SetActive(true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 30, LayerMask.GetMask("Player", "Obstacle", "Enemy"));
        
        if (hit)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<Player>().TakeDamage(10000);
            }
        }
    }

    void Laser_Moving()
    {
        if (m_isGoingTo2)
        {
            if (Vector2.Distance(transform.position, MovingPosition_2) > 0.1f)
            {
                Vector3 targetDir = MovingPosition_2 - (Vector2)transform.position;
                transform.position += targetDir * Time.deltaTime * MovingSpeed;
            }
            else m_isGoingTo2 = false;
        }
        else
        {
            if (Vector2.Distance(transform.position, MovingPosition_1) > 0.1f)
            {
                Vector3 targetDir = MovingPosition_1 - (Vector2)transform.position;
                transform.position += targetDir * Time.deltaTime * MovingSpeed;
            }
            else m_isGoingTo2 = true;
        }
    }

    void Laser_OnOff()
    {
        if (m_Active)
        {
            if(m_ActiveTime <= 0)
            {
                m_Active = false;
                m_ActiveTime = m_ActiveCopy;
            }
            else
            {
                m_ActiveTime -= Time.deltaTime;
            }
        }
        else
        {
            if (m_DisabledTime <= 0)
            {
                m_Active = true;
                m_DisabledTime = m_DisabledCopy;
            }
            else
            {
                m_DisabledTime -= Time.deltaTime;
            }
        }
    }


}
