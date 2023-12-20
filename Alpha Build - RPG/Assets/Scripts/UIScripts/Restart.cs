using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Restart : MonoBehaviour {
	public Button RestartButton;
	public Button QuitButton;
	public static string restartScene;

	void Start () {
		RestartButton.onClick.AddListener(TaskOnClick);
		QuitButton.onClick.AddListener(QuitApp);
		
	}

	void TaskOnClick(){
        FindObjectOfType<FadeFromBlack>().LoadScene(restartScene);
	}

	void QuitApp()
	{
        Application.Quit();
	}
}