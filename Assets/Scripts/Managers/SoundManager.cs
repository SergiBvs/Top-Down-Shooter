using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource m_AS;

    public AudioClip[] m_AudioClips;
  
    public void PlaySound(string SoundClip)
    {
        Instantiate(m_AS);

        foreach (AudioClip clip in m_AudioClips)
        {
            if (clip.name == SoundClip)
            {
                m_AS.clip = clip;
                m_AS.Play();
            }
        }

        /*if (cuando deje de sonar)
            Destroy(m_AS);*/
    }
}

