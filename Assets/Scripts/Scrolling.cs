using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float t;
    // Start is called before the first frame update
    private Camera Cam;
    private Vector3 a;
    private Vector3 b;
    private void Start()
    {
        Cam = Camera.main;
        transform.position = Cam.WorldToScreenPoint(new Vector3(0, -16, 0));
        print("fjf");
    }

    private void FixedUpdate()
    {
        a = Cam.ScreenToWorldPoint(transform.position);
        b = new Vector3(0, 0, 0);
        transform.position = Cam.WorldToScreenPoint(Vector3.Lerp(a, b, t));
    }
}
