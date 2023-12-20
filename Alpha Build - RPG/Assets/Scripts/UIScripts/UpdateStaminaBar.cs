using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStaminaBar : MonoBehaviour
{
    GameObject Parent;
    CharacterData ParentData;

    public Vector3 MaxStaminaSize;
    public float VerticalOffset;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        Parent = transform.parent.gameObject;
        ParentData = Parent.GetComponent<CharacterData>();
    }

    // Update is called once per frame
    void Update()
    {
        // timer += Time.deltaTime;
        // if(timer >= 1.0f)
        // {
        //     timer -= 1.0f;
        //     ParentData.SetHealth(ParentData.GetHealth() - 1);
        // }

        transform.localPosition = new Vector3(-((ParentData.GetMaxStamina() - ParentData.GetStamina()) * MaxStaminaSize.x) / (ParentData.GetMaxStamina() * 2), VerticalOffset, 0);
        transform.localScale = new Vector3((ParentData.GetStamina() * MaxStaminaSize.x) / (ParentData.GetMaxStamina()), MaxStaminaSize.y, MaxStaminaSize.z);
    }
}
