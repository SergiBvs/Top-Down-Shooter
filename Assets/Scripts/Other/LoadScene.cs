
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int SceneToLoad;
    public Vector2 Pos = new Vector2(-20,-16);

    public void Load()
    {
        switch (SceneToLoad)
        {
            case 4: //recepcion
                GameManager.instance.m_NeedsSpawnPosition = true;
                GameManager.instance.m_AlreadyInElevator = true;
                GameManager.instance.m_SpawnPosition = Pos;
                if (!GameManager.instance.m_ElevatorMusicPlaying)
                {
                    GameManager.instance.ChangeMusic(MusicManager.instance.m_ElevatorMusic[Random.Range(0, MusicManager.instance.m_ElevatorMusic.Length)]);
                    GameManager.instance.m_ElevatorMusicPlaying = true;
                }
                break;

            case 9: //arena
                GameManager.instance.ChangeMusic(MusicManager.instance.m_ArenaMusic);
                GameManager.instance.m_ElevatorMusicPlaying = false;
                break;

            default:
                GameManager.instance.ChangeMusic(MusicManager.instance.m_GameMusic[0]);
                MusicManager.instance.gameObject.GetComponent<AudioHighPassFilter>().enabled = false;
                GameManager.instance.m_ElevatorMusicPlaying = false;
                break;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Load();
    }
}
