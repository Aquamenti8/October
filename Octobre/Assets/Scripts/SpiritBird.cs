using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBird : MonoBehaviour {



    //Phase 1: Fait des allé retours dans une zone de 1x1. Deplacement (random x et y).RandomWait. Random Pickup. RandomWait. Deplacement ...

    //Phase 2: Joueur proche, s'envole a zone predeterminée.

    //Phase 3: Check si une cerise est sur le 1x1. Si oui et si FirstPiaf = phase4

    //Phase 4: Vole sur la cerise et revient fissa

    public GameObject zoneCheck;

    public bool FirstPiaf;
    public int state = 0;
    public GameObject target;

    public Vector2 FlyZone;

    public bool gauche;
    private float countdownMove = 0.5f;
    private float countdownWait;

    void Start()
    {
        state = 1;
    }


    void Update()
    {
        if (state == 1)
        {
            countdownMove -= Time.deltaTime;
            countdownWait -= Time.deltaTime;
            if (countdownMove > 0) //MOVE TIME
            {
                if (gauche)
                {
                    transform.Translate(Vector2.right*(Time.deltaTime/2));
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if(!gauche)
                {
                    transform.Translate(Vector2.left * (Time.deltaTime/2));
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                GetComponent<Animator>().SetBool("Move", true);
            }
            else if(countdownWait > 0) //WAIT TIME
            {
                float randomPickup = Random.Range(1, 30);
                if (randomPickup > 25) GetComponent<Animator>().SetTrigger("Pickup");

                GetComponent<Animator>().SetBool("Move", false);
            }
            else if(countdownWait <0) //MOVE AGAIN TIME
            {

                countdownWait = Random.Range(0.2f, 1f);

                float randomMove = Random.Range(1, 10);
                if (randomMove > 7)
                {
                    countdownMove = 0.5f;
                    gauche = !gauche;
                }

            }
        }
        if(state == 2)
        {
            //vole a zoneFly
            transform.position = Vector2.MoveTowards(transform.position, FlyZone, Time.deltaTime);
            
            if (transform.position.y == FlyZone.y)
            {
                state = 3;
                GetComponent<Animator>().SetBool("Fly", false);
                GetComponent<Animator>().SetBool("Move", false);
            }
        }
        if(state == 3)
        {
            //check les cerises
        }
        if(Vector2.Distance(GameObject.Find("Player").transform.position, transform.position) < 0.5f && state ==1)
        {
            state = 2;
            GetComponent<Animator>().SetBool("Fly", true);
        }
    }


}
