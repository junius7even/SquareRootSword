using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private float oldval = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float scalar;

    public void onSliderMove(float Value)
    {
        float diff = Value - oldval;
        transform.position += (new Vector3(0, diff * scalar, 0));
        oldval = Value;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
