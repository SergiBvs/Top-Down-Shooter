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
    public void WindowDamage(float l_amount, Vector2 l_particlePos)
    {
        m_WindowHealth -= l_amount;
        GameObject l_Particles = Instantiate(m_BreakParticles);
        l_Particles.transform.position = l_particlePos;
        l_Particles.transform.rotation = transform.rotation;
        l_Particles.SetActive(true);
        if(m_WindowHealth <= 0)
        {
            StartCoroutine(DestroyWindow());
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
