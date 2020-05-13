using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointScript : MonoBehaviour
{

    Transform m_PlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position =  m_PlayerPos.position + (-m_PlayerPos.position + mousePosition) / 4;
    }
}
