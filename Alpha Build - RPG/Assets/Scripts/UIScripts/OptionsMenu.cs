using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider SFXVolume;
    [SerializeField] Slider MusicVolume;
    [SerializeField] Slider FieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        SFXVolume.value = Options.sfxVolume;
        MusicVolume.value = Options.musicVolume;
        FieldOfView.value = Options.fieldOfVision;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSFXVolumeChange()
    {
        Options.sfxVolume = SFXVolume.value;
    }

    public void OnMusicVolumeChange()
    {
        Options.musicVolume = MusicVolume.value;
    }

    public void OnFieldOfViewChange()
    {
        Options.fieldOfVision = FieldOfView.value;
    }
}
