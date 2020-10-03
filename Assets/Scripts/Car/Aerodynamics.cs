using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerodynamics : MonoBehaviour
{
    [SerializeField] private float dragCoefficient;
    [SerializeField] private float frontalArea;
    private float dragConstant;
    private Rigidbody rb;

    // Density of air
    private const float rho = 1.29f;

    private void Start()
    {
        // Calculate dragConstant
        dragConstant = CalculateDragConstant();
        rb = GetComponent<Rigidbody>();
    }
    public void ApplyDrag()
    {
        // dragForce = C * v^2
        float dragForce = dragConstant * rb.velocity.magnitude * rb.velocity.magnitude;


        // Flip the drag force if the car is moving in reverse.
        if (rb.velocity.magnitude < 0.0f)
            dragForce = -dragForce;


        // Apply force to rigidbody
        rb.AddRelativeForce(0.0f, 0.0f, -dragForce, ForceMode.Force);
    }

    public float GetDragConstant()
    {
        return dragConstant;
    }

    private float CalculateDragConstant()
    {
        return 0.5f * dragCoefficient * frontalArea * rho;
    }
}
