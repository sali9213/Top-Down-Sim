using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Tyres))]
[RequireComponent(typeof(Engine))]
[RequireComponent(typeof(Transmission))]
[RequireComponent(typeof(Aerodynamics))]
[RequireComponent(typeof(Brakes))]
public class CarController : MonoBehaviour
{
    public InputManager im;
    public Tyres ty;
    public Engine engine;
    public Transmission trans;
    public Aerodynamics aero;
    public Brakes brakes;

    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;

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
        engine = GetComponent<Engine>();
        trans = GetComponent<Transmission>();
        aero = GetComponent<Aerodynamics>();
        brakes = GetComponent<Brakes>();

        if (CM)
        {
            rb.centerOfMass = CM.localPosition;
        }
    }

    private void FixedUpdate()
    {
        // Set engine rpm according to wheel rpm, gear ratio and final drive ratio.
        engine.SetEngineRPM(trans.GetEngineRPM(throttleWheels[0].rpm));
        float engineTorque = engine.GetTorque(im.throttle);
        float wheelTorque = trans.GetTorque(engineTorque);
        brakes.ApplyBrakes(im.brakes);

        foreach(WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = wheelTorque/throttleWheels.Count;
        }



        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * im.steer;
            wheel.gameObject.transform.localEulerAngles = new Vector3(0f, maxTurn * im.steer, 0f);

        }

        // Apply aero drag
        aero.ApplyDrag();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ShiftUp")) trans.ShiftUp();
        if (Input.GetButtonDown("ShiftDown")) trans.ShiftDown();
    }
}
