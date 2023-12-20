using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CharacterState { Idle, Active, Dead, Stunned };

public class CharacterData : MonoBehaviour
{
    protected float Health;
    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float HealthRegen;

    protected float Stamina;
    [SerializeField] protected float MaxStamina;
    [SerializeField] protected float StaminaRegen;

    [SerializeField] protected Sprite CharacterIcon;
    [SerializeField] protected SpriteRenderer IconTarget;

    [SerializeField] protected Animator Animate;
    [SerializeField] protected Animator WinLose;

    [SerializeField] protected AudioSource BackgroundMusic;
    [SerializeField] protected AudioClip WinSFX;
    [SerializeField] protected AudioClip LoseSFX;
    [SerializeField] protected AudioClip DamageSFX;

    protected GameObject AutoTargetEnemy;
    protected GameObject AutoTargetAlly;
    protected GameObject savedAutoTargetEnemy;
    protected GameObject savedAutoTargetAlly;
    public CharacterState State = CharacterState.Idle;
    protected Vector3 IdleTargetPosition;
    protected Vector3 DraggingTargetPosition;
    protected Vector3 ActiveTargetPosition;
    protected float AnimationTimer;

    protected Color targetColor;
    protected static bool pause = false;
    protected bool dead = false;
    protected float deathTimer = 5.0f;

    private int FlickerXTimes = 0;
    private int currentFlicker = 0;
    private float FlickerEveryXSeconds = 0.1f;
    private float flickerCounter = 0.0f;

    protected Color baseColor = new Color(1f, 1f, 1f, 1f);
    protected float colorTimer = 1.0f;

    private float metricTimer = 0.0f;
    protected bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        Health = MaxHealth;
        Stamina = MaxStamina;

        State = CharacterState.Idle;
        targetColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        metricTimer += Time.deltaTime;

        if(GetComponent<MovableCharacter>().GetPositionType() == PositionType.Front && !pause)
        {
            Stamina += StaminaRegen * Time.deltaTime;
            if(Stamina >= MaxStamina && State == CharacterState.Idle && AutoTargetEnemy)
            {
                if(AutoTargetEnemy.GetComponent<CharacterData>().State == CharacterState.Idle)
                {
                    Attack(AutoTargetEnemy);
                    Animate.gameObject.SetActive(true);
                    Animate.Play("Attacking", -1, 0);
                }
            }
        }
        if(GetComponent<MovableCharacter>().GetPositionType() == PositionType.Rear && !pause)
        {
            Stamina += StaminaRegen * Time.deltaTime;
            if(Stamina >= MaxStamina && State == CharacterState.Idle && AutoTargetAlly)
            {
                Debug.Log("Healing");
                if(AutoTargetAlly.GetComponent<CharacterData>().State == CharacterState.Idle)
                {
                    Animate.gameObject.SetActive(true);
                    Animate.Play("Healing", -1, 0);
                    Support(AutoTargetAlly);
                }
            }
        }
        if(GetComponent<MovableCharacter>().GetPositionType() == PositionType.Vanguard && TutorialManager.gameEnabled && !dead)
        {
            GuardPassive(Time.deltaTime);
        }
        if(Stamina > MaxStamina)
        {
            Stamina = MaxStamina;
        }
        if(Health <= 0.0f && !pause)
        {
            dead = true;
            pause = true;
            BackgroundMusic.Stop();
            WinLose.gameObject.SetActive(true);
            WinLose.Play("Defeat", -1, 0);
            GetComponent<AudioSource>().PlayOneShot(LoseSFX, 1f);
            GetComponent<SpriteRenderer>().color = Color.red;
            Health = 0.0f;
        }

        if(currentFlicker < FlickerXTimes && !pause)
        {
            flickerCounter += Time.deltaTime;
            if(flickerCounter >= FlickerEveryXSeconds)
            {
                flickerCounter -= FlickerEveryXSeconds;
                currentFlicker += 1;
                if(GetComponent<SpriteRenderer>().color.a == 1f)
                {
                    Color fade = GetComponent<SpriteRenderer>().color;
                    fade.a = 0.5f;
                    GetComponent<SpriteRenderer>().color = fade;
                }
                else
                {
                    Color fade = GetComponent<SpriteRenderer>().color;
                    fade.a = 1f;
                    GetComponent<SpriteRenderer>().color = fade;
                }
            }
        }
        else
        {
            currentFlicker = 0;
            FlickerXTimes = 0;
            flickerCounter = 0f;
        }

        if(dead)
        {
            Health = 0.0f;
            Stamina = 0.0f;
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
            if(deathTimer <= 0f && !loading)
            {
                loading = true;
                GameObject.FindObjectOfType<RecordCombatMetrics>().EndFightTime(metricTimer);
                FindObjectOfType<FadeFromBlack>().LoadScene("You Lose!");
            }
        }
        else if (!pause)
        {
            colorTimer += Time.deltaTime;
            Color toChange = Color.Lerp(Color.red, baseColor, colorTimer);
            toChange.a = GetComponent<SpriteRenderer>().color.a;
            GetComponent<SpriteRenderer>().color = toChange;
        }

        if(Stamina <= 0.0f)
        {
            Stamina = 0.0f;
        }

