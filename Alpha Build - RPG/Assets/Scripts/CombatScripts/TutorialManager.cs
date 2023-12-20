using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TMP_Text TutorialText;
    [SerializeField] FadeFromBlack Fade;
    
    int step = 0;
    public static bool gameEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        if(Options.playedTutorial)
        {
            TutorialText.text = "";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0f));
            gameEnabled = true;
            Time.timeScale = 1f;
            step = 22;
            return;
        }
        Time.timeScale = 1.0f;
        Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.6f));
        TutorialText.text = "Welcome to Reliquary's Combat!";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 0)
        {
            Time.timeScale = 0.0f;
            TutorialText.text = "You will notice that on the left, there are three columns of positions each character can be in.";
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 1)
        {
            TutorialText.text = "The leftmost one is called the 'Rear Guard'. The middle one is the 'Front Guard'. The rightmost one is the 'Vanguard'";
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 2)
        {
            TutorialText.text = "You will need to move characters across each of these positions, which you can do by dragging them onto certain positions.";
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 3)
        {
            TutorialText.text = "Try it now! Move a character to the Vanguard position";
            Time.timeScale = 1.0f;
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.2f));
            step++;
            return;
        }
        if(step == 5)
        {
            TutorialText.text = "Great Job! Characters in the Vanguard will take less damage from enemy attacks, but will drain STAMINA over time.";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.6f));
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 6)
        {
            
            TutorialText.text = "Alright! Now try moving a character to the Rear Guard!";
            Time.timeScale = 1.0f;
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.2f));
            step++;
            return;
        }
        if(step == 8)
        {
            TutorialText.text = "Nice! While in the Rear Guard, you can drag the character ONTO an ally's position to set it as a target for a heal";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.6f));
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 9)
        {
            TutorialText.text = "Try it! Drag the character in the rear ONTO an ally!";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.2f));
            step++;
            return;
        }
        if(step == 11)
        {
            TutorialText.text = "Great Job! You will notice that the character is now healing the ally periodically.";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.6f));
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 12)
        {
            TutorialText.text = "When you set a target for an attack OR heal, the action will automatically happen once the character's STAMINA bar is filled!";
            Time.timeScale = 0.0f;
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 13)
        {
            
            TutorialText.text = "Alright! Now try moving a character to the Front Guard!";
            Time.timeScale = 1.0f;
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.2f));
            step++;
            return;
        }
        if(step == 15)
        {
            TutorialText.text = "Nice! While in the Front Guard, you can drag the character ONTO an enemy's position to set it as a target for an attack";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.6f));
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 16)
        {
            TutorialText.text = "Try it! Drag the character in the front ONTO an enemy!";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.2f));
            step++;
            return;
        }
        if(step == 18)
        {
            TutorialText.text = "Great Job! You will notice that the character is now attacking the enemy.";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.6f));
            step++;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 19)
        {
            TutorialText.text = "Good luck! Try to beat this level!";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0.6f));
            step++;
            return;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && step == 20)
        {
            Options.playedTutorial = true;
            step++;
            return;
        }
        if(step == 21)
        {
            TutorialText.text = "";
            Fade.SetTargetColor(new Color(0f, 0f, 0f, 0f));
            gameEnabled = true;
            Time.timeScale = 1f;
            step++;
            return;
        }
    }

    public void ChangedVanguard()
    {
        if(step == 4)
        {
            step++;
        }
    }

    public void MovedToRearGuard()
    {
        if(step == 7)
        {
            step++;
        }
    }

    public void SetAlliedTarget()
    {
        if(step == 10)
        {
            step++;
        }
    }

    public void MovedToFrontGuard()
    {
        if(step == 14)
        {
            step++;
        }
    }

    public void SetEnemyTarget()
    {
        if(step == 17)
        {
            step++;
        }
    }
}
