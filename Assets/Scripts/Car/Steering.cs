using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    Wheels wheels;
    public float maxTurn = 20f;

    // Start is called before the first frame update
    void Start()
    {
        wheels = gameObject.GetComponent<Wheels>();
            
    }

    public void ApplySteering(float input)
    {
        for(int i = 0; i < 2; i++)
        {
            wheels.WC[i].steerAngle = maxTurn * input;
            wheels.WC[i].gameObject.transform.localEulerAngles = new Vector3(0f, maxTurn * input, 0f);
        }
    }
}
