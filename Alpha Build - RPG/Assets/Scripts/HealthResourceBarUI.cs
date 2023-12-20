using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthResourceBarUI : MonoBehaviour
{
    [SerializeField] CharacterData ParentData;

    Vector3 MaxHealthSize;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealthSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<RectTransform>())
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-((ParentData.GetMaxHealth() - ParentData.GetHealth()) * GetComponent<RectTransform>().rect.width) / (ParentData.GetMaxHealth() * 2f), 0f);
            GetComponent<RectTransform>().localScale = new Vector3((ParentData.GetHealth()) / (ParentData.GetMaxHealth()), 1f, 1f);
        }
        else
        {
            transform.localPosition = new Vector3(-((ParentData.GetMaxHealth() - ParentData.GetHealth()) * MaxHealthSize.x) / (ParentData.GetMaxHealth() * 2), 0, 0);
            transform.localScale = new Vector3((ParentData.GetHealth() * MaxHealthSize.x) / (ParentData.GetMaxHealth()), MaxHealthSize.y, MaxHealthSize.z);
        }
    }
}
