using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    Wheels wheels;
    WheelCollider[] steeringWheels;
    public float maxTurn = 20f;

    // Start is called before the first frame update
    void Start()
    {
        wheels = gameObject.GetComponent<Wheels>();
        steeringWheels = wheels.GetSteeringWheels();
            
    }

    public void ApplySteering(float input)
    {
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * input;
            wheel.gameObject.transform.localEulerAngles = new Vector3(0f, maxTurn * input, 0f);
        }
    }
}
