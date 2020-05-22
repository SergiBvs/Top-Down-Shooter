
using System.Collections;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public bool m_ElevatorOpen = false;
    bool inside = false;
    public SpriteRenderer m_LightSprite;

    public GameObject ElevatorPanel;

    private void Start()
    {
        if (!m_ElevatorOpen)
        {
            m_LightSprite.color = Color.red;
        }
        else
        {
            ElevatorOpen();
        }
    }

    public void ElevatorOpen()
    {
        m_ElevatorOpen = true;
        m_LightSprite.color = Color.green;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.CompareTag("Elevator"))
        {
            if (!inside)
            {
                inside = true;
                GameManager.instance.EnteredElevator();
                GetComponent<Animator>().SetTrigger("CLOSE");
                StartCoroutine(Wait());

            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        ElevatorPanel.SetActive(true);

    }

    


}
