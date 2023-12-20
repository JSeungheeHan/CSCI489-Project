using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum PositionType {  Rear, Front, Vanguard, Enemy   };

public class MovableCharacter : MonoBehaviour
{
    List<GameObject> FrontGuardPositions;
    private GameObject FrontGuardParent;
    List<GameObject> RearGuardPositions;
    private GameObject RearGuardParent;
    private GameObject VanguardGuardPosition;
    [SerializeField] private GameObject EnemyPositionParent;
    List<GameObject> EnemyPositions;

    Vector3 PreviousMousePosition;
    Vector3 CurrentMousePosition;
    [SerializeField] private GameObject CurrentPosition;
    private PositionType CurrentPositionType;
    BoxCollider2D CharacterCollision;
    CharacterData CurrentData;

    [SerializeField] private Sprite characterSprite;
    [SerializeField] SpriteRenderer MouseCharacter;
    GameObject MouseDrag;
    private bool dragging;

    Color MovablePositionColor = new Color(0.4909042f, 0.8207547f, 0.793255f);
    Color CurrentPositionColor = new Color(0.9191202f, 0.9622642f, 0.4938412f);
    Color SupportablePositionColor = new Color(0.5649448f, 0.8962264f, 0.5444998f);
    Color AttackablePositionColor = new Color(1.0f, 0.5226415f, 0.5273578f);

    Color MovablePositionColorActive = new Color(0f, 0.0f, 1f);
    Color SupportablePositionColorActive = new Color(0f, 0.5f, 0f);
    Color AttackablePositionColorActive = new Color(1.0f, 0f, 0f);

    [SerializeField] public OutlineRedrawer Outline;

    enum MoveType { Attack, Move, Support, Buff, None };

    // Start is called before the first frame update
    void Start()
    {
        FrontGuardParent = GameObject.Find("Front Guard");
        RearGuardParent = GameObject.Find("Rear Guard");
        VanguardGuardPosition = GameObject.Find("Vanguard Position");

        FrontGuardPositions = new List<GameObject>();
        foreach (Transform child in FrontGuardParent.transform)
        {
            if (null == child)
                continue;
            FrontGuardPositions.Add(child.gameObject);
        }
        RearGuardPositions = new List<GameObject>();
        foreach (Transform child in RearGuardParent.transform)
        {
            if (null == child)
                continue;
            RearGuardPositions.Add(child.gameObject);
        }
        EnemyPositions = new List<GameObject>();
        foreach (Transform child in EnemyPositionParent.transform)
        {
            if (null == child)
                continue;
            EnemyPositions.Add(child.gameObject);
        }

        MouseDrag = GameObject.Find("Mouse Object");
        CharacterCollision = MouseDrag.GetComponent<BoxCollider2D>();
        
        OccupiedState CurrentPositionOccupiedState = CurrentPosition.GetComponent<OccupiedState>();
        CurrentPositionOccupiedState.SetOccupied(transform.gameObject);
        CurrentPositionType = PositionType.Front;

        CurrentData = gameObject.GetComponent<CharacterData>();
        GetComponent<CharacterData>().SetIdleTargetPosition(CurrentPosition.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GameObject.FindObjectOfType<RecordCombatMetrics>().ClickTotal(mousePosition);
        }
    }

