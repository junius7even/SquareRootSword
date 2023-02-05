using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    private float tm = 0;
    private float t = 0.5f;
    // Start is called before the first frame update
    private Camera Cam;
    private Vector3 a;
    private Vector3 b;
    private void Start()
    {
        Cam = Camera.main;
        transform.position = Cam.WorldToScreenPoint(new Vector3(0, -16, 0));
        tm = 0;
        t = 0.5f;
        print("fjf");
    }

    private void Update()
    {
        tm = Mathf.Lerp(t, 1f, t * Time.deltaTime);
        a = Cam.ScreenToWorldPoint(transform.position);
        b = new Vector3(0, 0, 0);
        transform.position = Cam.WorldToScreenPoint(Vector3.Lerp(a, b, tm*Time.deltaTime));
    }
}
