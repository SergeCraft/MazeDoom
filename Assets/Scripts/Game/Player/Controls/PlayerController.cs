using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //[SerializeField] private float playerSpeed = 2.0f;
    //[SerializeField] private float jumpHeight = 1.0f;
    //[SerializeField] private float gravityValue = -9.81f;

    protected CharacterController controller;
    protected PlayerControls playerInput;
    protected Rigidbody rb;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Text console;
    private UnityEngine.InputSystem.Gyroscope gyroscope;

    private void Awake()
    {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        playerInput = new PlayerControls();
        //gyroscope = UnityEngine.InputSystem.Gyroscope.current;
        //InputSystem.EnableDevice(gyroscope);
        //Debug.Log($"Gyroscope is enabled: {gyroscope.enabled}");
        //console = GameObject.Find("Console").GetComponent<Text>();
        //console.text += $"Gyroscope is enabled: {gyroscope.enabled}";
    }

    private void Update()
    {
        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        //Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        Vector2 movement = playerInput.DefaultMap.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        //controller.Move(move * Time.deltaTime * playerSpeed);
        

        //console.text = $"Gyro: {gyroscope.ReadDefaultValueAsObject()}";
        if (move != Vector3.zero)
        {
            //gameObject.transform.forward = move;
            rb.AddForce(move * 10);
        }

        // bool jumpPress = playerInput.Player.Jump.IsPressed();
        //bool jumpPress = playerInput.DefaultMap.Jump.triggered;
        //if (jumpPress && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
    }

    internal void Respawn()
    {
        rb.Sleep();
        transform.position = new Vector3(0f, 1f, 0f);
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
