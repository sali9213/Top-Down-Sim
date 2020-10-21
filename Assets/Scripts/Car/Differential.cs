using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Differential : MonoBehaviour
{
    [SerializeField] private float friction = 0.5f;
    [SerializeField] private float preload = 100.0f;
    [SerializeField] private float accelRamp = 1.0f;
    [SerializeField] private float decelRamp = 0.0f;

    [SerializeField] private float leftWheelTorque;
    [SerializeField] private float rightWheelTorque;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float[] DiffOutput(float torque)
    {
        //float LSDClutchTorque = 0.0f;

        //float[] wheelTorques = new float[2];

        //if (torque > 0.0f)
        //    LSDClutchTorque = preload * friction + accelRamp * torque * friction;
        //else
        //    LSDClutchTorque = preload * friction + decelRamp * -torque * friction;

        //WheelCollider[] wheels = gameObject.GetComponent<Wheels>().GetRearWheels();
        //float speeddelta = wheels[0].rpm - wheels[1].rpm;

        //if(speeddelta > 0) //left wheel is faster than right wheel
        //{
        //    wheelTorques[0] = 0.5f * torque - LSDClutchTorque;
        //    wheelTorques[1] = 0.5f * torque + LSDClutchTorque;
        //}
        //else
        //{
        //    wheelTorques[0] = 0.5f * torque + LSDClutchTorque;
        //    wheelTorques[1] = 0.5f * torque - LSDClutchTorque;
        //}

        //leftWheelTorque = wheelTorques[0];
        //rightWheelTorque = wheelTorques[1];
        //wheelTorques[0] = torque / 2;
        //wheelTorques[1] = torque / 2;
        //return wheelTorques;

        // This simulates an open differential for now. I can change it later. 
        float[] wheelTorques = new float[2];
        wheelTorques[0] = torque / 2;
        wheelTorques[1] = torque / 2;
        return wheelTorques;

    }
}
