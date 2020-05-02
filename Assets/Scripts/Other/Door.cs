using UnityEngine;

public class Door : MonoBehaviour
{

    private GameObject m_Player;
    private bool m_Open = false;

    [SerializeField] private bool isHorizontal = false;
    [SerializeField] private bool isVertical = false;

    private Quaternion m_InitialRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_InitialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!m_Open)
                {
                    OpenDoor();
                }
                else
                {
                    CloseDoor();
                }
            }
        }
    }

    void OpenDoor()
    {
        if (isHorizontal)
        {
            if (m_Player.transform.position.y > transform.position.y)
            {
                transform.Rotate(0, 0, -90);
                m_Open = true;
            }
            else
            {
                transform.Rotate(0, 0, 90);
                m_Open = true;
            }
        }
        else if (isVertical)
        {
            if (m_Player.transform.position.x > transform.position.x)
            {
                transform.Rotate(0, 0, -90);
                m_Open = true;
            }
            else
            {
                transform.Rotate(0, 0, 90);
                m_Open = true;
            }
        }
    }

    void CloseDoor()
    {
        transform.rotation = m_InitialRotation;
        m_Open = false;
    }
}
