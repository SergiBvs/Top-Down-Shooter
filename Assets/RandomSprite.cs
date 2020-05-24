using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSprite : MonoBehaviour
{

    public Sprite[] spr;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spr[Random.Range(0, spr.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
