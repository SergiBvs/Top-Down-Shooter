using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerTrigger : MonoBehaviour
{

    public int SceneToGo;
    public Vector2 nextStartPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.m_NeedsSpawnPosition = true;
            GameManager.instance.m_SpawnPosition = nextStartPosition;
            SceneManager.LoadScene(SceneToGo);
        }
    }
}
