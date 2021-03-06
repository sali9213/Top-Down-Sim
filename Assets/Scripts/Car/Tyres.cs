﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyres : MonoBehaviour
{
    [SerializeField] private float frontTyreGrip = 1;
    [SerializeField] private float rearTyreGrip = 1;
    [SerializeField] private float friction = 1;
    [SerializeField] private float rollingResistanceConstant;

    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] rearWheels;

    private Rigidbody rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Forward WFC for front wheels
        WheelFrictionCurve frontForwardFriction = new WheelFrictionCurve();
        frontForwardFriction.extremumSlip = 1.0f;
        frontForwardFriction.extremumValue = 2.0f;
        frontForwardFriction.asymptoteSlip = 0.70f;
        frontForwardFriction.asymptoteValue = 0.60f;
        frontForwardFriction.stiffness = frontTyreGrip;

        // Sideways WFC for for front wheels
        WheelFrictionCurve frontSidewaysFriction = new WheelFrictionCurve();
        frontSidewaysFriction.extremumSlip = 2.0f;
        frontSidewaysFriction.extremumValue = 4.0f;
        frontSidewaysFriction.asymptoteSlip = 0.80f;
        frontSidewaysFriction.asymptoteValue = 0.70f;
        frontSidewaysFriction.stiffness = frontTyreGrip;

        // Forward WFC for rear wheels
        WheelFrictionCurve rearForwardFriction = new WheelFrictionCurve();
        rearForwardFriction.extremumSlip = 1.0f;
        rearForwardFriction.extremumValue = 2.0f;
        rearForwardFriction.asymptoteSlip = 0.70f;
        rearForwardFriction.asymptoteValue = 0.70f;
        rearForwardFriction.stiffness = rearTyreGrip;

        // Sideways WFC for rear wheels
        WheelFrictionCurve rearSidewaysFriction = new WheelFrictionCurve();
        rearSidewaysFriction.extremumSlip = 2.0f;
        rearSidewaysFriction.extremumValue = 4.0f;
        rearSidewaysFriction.asymptoteSlip = 0.80f;
        rearSidewaysFriction.asymptoteValue = 0.70f;
        rearSidewaysFriction.stiffness = rearTyreGrip;

        foreach (WheelCollider wheel in frontWheels)
        {
            wheel.ConfigureVehicleSubsteps(30, 8, 20);
            wheel.forwardFriction = frontForwardFriction;
            wheel.sidewaysFriction = frontSidewaysFriction;
        }

        foreach(WheelCollider wheel in rearWheels)
        {
            wheel.ConfigureVehicleSubsteps(30, 8, 20);
            wheel.forwardFriction = rearForwardFriction;
            wheel.sidewaysFriction = rearSidewaysFriction;
        }

        new WaitForSeconds(1);

        // Rolling resistance is 30 * dragConstant.
        rollingResistanceConstant = gameObject.GetComponent<Aerodynamics>().GetDragConstant() * 30;
    }

    // Apply rolling resistance to vehicle rigidbody.
    public void ApplyRollingResistance(/*WheelCollider wheel*/)
    {
        float vel = rb.velocity.magnitude;
        float resistanceForce = rollingResistanceConstant * vel;
        if (vel < 0.0f)
            resistanceForce = -resistanceForce;
        rb.AddRelativeForce(0.0f, 0.0f, -resistanceForce, ForceMode.Force);
    }
}
