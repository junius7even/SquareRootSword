using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIndicator : MonoBehaviour
{
    private Vector3 startPos, endPos;

    private Camera sceneCamera;

    private Vector3 camOffset = new Vector3(0, 0, 10);

    private LineRenderer lr;
    private Color c1 = Color.white;
    private Color c2 = Color.white;

    [SerializeField] private AnimationCurve ac;
    // Start is called before the first frame update
    void Start()
    {
        sceneCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.startColor = c1;
                lr.endColor = c2;
                lr.startWidth = 1;
                lr.endWidth = 5;
                lr.sortingLayerName = "UI";
            }
            
            lr.enabled = true;
            lr.positionCount = 2;
            startPos = sceneCamera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
            lr.widthCurve = ac;
            lr.numCapVertices = 10;
        }

        if (Input.GetMouseButton(0))
        {
            endPos = sceneCamera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            lr.SetPosition(1, endPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
        }
    }
}