    void OnMouseDown()
    {
        if(Outline.state == OutlineState.Hovering)
        {
            Outline.state = OutlineState.Dragging;
            Outline.SetTargetColor(MovablePositionColorActive);
        }
        if(GetComponent<CharacterData>().State == CharacterState.Idle || GetComponent<CharacterData>().State == CharacterState.Stunned)
        {
            dragging = true;
            OccupiedState OccupiedComponent;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.FindObjectOfType<RecordCombatMetrics>().ClickSucceed(mousePosition);
            mousePosition.z = 0;
            PreviousMousePosition = mousePosition;

            CharacterCollision.enabled = true;

            foreach (GameObject FrontPosition in FrontGuardPositions)
            {
                SpriteRenderer FrontPositionSprite;
                FrontPositionSprite = FrontPosition.GetComponent<SpriteRenderer>();
                OccupiedComponent = FrontPosition.GetComponent<OccupiedState>();
                if(OccupiedComponent.GetOccupied())
                {
                    FrontPositionSprite.color = Color.white;
                    if(CurrentPositionType == PositionType.Rear)
                    {
                        Debug.Log("Healable");
                        FrontPositionSprite.color = SupportablePositionColor;
                        OccupiedComponent.GetOccupied().GetComponent<MovableCharacter>().Outline.state = OutlineState.Supportable;
                        OccupiedComponent.DisplayArrow();
                        OccupiedComponent.GetOccupied().GetComponent<MovableCharacter>().Outline.SetTargetColor(SupportablePositionColor);
                    }
                }
                else
                {
                    Debug.Log("Movable");
                    FrontPositionSprite.color = MovablePositionColor;
                }
            }

            foreach (GameObject RearPosition in RearGuardPositions)
            {
                SpriteRenderer RearPositionSprite;
                RearPositionSprite = RearPosition.GetComponent<SpriteRenderer>();
                OccupiedComponent = RearPosition.GetComponent<OccupiedState>();
                if(OccupiedComponent.GetOccupied())
                {
                    RearPositionSprite.color = Color.white;
                }
                else
                {
                    RearPositionSprite.color = MovablePositionColor;
                }
            }

            foreach (GameObject EnemyPosition in EnemyPositions)
            {
                SpriteRenderer EnemyPositionSprite;
                EnemyPositionSprite = EnemyPosition.GetComponent<SpriteRenderer>();
                OccupiedComponent = EnemyPosition.GetComponent<OccupiedState>();
                EnemyPositionSprite.color = Color.white;
                if(OccupiedComponent.GetOccupied() && CurrentPositionType == PositionType.Front)
                {
                    EnemyPositionSprite.color = AttackablePositionColor;
                    OccupiedComponent.GetOccupied().GetComponent<GuardEnemyData>().Outline.state = OutlineState.Attackable;
                    OccupiedComponent.DisplayArrow();
                    OccupiedComponent.GetOccupied().GetComponent<GuardEnemyData>().Outline.SetTargetColor(AttackablePositionColor);
                }
            }

            SpriteRenderer VanguardPositionSprite;
            VanguardPositionSprite = VanguardGuardPosition.GetComponent<SpriteRenderer>();
            OccupiedComponent = VanguardGuardPosition.GetComponent<OccupiedState>();
            if(OccupiedComponent.GetOccupied())
            {
                VanguardPositionSprite.color = Color.white;
                if(CurrentPositionType == PositionType.Rear)
                {
                    VanguardPositionSprite.color = SupportablePositionColor;
                    OccupiedComponent.GetOccupied().GetComponent<MovableCharacter>().Outline.state = OutlineState.Supportable;
                    OccupiedComponent.DisplayArrow();
                    OccupiedComponent.GetOccupied().GetComponent<MovableCharacter>().Outline.SetTargetColor(SupportablePositionColor);
                }
            }
            else
            {
                VanguardPositionSprite.color = MovablePositionColor;
            }

            SpriteRenderer CurrentPositionSprite = CurrentPosition.GetComponent<SpriteRenderer>();
            CurrentPositionSprite.color = CurrentPositionColor;
        }
    }

