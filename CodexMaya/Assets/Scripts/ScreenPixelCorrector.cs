using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPixelCorrector : MonoBehaviour
{
    //public int pixelWidth = 0;
    private int pixelHeight = 2160;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        //cam.enabled = true;

    }

    public void RearangeScreenSize(int pixelWidth)
    {
        Debug.Log("Rearranging screens");
        float aspectRatio = pixelHeight / pixelWidth;

        cam.orthographicSize = pixelWidth * 0.5f / aspectRatio;
    }
}
