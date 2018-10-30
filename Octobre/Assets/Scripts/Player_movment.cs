using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movment : MonoBehaviour {

    public CharacterController2D controller;

    public float runSpeed = 10f;

    float horizontalMove = 0f;
    bool jump = false;

    public bool dontMove = false;
    public bool sleep = true;
    public bool cutscene = false;
    public GameObject mMenu;
    public bool charging = false;
    public bool thinking = false;

	// Update is called once per frame
	void Update () {

        if (!dontMove)
        {
            if (!thinking)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            }
            else
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * 4;
            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            //GetComponent<Animator>().SetFloat("Movement", horizontalMove);
            bool run;
            if (horizontalMove == 0) run = false;
            else run = true;
            GetComponent<Animator>().SetBool("Run", run);
        }

        if (sleep)
        {
            dontMove = true;
            mMenu.SetActive(true);
        }
        if (charging)
        {
            dontMove = true;
        }
        if (!sleep && !cutscene && !charging)
        {
            dontMove = false;
        }
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player_reveil"))
        {
            dontMove = true;
        }
	}


    private float InactivityCountdown = 5f;

    void FixedUpdate()
    {
        // Move character
        if (!dontMove)
        {
            InactivityCountdown -= Time.deltaTime;
            if(horizontalMove!=0 || jump)
            {
                InactivityCountdown = Random.Range(5, 15);
            }
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            if (jump)
            {
                GetComponent<Animator>().SetTrigger("Jump");
            }
            jump = false;

            if (InactivityCountdown < 0)
            {
                GetComponent<Animator>().SetBool("Inactive", true);
            }
            else GetComponent<Animator>().SetBool("Inactive", false);

        }
        else
        {
            controller.Move(0, false);
        }
    }
}
