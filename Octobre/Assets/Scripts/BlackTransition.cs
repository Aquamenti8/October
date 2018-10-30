using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackTransition : MonoBehaviour {


    private bool transition;
    public float a;

    private bool toBlack;
    private bool toTransparent;

    private bool gameLaunch;
    public GameObject TextPP;

    Player_movment playerMvt;

    // Update is called once per frame
    private void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
        a = 1;
        gameLaunch = true;

        playerMvt = GameObject.Find("Player").GetComponent<Player_movment>();
    }
    void Update () {

        if (gameLaunch)
        {
            a -= Time.deltaTime /4;
            GetComponent<Image>().color = new Color(0, 0, 0, a);
            playerMvt.charging = true;

            if (a <= 0) {
                gameLaunch = false;
                playerMvt.charging = false;
            }
            
        }

        else if (transition)
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
                if (a <= 0) { transition = false; toTransparent = false;
                }
            }
        }

        
	}

    

    public void FadeBlack()
    {
        transition = true;
        toBlack = true;
        a = 0;
        Debug.Log("Fade");
        playerMvt.charging = true;
    }

    IEnumerator wait ()
    {
        
        yield return new WaitForSeconds(0.3f);
        toTransparent = true;
        playerMvt.charging = false;
    }

    IEnumerator PP_DalimaWants()
    {
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = true;
        //when thinking, cant talk and cant open menu, walk slowly, can't warp
        GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
        //Affiche les lettres les unes apres les autres
        string txt = "Hum,Dalima is worried";
        string txt2 = "Kolimo is not acting normaly";
        TextPP.GetComponent<Text>().text = "";

        yield return new WaitForSeconds(0.5f);

        //Draw text1
        TextPP.GetComponent<Text>().text = txt;
        float al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);

        //Fade text
         al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //Reset
        al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        TextPP.GetComponent<Text>().text = txt2;

        //Drawtext2
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);


        //Fade text2
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        TextPP.GetComponent<Text>().text = "";
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = false;
        GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator PP_Intro()
    {
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = true;
        GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
        string txt = "Where am I?";
        string txt2 = "Where is 'he'";
        TextPP.GetComponent<Text>().text = "";

        yield return new WaitForSeconds(0.5f);

        //Draw text1
        TextPP.GetComponent<Text>().text = txt;
        float al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);
        //Fade text
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //Reset
        al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        TextPP.GetComponent<Text>().text = txt2;

        //Drawtext2
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);


        //Fade text2
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        TextPP.GetComponent<Text>().text = "";
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = false;
        GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        yield return new WaitForSeconds(0.5f);
    }


    IEnumerator PP_Intro2()
    {
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = true;
        GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
        string txt = "I... I dont remember his name";
        string txt2 = "I just remember... I need to... Find a way to leave... With 'him'";
        TextPP.GetComponent<Text>().text = "";

        yield return new WaitForSeconds(0.5f);

        //Draw text1
        TextPP.GetComponent<Text>().text = txt;
        float al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al < 1)
        {
            al += Time.deltaTime *2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(2f);
        //Fade text
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //Reset
        al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        TextPP.GetComponent<Text>().text = txt2;

        //Drawtext2
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(2f);


        //Fade text2
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        TextPP.GetComponent<Text>().text = "";
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = false;
        GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator PP_KolimoSusp()
    {
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = true;
        GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);

        string txt = "Kolimo seems up to something";
        string txt2 = "But he doesn't want to tell me";
        string txt3 = "I need to learn more about him";
        TextPP.GetComponent<Text>().text = "";

        yield return new WaitForSeconds(1f);
        //Draw text1
        TextPP.GetComponent<Text>().text = txt;
        float al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);

        //Fade text
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //Reset
        al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        TextPP.GetComponent<Text>().text = txt2;

        //Drawtext2
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);


        //Fade text2
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //Reset
        al = 0;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        TextPP.GetComponent<Text>().text = txt3;

        //Drawtext3
        while (al < 1)
        {
            al += Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(1f);


        //Fade text3
        al = 1;
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
        while (al > 0)
        {
            al -= Time.deltaTime * 2;
            TextPP.GetComponent<Text>().color = new Color(1, 1, 1, al);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        TextPP.GetComponent<Text>().text = "";
        TextPP.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        GameObject.Find("Player").GetComponent<Player_movment>().thinking = false;
        GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        yield return new WaitForSeconds(0.5f);
    }
}
