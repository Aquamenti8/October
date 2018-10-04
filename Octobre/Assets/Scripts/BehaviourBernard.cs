using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourBernard : MonoBehaviour {

    private PNJ_base pnj;
    public string[] dialogue1;

	// Use this for initialization
	void Start () {
        pnj = this.GetComponent<PNJ_base>();

        dialogue1[0] = "Heya am a tast!";
        dialogue1[1] = "Nice ta meetya";
        dialogue1[2] = "bbmmbmbmbmmb";
	}
	
	// Update is called once per frame
	void Update () {
        if ((Vector2.Distance(this.transform.position, pnj.Player.transform.position) < pnj.TalkRange) && (Input.GetMouseButtonDown(0) == true))
        {
            pnj.Talk(dialogue1);
        }
        if ((Vector2.Distance(this.transform.position, pnj.Player.transform.position) > pnj.HearRange) && pnj.TextboxInstance != null)
        {
            Destroy(pnj.TextboxInstance.gameObject);
        }
    }
}
