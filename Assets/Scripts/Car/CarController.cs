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
[RequireComponent(typeof(Steering))]
[RequireComponent(typeof(Wheels))]
public class CarController : MonoBehaviour
{
    public InputManager im;
    public Tyres ty;
    public Engine engine;
    public Transmission trans;
    public Aerodynamics aero;
    public Brakes brakes;
    public Differential diff;
    public Steering steer;
    public Wheels wheels;

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
        steer = GetComponent<Steering>();
        wheels = GetComponent<Wheels>();

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
        float engineBrake = engine.GetEngineBrakeTorque();
        brakes.ApplyBrakes(im.brakes, engineBrake);
        steer.ApplySteering(im.steer);
        wheels.ApplyThrottleTorque(wheelTorques[0], wheelTorques[1]); 

        // Apply aero drag
        aero.ApplyDrag();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ShiftUp")) trans.ShiftUp();
        if (Input.GetButtonDown("ShiftDown")) trans.ShiftDown();
    }
}
