using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movment : MonoBehaviour {

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public bool dontMove = false;
	
	// Update is called once per frame
	void Update () {

        if (!dontMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
	}


    private float InactivityCountdown;

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
    }
}
