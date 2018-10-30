using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBehaviour : MonoBehaviour {

    /* COMPORTMENT DE L'ESPRIT
     * 
     * Phase 1: Caché, ckeck si objet collectable proche
     * 
     * OnPlayerEnter, declenche une anim du buisson.
     * 
     * Phase 2: Sort de sa cachette
     * 
     * Phase 3: Avance vers collectable
     * 
     * Phase 4: contact collectable, wait puis pickup
     * 
     * Phase 5: cours vers la cachette et disparait 
     * 
     */

    public GameObject bush;
    public float speed;
    public int state = 0;
    public GameObject target;

    private Rigidbody2D m_Rigidbody2D;
    private float move = 1;

    public GameObject player;
    public GameObject radar;
    public GameObject HomeLand;

    private float resetX;
    private float resetY;

    private float countdown;



    void Start () {
        //STATE 1
        resetX = transform.position.x;
        resetY = transform.position.y;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<SpriteRenderer>().enabled = false;
        state = 1;
        
    }

    public void MoveBush()
    {
        bush.GetComponent<Animator>().SetTrigger("Move");
        Debug.Log("Move buisson!");
    }

    public void OutBush(GameObject targeto)
    {
        GetComponent<Animator>().SetTrigger("Out");
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<SpriteRenderer>().enabled = true;

        target = targeto;

        state = 2;
    }

    void Update () {


        if (!HomeLand.activeSelf)
        {
            if (state==5 || state == 2)
            {
                state = 1;
                transform.position = new Vector2(resetX, resetY);
                GetComponent<BoxCollider2D>().isTrigger = true;
                m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (state == 3)
            {
                state = 4;
                transform.position = new Vector2(resetX, resetY);
                GetComponent<BoxCollider2D>().isTrigger = true;
                m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if(state == 2) //OBJECT IN RANGE = GO TAKE COLLECTABLE
        {
            GetComponent<Animator>().SetBool("run", true);
            GetComponent<Animator>().SetBool("lost", false);
            if (target != null)
            {
                if (target.transform.position.x > this.transform.position.x)
                {
                    move = 1;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else { move = -1; GetComponent<SpriteRenderer>().flipX = false; }

                Vector2 targetVelocity = new Vector2(move * speed, m_Rigidbody2D.velocity.y);

                m_Rigidbody2D.velocity = targetVelocity;

                

                if (Vector3.Distance(target.transform.position, transform.position) < 0.02f)
                {
                    state = 3;
                    Destroy(target.gameObject);
                }
            }
            else
            {
                state = 5;
                countdown = 5;
            }
        }

        if (state == 3) //COLLECTABLE GET = RETURN BUSH
        {
            GetComponent<Animator>().SetBool("run", true);
            GetComponent<Animator>().SetBool("lost", false);
            if (bush.transform.position.x > this.transform.position.x)
            {
                move = 1;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                move = -1;
                GetComponent<SpriteRenderer>().flipX = false;
            }

            Vector2 targetVelocity = new Vector2(move * speed*2, m_Rigidbody2D.velocity.y);

            m_Rigidbody2D.velocity = targetVelocity;

            if(Vector3.Distance(bush.transform.position, transform.position ) < 0.2f)
            {
                state = 4;
                GetComponent<BoxCollider2D>().isTrigger = true;
                m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<SpriteRenderer>().enabled = false;

                m_Rigidbody2D.velocity = Vector3.zero;
            }
        }
		
        if(state == 4)
        {
            GetComponent<Animator>().SetBool("run", false);
            GetComponent<Animator>().SetBool("lost", false);
        }
        if (state == 5) // COLLECTABLE LOST = CHECK FOR COLLECTABLE THEN RETURN BUSH
        {
            GetComponent<Animator>().SetBool("run", false);
            GetComponent<Animator>().SetBool("lost", true);
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                GetComponent<Animator>().SetBool("run", true);
                GetComponent<Animator>().SetBool("lost", false);
                if (bush.transform.position.x > this.transform.position.x)
                {
                    move = 1;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    move = -1;
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                Vector2 targetVelocity = new Vector2(move * speed * 2, m_Rigidbody2D.velocity.y);

                m_Rigidbody2D.velocity = targetVelocity;

                if (Vector3.Distance(bush.transform.position, transform.position) < 0.2f)
                {
                    state = 1;
                    GetComponent<BoxCollider2D>().isTrigger = true;
                    m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                    GetComponent<SpriteRenderer>().enabled = false;

                    m_Rigidbody2D.velocity = Vector3.zero;
                }
            }
        }
	}

}
