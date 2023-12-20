using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{
    Color currentColor;
    Color targetColor = new Color(0f, 0f, 0f, 0f);
    string toLoad = "";

    public static float musicVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = Color.black;
        currentColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        currentColor = GetComponent<Image>().color;
        if(Mathf.Abs((targetColor - currentColor).a) > 0.01)
        {
            musicVolume = (1f - currentColor.a);
            GetComponent<Image>().color += (targetColor - currentColor) * Time.deltaTime * 5.0f;
        }
        else if(!string.IsNullOrWhiteSpace(toLoad))
        {
            SceneManager.LoadScene(toLoad, LoadSceneMode.Single);
        }
        else
        {
            //gameObject.SetActive(false);
        }
    }

    public void LoadScene(string input)
    {
        //gameObject.SetActive(true);
        toLoad = input;
        targetColor = new Color(0f, 0f, 0f, 1f);
    }

    public void SetTargetColor(Color input)
    {
        targetColor = input;
    }
}
