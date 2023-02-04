using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = GetMouseTransformPosition() + offset;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseTransformPosition();
        dragging = true;
    }
    private void OnMouseUp()
    {
        dragging = false;
    }

    private Vector3 GetMouseTransformPosition()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }
}
