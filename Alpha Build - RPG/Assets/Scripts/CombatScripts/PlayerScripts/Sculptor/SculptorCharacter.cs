using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptorCharacter : CharacterData
{
    // Start is called before the first frame update
    // public override void Attack(GameObject Enemy) { 
    //     if(Stamina <= 6)
    //     {
    //         return;
    //     }
    //     State = CharacterState.Active;
    //     ActiveTargetPosition = Enemy.transform.position - new Vector3(1.2f, 1.1f, 0f);
    //     Stamina -= 10;
    //     CharacterData EnemyData = Enemy.GetComponent<CharacterData>();
    //     Enemy.GetComponent<CharacterData>().State = CharacterState.Stunned;
    //     savedAutoTargetEnemy = AutoTargetEnemy;
    //     if(EnemyData)
    //     {
    //         if(TutorialManager.gameEnabled)
    //         {
    //             EnemyData.TakeDamage(12, gameObject);
    //             GameObject.FindObjectOfType<RecordCombatMetrics>().Attack(12);
    //         }
    //     }
    //     AnimationTimer = 1.0f;
    // }
}
