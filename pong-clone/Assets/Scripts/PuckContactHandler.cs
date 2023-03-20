using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuckContactHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("RightScoreBox")) //string comparison is terrible, but only used twice
        {
            GameManager.instance.scoreCallbacks.Invoke(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        throw new NotImplementedException();
    }
    
    
}
