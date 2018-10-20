using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movment : MonoBehaviour {

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

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

    void FixedUpdate()
    {
        // Move character
        controller.Move(horizontalMove*Time.fixedDeltaTime, jump);
        if (jump)
        {
            GetComponent<Animator>().SetTrigger("Jump");
        }
        jump = false;
    }
}
