using UnityEngine;

public class Setup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.ChangeMusic(GameManager.instance.MManager.m_GameMusic[0]);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
