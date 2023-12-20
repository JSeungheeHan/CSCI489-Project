using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = Options.musicVolume * FadeFromBlack.musicVolume;
    }
}
