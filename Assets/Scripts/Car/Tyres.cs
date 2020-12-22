using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyres : MonoBehaviour
{
    [SerializeField] private float frontTyreGrip = 1;
    [SerializeField] private float rearTyreGrip = 1;
    //[SerializeField] private float friction = 1;
    [SerializeField] private float rollingResistanceConstant;

    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] rearWheels;

    private Rigidbody rb;

    private void Update()
    {   
        //if(rearWheels[0].GetGroundHit (out WheelHit hit))
        //{
        //    Debug.Log(hit.forwardSlip);
        //}
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Forward WFC for front wheels
        WheelFrictionCurve frontForwardFriction = new WheelFrictionCurve();
        frontForwardFriction.extremumSlip = 0.15f;
        frontForwardFriction.extremumValue = 1.0f;
        frontForwardFriction.asymptoteSlip = 0.70f;
        frontForwardFriction.asymptoteValue = 0.60f;
        frontForwardFriction.stiffness = frontTyreGrip;

        // Sideways WFC for for front wheels
        WheelFrictionCurve frontSidewaysFriction = new WheelFrictionCurve();
        frontSidewaysFriction.extremumSlip = 0.11f;
        frontSidewaysFriction.extremumValue = 1f;
        frontSidewaysFriction.asymptoteSlip = 0.80f;
        frontSidewaysFriction.asymptoteValue = 0.70f;
        frontSidewaysFriction.stiffness = frontTyreGrip;

        // Forward WFC for rear wheels
        WheelFrictionCurve rearForwardFriction = new WheelFrictionCurve();
        rearForwardFriction.extremumSlip = 0.15f;
        rearForwardFriction.extremumValue = 1.0f;
        rearForwardFriction.asymptoteSlip = 0.7f;
        rearForwardFriction.asymptoteValue = 0.70f;
        rearForwardFriction.stiffness = rearTyreGrip;

        // Sideways WFC for rear wheels
        WheelFrictionCurve rearSidewaysFriction = new WheelFrictionCurve();
        rearSidewaysFriction.extremumSlip = 0.11f;
        rearSidewaysFriction.extremumValue = 1f;
        rearSidewaysFriction.asymptoteSlip = 0.80f;
        rearSidewaysFriction.asymptoteValue = 0.70f;
        rearSidewaysFriction.stiffness = rearTyreGrip;

        foreach (WheelCollider wheel in frontWheels)
        {
            wheel.ConfigureVehicleSubsteps(30, 30, 30);
            wheel.forwardFriction = frontForwardFriction;
            wheel.sidewaysFriction = frontSidewaysFriction;
        }

        foreach(WheelCollider wheel in rearWheels)
        {
            wheel.ConfigureVehicleSubsteps(30, 30, 30);
            wheel.forwardFriction = rearForwardFriction;
            wheel.sidewaysFriction = rearSidewaysFriction;
        }

        StartCoroutine(_wait(1, ApplyRollingResistance));
    }

    // Apply rolling resistance to vehicle rigidbody.
    public void ApplyRollingResistance(/*WheelCollider wheel*/)
    {
        // Rolling resistance is 30 * dragConstant.
        rollingResistanceConstant = gameObject.GetComponent<Aerodynamics>().GetDragConstant() * 30;

        float vel = rb.velocity.magnitude;
        float resistanceForce = rollingResistanceConstant * vel;
        if (vel < 0.0f)
            resistanceForce = -resistanceForce;
        rb.AddRelativeForce(0.0f, 0.0f, -resistanceForce, ForceMode.Force);
    }

    private IEnumerator _wait(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}
