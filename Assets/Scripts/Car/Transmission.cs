using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour
{
    [SerializeField] private float firstGearRatio;
    [SerializeField] private float secondGearRatio;
    [SerializeField] private float thirdGearRatio;
    [SerializeField] private float fourthGearRatio;
    [SerializeField] private float fifthGearRatio;
    [SerializeField] private float sixthGearRatio;
    [SerializeField] private float seventhGearRatio;
    [SerializeField] private float eighthGearRatio;
    [SerializeField] private float finalDrive;
    [SerializeField] private float transmissionEffeciency = 1f;

    private float[] gearRatios = new float[9];
    [SerializeField] public int CurrentGear = 1;
    // Start is called before the first frame update
    void Start()
    {
        gearRatios[0] = 0;
        gearRatios[1] = firstGearRatio;
        gearRatios[2] = secondGearRatio;
        gearRatios[3] = thirdGearRatio;
        gearRatios[4] = fourthGearRatio;
        gearRatios[5] = fifthGearRatio;
        gearRatios[6] = sixthGearRatio;
        gearRatios[7] = seventhGearRatio;
        gearRatios[8] = eighthGearRatio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftUp()
    {
        // Shift up only if there is a valid next gear.
        if(CurrentGear < gearRatios.Length - 1 && gearRatios[CurrentGear + 1] != 0f)
            CurrentGear++;
    }

    public void ShiftDown()
    {
        if(CurrentGear > 1)
            CurrentGear--;
    }

    public float GetTorque(float engineTorque)
    {
        return engineTorque * gearRatios[CurrentGear] * finalDrive * transmissionEffeciency;
    }

    public float GetEngineRPM()
    {
        WheelCollider[] wheels = GetComponent<Wheels>().GetRearWheels();
        float wheelRPM = wheels[0].rpm > wheels[1].rpm ? wheels[0].rpm : wheels[1].rpm;
        return wheelRPM * gearRatios[CurrentGear] * finalDrive;
    }
}
