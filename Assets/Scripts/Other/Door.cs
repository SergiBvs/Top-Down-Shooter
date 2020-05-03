using UnityEngine;

public class Door : MonoBehaviour
{

    private enum DoorType { Horizontal, Vertical}
    [SerializeField] private DoorType doorType;

    private GameObject m_Player;
    private bool m_Open = false;

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
        if (doorType == DoorType.Horizontal)
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
        else if (doorType == DoorType.Vertical)
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
