using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    public float X = 0f;
    public float Y = 10f;
    public float Z = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = new Vector3(X, Y, Z);
        gameObject.transform.LookAt(gameObject.transform.parent.transform);
    }
}
