using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BallJoystickController : MonoBehaviour
{


    protected CharacterController controller;
    protected PlayerControls playerInput;
    protected Rigidbody rb;

    private float maxSpeed = 5.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = new PlayerControls();
    }

    private void Update()
    {
        Vector2 movement = playerInput.DefaultMap.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        

        if (move != Vector3.zero && rb.velocity.sqrMagnitude <= maxSpeed)
        {
            rb.AddForce(move * 10);
        }

    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

}
