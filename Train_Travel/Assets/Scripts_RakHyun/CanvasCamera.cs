using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamera : MonoBehaviour
{
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            if (Camera.main != null)
            {
                canvas.worldCamera = Camera.main;
            }
        }
    }
}
