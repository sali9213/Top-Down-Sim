using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    //Engine properties.
    [SerializeField] private float IDLERpm = 1000f;
    [SerializeField] private float MaxRPM = 7500f;

    // Should not be serialized (serialized for development purposes only and will be removed later)
    [SerializeField] private float CurrentRPM;
    [SerializeField] private float currentTorque;

    [SerializeField] private float[] torqueCurveRPM;
    [SerializeField] private float[] torqueCurveTorque;

    // Start is called before the first frame update
    void Start()
    {
        if (torqueCurveRPM.Length > 0)
        {
            IDLERpm = torqueCurveRPM[0];
            MaxRPM = torqueCurveRPM[torqueCurveRPM.Length - 1];
            CurrentRPM = IDLERpm;
        }
    }

    void Update()
    {
        // make sure rpm does not drop below the idle rpm or go above the max rpm.
        //if (CurrentRPM < IDLERpm) CurrentRPM = IDLERpm;
        //if (CurrentRPM > MaxRPM) CurrentRPM = MaxRPM;
    }

    public float GetTorque(float throttle)
    {
        float immediateTorque = 0f;
        if (throttle < 0f)
            return 0f;

        if (CurrentRPM >= MaxRPM)
        {
            // Wheels shouldnt get any torque if rev limit is being hit.
            return 0f;

        } else
        {
            for(int i = 0; i < torqueCurveRPM.Length; i++)
            {
                if(CurrentRPM == torqueCurveRPM[i])
                {
                    immediateTorque = torqueCurveTorque[i] * throttle;
                    break;
                }

                if(CurrentRPM < torqueCurveRPM[i])
                {
                    // Calculate torque by calculating where the current rpm sits in the rpm array and use that to calculate the torque.

                    float excessRPM = CurrentRPM - torqueCurveRPM[i - 1];
                    float interval = torqueCurveRPM[i] - torqueCurveRPM[i - 1];
                    float excessPercentage = excessRPM / interval;
                    float torqueInterval = torqueCurveTorque[i] - torqueCurveTorque[i-1];

                    immediateTorque = (torqueCurveTorque[i-1] + (torqueInterval * excessPercentage)) * throttle;
                    break;
                }
            }
        }

        currentTorque = immediateTorque;
        return immediateTorque;

    }

    public void SetEngineRPM(float RPM)
    {
        CurrentRPM = RPM;
        if (RPM < IDLERpm)
        {
            CurrentRPM = IDLERpm;
        }
        else if (RPM > MaxRPM)
        {
            CurrentRPM = MaxRPM;
        }
            
    }
}
