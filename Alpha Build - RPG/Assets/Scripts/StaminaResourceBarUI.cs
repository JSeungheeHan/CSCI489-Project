using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaResourceBarUI : MonoBehaviour
{
    [SerializeField] CharacterData ParentData;

    float timer;
    Vector3 MaxStaminaSize;

    // Start is called before the first frame update
    void Start()
    {
        MaxStaminaSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<RectTransform>())
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-((ParentData.GetMaxStamina() - ParentData.GetStamina()) * GetComponent<RectTransform>().rect.width) / (ParentData.GetMaxStamina() * 2f), 0);
            GetComponent<RectTransform>().localScale = new Vector3((ParentData.GetStamina()) / (ParentData.GetMaxStamina()), 1f, 1f);
        }
        else
        {
            transform.localPosition = new Vector3(-((ParentData.GetMaxStamina() - ParentData.GetStamina()) * MaxStaminaSize.x) / (ParentData.GetMaxStamina() * 2), 0, 0);
            transform.localScale = new Vector3((ParentData.GetStamina() * MaxStaminaSize.x) / (ParentData.GetMaxStamina()), MaxStaminaSize.y, MaxStaminaSize.z);
        }
    }
}
