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

        foreach (AudioClip clip in m_AudioClips)
        {
            if (clip.name == SoundClip)
            {
                AudioSource aS = Instantiate(GUIHelp.m_AS);
                aS.clip = clip;
                aS.volume = volume;
                aS.pitch = pitch;
                aS.Play();
            }
        }
    }
}

