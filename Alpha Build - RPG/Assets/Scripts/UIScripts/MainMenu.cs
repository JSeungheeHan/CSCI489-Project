using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public Button StartButton;
    [SerializeField] Button QuitButton;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip menuSelect;
    
    // Start is called before the first frame update
    void Start()
    {
        Button Start = StartButton.GetComponent<Button>();
		Start.onClick.AddListener(StartGame);

        Button Quit = QuitButton.GetComponent<Button>();
        Quit.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        GameObject.FindObjectOfType<FadeFromBlack>().LoadScene("Opening Cutscene");
    }

    void QuitGame()
    {
        Application.Quit();
    }

    public void SelectSound()
    {
        audio.PlayOneShot(menuSelect, 1f);
    }
}
