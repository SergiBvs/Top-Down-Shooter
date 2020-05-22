using UnityEngine;

public class Setup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        GameManager.instance.ChangeMusic(GameManager.instance.MManager.m_GameMusic[0]);
    }

}
