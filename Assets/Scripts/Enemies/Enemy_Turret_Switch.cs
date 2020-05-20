using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Enemy_Turret_Switch : MonoBehaviour
{

    public Enemy_Turret turret;
    public Sprite deactivatedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                turret.TurretDeactivate();
                turret.gameObject.GetComponent<SpriteRenderer>().sprite = deactivatedSprite;
                this.GetComponent<SpriteRenderer>().color = Color.gray;
                this.GetComponent<Enemy_Turret_Switch>().enabled = false;
            }
        }
    }
}
