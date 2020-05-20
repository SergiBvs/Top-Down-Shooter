using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorScript : MonoBehaviour
{
    public ElevatorScript EScript;
    public Animator EAnim;

    private void Start()
    {
        if (!EScript.m_ElevatorOpen)
        {

            EAnim.SetTrigger("CLOSE");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("HI");
            if (EScript.m_ElevatorOpen)
            {
                EAnim.SetTrigger("OPEN");
            }
        }   
    }
}
