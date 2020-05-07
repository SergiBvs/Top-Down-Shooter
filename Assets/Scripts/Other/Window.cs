using System.Collections;
using UnityEngine;

public class Window : MonoBehaviour
{

    public float m_WindowHealth = 50;
    public GameObject m_DestroyParticles;
    public GameObject m_BreakParticles;
    private Transform m_Player;

    private Vector3 m_initRot;
    private Vector3 m_Rot;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_initRot = transform.rotation.eulerAngles;
        m_Rot = transform.rotation.eulerAngles;
    }


    public void WindowDamage(float l_amount, Vector2 l_particlePos)
    {
        if(m_Player.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0, -m_initRot.z);
            m_Rot = transform.rotation.eulerAngles;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, m_initRot.z);
            m_Rot = transform.rotation.eulerAngles;
        }
        m_WindowHealth -= l_amount;
        GameObject l_Particles = Instantiate(m_BreakParticles);
        l_Particles.transform.position = l_particlePos;
        l_Particles.transform.rotation = Quaternion.Euler(-m_Rot);
        l_Particles.SetActive(true);
        if(m_WindowHealth <= 0)
        {
            StartCoroutine(DestroyWindow());
        }
    }

    IEnumerator DestroyWindow()
    {
        m_DestroyParticles.transform.rotation = Quaternion.Euler(-m_Rot);
        m_DestroyParticles.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
