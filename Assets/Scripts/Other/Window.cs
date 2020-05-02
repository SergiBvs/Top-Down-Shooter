using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Window : MonoBehaviour
{

    public float m_WindowHealth = 50;
    public GameObject m_DestroyParticles;
    public GameObject m_BreakParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WindowDamage(float l_amount, Vector2 l_particlePos)
    {
        m_WindowHealth -= l_amount;
        m_BreakParticles.transform.position = l_particlePos;
        m_BreakParticles.SetActive(true);
        if(m_WindowHealth <= 0)
        {
            StartCoroutine(DestroyWindow());
            //m_DestroyParticles.transform.SetParent(null);
            //m_DestroyParticles.SetActive(true);
            //gameObject.SetActive(false);
        }
    }

    IEnumerator DestroyWindow()
    {
        m_DestroyParticles.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
