using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReceptionTextScript : MonoBehaviour
{

    public GameObject m_TextPanel;
    private TMP_Text m_Text;
    public GameObject m_PressEText;

    private bool m_InConversation = false;
    private bool m_TextDone = false;

    private int m_TextInt;


    // Start is called before the first frame update
    void Start()
    {
        m_Text = m_TextPanel.GetComponentInChildren<TMP_Text>();
        m_Text.text = ReceptionText.Text[0];
        m_PressEText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!m_InConversation)
                m_PressEText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!m_InConversation)
                {
                    m_PressEText.SetActive(false);
                    m_TextPanel.SetActive(true);
                    StartCoroutine(BuildText(ReceptionText.Text[m_TextInt], 0.01f));
                    m_InConversation = true;
                    m_TextDone = false;
                }
                else
                {
                    if (m_TextDone)
                    {
                        if (m_TextInt < ReceptionText.Text.Length -1)
                        {
                            m_TextInt++;
                            StartCoroutine(BuildText(ReceptionText.Text[m_TextInt], 0.01f));
                            m_TextDone = false;
                        }
                        else
                        {
                            EndConversation();
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EndConversation();
        m_PressEText.SetActive(false);
    }

    void EndConversation()
    {
        m_InConversation = false;
        m_TextDone = true;
        m_TextInt = 0;
        m_TextPanel.SetActive(false);
    }

    //Para escribir letra a letra
    private IEnumerator BuildText(string text, float waitTime)
    {
        m_TextDone = false;
        m_Text.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            m_Text.text = string.Concat(m_Text.text, text[i]);
            yield return new WaitForSeconds(waitTime);
        }
        m_TextDone = true;
    }
}

public class ReceptionText
{
    public static string[] Text =
    {
        "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis ",
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud",
        "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis "
    };
}
