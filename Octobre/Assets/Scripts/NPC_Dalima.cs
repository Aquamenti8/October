using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dalima : MonoBehaviour {

    bool convOn;

    public float HearRange = 2;
    public float TalkRange = 0.5f;

    private string ReceivedCol = "";

    public void TriggerDialogue(Dialogue t_dialogue)
    {
        if (t_dialogue != null)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(t_dialogue);
            //FindObjectOfType<DialogueManager>().dialogueBox.alignment = TextAnchor.LowerLeft;

            convOn = true;

            float playerSign = 0.8f;
            //if ((this.transform.position.x > GameObject.Find("Player").transform.position.x)) playerSign = -0.8f;

            FindObjectOfType<DialogueManager>().dialogueBox.rectTransform.position = new Vector3(this.transform.position.x + 0.8f * playerSign, this.transform.position.y + 0.2f, 0);
        }
    }

    private void Update()
    {
        //this.GetComponent<Animator>().SetBool("ConvOn", convOn);
        DialogueManager dia = FindObjectOfType<DialogueManager>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //JOUEUR HEARANGE + CLICK + CONVON = DISPLAY NEXT
        if ((Vector2.Distance(this.transform.position, player.transform.position) < HearRange) && (Input.GetMouseButtonDown(0) == true && (convOn)))
        {
            dia.DisplayNextSentence();

        }

        //JOUEUR TALKRANGE + CLICK = LANCE CONV
        if ((Vector2.Distance(this.transform.position, player.transform.position) < TalkRange) && (Input.GetMouseButtonDown(0) == true && (!convOn)) && GameObject.Find("ActionCollider").GetComponent<PickupCircle>().callingName =="")
        {
            if ((GameObject.Find("ActionCollider").GetComponent<PickupCircle>().activeMenu == "") && (!GameObject.Find("ActionCollider").GetComponent<PickupCircle>().calling) && !player.GetComponent<Player_movment>().dontMove && !player.GetComponent<Player_movment>().thinking)
            {
                TriggerDialogue(ChooseDialogue("self"));
            }
        }

        //JOUEUR TALKRANGE = PICKUPCIRCLE.nearNPC = "Dalima"
        if ((Vector2.Distance(this.transform.position, player.transform.position) < TalkRange))
        {
            GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearNPC = "Dalima";
            GetComponent<Animator>().SetBool("InTalkRange", true);
        }
        else {
            if (GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearNPC == "Dalima") {
                GameObject.Find("ActionCollider").GetComponent<PickupCircle>().nearNPC = "";
            }
            GetComponent<Animator>().SetBool("InTalkRange", false);
        }

        //JOUEUR TALKRANGE + INVENTORY OPEN + CLICK = ANIM GIVE + ANIM RECEIVE + RETIRE UN OBJET + CHOOSE DIALOGUE bouffe
        if ((Vector2.Distance(this.transform.position, player.transform.position) < TalkRange))
        {
            if(GameObject.Find("ActionCollider").GetComponent<PickupCircle>().giving == true)
            {
                Debug.Log("player is giving!");
                StartCoroutine("Receive");
            }
        }


        //JOUEUR A DROITE flip = false JOUEUR A GAUCHE flip = true
        if ((player.transform.position.x < this.transform.position.x))
        {
            GetComponent<Animator>().SetBool("Flip",false);
        }
        else if ((player.transform.position.x > this.transform.position.x))
        {
            GetComponent<Animator>().SetBool("Flip",true);
        }
  

        //TEXTE VIDE + CONV ON = CONV OFF
        if (dia.dialogueBox.text == "" && convOn) {
        convOn = false; }


        //JOUEUR LOIN = END CONV
        if ((Vector2.Distance(this.transform.position, player.transform.position) > HearRange) && convOn)
        {
            dia.dialogueBox.text = "";
            convOn = false;
            dia.EndDialogue();
        }

        //JOUEUR CALL = LANCE CONV
        if ((Vector2.Distance(this.transform.position, player.transform.position) < HearRange))
        {
            if ((GameObject.Find("ActionCollider").GetComponent<PickupCircle>().calling) && (GameObject.Find("ActionCollider").GetComponent<PickupCircle>().activeMenu == ""))
            {
                    TriggerDialogue(ChooseDialogue(GameObject.Find("ActionCollider").GetComponent<PickupCircle>().callingName));
            }  
        }
    }
    public Dialogue dialogue;
    public Dialogue dialogue2;
    public Dialogue dialogue3;

    public Dialogue dia_unknown;
    public Dialogue dia_keeper;
    public Dialogue dia_kolimo;
    public Dialogue dia_kolimo2;
    public Dialogue dia_kolimo3;
    public Dialogue dia_kolimo4;
    public Dialogue dia_kolimo5;
    public Dialogue dia_dalima;

    public Dialogue dia_receive;

    private Dialogue ChooseDialogue(string who) //de qui on parle: self ou callingname
    {
        Dialogue tempDia = dialogue;
        bool[] trig = GameObject.Find("TheReminder").GetComponent<TriggerReminder>().trigger;

        switch (who)
        {
            case "self": {
                    if (trig[1]) tempDia = dialogue2;
                    if (trig[2]) tempDia = dialogue3;
                    break; }
            case "Unknown": tempDia = dia_unknown; break;
            case "Keeper": tempDia = dia_keeper; break;
            case "Kolimo": {
                    tempDia = dia_kolimo;
                    if (trig[3]) tempDia = dia_kolimo2;
                    if (trig[4]) tempDia = dia_kolimo3;
                    if (trig[8]) tempDia = dia_kolimo4;
                    if (trig[9]) tempDia = dia_kolimo5;
                    break;
                }
            case "Dalima": tempDia = dia_dalima; break;

            case "Collectable":
                {
                    switch (ReceivedCol)
                    {
                        case "Cherry": tempDia = dia_receive; break;
                        default: tempDia = dia_receive; break;
                    }
                    break;
                }
            default: tempDia = null; break;
        }

        return tempDia;
    }

    IEnumerator Receive()
    {
        //recupere l'instance de l'objet, attend, detruit l'objet et lance un dialogue
        Transform t = GameObject.Find("Player").transform;
        Transform pop = null;
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == "Collectable")
            {
                pop = t.GetChild(i);
            }

        }
        pop.position = new Vector2(this.transform.position.x, this.transform.position.y);

        GetComponent<Animator>().SetBool("Giving", true);

        yield return new WaitForSeconds(0.5f);

        ReceivedCol = pop.gameObject.GetComponent<ITEM>().collectable.name;

        Destroy(pop.gameObject);
        GetComponent<Animator>().SetBool("Giving", false);
        GameObject.Find("ActionCollider").GetComponent<PickupCircle>().giving = false;
        GameObject.Find("Player").GetComponent<Player_movment>().cutscene = false;

        GameObject.Find("Player").GetComponent<Animator>().SetBool("Giving", false);


        TriggerDialogue(ChooseDialogue("Collectable"));
        ReceivedCol = "";

    }
}
