using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReminder : MonoBehaviour {


    public bool[] trigger;
    private void Start()
    {
        //0 dont exist
        trigger = new bool[10]; //Set lenght
        trigger[1] = false; // DalimaDia1
        trigger[2] = false; // DalimaDia2
        trigger[3] = false; // Dalima hear about Kolimo 1 time
        trigger[4] = false; // Dalima hear about Kolimo x time, you don't kwnow Kolimo
        trigger[5] = false; // MorganaDia1
    }
}
