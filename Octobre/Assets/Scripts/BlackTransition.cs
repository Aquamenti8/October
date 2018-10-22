using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackTransition : MonoBehaviour {


    private bool transition;
    public float a;

    private bool toBlack;
    private bool toTransparent;


    // Update is called once per frame
    void Update () {

        if (transition)
        {

            if (toBlack) {
                a += Time.deltaTime*2;
                GetComponent<Image>().color = new Color(0, 0, 0, a);
                if (a >= 1) { 
                    StartCoroutine("wait");
                    toBlack = false; }

            }

            if (toTransparent)
            {
                a -= Time.deltaTime*2;
                GetComponent<Image>().color = new Color(0, 0, 0, a);
                if (a <= 0) { transition = false; toTransparent = false; }
            }
        }
	}

    

    public void FadeBlack()
    {
        transition = true;
        toBlack = true;
        a = 0;
        Debug.Log("Fade");
    }

    IEnumerator wait ()
    {
        
        yield return new WaitForSeconds(0.3f);
        toTransparent = true;
    }
}
