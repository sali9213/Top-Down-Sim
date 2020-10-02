using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Tyres))]
public class CarController : MonoBehaviour
{
    public InputManager im;
    public Tyres ty;
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheels;

    public List<GameObject> leftWheels;
    public List<GameObject> rightWheels;

    public float strengthCoeffecient = 20000f;
    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;
    public float brakeStrength = 200f;

    // Initialisation
    private void Start()
    {
        im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        ty = GetComponent<Tyres>();

        if (CM)
        {
            rb.centerOfMass = CM.localPosition;
        }
    }

    private void FixedUpdate()
    {
        
        foreach(WheelCollider wheel in throttleWheels)
        {
            if (im.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrength;

            } else
            {
                wheel.motorTorque = strengthCoeffecient * Time.deltaTime * im.throttle;
                wheel.brakeTorque = 0f;
            }

            ty.ApplyFriction(wheel);
        }

        foreach (GameObject wheel in steeringWheels)
        {
            if (im.brake)
            {
                wheel.GetComponent<WheelCollider>().brakeTorque = brakeStrength;
            }
            else
            {
                wheel.GetComponent<WheelCollider>().brakeTorque = 0f;
            }

            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, maxTurn * im.steer, 0f);

            ty.ApplyFriction(wheel.GetComponent<WheelCollider>());

        }

        //Left and right have been separated because their positive rotation directions are the opposite of eachother.
        foreach (GameObject wheel in rightWheels)
        {
            wheel.transform.Rotate(wheel.transform.parent.GetComponent<WheelCollider>().rpm / 60 * 360 * Time.deltaTime, 0, 0);
        }

        foreach (GameObject wheel in leftWheels)
        {
            wheel.transform.Rotate(wheel.transform.parent.GetComponent<WheelCollider>().rpm / 60 * -360 * Time.deltaTime, 0, 0);
        }

    }
}
