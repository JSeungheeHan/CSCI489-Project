using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]  GameObject Pause;
    [SerializeField]  GameObject Options;
    [SerializeField]  GameObject Control;
	[SerializeField]  Button ResumeButton;
	[SerializeField]  Button ControlButton;
	[SerializeField]  Button ExitButton;
	[SerializeField]  Button ReturnButton;
	[SerializeField]  Button OptionsButton;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = ResumeButton.GetComponent<Button>();
		btn.onClick.AddListener(Resume);

        btn = ControlButton.GetComponent<Button>();
		btn.onClick.AddListener(Controls);
        
        btn = ExitButton.GetComponent<Button>();
		btn.onClick.AddListener(Exit);

        btn = ReturnButton.GetComponent<Button>();
		btn.onClick.AddListener(Return);

        btn = OptionsButton.GetComponent<Button>();
		btn.onClick.AddListener(Option);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            Time.timeScale = 0.0f;
            Pause.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = false;
            Time.timeScale = 1.0f;
            Pause.SetActive(false);
            Control.SetActive(false);
            Options.SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Pause.SetActive(false);
    }

    public void Controls()
    {
        Pause.SetActive(false);
        Control.SetActive(true);
    }

    public void Option()
    {
        Pause.SetActive(false);
        Options.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Return()
    {
        Pause.SetActive(true);
        Control.SetActive(false);
        Options.SetActive(false);
    }
}
