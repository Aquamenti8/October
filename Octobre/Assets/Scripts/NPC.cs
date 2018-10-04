using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void Update()
    {
        //Si le joueur est proche et lance la conversation
        if ((Vector2.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < dialogue.TalkRange) && (Input.GetMouseButtonDown(0) == true))
        {
            TriggerDialogue();
        }

        /*
        if ((Vector2.Distance(this.transform.position, Player.transform.position) > dialogue.HearRange) && pnj.TextboxInstance != null)
        {
           // Destroy(pnj.TextboxInstance.gameObject);
        }
        */
    
    }
}
