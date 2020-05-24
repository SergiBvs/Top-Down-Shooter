using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int SceneToLoad;

    public void Load()
    {
        switch (SceneToLoad)
        {
            case 4:
                GameManager.instance.m_NeedsSpawnPosition = true;
                GameManager.instance.m_AlreadyInElevator = true;
                GameManager.instance.m_SpawnPosition = new Vector2(45, -3);
                break;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
    }
}
