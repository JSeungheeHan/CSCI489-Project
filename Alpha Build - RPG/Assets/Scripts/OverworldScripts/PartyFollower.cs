using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyFollower : MonoBehaviour
{
    [SerializeField] GameObject PlayerCharacter;
    Rigidbody2D m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < PlayerCharacter.transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        if(Vector3.Distance(transform.position, PlayerCharacter.transform.position) > 2.4)
        {
            if((PlayerCharacter.transform.position - transform.position).x > 0.2 && m_Rigidbody.velocity.x < 2f)
            {
                m_Rigidbody.velocity += new Vector2(1.2f, 0f);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if ((PlayerCharacter.transform.position - transform.position).x < -0.2 && m_Rigidbody.velocity.x > -2f)
            {
                m_Rigidbody.velocity -= new Vector2(1.2f, 0f);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                m_Rigidbody.velocity += new Vector2((-m_Rigidbody.velocity.x * 0.2f), 0f);
            }

            if((PlayerCharacter.transform.position - transform.position).y > 0.1 && m_Rigidbody.velocity.y < 1f)
            {
                m_Rigidbody.velocity += new Vector2(0f, 0.7f);
            }
            else if ((PlayerCharacter.transform.position - transform.position).y < -0.1 && m_Rigidbody.velocity.y > -1f)
            {
                m_Rigidbody.velocity -= new Vector2(0f, 0.7f);
            }
            else
            {
                m_Rigidbody.velocity += new Vector2(0f, (-m_Rigidbody.velocity.y * 0.2f));
            }
        }
        else
        {
            m_Rigidbody.velocity += new Vector2((-m_Rigidbody.velocity.x * 0.2f), (-m_Rigidbody.velocity.y * 0.2f));
        }
    }
}
