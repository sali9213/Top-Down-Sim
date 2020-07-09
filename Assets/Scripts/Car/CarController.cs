using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public InputManager im;
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

        if (CM)
        {
            rb.centerOfMass = CM.localPosition;
        }
    }


    // Called once per frame
    private void FixedUpdate()
    {
        foreach(WheelCollider wheel in throttleWheels)
        {
            if (im.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrength * Time.deltaTime;
            } else
            {
                wheel.motorTorque = strengthCoeffecient * Time.deltaTime * im.throttle;
                wheel.brakeTorque = 0f;
            }
        }

        foreach (GameObject wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, maxTurn * im.steer, 0f);
        }

        //Left and right have been separated because their positive rotation directions are the opposite of eachother.
        foreach (GameObject wheel in rightWheels)
        {
            wheel.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.33f), 0f, 0f);
        }

        foreach (GameObject wheel in leftWheels)
        {
            wheel.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? -1 : 1) / (2 * Mathf.PI * 0.33f), 0f, 0f);
        }


    }
}
