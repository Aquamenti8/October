using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

    public Dialogue dialogue;
    bool convOn;

    public Dialogue dia_unknown;
    public Dialogue dia_keeper;

    public float HearRange = 2;
    public float TalkRange = 0.5f;

    public void TriggerDialogue(Dialogue t_dialogue)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(t_dialogue);

        FindObjectOfType<DialogueManager>().dialogueBox.alignment = TextAnchor.LowerLeft;
        

    }

    private void Update()
    {
        this.GetComponent<Animator>().SetBool("ConvOn", convOn);
        DialogueManager dia = FindObjectOfType<DialogueManager>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Si le joueur est dans la hearRange et click pour la suite ET que la conversation est entamée
        if ((Vector2.Distance(this.transform.position, player.transform.position) < HearRange) && (Input.GetMouseButtonDown(0) == true && (convOn)))
        {
            dia.DisplayNextSentence();

        }

        //Si le joueur est proche et lance la conversation
            if ((Vector2.Distance(this.transform.position, player.transform.position) < TalkRange) && (Input.GetMouseButtonDown(0) == true && (!convOn)))
        {
            if ((GameObject.Find("ActionCollider").GetComponent<PickupCircle>().activeMenu == "") && (!GameObject.Find("ActionCollider").GetComponent<PickupCircle>().calling))
            {
                TriggerDialogue(dialogue);
                convOn = true;

                float playerSign = 0.9f;
                if ((this.transform.position.x > GameObject.Find("Player").transform.position.x)) playerSign = -0.9f;

                dia.dialogueBox.rectTransform.position = new Vector3(this.transform.position.x + 0.8f * playerSign, this.transform.position.y + 0.2f, 0);
            }
        }

        if(dia.dialogueBox.text == "" && convOn) { convOn = false; }
   

       //Si le joueur s'eloigne trop
        if ((Vector2.Distance(this.transform.position, player.transform.position) >HearRange) && convOn)
        {
            dia.dialogueBox.text = "";
            convOn = false;
            dia.EndDialogue();
        }
        /*
        if (!convOn && dia.dialogueBox.text != "") dia.dialogueBox.text = "";
        */

        //si le player appelle
        if ((Vector2.Distance(this.transform.position, player.transform.position) < HearRange))
        {
            if ((GameObject.Find("ActionCollider").GetComponent<PickupCircle>().calling) && (GameObject.Find("ActionCollider").GetComponent<PickupCircle>().activeMenu == ""))
            {
                // TEMP TEMP TEMP SI LE PLAYER DIT UNKNOWN, PARLE DE UNKNOW
                if (GameObject.Find("ActionCollider").GetComponent<PickupCircle>().callingName == "Unknown")
                {
                    TriggerDialogue(dia_unknown); convOn = true;
                    int playerSign = 1;
                    if ((this.transform.position.x > GameObject.Find("Player").transform.position.x)) playerSign = -1;

                    dia.dialogueBox.rectTransform.position = new Vector3(this.transform.position.x + 0.8f * playerSign, this.transform.position.y + 0.2f, 0);
                }
            }
            if (GameObject.Find("ActionCollider").GetComponent<PickupCircle>().callingName == "Keeper")
            {
                TriggerDialogue(dia_keeper); convOn = true;
                int playerSign = 1;
                if ((this.transform.position.x > GameObject.Find("Player").transform.position.x)) playerSign = -1;

                dia.dialogueBox.rectTransform.position = new Vector3(this.transform.position.x + 0.8f * playerSign, this.transform.position.y + 0.2f, 0);

            }
        }

    }


}
