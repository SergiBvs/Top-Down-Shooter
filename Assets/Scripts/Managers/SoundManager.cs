using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] m_AudioClips;

    private GUIhelper GUIHelp;

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

    void Start()
    {
        ReassignObjs();
    }

    public void ReassignObjs()
    {
        GUIHelp = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIhelper>();
    }

    public void PlaySound(string SoundClip , float volume , float pitch)
    {
        //m_AS = Instantiate((AudioSource)Resources.Load("AudioSource/AudioSource"));

        foreach (AudioClip clip in m_AudioClips)
        {
            if (clip.name == SoundClip)
            {
                GUIHelp.m_AS.clip = clip;
                GUIHelp.m_AS.volume = volume;
                GUIHelp.m_AS.pitch = pitch;
                Instantiate(GUIHelp.m_AS);
                GUIHelp.m_AS.Play();
            }
        }
    }
}

