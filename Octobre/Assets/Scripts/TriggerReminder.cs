using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReminder : MonoBehaviour {


    public bool[] trigger;
    private void Start()
    {
        //0 dont exist
        trigger = new bool[20]; //Set lenght
        trigger[0] = false; //premier eveil dans le jeu
        trigger[1] = false; // DalimaDia1
        trigger[2] = false; // DalimaDia2
        trigger[3] = false; // Dalima hear about Kolimo 1 time
        trigger[4] = false; // Dalima hear about Kolimo x time, you don't kwnow Kolimo
        trigger[5] = false; // MorganaDia1
        trigger[6] = false; // CUTSCENE kolimo dash
        trigger[7] = false; // Kolimo hear bout Dalima 1  time
        trigger[8] = false; // Kolimo hear about Dalima x time
        trigger[9] = false; // Retalk to Dalima after Kolimo=> THINK Dalima is worried for Kolimo
        trigger[10] = false; // Retalk 2 to Dalima, 
        trigger[11] = false; // Kolima talk 2 he is suspticious
        trigger[12] = false; // first talk to Kolimo
        trigger[13] = false; // Kolimo is angry, he doesn't want to talk anymore => THINK suspicious => Morgana advise to wait the night.
        trigger[14] = false; // First Dialogue with Morgana
        trigger[15] = false; // Sleeping dialogue with morgana
        trigger[16] = false; //First dialogue with keeper
        trigger[17] = false; // Keeper warn he's going to leave
        trigger[18] = false; // Keeper left to the abyss
    }

    private bool PP_DalimaWants;
    private bool PP_KolimoSusp;
    private void Update()
    {
        if (trigger[9]&& !PP_DalimaWants)
        {
            GameObject.Find("Black").GetComponent<BlackTransition>().StartCoroutine("PP_DalimaWants");
            PP_DalimaWants = true;
        }
        if(trigger[13] && !PP_KolimoSusp)
        {
            GameObject.Find("Black").GetComponent<BlackTransition>().StartCoroutine("PP_KolimoSusp");
            PP_KolimoSusp = true;
        }
    }
}
