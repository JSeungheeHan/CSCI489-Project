using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cutscene : MonoBehaviour
{
    [SerializeField] List<string> Script;
    [SerializeField] float TextSpeed;
    [SerializeField] TMP_Text textField;
    [SerializeField] string SceneToLoad;

    private int currentLine;
    private float internalTimer;
    private bool writing;

    string currentString;
    string toWriteString;
    private int currentChar;

    // Start is called before the first frame update
    void Start()
    {
        PlayConversation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !writing)
        {
            if (currentLine >= Script.Count)
            {
                TerminateConversation();
                return;
            }

            toWriteString = Script[currentLine];
            toWriteString = toWriteString.Replace("\\n", "\n");
            currentString = "";
            currentChar = 0;
            internalTimer = 0.0f;
            writing = true;
            currentLine++;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && writing)
        {
            currentString = toWriteString;
            textField.text = currentString;

            writing = false;
        }

        internalTimer += Time.deltaTime * TextSpeed;
        if (internalTimer >= 1f && writing)
        {
            internalTimer -= 1f;
            if (currentString.Length < toWriteString.Length)
            {
                currentString = currentString + toWriteString[currentChar];
                currentChar++;
                textField.text = currentString;
            }
            else
            {
                writing = false;
            }
        }
    }

    public void PlayConversation()
    {
        currentLine = 0;

        if (Script.Count == 0)
        {
            TerminateConversation();
            return;
        }
        toWriteString = Script[currentLine];
        currentString = "";
        currentChar = 0;
        writing = true;
        internalTimer = 0.0f;

        currentLine++;
    }

    private void TerminateConversation()
    {
        Debug.Log("Conversation Ended");
        FindObjectOfType<FadeFromBlack>().LoadScene(SceneToLoad);
        //gameObject.SetActive(false);
    }
}
