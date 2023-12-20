using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverworldInteractable : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D Self;
    [SerializeField] protected BoxCollider2D Player;
    [SerializeField] protected TMP_Text DialogueText;
    [SerializeField] protected Image DialogueSprite;
    [SerializeField] protected Image TextField;

    [SerializeField] protected List<ScriptComponent> Script;
    [SerializeField] float TextSpeed;

    [System.Serializable]
    public struct ScriptComponent { public Sprite speaker; public string text; public Sprite textBox; public AudioClip SFX; }

    protected int currentLine = 0;
    protected float internalTimer = 0f;
    protected float automaticTimer = 0f;
    protected bool writing = false;

    protected string currentString = "";
    protected string toWriteString = "";
    protected int currentChar = 0;

    protected bool showDialogue = false;
    protected bool interactedWith = false;

    public bool Interactable = true;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interactedWith)
        {
            if ((Input.GetKeyDown(KeyCode.Mouse0) || automaticTimer > 3.0f) && !writing)
            {
                if (currentLine >= Script.Count)
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
                toWriteString = toWriteString.Replace("\\n", "\n");
                currentString = "";
                currentChar = 0;
                internalTimer = 0.0f;
                automaticTimer = 0.0f;
                writing = true;
                currentLine++;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && writing)
            {
                currentString = toWriteString;
                DialogueText.text = currentString;

                writing = false;
            }
            else if (!writing)
            {
                automaticTimer += Time.deltaTime;
            }

            internalTimer += Time.deltaTime * TextSpeed;
            if (internalTimer >= 1f && writing)
            {
                internalTimer -= 1f;
                if (currentString.Length < toWriteString.Length)
                {
                    currentString = currentString + toWriteString[currentChar];
                    currentChar++;
                    DialogueText.text = currentString;
                }
                else
                {
                    writing = false;
                }
            }
        }

        UpdateChild(Time.deltaTime);
    }

    public void ShowDialoguePrompt()
    {
        GameObject.FindObjectOfType<DialogueScript>().ShowDialoguePrompt();
    }

    public void HideDialoguePrompt()
    {
        GameObject.FindObjectOfType<DialogueScript>().HideDialoguePrompt();
    }

    public virtual void Interact()
    {
        
        interactedWith = true;

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
        automaticTimer = 0.0f;
        currentLine++;
    }

    public virtual void StopInteracting()
    {
        if(interactedWith)
        {
            interactedWith = false;
            Interactable = false;
            HideDialoguePrompt();
            FindObjectOfType<OverworldMovement>().StopInteracting();
        }
    }

    public virtual void UpdateChild(float deltaTime)
    {

    }
}