        AnimationTimer -= Time.deltaTime;
        if(AnimationTimer < 0.4f && State == CharacterState.Active)
        {
            ActiveTargetPosition = IdleTargetPosition;
        }
        if(AnimationTimer < 0f && State == CharacterState.Active)
        {
            if(savedAutoTargetAlly)
            {
                savedAutoTargetAlly.GetComponent<CharacterData>().State = CharacterState.Idle;
            }
            if(savedAutoTargetEnemy)
            {
                savedAutoTargetEnemy.GetComponent<CharacterData>().State = CharacterState.Idle;
            }
            savedAutoTargetAlly = null;
            savedAutoTargetEnemy = null;
            State = CharacterState.Idle;
        }


        if(State == CharacterState.Idle || State == CharacterState.Stunned )
        { 
            transform.position += (IdleTargetPosition + new Vector3(0f, 1.2f, 0.0f) - transform.position) * Time.deltaTime * 6f;
        }
        else if (State == CharacterState.Active)
        {
            transform.position += (ActiveTargetPosition + new Vector3(0f, 1.2f, 0.0f) - transform.position) * Time.deltaTime * 6f;
        }
   }

   public void SetAttack(GameObject Enemy)
   {
        ResetTargets();
        Enemy.GetComponent<CharacterData>().GetTargetIcon().enabled = true;
        Enemy.GetComponent<CharacterData>().GetTargetIcon().sprite = CharacterIcon;
        AutoTargetEnemy = Enemy;
        if(GameObject.FindObjectOfType<TutorialManager>())
        {
            GameObject.FindObjectOfType<TutorialManager>().SetEnemyTarget();
        }
   }

    public virtual void Attack(GameObject Enemy) { 
        if(Stamina <= 6)
        {
            return;
        }
        State = CharacterState.Active;
        ActiveTargetPosition = Enemy.transform.position - new Vector3(1.2f, 1.1f, 0f);
        Stamina -= 10;
        CharacterData EnemyData = Enemy.GetComponent<CharacterData>();
        Enemy.GetComponent<CharacterData>().State = CharacterState.Stunned;
        savedAutoTargetEnemy = AutoTargetEnemy;
        if(EnemyData)
        {
            if(TutorialManager.gameEnabled)
            {
                EnemyData.TakeDamage(8, gameObject);
                GameObject.FindObjectOfType<RecordCombatMetrics>().Attack(8);
            }
        }
        AnimationTimer = 1.0f;
    }

    public void SetSupport(GameObject Ally)
    {
        ResetTargets();
        Ally.GetComponent<CharacterData>().GetTargetIcon().enabled = true;
        Ally.GetComponent<CharacterData>().GetTargetIcon().sprite = CharacterIcon;
        AutoTargetAlly = Ally;
        if(GameObject.FindObjectOfType<TutorialManager>())
        {
            GameObject.FindObjectOfType<TutorialManager>().SetAlliedTarget();
        }
    }

    public virtual void Support(GameObject Ally) {
        if(Stamina <= 6)
        {
            return;
        }
        State = CharacterState.Active;
        ActiveTargetPosition = Ally.transform.position - new Vector3(2f, 1.2f, 0f);
        Stamina -= 6;
        Ally.GetComponent<CharacterData>().State = CharacterState.Stunned;
        savedAutoTargetAlly = AutoTargetAlly;
        CharacterData AllyData = Ally.GetComponent<CharacterData>();
        if(AllyData)
        {
            AllyData.SetHealth(Mathf.Min(AllyData.GetHealth() + 4, AllyData.MaxHealth));
        }
        AnimationTimer = 1.0f;
    }

    public virtual void Guard(float damage, GameObject dealer) {
        MovableCharacter GuardCheck = transform.GetComponent<MovableCharacter>();
        if(GuardCheck)
        {
            if(GuardCheck.GetPositionType() == PositionType.Vanguard)
            {
                Health += damage / 2;
            }
        }
    }

    public virtual void GuardPassive(float deltaTime) {
        Stamina -= deltaTime * 0.5f;
        if(Stamina < 0)
        {
            Health += Stamina * 2;
            Stamina = 0;
        }
    }

    public virtual void Buff() {

    }

    public virtual void Passive(float deltaTime) {
        
    }

    public virtual void TakeDamage(float damage, GameObject dealer)
    {
        Health -= damage;
        FlickerXTimes = 6;
        colorTimer = 0.0f;
        GetComponent<SpriteRenderer>().color = Color.red;
        if(DamageSFX)
        {
            GetComponent<AudioSource>().PlayOneShot(DamageSFX);
        }
        Guard(damage, dealer);
    }

    public void ResetTargets()
    {
        if(AutoTargetAlly)
        {
            AutoTargetAlly.GetComponent<CharacterData>().GetTargetIcon().enabled = false;
        }
        if(AutoTargetEnemy)
        {
            AutoTargetEnemy.GetComponent<CharacterData>().GetTargetIcon().enabled = false;
        }
        AutoTargetAlly = null;
        AutoTargetEnemy = null;
    }

    public float GetHealth()
    {
        return Health;
    }

    public float GetStamina() 
    {
        return Stamina;
    }

    public float GetMaxStamina()
    {
        return MaxStamina;
    }

    public void SetHealth(float input)
    {
        Health = input;
    }

    public void SetStamina(float input)
    {
        Stamina = input;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public void SetIdleTargetPosition(Vector3 input)
    {
        IdleTargetPosition = input;
    }

    public void SetActiveTargetPosition(Vector3 input)
    {
        ActiveTargetPosition = input;
    }

    public SpriteRenderer GetTargetIcon()
    {
        return IconTarget;
    }
}
