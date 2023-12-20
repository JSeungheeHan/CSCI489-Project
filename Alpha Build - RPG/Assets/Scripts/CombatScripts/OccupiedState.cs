using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedState : MonoBehaviour
{
    GameObject OccupyingCharacter;
    public PositionType PositionType;
    [SerializeField] private int PositionPriority;
    [SerializeField] SpriteRenderer Arrow;
    [SerializeField] Animator ArrowAnimation;
    private bool showingArrow = false;
    private Color currentColor = Color.white;
    private float timer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(ArrowAnimation)
        {
            ArrowAnimation.Play("Basic", -1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Arrow)
        {
            timer += Time.deltaTime;
            if(!showingArrow)
            {
                Arrow.color = Color.Lerp(currentColor, new Color(1f, 1f, 1f, 0f), timer / 0.3f);
            }
            else
            {
                Arrow.color = Color.Lerp(currentColor, Color.white, timer / 0.3f);
            }
        }
    }

    public GameObject GetOccupied()
    {
        return OccupyingCharacter;
    }
    
    public void SetOccupied(GameObject input)
    {
        OccupyingCharacter = input;
    }

    public int GetPositionPriority()
    {
        return PositionPriority;
    }

    public void DisplayArrow()
    {
        if(Arrow)
        {
            currentColor = Arrow.color;
            showingArrow = true;
            timer = 0f;
        }
    }

    public void HideArrow()
    {
        if(Arrow)
        {
            currentColor = Arrow.color;
            showingArrow = false;
            timer = 0f;
        }
    }
}
