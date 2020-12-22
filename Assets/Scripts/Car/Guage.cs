using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Guage : MonoBehaviour
{
    [SerializeField] private float maxWidth;
    [SerializeField] private float minWidth;
    [SerializeField] private RawImage tach;
    [SerializeField] private TextMeshProUGUI rpmText;
    [SerializeField] private TextMeshProUGUI speedText;
    private float MaxRPM = 0f;
    private Engine engine;

    // Start is called before the first frame update
    void Start()
    {
        engine = gameObject.GetComponent<Engine>();
        MaxRPM = engine.getMaxRPM();
    }

    // Update is called once per frame
    void Update()
    {
        if (engine.CurrentRPM == MaxRPM)
        {
            tach.color = Color.red;
        } else if (engine.CurrentRPM > MaxRPM - 200)
        {
            tach.color = Color.green;
        } else
        {
            tach.color = Color.white;
        }

        float width = (engine.CurrentRPM / engine.getMaxRPM()) * maxWidth;
        tach.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rpmText.text = engine.CurrentRPM.ToString("n0");
        speedText.text = GetSpeed();
    }

    string GetSpeed()
    {
        float velocity = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        float speed = (velocity / 1000) * 3600;
        return speed.ToString("n0");
    }
}
