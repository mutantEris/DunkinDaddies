using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_rendering : MonoBehaviour
{
    public LineRenderer myLineRenderer;
    public GameObject attachpoint1;
    public GameObject attachpoint2;
    public Color line_color_start;
    public Color line_color_end;
    void Start()
    {
        myLineRenderer.positionCount = 2;
        myLineRenderer.startColor = line_color_start;
        myLineRenderer.endColor = line_color_end;

    }

    // Update is called once per frame
    void Update()
    {
        myLineRenderer.SetPosition(0, attachpoint1.transform.position);
        myLineRenderer.SetPosition(1, attachpoint2.transform.position);
    }
}
