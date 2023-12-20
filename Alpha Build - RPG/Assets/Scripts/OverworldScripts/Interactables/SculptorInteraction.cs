using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SculptorInteraction : OverworldInteractable
{
    string currentSpeech = "";
    int numinteract = 0;

    // Update is called once per frame
    public override void UpdateChild(float deltaTime)
    {
        // if(interactedWith && numinteract == 0)
        // {
        //     internalTimer += deltaTime;
        //     if(internalTimer > 2.0f)
        //     {
        //         DialogueBox.GetComponent<TextMeshPro>().text = "Be careful! We don't want to get caught";
        //     }
        //     if(internalTimer > 5.0f)
        //     {
        //         DialogueBox.GetComponent<TextMeshPro>().text = "Slow and steady now...";
        //     }
        //     if(internalTimer > 7.0f)
        //     {
        //         numinteract = 1;
        //         interactedWith = false;
        //         HideDialoguePrompt();
        //         FindObjectOfType<OverworldMovement>().StopInteracting();
        //     }
        // }

        // if(interactedWith && numinteract == 1)
        // {
        //     internalTimer += deltaTime;
        //     if(internalTimer > 2.0f)
        //     {
        //         DialogueBox.GetComponent<TextMeshPro>().text = "We might need to try and lie.";
        //     }
        //     if(internalTimer > 5.0f)
        //     {
        //         DialogueBox.GetComponent<TextMeshPro>().text = "Shall we proceed?";
        //     }
        //     if(internalTimer > 7.0f)
        //     {
        //         Interactable = false;
        //         HideDialoguePrompt();
        //         FindObjectOfType<OverworldMovement>().StopInteracting();
        //         interactedWith = false;
        //     }
        //     numinteract = 1;
        // }
    }

    public override void Interact()
    {
        Debug.Log("Sculptor interacted with");
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
        // interactedWith = true;
        // internalTimer = 0f;
        // DialogueBox.GetComponent<TextMeshPro>().text = "Agh, a guard ahead";
        
        // if(numinteract == 1)
        // {
        //     DialogueBox.GetComponent<TextMeshPro>().text = "They're blocking the exit...";
        // }
    }
}
