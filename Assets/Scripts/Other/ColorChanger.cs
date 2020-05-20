using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ColorChanger : MonoBehaviour
{

    float r;
    float g;
    float b;

    SpriteRenderer sr;
    Color col;

    float cooldown;


    // Start is called before the first frame update
    void Start()
    {
        r = 1;
        g = 1;
        b = 1;
        sr = GetComponent<SpriteRenderer>();
        col.a = 29f / 255f;
        cooldown = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            r = Random.Range(0f, 1f);
            g = Random.Range(0f, 1f);
            b = Random.Range(0f, 1f);

            col.r = r;
            col.g = g;
            col.b = b;

            sr.color = col;
            cooldown = 0.2f;
        }
        else
            cooldown -= Time.deltaTime;
        
    }
}
