using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Act2Scene1Teapot : OverworldInteractable
{
    [SerializeField] PartyFollower follow;
    [SerializeField] Rigidbody2D body;
    bool join = false;


    public override void StopInteracting()
    {
        if(interactedWith)
        {
            interactedWith = false;
            Interactable = false;
            join = true;
            Self.enabled = false;
            HideDialoguePrompt();
            FindObjectOfType<OverworldMovement>().StopInteracting();
        }
    }

    // Update is called once per frame
    public override void UpdateChild(float deltaTime)
    {
        if(join)
        {
            body.isKinematic = true;
            follow.enabled = true;
        }
        else
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
        // interactedWith = true;
        // internalTimer = 0f;
        // DialogueBox.GetComponent<TextMeshPro>().text = "Agh, a guard ahead";
        
        // if(numinteract == 1)
        // {
        //     DialogueBox.GetComponent<TextMeshPro>().text = "They're blocking the exit...";
        // }
    }
}
