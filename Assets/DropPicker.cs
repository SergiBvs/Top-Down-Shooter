using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPicker : MonoBehaviour
{

    private string thisTag = "";

    // Start is called before the first frame update
    void Start()
    {
        thisTag = this.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (thisTag)
            {
                case "Coin":
                    GameManager.instance.SetCoins(1);
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
