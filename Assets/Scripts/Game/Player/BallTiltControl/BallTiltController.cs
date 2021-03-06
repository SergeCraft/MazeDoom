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

    // accelerometer settings

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    float lowPassKernelWidthInSeconds = 0.05f;

    private float lowPassFilterFactor;
    private Vector3 lowPassValue = Vector3.zero;

    private float tiltAnchorY = -0.6f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

        if (move != Vector3.zero)
        {
            rb.AddForce(move * 50);
        }

    }

    Vector3 LowPassFilterAccelerometer(Vector3 prevValue)
    {
        Vector3 newValue = Vector3.Lerp(prevValue, Input.acceleration, lowPassFilterFactor);
        return newValue;
    }

    public void SetupAnchors()
    {
        tiltAnchorY = lowPassValue.y;
    }
}
