using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasModulator : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanvas, _readmeCanvas;

    public void Modulate()
    {
        _mainCanvas.enabled = !_mainCanvas.isActiveAndEnabled;
        _readmeCanvas.enabled = !_readmeCanvas.isActiveAndEnabled;
    }
}
