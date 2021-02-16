using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 0.5f;
    public float crouchHeight = 0.7f;
    public float normalHeight = 1f;

    public Transform groundCheck;
    public Transform body;

    public LayerMask groundMask;

    Vector3 velocity;
    
    bool isGrounded;
    bool isCrouched;

    void Start()
    {
        isCrouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            controller.slopeLimit = 45;
        }
        else
        {
            controller.slopeLimit = 90;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouched = true;
            controller.height = crouchHeight*2;
            transform.localScale = new Vector3(1, crouchHeight, 1);
            body.transform.localScale = new Vector3(1,crouchHeight,1);
            speed = 3;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouched = false;
            controller.height = normalHeight*2;
            transform.localScale = new Vector3(1, normalHeight, 1);
            body.transform.localScale = new Vector3(1, normalHeight, 1);
            speed = 5f;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouched)
        {
            speed = 8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            speed = 5f;
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * speed * Time.deltaTime);


    }

}
