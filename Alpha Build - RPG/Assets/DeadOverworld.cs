using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadOverworld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Color a = GetComponent<SpriteRenderer>().color;
        a.a -= Time.deltaTime;
        GetComponent<SpriteRenderer>().color = a;

        if(a.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
