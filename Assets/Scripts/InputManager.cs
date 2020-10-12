using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float throttle;
    public float steer;
    public float brakes;

    // Update is called once per frame
    void Update()
    {
        throttle = Input.GetAxis("Throttle");
        brakes = Input.GetAxis("Brakes");
        steer = Input.GetAxis("Horizontal");
    }
}
