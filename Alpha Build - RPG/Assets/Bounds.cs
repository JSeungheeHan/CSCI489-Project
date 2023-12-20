using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x + 10)
        {
            Debug.Log(player.transform.position.x - (transform.position.x + 10 ));
            transform.Translate(new Vector3(Time.deltaTime, 0f, 0f));
        }
    }
}
