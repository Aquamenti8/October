using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutsceneSwitch : MonoBehaviour{

    public string CutSceneName;
    public int trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(CutSceneName!= "")
            {
                GameObject.Find("CutsceneManager").GetComponent<CutsceneManager>().StartCutScene(CutSceneName);
                GameObject.Find("TheReminder").GetComponent<TriggerReminder>().trigger[trigger] = true;
                Destroy(this.gameObject);
            }
        }
    }
}
