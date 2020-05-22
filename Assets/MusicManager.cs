﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource m_MusicSource;
    public AudioClip[] m_ElevatorMusic;
    public AudioClip[] m_GameMusic;
    public AudioClip m_StoreMusic;


    public void ChangeMusic(AudioClip nextSong)
    {
        StartCoroutine(MusicFade(nextSong));
    }

    IEnumerator MusicFade(AudioClip nextSong)
    {

        while (m_MusicSource.volume > 0f)
        {
            m_MusicSource.volume -= Time.deltaTime;
            yield return null;
        }

        m_MusicSource.Stop();
        m_MusicSource.clip = nextSong;
        m_MusicSource.Play();

        while (m_MusicSource.volume < 1f)
        {
            m_MusicSource.volume += Time.deltaTime/2;
            yield return null;
        }
    }
}
