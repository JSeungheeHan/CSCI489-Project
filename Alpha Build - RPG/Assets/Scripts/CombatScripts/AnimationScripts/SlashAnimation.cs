using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnimation : MonoBehaviour
{
    [SerializeField] AudioClip Attack;
    [SerializeField] AudioClip Heal;
    [SerializeField] AudioClip TakeDamage;
    [SerializeField] Sprite AttackSprite;
    [SerializeField] Sprite BaseSprite;
    [SerializeField] SpriteRenderer Character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishAnimation()
    {
        Character.sprite = BaseSprite;
        gameObject.SetActive(false);
    }

    public void PlayAttackSound()
    {
        GetComponent<AudioSource>().PlayOneShot(Attack, 1f);
    }

    public void PlayHealSound()
    {
        GetComponent<AudioSource>().PlayOneShot(Heal, 1f);
    }

    public void PlayDamageSound()
    {

    }

    public void SetAttackSprite()
    {
        Character.sprite = AttackSprite;
    }
}