    void OnMouseDrag()
    {
        if(dragging)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            Vector2 mousePosition = new Vector2(mouse.x, mouse.y);
            Vector2 charPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 topPoint = (mousePosition + charPosition) / 2f;
            topPoint.y += 4f;

            MouseDrag.transform.position = mouse;

            Color lineColor = MovablePositionColor;

            bool acted = false;
            if(CurrentPositionType == PositionType.Rear)
            {
                GameObject Result = null;
                foreach (GameObject AllyPosition in FrontGuardPositions)
                {
                    if(AllyPosition.GetComponent<OccupiedState>().GetOccupied())
                    {
                        AllyPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<MovableCharacter>().Outline.state = OutlineState.Supportable;
                        AllyPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<MovableCharacter>().Outline.SetTargetColor(SupportablePositionColor);
                    }
                    if(VerifyCanSupport(AllyPosition))
                    {
                        Result = AllyPosition;
                    }
                }
                if(VanguardGuardPosition.GetComponent<OccupiedState>().GetOccupied())
                {
                    VanguardGuardPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<MovableCharacter>().Outline.state = OutlineState.Supportable;
                    VanguardGuardPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<MovableCharacter>().Outline.SetTargetColor(SupportablePositionColor);
                }
                if(VerifyCanSupport(VanguardGuardPosition))
                {
                    Result = VanguardGuardPosition;
                }
                if(Result)
                {
                    Result.GetComponent<OccupiedState>().GetOccupied().GetComponent<MovableCharacter>().Outline.state = OutlineState.Supporting;
                    Result.GetComponent<OccupiedState>().GetOccupied().GetComponent<MovableCharacter>().Outline.SetTargetColor(SupportablePositionColorActive);
                    lineColor = SupportablePositionColorActive;
                    acted = true;
                }
            }
            if(CurrentPositionType == PositionType.Front)
            {
                GameObject Result = null;
                foreach (GameObject EnemyPosition in EnemyPositions)
                {
                    if(EnemyPosition.GetComponent<OccupiedState>().GetOccupied())
                    {
                        EnemyPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<GuardEnemyData>().Outline.state = OutlineState.Attackable;
                        EnemyPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<GuardEnemyData>().Outline.SetTargetColor(AttackablePositionColor);
                    }
                    if(VerifyCanAttack(EnemyPosition))
                    {
                        Result = EnemyPosition;
                    }
                }
                if(Result)
                {
                    Result.GetComponent<OccupiedState>().GetOccupied().GetComponent<GuardEnemyData>().Outline.state = OutlineState.Attacking;
                    Result.GetComponent<OccupiedState>().GetOccupied().GetComponent<GuardEnemyData>().Outline.SetTargetColor(AttackablePositionColorActive);
                    lineColor = AttackablePositionColorActive;
                    acted = true;
                }
            }

            if(GetComponent<LineRenderer>())
            {
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.SetColors(lineRenderer.startColor + 5 * ((lineColor - lineRenderer.startColor) * Time.deltaTime), lineRenderer.startColor + 5 * ((lineColor - lineRenderer.startColor) * Time.deltaTime));
                lineRenderer.positionCount = 40;
                var points = new Vector3[40];
                float iterate = 0;
                for(int i = 0; i < 40; i++)
                {
                    iterate += 1f;
                    points[i] = BezzierCurve.Bezier(transform.position, topPoint, mousePosition, iterate / 40f);
                }
                lineRenderer.SetPositions(points);
            }

            if(!acted)
            {
                GameObject PositionMove = FindPositionToMove(true);
                if(PositionMove)
                {
                    MouseCharacter.gameObject.transform.position = PositionMove.transform.position + new Vector3(0f, 1.2f, 0f);
                    MouseCharacter.sprite = characterSprite;
                    MouseCharacter.color = new Color(1f, 1f, 1f, 0.6f);
                }
                else
                {
                    MouseCharacter.color = new Color(0f, 0f, 0f, 0f);
                }
            }
            else
            {
                MouseCharacter.color = new Color(1f, 1f, 1f, 0.0f);
            }
        }
    }

    void OnMouseUp()
    {
        if(GetComponent<LineRenderer>())
        {
            GetComponent<LineRenderer>().positionCount = 0;
        }
        if(Outline.state == OutlineState.Dragging)
        {
            Outline.state = OutlineState.Idle;
            Outline.SetTargetColor(new Color(0f, 0f, 0f));
            GetComponent<LineRenderer>().SetColors(MovablePositionColor, MovablePositionColor);
        }
        if(dragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GameObject.FindObjectOfType<RecordCombatMetrics>().Drop(mousePosition);
            bool acted = false;
            dragging = false;

            if(CurrentPositionType == PositionType.Rear)
            {
                GameObject PositionSupport = FindPositionToSupport();
                if(PositionSupport)
                {
                    ApplySupport(PositionSupport);
                    acted = true;
                }
            }
            if(CurrentPositionType == PositionType.Front)
            {
                GameObject PositionAttack = FindPositionToAttack();
                if(PositionAttack)
                {
                    ApplyAttack(PositionAttack);
                    acted = true;
                }
            }

            if(!acted)
            {
                GameObject PositionMove = FindPositionToMove(false);
                if(PositionMove)
                {
                    if(CurrentPosition != PositionMove)
                    {
                        ApplyMove(PositionMove);
                        acted = true;
                        GetComponent<CharacterData>().ResetTargets();
                    }
                }
            }

            GetComponent<CharacterData>().SetIdleTargetPosition(CurrentPosition.transform.position);

            if(!acted)
            {
                GameObject.FindObjectOfType<RecordCombatMetrics>().FailedDrop(mousePosition);
            }

            foreach (GameObject RearPosition in RearGuardPositions)
            {
                ClearHighlights(RearPosition);
            }
            foreach (GameObject FrontPosition in FrontGuardPositions)
            {
                ClearHighlights(FrontPosition);
            }
            ClearHighlights(VanguardGuardPosition);
            
            CharacterCollision.enabled = false;

            foreach (GameObject EnemyPosition in EnemyPositions)
            {
                EnemyPosition.GetComponent<SpriteRenderer>().color = Color.white;
                EnemyPosition.GetComponent<OccupiedState>().HideArrow();
                if(EnemyPosition.GetComponent<OccupiedState>().GetOccupied())
                {
                    
                    EnemyPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<GuardEnemyData>().Outline.state = OutlineState.Idle;
                    EnemyPosition.GetComponent<OccupiedState>().GetOccupied().GetComponent<GuardEnemyData>().Outline.SetTargetColor(new Color(0f, 0f, 0f, 0f));
                }
            }
        }
    }

    private GameObject FindPositionToMove(bool hover)
    {
        GameObject Result = null;

        foreach (GameObject RearPosition in RearGuardPositions)
        {
            if(VerifyCanMove(RearPosition))
            {
                Result = RearPosition;
                if(!hover)
                {
                    CurrentPositionType = PositionType.Rear;
                    if(GameObject.FindObjectOfType<TutorialManager>())
                    {
                        GameObject.FindObjectOfType<TutorialManager>().MovedToRearGuard();
                    }
                }
            }
        }
        foreach (GameObject FrontPosition in FrontGuardPositions)
        {
            if(VerifyCanMove(FrontPosition))
            {
                Result = FrontPosition;
                if(!hover)
                {
                    CurrentPositionType = PositionType.Front;
                    if(GameObject.FindObjectOfType<TutorialManager>())
                    {
                        GameObject.FindObjectOfType<TutorialManager>().MovedToFrontGuard();
                    }
                }
            }
        }
        if(VerifyCanMove(VanguardGuardPosition))
        {
            Result = VanguardGuardPosition;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            GameObject.FindObjectOfType<RecordCombatMetrics>().ChangedVanguard();
            if(!hover)
            {
                CurrentPositionType = PositionType.Vanguard;
                if(GameObject.FindObjectOfType<TutorialManager>())
                {
                    GameObject.FindObjectOfType<TutorialManager>().ChangedVanguard();
                }
            }
        }
        return Result;
    }

    private bool VerifyCanMove(GameObject Position)
    {
        BoxCollider2D PositionCollider = Position.GetComponent<BoxCollider2D>();
        OccupiedState OccupiedComponent = Position.GetComponent<OccupiedState>();
        SpriteRenderer PositionSprite = Position.GetComponent<SpriteRenderer>();
        if(OccupiedComponent.GetOccupied())
        { 
            if(CharacterCollision.bounds.Intersects(PositionCollider.bounds) && OccupiedComponent.GetOccupied() == gameObject)
            {
                return true;
            }
        }

        if (CharacterCollision.bounds.Intersects(PositionCollider.bounds) && !OccupiedComponent.GetOccupied())
        {
            return true;
        }
        return false;
    }

    private void ClearHighlights(GameObject Position)
    {
        OccupiedState OccupiedComponent = Position.GetComponent<OccupiedState>();
        OccupiedComponent.HideArrow();
        Position.GetComponent<SpriteRenderer>().color = Color.white;
        if(OccupiedComponent.GetOccupied())
        {
            OccupiedComponent.GetOccupied().GetComponent<MovableCharacter>().Outline.state = OutlineState.Idle;
            OccupiedComponent.GetOccupied().GetComponent<MovableCharacter>().Outline.SetTargetColor(new Color(0f, 0f, 0f, 0f));   
        }
    }

    private void ApplyMove(GameObject Position)
    {
        OccupiedState CurrentPositionOccupiedState = CurrentPosition.GetComponent<OccupiedState>();
        OccupiedState OccupiedComponent = Position.GetComponent<OccupiedState>();

        CurrentPositionOccupiedState.SetOccupied(null);
        CurrentPosition = Position;
        GetComponent<SpriteRenderer>().sortingOrder = Position.GetComponent<OccupiedState>().GetPositionPriority() + 1;
        // Vector3 toPos = transform.position;
        // toPos.z = Position.GetComponent<OccupiedState>().GetPositionPriority() + 1;
        // transform.position = toPos;
        OccupiedComponent.SetOccupied(transform.gameObject);
    }

    private GameObject FindPositionToSupport()
    {
        GameObject Result = null;

        foreach (GameObject FrontPosition in FrontGuardPositions)
        {
            if(VerifyCanSupport(FrontPosition))
            {
                Result = FrontPosition;
            }
        }
        if(VerifyCanSupport(VanguardGuardPosition))
        {
            Result = VanguardGuardPosition;
        }
        return Result;
    }

    private bool VerifyCanSupport(GameObject Position)
    {
        BoxCollider2D PositionCollider = Position.GetComponent<BoxCollider2D>();
        OccupiedState OccupiedComponent = Position.GetComponent<OccupiedState>();
        if (CharacterCollision.bounds.Intersects(PositionCollider.bounds) && OccupiedComponent.GetOccupied())
        {
            return true;
        }
        return false;
    }

    private void ApplySupport(GameObject Position)
    {
        OccupiedState OccupiedComponent = Position.GetComponent<OccupiedState>();
        
        CurrentData.SetSupport(OccupiedComponent.GetOccupied());
    }

    private GameObject FindPositionToAttack()
    {
        GameObject Result = null;

        foreach (GameObject EnemyPosition in EnemyPositions)
        {
            if(VerifyCanAttack(EnemyPosition))
            {
                Result = EnemyPosition;
            }
        }
        return Result;
    }

    private bool VerifyCanAttack(GameObject Position)
    {
        BoxCollider2D PositionCollider = Position.GetComponent<BoxCollider2D>();
        OccupiedState OccupiedComponent = Position.GetComponent<OccupiedState>();
        if (CharacterCollision.bounds.Intersects(PositionCollider.bounds) && OccupiedComponent.GetOccupied())
        {
            return true;
        }
        return false;
    }

    private void ApplyAttack(GameObject Position)
    {
        OccupiedState OccupiedComponent = Position.GetComponent<OccupiedState>();
        CurrentData.SetAttack(OccupiedComponent.GetOccupied());
    }

    public PositionType GetPositionType()
    {
        return CurrentPositionType;
    }

    public GameObject GetCurrentPosition()
    {
        return CurrentPosition;
    }

    void OnMouseOver()
    {
        // Change the Color of the GameObject when the mouse hovers over it
        // Outline.SetTargetColor(MovablePositionColor);
        if(GetComponent<CharacterData>().State == CharacterState.Idle || GetComponent<CharacterData>().State == CharacterState.Stunned)
        {
            Outline.state = OutlineState.Hovering;
            Outline.SetTargetColor(MovablePositionColor);
        }
    }

    void OnMouseExit()
    {
        if(Outline.state == OutlineState.Hovering)
        {
            Outline.state = OutlineState.Idle;
            Outline.SetTargetColor(new Color(0f, 0f, 0f, 0f));
        }
    }
}
