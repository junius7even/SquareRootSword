using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spark : MonoBehaviour
{
    private bool reverse = false;
    float LerpTime = 1.1f;
    public TextMeshProUGUI text;
    float t = 0f;
    int ColorIndex = 0;
    Color[] colors = {new Color(255f, 255f, 255f, 0f), new Color(255f, 255f, 255f, 1f)};
    // Start is called before the first frame update
    void Start()
    {
        text.color = colors[1];
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Lerp(t, 1f, LerpTime * Time.deltaTime);
        text.color = Color.Lerp(text.color, colors[ColorIndex], t * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            text.color = colors[ColorIndex];
            ColorIndex++;
            ColorIndex = (ColorIndex >= colors.Length) ? 0 : ColorIndex;
        }
    }
}
