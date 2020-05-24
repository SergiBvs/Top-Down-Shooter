using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int SceneToLoad;
    public Vector2 Pos;

    public void Load()
    {
        switch (SceneToLoad)
        {
            case 4: //recepcion
                GameManager.instance.m_NeedsSpawnPosition = true;
                GameManager.instance.m_AlreadyInElevator = true;
                GameManager.instance.m_SpawnPosition = Pos;
                GameManager.instance.ChangeMusic(MusicManager.instance.m_ElevatorMusic[Random.Range(0, MusicManager.instance.m_ElevatorMusic.Length)]);
                break;

            case 9: //arena
                GameManager.instance.ChangeMusic(MusicManager.instance.m_ArenaMusic);
                break;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
    }
}
