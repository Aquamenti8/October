﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue  {

    //public string name;

    [TextArea(3,10)]
    public string[] sentences;

    //public float TalkRange;
    //public float HearRange;

    public float typingSpeed = 0.02f;
    public string addFace;
    public int trigger;
}
