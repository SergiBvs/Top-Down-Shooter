using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    int m_currentWave = 0;
    public float g_Difficulty = 0;
    int m_EnemySpawnNumber = 0;
    public Transform[] m_EnemySpawners;
    bool waveStarted = false;
    bool waveEnded = false;

    bool Spawning = false;

    public GameObject[] enemies;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (waveStarted)
        {
            if (!Spawning)
            {
                if (m_EnemySpawnNumber > 0)
                {
                    StartCoroutine(EnemySpawn());
                    Spawning = true;
                }
                else
                {
                    waveEnded = true;
                    waveStarted = false;
                }
            }
        }
        else if (waveEnded)
        {
            StartCoroutine(WaveStart());
        }
    }

    IEnumerator WaveStart()
    {
        yield return new WaitForSeconds(3);
        m_currentWave++;
        m_EnemySpawnNumber = Random.Range(5 + m_currentWave, 10 + m_currentWave);


        waveStarted = true;
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        Instantiate(enemies[Random.Range(0, enemies.Length)], m_EnemySpawners[Random.Range(0, m_EnemySpawners.Length)].position, Quaternion.identity);
        m_EnemySpawnNumber--;
        Spawning = false;
    }
}
