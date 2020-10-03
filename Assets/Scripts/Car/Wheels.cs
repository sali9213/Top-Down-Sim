﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    [SerializeField] WheelCollider[] frontWheels;
    [SerializeField] WheelCollider[] rearWheels;

    [SerializeField] float wheelRadiusFrontInches = 12.9921f;
    [SerializeField] float wheelRadiusRearInches = 12.9921f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(WheelCollider wheel in frontWheels)
        {
            wheel.radius = inchesToM(wheelRadiusFrontInches);
        }

        foreach(WheelCollider wheel in rearWheels)
        {
            wheel.radius = inchesToM(wheelRadiusRearInches);
        }
    }

    // Update is called once per frame
    private float inchesToM(float inches)
    {
        return (inches * 2.54f) / 100;
    }
}