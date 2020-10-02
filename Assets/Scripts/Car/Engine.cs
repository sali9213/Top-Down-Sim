using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    //Engine properties.
    [SerializeField] private float IDLERpm = 1000f;
    [SerializeField] private float IDLERpmTorque = 275f;
    [SerializeField] private float PeakTorqueRPM = 5200f;
    [SerializeField] private float PeakTorqueRPMTorque = 450f;
    [SerializeField] private float MaxRPM = 7500f;
    [SerializeField] private float MaxRPMTorque = 313f;

    // Should not be serialized (serialized for development purposes only and will be removed later)
    [SerializeField] private float CurrentRPM;
    [SerializeField] private float currentTorque;

    // Gradient
    private float PrePeakTorqueRPM_M;
    // Y-Intercept
    private float PrePeakTorqueRPM_C;

    private float PostPeakTorqueRPM_M;
    private float PostPeakTorqueRPM_C;

    // Start is called before the first frame update
    void Start()
    {
        CalculatePrePeakTorqueLine();
        CalculatePostPeakTorqueLine();
    }

    void FixedUpdate()
    {
        // make sure rpm does not drop below the idle rpm or go above the max rpm.
        if (CurrentRPM < IDLERpm) CurrentRPM = IDLERpm;
        if (CurrentRPM > MaxRPM) CurrentRPM = MaxRPM;
    }

    public float GetTorque(float throttle)
    {
        if(CurrentRPM < PeakTorqueRPM)
        {
            currentTorque =  ((PrePeakTorqueRPM_M * CurrentRPM) + PrePeakTorqueRPM_C) * throttle;
            return currentTorque;
        }
        else
        {
            if (CurrentRPM >= MaxRPM)
            {
                return 0f;
            }
            else
            {
                currentTorque = ((PostPeakTorqueRPM_M * CurrentRPM) + PostPeakTorqueRPM_C) * throttle;
                return currentTorque;
            }
        }

    }

    private void CalculatePrePeakTorqueLine()
    {
        float gradient = (PeakTorqueRPMTorque - IDLERpmTorque) / (PeakTorqueRPM - IDLERpm);
        PrePeakTorqueRPM_M = gradient;
        PrePeakTorqueRPM_C = PeakTorqueRPMTorque - (gradient * PeakTorqueRPM);
    }

    private void CalculatePostPeakTorqueLine()
    {
        float gradient = (MaxRPMTorque - PeakTorqueRPMTorque) / (MaxRPM - PeakTorqueRPM);
        PostPeakTorqueRPM_M = gradient;
        PostPeakTorqueRPM_C = PeakTorqueRPMTorque - (gradient * PeakTorqueRPM);
    }

    public void SetEngineRPM(float wheelRPM)
    {
        wheelRPM = wheelRPM * 3.91f * 3.44f;
        if (wheelRPM < IDLERpm)
        {
            CurrentRPM = IDLERpm;
        }
        else if (wheelRPM > MaxRPM)
        {
            CurrentRPM = MaxRPM;
        } else
        {
            CurrentRPM = wheelRPM;
        }
            
    }
}
