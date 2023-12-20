using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    
    [SerializeField] protected TMP_Text DialogueText;
    [SerializeField] protected Image DialogueSprite;
    [SerializeField] protected Image DialogueBox;
    
    protected bool showDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(showDialogue && DialogueBox.color.a < 1)
        {
            Color change = DialogueBox.color;
            change.a += 5.0f * Time.deltaTime;
            DialogueBox.color = change;
            DialogueText.color = change;
            DialogueSprite.color = change;
        }
        if(!showDialogue && DialogueBox.color.a > 0)
        {
            Color change = DialogueBox.color;
            change.a -= 5f * Time.deltaTime;
            if(change.a < 0f)
            {
                change.a = 0f;
            }
            DialogueBox.color = change;
            DialogueText.color = change;
            DialogueSprite.color = change;
        }
    }

    public void ShowDialoguePrompt()
    {
        showDialogue = true;

    }

    public void HideDialoguePrompt()
    {
        showDialogue = false;
    }
}
