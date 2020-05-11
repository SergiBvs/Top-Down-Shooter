using UnityEngine;

public class Door : MonoBehaviour
{

    private enum DoorType { Horizontal, Vertical}
    [SerializeField] private DoorType doorType = DoorType.Horizontal;

    private GameObject m_Player;
    public bool m_Open = false;

    private Quaternion m_InitialRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_InitialRotation = transform.rotation;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!m_Open)
                {
                    OpenDoor(m_Player.transform);
                }
                else
                {
                    CloseDoor();
                }
            }
        }
    }

    public void OpenDoor(Transform whoOpens)
    {
        if (doorType == DoorType.Horizontal)
        {
            if (whoOpens.transform.position.y > transform.position.y)
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
            if (whoOpens.transform.position.x > transform.position.x)
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

    public void CloseDoor()
    {
        transform.rotation = m_InitialRotation;
        m_Open = false;
    }
}
