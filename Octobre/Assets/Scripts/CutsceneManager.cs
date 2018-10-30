using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    /*Cutscenes: 
     * concerne un certain nombre de pnj, ils ont des double cinematiques
     * quand une cutscene est trigger, les originaux sont desactivés(juste le sprite renderer) et leur copies sont affichée a leur place exacte.
     * 
    */

    private GameObject player;
    public GameObject NPC_Dalima;
    public GameObject NPC_Keeper;
    public GameObject NPC_Kolimo;
    public GameObject NPC_Morgana;

    public GameObject Cut_Player;
    public GameObject Cut_Dalima;
    public GameObject Cut_Keeper;
    public GameObject Cut_Kolimo;
    public GameObject Cut_Morgana;

    public GameObject S_KolimoDash;

    public void StartCutScene (string cutsceneName)
    {
        //en fonction du nom de la cutscene lance un Ienumarator
        StartCoroutine("Cut_KolimoDash");
    }

	void Start () {
        player = GameObject.Find("Player");
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            //Change cutscene by player
            player.GetComponent<Player_movment>().sleep = false;
            player.GetComponent<Animator>().Play("Player_idle");
            player.GetComponent<Player_movment>().cutscene = false;
            player.GetComponent<SpriteRenderer>().enabled = true;
            player.transform.position = Cut_Player.transform.position;
            Cut_Player.SetActive(false);
            Cut_Player.GetComponent<SpriteRenderer>().flipX = false;
        }
        
    }

    IEnumerator Cut_KolimoDash()
    {
        //se trigger lorsque player arrive a un certain switch (un gameobject avec un trigger on enter)

        //INITIALISE
        player.GetComponent<Player_movment>().cutscene = true;
        player.GetComponent<Player_movment>().dontMove = true;
        player.GetComponent<SpriteRenderer>().enabled = false;
        Cut_Player.SetActive(true);
        Cut_Player.transform.position = player.transform.position;
        Cut_Kolimo.SetActive(true);
        Cut_Kolimo.GetComponent<SpriteRenderer>().color = new Color(127, 107, 185);
        Cut_Kolimo.transform.position = new Vector2(8.14f, -0.19f);

        //1 Player stop
        Cut_Player.GetComponent<Animator>().Play("Player_idle");
        Cut_Player.GetComponent<Animator>().SetBool("Grounded", true);

        Cut_Kolimo.GetComponent<Animator>().Play("KolimoRun");
        //2 Kolimo Dash
        for(float i = 0; i<0.5f; i+= Time.deltaTime)
        {
            Cut_Kolimo.transform.Translate(Vector2.left * 8 * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //3 Player se retourne;
        Cut_Player.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(1);

        //RESET
        player.GetComponent<Player_movment>().cutscene = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.transform.position = Cut_Player.transform.position;
        Cut_Player.SetActive(false);
        Cut_Player.GetComponent<SpriteRenderer>().flipX = false;
        Cut_Kolimo.SetActive(false);
        Cut_Kolimo.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        NPC_Kolimo.SetActive(true);
        yield return new WaitForSeconds(1);
       
    }

    IEnumerator Intro()
    {
        //Se reveille, appelle, pense "where am I?" "Where is he?"
        //INITIALISE
        player.GetComponent<Player_movment>().cutscene = true;
        player.GetComponent<Player_movment>().dontMove = true;
        player.GetComponent<SpriteRenderer>().enabled = false;
        Cut_Player.SetActive(true);
        Cut_Player.transform.position = player.transform.position;

        //SE REVEILLE
        Cut_Player.GetComponent<Animator>().SetBool("Grounded", true);
        Cut_Player.GetComponent<Animator>().Play("Player_reveil");
        yield return new WaitForSeconds(2);

        //Pense
        GameObject.Find("Black").GetComponent<BlackTransition>().StartCoroutine("PP_Intro");

        //Regarde a gauche et a droite
        Cut_Player.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        Cut_Player.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(3f);
        Cut_Player.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        Cut_Player.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(3f);

        //APPELLE
        //OpenMenu de base
        PickupCircle Pickup = GameObject.Find("ActionCollider").GetComponent<PickupCircle>();
        MenuBehaviour MenuB = GameObject.Find("ActionCollider").GetComponent<MenuBehaviour>();
        Pickup.Menu.SetActive(true);
        Pickup.activeMenu = "Main";
        MenuB.ChangeCurrent(0);
        MenuB.UpdateSprite();
        yield return new WaitForSeconds(1);
        //Open Call menu
        Pickup.activeMenu = "Call";
        MenuB.ChangeCurrent(0);
        MenuB.UpdateSprite();
        yield return new WaitForSeconds(1.5f);
        //Call
        Pickup.activeMenu = "";
        Pickup.Menu.SetActive(false);
        Pickup.callingName = "Unknown";
        Pickup.StartCoroutine("CallEffect");
        yield return new WaitForSeconds(1);

        
        //Regarde a gauche et a droite
        Cut_Player.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        Cut_Player.GetComponent<SpriteRenderer>().flipX = false;
        //
        GameObject.Find("Black").GetComponent<BlackTransition>().StartCoroutine("PP_Intro2");

        yield return new WaitForSeconds(5);

        //Change cutscene by player
        player.GetComponent<Player_movment>().sleep = false;
        player.GetComponent<Animator>().Play("Player_idle");
        player.GetComponent<Player_movment>().cutscene = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.transform.position = Cut_Player.transform.position;
        Cut_Player.SetActive(false);
        Cut_Player.GetComponent<SpriteRenderer>().flipX = false;
    }
}
