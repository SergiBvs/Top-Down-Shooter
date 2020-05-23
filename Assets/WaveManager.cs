using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    int m_currentWave = 0;
    public float g_Difficulty = 0;
    int m_EnemySpawnNumber = 0;
    int m_CurrentEnemySpawned = 0;
    public int m_EnemiesDefeated = 0;
    public int m_WaveEnemiesDefeated = 0;
    public Transform[] m_EnemySpawners;
    bool waveStarted = false;
    bool waveEnded = false;

    public GameObject door;

    bool Spawning = false;

    public GameObject[] enemies;

    
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
        StartCoroutine(WaveStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (waveStarted)
        {
            if (!Spawning)
            {
                if (m_CurrentEnemySpawned < m_EnemySpawnNumber)
                {
                    StartCoroutine(EnemySpawn());
                    Spawning = true;
                }
                else if(m_WaveEnemiesDefeated >= m_EnemySpawnNumber)
                {
                    m_WaveEnemiesDefeated = 0;
                    m_CurrentEnemySpawned = 0;
                    waveEnded = true;
                    waveStarted = false;
                }
            }
        }
        else if (waveEnded)
        {
            door.SetActive(false);
            StartCoroutine(WaveStart());
            waveEnded = false;
        }
    }

    IEnumerator WaveStart()
    {
        
        yield return new WaitForSeconds(5);
        door.SetActive(true);
        m_currentWave++;
        m_EnemySpawnNumber = Random.Range(5 + m_currentWave, 8 + m_currentWave);
        g_Difficulty += 1;

        waveStarted = true;
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        Instantiate(enemies[Random.Range(0, enemies.Length)], m_EnemySpawners[Random.Range(0, m_EnemySpawners.Length)].position, Quaternion.identity);
        m_CurrentEnemySpawned++;
        Spawning = false;
    }
}
