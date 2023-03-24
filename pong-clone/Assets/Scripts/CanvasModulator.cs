using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasModulator : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas, readmeCanvas;

    public void Modulate()
    {
        mainCanvas.enabled = !mainCanvas.isActiveAndEnabled;
        readmeCanvas.enabled = !readmeCanvas.isActiveAndEnabled;
    }
}
