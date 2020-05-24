using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

    public AudioSource m_MusicSource;
    public AudioClip[] m_ElevatorMusic;
    public AudioClip[] m_GameMusic;
    public AudioClip m_StoreMusic;

    void Awake()
    {
        CreateInstance();
    }

    public void CreateInstance()
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

        while (m_MusicSource.volume < 0.7f)
        {
            m_MusicSource.volume += Time.deltaTime/2;
            yield return null;
        }
    }
}
