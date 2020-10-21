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
[RequireComponent(typeof(Differential))]
public class CarController : MonoBehaviour
{
    public InputManager im;
    public Tyres ty;
    public Engine engine;
    public Transmission trans;
    public Aerodynamics aero;
    public Brakes brakes;
    public Differential diff;

    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;

    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;

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
        diff = GetComponent<Differential>();

        if (CM)
        {
            rb.centerOfMass = CM.localPosition;
        }
    }

    private void FixedUpdate()
    {
        // Set engine rpm according to wheel rpm, gear ratio and final drive ratio.
        engine.SetEngineRPM(trans.GetEngineRPM());
        float engineTorque = engine.GetTorque(im.throttle);
        float transTorque = trans.GetTorque(engineTorque);
        float[] wheelTorques = diff.DiffOutput(transTorque);
        brakes.ApplyBrakes(im.brakes);

        //foreach(WheelCollider wheel in throttleWheels)
        //{
        //    wheel.motorTorque = wheelTorque/throttleWheels.Count;
        //}

        throttleWheels[0].motorTorque = wheelTorques[0];
        throttleWheels[1].motorTorque = wheelTorques[1];

        // Implement engine braking as brake torque to the wheels
        if (im.throttle == 0f)
        {
            float engineBrake = engine.GetEngineBrakeTorque();
            throttleWheels[0].brakeTorque = engineBrake / 2;
            throttleWheels[1].brakeTorque = engineBrake / 2;
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
