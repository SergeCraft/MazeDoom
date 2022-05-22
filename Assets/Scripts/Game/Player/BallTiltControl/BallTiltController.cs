using Assets.Scripts.Game.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BallTiltController : MonoBehaviour
{


    protected Rigidbody rb;
    private GameObject uiGroup;
    private GameObject uiSwitchJoystickModeButton;

    private PlayerSparksController playerSparksController;


    // Auto setting up anchors settings    

    private float tiltAnchorY = -0.6f;
    private bool isAutoSetupInProgress;
    private float autoSetupPeriod = 1.0f;

    // accelerometer settings

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    float lowPassKernelWidthInSeconds = 0.05f;

    private float lowPassFilterFactor;
    private Vector3 lowPassValue = Vector3.zero;

    // speed limit
    private float maxSpeed = 5.0f;

    // events
    public event AutoSetupStateChanged autoSetupStateChanged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        uiGroup = GameObject.Find("BallTiltControl");
        playerSparksController = GetComponentInChildren<PlayerSparksController>();
        uiSwitchJoystickModeButton = GameObject.Find("SwitchJoystickModeButton");
        GameObject.Find("GameController").GetComponent<GameController>().playerManager.ModeChanged 
            += OnModeChanged;
    }

    private void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Input.acceleration;
    }

    private void Update()
    {
        lowPassValue = LowPassFilterAccelerometer(lowPassValue);

        Vector3 move = new Vector3(lowPassValue.x, 0, lowPassValue.y - tiltAnchorY);

        if (move != Vector3.zero && !isAutoSetupInProgress && rb.velocity.sqrMagnitude <= maxSpeed)
        {
            rb.AddForce(move * 50);
        }

    }

    private void OnModeChanged(PlayerControllerModes mode)
    {
        if (mode == PlayerControllerModes.BallTiltControl)
            StartCoroutine(AutoSetupAnchors());
    }

    private IEnumerator AutoSetupAnchors()
    {
        isAutoSetupInProgress = true;
        autoSetupStateChanged?.Invoke(isAutoSetupInProgress);

        float startTime = Time.time;
        int measureCount = 0;
        float anchorYAccumulated = 0.0f;

        while (Time.time < startTime + autoSetupPeriod)
        {
            measureCount++;
            anchorYAccumulated += lowPassValue.y;

            yield return new WaitForSeconds(0.1f);
        }

        tiltAnchorY = anchorYAccumulated / measureCount;                        

        isAutoSetupInProgress = false;
        autoSetupStateChanged?.Invoke(isAutoSetupInProgress);
    }

    Vector3 LowPassFilterAccelerometer(Vector3 prevValue)
    {
        Vector3 newValue = Vector3.Lerp(prevValue, Input.acceleration, lowPassFilterFactor);
        return newValue;
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.other.name.Contains("Floor"))
        {
            playerSparksController.BurstAtCollision(collision);
        }
    }

}

public delegate void AutoSetupStateChanged(bool state);