using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wc;
    private List<int> drivenWheels = new List<int>();

    public enum dr
    {
        FWD,
        RWD,
        FOURWD
    }

    [SerializeField] private dr drivetrain = dr.RWD;
    [SerializeField] float wheelRadiusFrontInches = 12.9921f;
    [SerializeField] float wheelRadiusRearInches = 12.9921f;


    public List<int> DrivenWheels { get { return drivenWheels; } }
    public WheelCollider[] WC { get { return wc; } }
    public dr Drivetrain { get { return drivetrain; } }
    public float FrontRadius {  get { return wc[0].radius; } }
    public float RearRadius { get { return wc[2].radius; } }



    // Start is called before the first frame update
    void Start()
    {
        wc = GetComponentsInChildren<WheelCollider>();

        if (drivetrain == dr.FWD || drivetrain == dr.FOURWD)
        {
            drivenWheels.Add(0);
            drivenWheels.Add(1);
        }
        if (drivetrain == dr.RWD || drivetrain == dr.FOURWD)
        {
            drivenWheels.Add(2);
            drivenWheels.Add(3);

        }

        for (int i = 0; i < wc.Length; i++)
        {
            if (i <= 1)
            {
                wc[i].radius = inchesToM(wheelRadiusFrontInches);
            } else
            {
                wc[i].radius = inchesToM(wheelRadiusRearInches);
            }
        }

    }

    // Update is called once per frame
    private float inchesToM(float inches)
    {
        return (inches * 2.54f) / 100;
    }

    public void ApplyThrottleTorque(float[] torques)
    {
        for(int i = 0; i < torques.Length; i++)
        {
            wc[i].motorTorque = torques[i];
        }
    }
}
