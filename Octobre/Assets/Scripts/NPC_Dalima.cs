using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dalima : MonoBehaviour {

    bool convOn;

    public float HearRange = 2;
    public float TalkRange = 0.5f;

    public void TriggerDialogue(Dialogue t_dialogue)
    {
        if (t_dialogue != null)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(t_dialogue);
            FindObjectOfType<DialogueManager>().dialogueBox.alignment = TextAnchor.LowerLeft;

            convOn = true;

            float playerSign = 0.8f;
            if ((this.transform.position.x > GameObject.Find("Player").transform.position.x)) playerSign = -0.8f;

            FindObjectOfType<DialogueManager>().dialogueBox.rectTransform.position = new Vector3(this.transform.position.x + 0.8f * playerSign, this.transform.position.y + 0.2f, 0);
        }
    }

    private void Update()
    {
        this.GetComponent<Animator>().SetBool("ConvOn", convOn);
        DialogueManager dia = FindObjectOfType<DialogueManager>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //JOUEUR HEARANGE + CLICK + CONVON = DISPLAY NEXT
        if ((Vector2.Distance(this.transform.position, player.transform.position) < HearRange) && (Input.GetMouseButtonDown(0) == true && (convOn)))
        {
            dia.DisplayNextSentence();

        }

        //JOUEUR TALKRANGE + CLICK = LANCE CONV
        if ((Vector2.Distance(this.transform.position, player.transform.position) < TalkRange) && (Input.GetMouseButtonDown(0) == true && (!convOn)))
        {
            if ((GameObject.Find("ActionCollider").GetComponent<PickupCircle>().activeMenu == "") && (!GameObject.Find("ActionCollider").GetComponent<PickupCircle>().calling))
            {
                TriggerDialogue(ChooseDialogue("self"));
            }
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
    public Dialogue dia_dalima;

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
                        break;
                }
            case "Dalima": tempDia = dia_dalima; break;
            default: tempDia = null; break;
        }

        return tempDia;
    }
}
