using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuckContactHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("RightScoreBox")) //alternatively, use an empty component and check GetComponent null
        {
            GameManager.Instance.ScoreCallbacks.Invoke(col.gameObject);
        }
    }
}
