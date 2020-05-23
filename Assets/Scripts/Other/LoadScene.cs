using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public int SceneToLoad;

    public void Load()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
    }
}
