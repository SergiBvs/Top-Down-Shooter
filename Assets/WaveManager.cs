
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public TMP_Text WaveText;
    public TMP_Text KillText;

    public Slider NextBar;
    public GameObject NextBarObj;

    public GameObject door;

    bool Spawning = false;

    public GameObject[] enemies;

    public GameObject[] bloodPools;
    public int bloodPoolNum = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        bloodPools = new GameObject[15];
        door.SetActive(false);
        NextBar = NextBarObj.GetComponent<Slider>();
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
        NextBarObj.SetActive(true);
        NextBar.value = 1;
        while(NextBar.value > 0)
        {
            NextBar.value -= Time.deltaTime/7;
            yield return null;
        }

        NextBarObj.SetActive(false);
        door.SetActive(true);
        m_currentWave++;
        WaveText.text = "Wave: " + m_currentWave;

        m_EnemySpawnNumber = ThrowDice(5+m_currentWave, 8+m_currentWave);
        g_Difficulty += 1;

        waveStarted = true;
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        int i = ThrowDice(0, 100);
        if(i < 70)
            Instantiate(enemies[0], m_EnemySpawners[Random.Range(0, m_EnemySpawners.Length)].position, Quaternion.identity);
        else if(i > 70)
            Instantiate(enemies[1], m_EnemySpawners[Random.Range(0, m_EnemySpawners.Length)].position, Quaternion.identity);

        m_CurrentEnemySpawned++;
        Spawning = false;
    }

    int ThrowDice(int min, int max)
    {
        int l_random = Random.Range(min, max);
        return l_random;
    }
}
