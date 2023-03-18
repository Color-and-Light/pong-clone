using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class DefaultPuck : MonoBehaviour, IPuck
{
   private Rigidbody2D rb;
 
   private void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   public void Punch()
   {
      float direction = Random.Range(0, 1);
      if (direction >= 0.5)
      {
         rb.velocity += GameManager.instance.puckSpeed;
      }
      else rb.velocity += Vector2.left * GameManager.instance.puckSpeed;
   }
   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("LeftScoreBox"))
      {
         Debug.Log("Score Left !");
      }

      if (col.CompareTag("RightScoreBox"))
      {
         Debug.Log("Score Right!");

      }
   }
}
