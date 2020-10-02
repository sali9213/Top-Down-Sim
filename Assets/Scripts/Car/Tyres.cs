using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyres : MonoBehaviour
{
    [SerializeField] private float frontTyreGrip = 1;
    [SerializeField] private float rearTyreGrip = 1;
    [SerializeField] private float friction = 1;

    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] rearWheels;
   
    void Start()
    {
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
        frontSidewaysFriction.extremumValue = 1.0f;
        frontSidewaysFriction.asymptoteSlip = 0.80f;
        frontSidewaysFriction.asymptoteValue = 0.70f;
        frontSidewaysFriction.stiffness = frontTyreGrip;

        // Forward WFC for rear wheels
        WheelFrictionCurve rearForwardFriction = new WheelFrictionCurve();
        rearForwardFriction.extremumSlip = 0.25f;
        rearForwardFriction.extremumValue = 1.0f;
        rearForwardFriction.asymptoteSlip = 0.70f;
        rearForwardFriction.asymptoteValue = 0.70f;
        rearForwardFriction.stiffness = rearTyreGrip;

        // Sideways WFC for rear wheels
        WheelFrictionCurve rearSidewaysFriction = new WheelFrictionCurve();
        rearSidewaysFriction.extremumSlip = 0.11f;
        rearSidewaysFriction.extremumValue = 1.0f;
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
        
    }

    public void ApplyFriction(WheelCollider wheel)
    {
        if(Mathf.Abs(wheel.rpm) > 100)
        {
            wheel.brakeTorque += friction;
        }
    }
}
