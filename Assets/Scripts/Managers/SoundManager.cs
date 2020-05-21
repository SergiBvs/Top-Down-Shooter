using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [HideInInspector] public AudioSource m_AS;

    public AudioClip[] m_AudioClips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void PlaySound(string SoundClip)
    {
        m_AS = Instantiate((AudioSource)Resources.Load("AudioSource/AudioSource"));

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

