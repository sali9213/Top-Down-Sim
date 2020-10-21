using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlipLogger : MonoBehaviour
{
    [SerializeField] List<WheelCollider> Wheels;
    [SerializeField] Text FL_Data;
    [SerializeField] Text FR_Data;
    [SerializeField] Text BL_Data;
    [SerializeField] Text BR_Data;

    // Update is called once per frame
    void Update()
    {
        foreach (WheelCollider wheel in Wheels)
        {
            switch (wheel.gameObject.name)
            {
                case "FL":
                    FL_Data.text = GetSlipValues(wheel);
                    break;
                case "FR":
                    FR_Data.text = GetSlipValues(wheel);
                    break;
                case "BL":
                    BL_Data.text = GetSlipValues(wheel);
                    break;
                case "BR":
                    BR_Data.text = GetSlipValues(wheel);
                    break;

                default:
                    break;
            }
        }
    }

    string GetSlipValues(WheelCollider wheel)
    {
        string slipValues = "";
        if (wheel.GetGroundHit(out WheelHit hit))
        {
            slipValues = " " + hit.forwardSlip.ToString("n2") + " " + wheel.brakeTorque.ToString("n0") + " " + wheel.motorTorque.ToString("n0") + " " +  GetSpeed();
        }
        return slipValues;

    }

    string GetSpeed()
    {
        float velocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        float speed = (velocity / 1000) * 3600;
        return speed.ToString("n0");
    }
}
