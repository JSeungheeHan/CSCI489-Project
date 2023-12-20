using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Act1Zone1Sculptor : OverworldInteractable
{

    // Update is called once per frame
    public override void UpdateChild(float deltaTime)
    {
        
    }

    public override void Interact()
    {
        
        interactedWith = true;
        internalTimer = 0f;

        currentLine = 0;

        if (Script.Count == 0)
        {
            StopInteracting();
            return;
        }
        toWriteString = Script[currentLine].text;
        DialogueSprite.sprite = Script[currentLine].speaker;
        TextField.sprite = Script[currentLine].textBox;
        if(Script[currentLine].SFX)
        {
            GetComponent<AudioSource>().PlayOneShot(Script[currentLine].SFX, 1f);
        }
        currentString = "";
        currentChar = 0;
        writing = true;
        internalTimer = 0.0f;
        currentLine++;
    }
}
