using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

    public Dialogue dialogue;
    private bool convOn;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void Update()
    {
        DialogueManager dia = FindObjectOfType<DialogueManager>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Si le joueur est dans la hearRange et click pour la suite ET que la conversation est entamée
        if ((Vector2.Distance(this.transform.position, player.transform.position) < dialogue.HearRange) && (Input.GetMouseButtonDown(0) == true && (convOn)))
        {
            dia.DisplayNextSentence();

        }

        //Si le joueur est proche et lance la conversation
        if ((Vector2.Distance(this.transform.position, player.transform.position) < dialogue.TalkRange) && (Input.GetMouseButtonDown(0) == true && (!convOn)))
        {
            TriggerDialogue();
            convOn = true;

            dia.dialogueBox.rectTransform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0);
        }

        if(dia.dialogueBox.text == "" && convOn) { convOn = false; }
   

       //Si le joueur s'eloigne trop
        if ((Vector2.Distance(this.transform.position, player.transform.position) > dialogue.HearRange) && convOn)
        {
            dia.dialogueBox.text = "";
            convOn = false;
            //dia.StopAllCoroutines();
            dia.EndDialogue();
        }
        if (!convOn && dia.dialogueBox.text != "") dia.dialogueBox.text = "";



    }


}
