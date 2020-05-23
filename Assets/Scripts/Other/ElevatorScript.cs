
using System.Collections;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public bool m_ElevatorOpen = false;
    bool inside = false;
    public SpriteRenderer m_LightSprite;

    public GameObject ElevatorPanel;

    MusicManager musicManager;

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

        musicManager = MusicManager.instance;
    }

    public void ElevatorOpen()
    {
        m_ElevatorOpen = true;
        m_LightSprite.color = Color.green;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.m_AlreadyInElevator)
        {
            if (collision.CompareTag("Player") && this.CompareTag("Elevator"))
            {
                if (!inside)
                {
                    inside = true;
                    GameManager.instance.ChangeMusic(musicManager.m_ElevatorMusic[Random.Range(0, musicManager.m_ElevatorMusic.Length)]);
                    MusicManager.instance.gameObject.GetComponent<AudioHighPassFilter>().enabled = true;
                    GetComponent<Animator>().SetTrigger("CLOSE");
                    StartCoroutine(Wait());

                }
            }
        }
        else
        {
            GetComponent<Animator>().SetTrigger("OPEN");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            
            MusicManager.instance.gameObject.GetComponent<AudioHighPassFilter>().enabled = false;
            if (GameManager.instance.m_AlreadyInElevator)
            {
                GameManager.instance.m_AlreadyInElevator = false;
            }
            else
            {
                GameManager.instance.ChangeMusic(musicManager.m_GameMusic[0]);
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
        ElevatorPanel.SetActive(true);

    }

    


}
