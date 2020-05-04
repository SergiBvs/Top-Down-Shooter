
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    GameObject m_Player;
    public float m_SmoothSpeed = 10f;
    Vector3 m_SmoothMovement;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 l_TargetPos = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y, -10);
        m_SmoothMovement = Vector3.Lerp(transform.position, l_TargetPos, m_SmoothSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.position = m_SmoothMovement;
    }
}
