using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyData : CharacterData
{
    [SerializeField] protected List<GameObject> PlayerCharacters;

    // Start is called before the first frame update
    void Start()
    {
        // OccupiedState CurrentPositionOccupiedState = GameObject.Find("Enemy Front Position 2").GetComponent<OccupiedState>();
        // CurrentPositionOccupiedState.SetOccupied(transform.gameObject);
        // Health = MaxHealth;
        // Stamina = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Stamina += Time.deltaTime * StaminaRegen;

        // if(Stamina >= MaxStamina)
        // {
        //     Attack(gameObject);
        //     Stamina = Stamina - MaxStamina;
        // }

        // if(Health <= 0.0f)
        // {
        //     SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
        // }
    }

    public override void Attack(GameObject Enemy) { 

        // GameObject Vanguard = GameObject.Find("Vanguard Position").GetComponent<OccupiedState>().GetOccupied();
        // if(Vanguard)
        // {
        //     Vanguard.GetComponent<CharacterData>().TakeDamage(5, gameObject);
        // }
        // else
        // {
        //     int characterIndex = Random.Range(0, PlayerCharacters.Count);
        //     PlayerCharacters[characterIndex].GetComponent<CharacterData>().TakeDamage(5, gameObject);
        // }
    }
}
