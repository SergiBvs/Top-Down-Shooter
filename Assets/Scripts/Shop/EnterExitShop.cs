using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterExitShop : MonoBehaviour
{

    public GameObject m_PressBtoBuyText;
    public GameObject m_GUICanvas;
    public GameObject m_ReloadBarCanvas;
    public GameObject m_ShopPanel;

    public bool m_IsNearShop;
    public TextMeshProUGUI m_CurrencyText;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B) && !GameManager.instance.m_IsInShop && m_IsNearShop)
            EnterShop();
        else if (Input.GetKeyDown(KeyCode.B) && GameManager.instance.m_IsInShop)
            ExitShop();

        if(GameManager.instance.m_IsInShop)
        {
            m_CurrencyText.text = GameManager.instance.m_Currency + "$";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_PressBtoBuyText.SetActive(true);
            m_IsNearShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_PressBtoBuyText.SetActive(false);
            m_IsNearShop = false;
        }
    }

    public void EnterShop()
    {
        m_PressBtoBuyText.SetActive(false);
        m_GUICanvas.SetActive(false);
        m_ReloadBarCanvas.SetActive(false);
        Time.timeScale = 0;
        GameManager.instance.m_IsInShop = true;
        m_ShopPanel.SetActive(true);
    }

    public void ExitShop()
    {
        m_PressBtoBuyText.SetActive(true);
        m_GUICanvas.SetActive(true);
        Time.timeScale = 1;
        GameManager.instance.m_IsInShop = false;
        m_ShopPanel.SetActive(false);
    }
}