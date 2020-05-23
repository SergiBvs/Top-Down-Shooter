using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(GameManager.instance == null)
        {
            SceneManager.LoadScene(0);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
