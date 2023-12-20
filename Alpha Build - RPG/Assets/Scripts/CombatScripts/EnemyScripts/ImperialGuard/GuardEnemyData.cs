using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEnemyData : EnemyData
{
    [SerializeField] GameObject VanguardPos;
    [SerializeField] GameObject InitialPos;
    [SerializeField] string WinScene;
    [SerializeField] int numEnemies;

    public static int enemyCounter;
    public OutlineRedrawer Outline;
    private float metricTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        OccupiedState CurrentPositionOccupiedState = InitialPos.GetComponent<OccupiedState>();
        CurrentPositionOccupiedState.SetOccupied(transform.gameObject);
        IdleTargetPosition = transform.position;
        Health = MaxHealth;
        Stamina = 0;
        State = CharacterState.Idle;
        enemyCounter = numEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if(!TutorialManager.gameEnabled)
        {
            return;
        }
        if(!pause)
        {
            Stamina += Time.deltaTime * StaminaRegen;
        }    
        metricTimer += Time.deltaTime;

        if(Stamina >= MaxStamina && !pause)
        {
            Stamina = MaxStamina;
            if(State == CharacterState.Idle && !dead)
            {
                Attack(gameObject);
            }
        }

        if(Health <= 0.0f && !dead)
        {
            Debug.Log(enemyCounter);
            dead = true;
            enemyCounter--;
            Health = 0.0f;
        }
        if(enemyCounter <= 0 && !pause)
        {
            WinLose.gameObject.SetActive(true);
            WinLose.Play("Victory", -1, 0);
            BackgroundMusic.Stop();
            GetComponent<AudioSource>().PlayOneShot(WinSFX, 1f);
            pause = true;
        }

        if(dead)
        {
            Health = 0.0f;
            deathTimer -= Time.deltaTime;
            Color fade = GetComponent<SpriteRenderer>().color;
            if(fade.a - (0.4f * Time.deltaTime) <= 0)
            {
                fade.a = 0f;
            }
            else
            {
                fade.a = fade.a - (0.4f * Time.deltaTime);
            }
            GetComponent<SpriteRenderer>().color = fade;
            if(savedAutoTargetEnemy)
            {
                savedAutoTargetEnemy.GetComponent<CharacterData>().State = CharacterState.Idle;
            }
            savedAutoTargetEnemy = null;
            if(deathTimer <= 0f && !loading)
            {
                if(enemyCounter == 0)
                {
                    loading = true;
                    GameObject.FindObjectOfType<RecordCombatMetrics>().EndFightTime(metricTimer);
                    FindObjectOfType<FadeFromBlack>().LoadScene(WinScene);
                }
                Destroy(gameObject);
            }
        }
        else if (!pause)
        {
            colorTimer += Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, baseColor, colorTimer);
        }

        AnimationTimer -= Time.deltaTime;
        if(AnimationTimer < 0.8f && State == CharacterState.Active)
        {
            ActiveTargetPosition = IdleTargetPosition;
        }
        if(AnimationTimer < 0f && State == CharacterState.Active)
        {
            if(savedAutoTargetEnemy)
            {
                savedAutoTargetEnemy.GetComponent<CharacterData>().State = CharacterState.Idle;
            }
            savedAutoTargetEnemy = null;
            State = CharacterState.Idle;
        }


        if(State == CharacterState.Idle || State == CharacterState.Stunned)
        { 
            transform.position += (IdleTargetPosition + new Vector3(0f, 0f, 0.0f) - transform.position) * Time.deltaTime * 6f;
        }
        else if (State == CharacterState.Active)
        {
            transform.position += (ActiveTargetPosition + new Vector3(0f, 1.2f, 0.0f) - transform.position) * Time.deltaTime * 6f;
        }
    }

    public override void Attack(GameObject Enemy) { 
        GameObject Vanguard = VanguardPos.GetComponent<OccupiedState>().GetOccupied();
        if(Vanguard)
        {
            if(Vanguard.GetComponent<CharacterData>().State == CharacterState.Idle)
            {
                Vanguard.GetComponent<CharacterData>().TakeDamage(4, gameObject);
                ActiveTargetPosition = Vanguard.GetComponent<MovableCharacter>().GetCurrentPosition().transform.position + new Vector3(1.5f, 1.5f, 0f);
                AutoTargetEnemy = Vanguard;
                AutoTargetEnemy.GetComponent<CharacterData>().State = CharacterState.Stunned;
                savedAutoTargetEnemy = AutoTargetEnemy;
                Stamina = Stamina - MaxStamina;
                State = CharacterState.Active;
                AnimationTimer =  1.0f;
            }
        }
        else
        {
            int characterIndex = Random.Range(0, PlayerCharacters.Count);
            if(PlayerCharacters[characterIndex].GetComponent<CharacterData>().State == CharacterState.Idle)
            {
                PlayerCharacters[characterIndex].GetComponent<CharacterData>().TakeDamage(4, gameObject);
                ActiveTargetPosition = PlayerCharacters[characterIndex].GetComponent<MovableCharacter>().GetCurrentPosition().transform.position + new Vector3(1.5f, 1.5f, 0f);
                AutoTargetEnemy = PlayerCharacters[characterIndex];
                AutoTargetEnemy.GetComponent<CharacterData>().State = CharacterState.Stunned;
                savedAutoTargetEnemy = AutoTargetEnemy;
                Stamina = Stamina - MaxStamina;
                State = CharacterState.Active;
                AnimationTimer =  1.0f;
            }
        }
    }
}
