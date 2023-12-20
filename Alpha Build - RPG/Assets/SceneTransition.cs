using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string ToChange;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(player == col.gameObject)
        {
            GameObject.FindObjectOfType<FadeFromBlack>().LoadScene(ToChange);
            Debug.Log("Hit");
        }
    }
}
