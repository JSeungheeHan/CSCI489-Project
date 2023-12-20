using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OutlineState { Idle, Hovering, Dragging, Supportable, Supporting, Attackable, Attacking, };

public class OutlineRedrawer : MonoBehaviour
{

    Material material;
    Color targetColor = new Color(0f, 0f, 0f, 0f);
    Color currentColor;
    public OutlineState state = OutlineState.Idle;

    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        material = GetComponent<Renderer>().material;
        currentColor = material.GetColor("_SolidOutline");
        //targetColor = currentColor;
    }

    void Update()
    {
        // if(Mathf.Abs((targetColor - currentColor).a) > 0.01)
        // {
            currentColor += (targetColor - currentColor) * Time.deltaTime * 5.0f;
            material.SetColor("_SolidOutline", currentColor);
        // }
    }

    public void SetTargetColor(Color input)
    {
        targetColor = input;
    }
}
