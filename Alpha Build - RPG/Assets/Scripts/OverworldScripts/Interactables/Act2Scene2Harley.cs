using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Act2Scene2Harley : OverworldInteractable
{
    bool join = false;
    [SerializeField] PartyFollower follow;
    [SerializeField] BoxCollider2D progress;


    public override void StopInteracting()
    {
        if(interactedWith)
        {
            interactedWith = false;
            Interactable = false;
            join = true;
            progress.enabled = false;
            Self.enabled = false;
            HideDialoguePrompt();
            FindObjectOfType<OverworldMovement>().StopInteracting();
        }
    }

    // Update is called once per frame
    public override void UpdateChild(float deltaTime)
    {
        // if(join)
        // {
        //     follow.enabled = true;
        //     //secondPart.enabled = true;
        // }
        if(currentLine == 4)
        {
            follow.enabled = true;
        }
        else if (follow.enabled == false)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        }
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
