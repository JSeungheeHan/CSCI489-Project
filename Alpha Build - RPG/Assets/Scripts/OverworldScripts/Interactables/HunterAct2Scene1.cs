using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HunterAct2Scene1 : OverworldInteractable
{
    bool attack = false;

    // Update is called once per frame
    public override void StopInteracting()
    {
        if(interactedWith)
        {
            interactedWith = false;
            Interactable = false;
            attack = true;
            HideDialoguePrompt();
            FindObjectOfType<OverworldMovement>().StopInteracting();
        }
    }

    public override void UpdateChild(float deltaTime)
    {
        if(attack)
        {
            Restart.restartScene = "Act2 Scene1";
            transform.position += (Player.gameObject.transform.position + new Vector3(1.2f, 0.5f, 0.0f) - transform.position) * Time.deltaTime * 4f;
            Debug.Log("Scene Change");
            interactedWith = false;
            HideDialoguePrompt();
            FindObjectOfType<OverworldMovement>().StopInteracting();
            FindObjectOfType<FadeFromBlack>().LoadScene("Act2 Combat1");
        }
    }

    public override void Interact()
    {
        Debug.Log("Guard interacted with");
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
