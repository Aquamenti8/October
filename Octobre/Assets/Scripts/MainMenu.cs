using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public int index = 0;
    int total = 3;
    float xOffset = 300f;
    public GameObject Controls;
    public GameObject arrow;
    public Player_movment playerMvt;
	// Use this for initialization
	void Start () {
        playerMvt = GameObject.Find("Player").GetComponent<Player_movment>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!playerMvt.charging)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (index < total - 1)
                {
                    index++;
                    arrow.transform.position = new Vector2(arrow.transform.position.x + xOffset, arrow.transform.position.y);

                }
                Controls.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (index > 0)
                {
                    index--;
                    arrow.transform.position = new Vector2(arrow.transform.position.x - xOffset, arrow.transform.position.y);

                    Controls.SetActive(false);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (index == 0)
                {
                    //PLAY
                    if (GameObject.Find("TheReminder").GetComponent<TriggerReminder>().trigger[0])
                    {
                        GameObject.Find("TheReminder").GetComponent<TriggerReminder>().trigger[0] = true;
                        GameObject.Find("MainMenu").SetActive(false);
                        GameObject.Find("Player").GetComponent<Player_movment>().sleep = false;
                        GameObject.Find("CutsceneManager").GetComponent<CutsceneManager>().StartCoroutine("Intro");
                        
                    }
                    else
                    {
                        GameObject.Find("Player").GetComponent<Player_movment>().sleep = false;
                        GameObject.Find("MainMenu").SetActive(false);
                    }
                }
                if (index == 1)
                {
                    //KEYS
                    Controls.SetActive(true);
                }
                if (index == 2)
                {   //QUIT
                    Application.Quit();

                }
            }
        }
    }
}
