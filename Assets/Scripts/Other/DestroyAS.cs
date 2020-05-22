using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAS : MonoBehaviour
{

    AudioSource m_ThisAS;

    void Start()
    {
        m_ThisAS = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!m_ThisAS.isPlaying)
            Destroy(this.gameObject);
    }
}
