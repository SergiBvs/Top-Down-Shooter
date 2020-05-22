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

    bool m_TalkedOnce = false;
    int rows = 0;

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
                    if (m_TalkedOnce)
                    {
                        m_TextInt = 10;
                        rows = 14;
                    }
                    else
                    {
                        rows = 9;
                    }
                        
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
                        if (m_TextInt < rows)
                        {
                            m_TextInt++;
                            StartCoroutine(BuildText(ReceptionText.Text[m_TextInt], 0.01f));
                            m_TextDone = false;
                        }
                        else
                        {
                            m_TalkedOnce = true;
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
        rows = 0;
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
        "I can't believe i lost that thing... Where could it be!?... I must find it before the boss finds out, or else he'll...",
        "...",
        "Oh!",
        "I didn't see you there. Sorry.",
        "Welcome to [Name of the game], do you have any bussiness here?",
        "Oh you have an 'appointment' with the boss? Ok...",
        "Wait over there until someone comes for you. Or not, I don't really care. You do you.",
        "Just don't make anything stupid, these people don't play around.",
        "You should be prepared, i suggest you check out the store just outside, could be really helpful.",
        "Have a nice day! And good luck.",

        "What? You're still around here? Don't you have somenthing better to do? You know... 'Meeting' the boss and all...",
        "I'm not going to stop you from doing it, i'm just here to pay for college. Risking my life for my boss isn't really worth it.",
        "Go to that elevator over there. It'll take you to the next floor.",
        "By the way, I really don't know who designed this place, but there's an elevator for each floor. You'll have to manage to activate each one of them.",
        "Anyways I need to do some paperwork so... Bye."
    };
}
