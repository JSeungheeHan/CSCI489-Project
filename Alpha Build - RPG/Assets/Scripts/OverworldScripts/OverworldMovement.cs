using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverworldMovement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    [SerializeField] BoxCollider2D InteractionCollider;
    [SerializeField] AudioSource WalkingSFX;
    [SerializeField] protected Animator Animate;
    
    bool CanMove = true;
    private bool playingWalkSFX = false;

    GameObject InteractingObject;
    public bool CurrentlyInteracting;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        CurrentlyInteracting = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(CanMove)
        {
            bool moving = false;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                if(Input.GetAxis("Vertical") > 0)
                {
                    if(m_Rigidbody.velocity.y < 1f)
                    {
                        m_Rigidbody.velocity += new Vector2(0, 0.7f);
                    }
                    moving = true;
                }
                if(Input.GetAxis("Vertical") < 0)
                {
                    if(m_Rigidbody.velocity.y > -1f)
                    {
                        m_Rigidbody.velocity += new Vector2(0, -0.7f);
                    }
                    moving = true;
                }
            }
            else
            {
                float hold = (float) (-m_Rigidbody.velocity.y * 0.2);
                m_Rigidbody.velocity += new Vector2(0, hold);
            }


            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if(Input.GetAxis("Horizontal") > 0)
                {
                    if(m_Rigidbody.velocity.x < 2f)
                    {
                        m_Rigidbody.velocity += new Vector2(1.2f, 0);
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                    moving = true;
                }
                if(Input.GetAxis("Horizontal") < 0)
                {
                    if(m_Rigidbody.velocity.x > -2f)
                    {
                        m_Rigidbody.velocity += new Vector2(-1.2f, 0);
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    moving = true;
                }
            }
            else
            {
                float hold = (float) (-m_Rigidbody.velocity.x * 0.2);
                m_Rigidbody.velocity += new Vector2(hold, 0);
            }

            if(moving)
            {
                if(!Animate.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
                {
                    Animate.Play("Walking", -1, 0);
                }
                if(!playingWalkSFX)
                {
                    WalkingSFX.Play();
                    playingWalkSFX = true;
                }
            }
            else
            {
                Animate.Play("Idle", -1, 0);
                playingWalkSFX = false;
                WalkingSFX.Stop();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // EnemyData enemy = col.gameObject.GetComponent<EnemyData>();

        // if(enemy)
        // {
        //     FindObjectOfType<FadeFromBlack>().LoadScene("SliceScene");
        // }
        if(!CurrentlyInteracting)
        {
            Debug.Log(col.gameObject.name);
            OverworldInteractable interact = col.gameObject.GetComponent<OverworldInteractable>();
            if(interact)
            {
                if(interact.Interactable)
                {
                    InteractingObject = col.gameObject;
                    interact.ShowDialoguePrompt();
                    CurrentlyInteracting = true;
                    InteractingObject.GetComponent<OverworldInteractable>().Interact();  
                }
            }
        }
    }

    public void StartInteracting()
    {
        CurrentlyInteracting = true;
    }

    public void StopInteracting()
    {
        CurrentlyInteracting = false;
        InteractionCollider.enabled = false;
        InteractionCollider.enabled = true;
    }
}
