using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act2Scene3Final : OverworldInteractable
{
    // Start is called before the first frame update
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
            Restart.restartScene = "Act1 Scene2";
            Debug.Log("Scene Change");
            interactedWith = false;
            HideDialoguePrompt();
            FindObjectOfType<OverworldMovement>().StopInteracting();
            FindObjectOfType<FadeFromBlack>().LoadScene("Ending Cutscene");
        }
    }
}
