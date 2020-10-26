using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brakes : MonoBehaviour
{
    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] rearWheels;

    [SerializeField] private float brakeBias = 0.6f;
    // Deceleration in m/s
    [SerializeField] private float maxDecel = 1f;

    private Wheels wheels;

    // Start is called before the first frame update
    void Start()
    {
        wheels = gameObject.GetComponent<Wheels>();
        frontWheels = wheels.GetFrontWheels();
        rearWheels = wheels.GetRearWheels();
    }

    public void ApplyBrakes(float input, float engineTorque)
    {
        float decel = maxDecel * input;
        float carMass = gameObject.GetComponent<Rigidbody>().mass;

        float totalBrakeForce = carMass * decel;
        float frontBrakeForce = totalBrakeForce * brakeBias;
        float rearBrakeForce = totalBrakeForce - frontBrakeForce;

        float frontBrakeTorque = ((frontBrakeForce / 2) * frontWheels[0].radius) + engineTorque / 2;
        float rearBrakeTorque = ((rearBrakeForce / 2) * rearWheels[0].radius) + engineTorque / 2;

        foreach (var wheel in frontWheels)
        {
            wheel.brakeTorque = frontBrakeTorque;
        }

        foreach(var wheel in rearWheels)
        {
            wheel.brakeTorque = rearBrakeTorque;
        }
    }
}
