using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballCharacter : CharacterData
{
    // Start is called before the first frame update

    // public override void Guard(float damage, GameObject dealer) {
    //     MovableCharacter GuardCheck = transform.GetComponent<MovableCharacter>();
    //     if(GuardCheck)
    //     {
    //         if(GuardCheck.GetPositionType() == PositionType.Vanguard)
    //         {
    //             Health += damage / 2;
    //             Animate.gameObject.SetActive(true);
    //             Animate.Play("Attacking", -1, 0);
    //             dealer.GetComponent<CharacterData>().TakeDamage(4, gameObject);
                
    //             GameObject.FindObjectOfType<RecordCombatMetrics>().CounterAttack(8);
    //         }
    //     }
    // }

}
