using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class DefaultPuck : MonoBehaviour, IPuck
{
   private Rigidbody2D rb;
 
   public void Init()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   public void Punch()
   {
      float direction = Random.Range(0f, 1f);
      if (direction >= 0.5)
      {
         rb.velocity = GameManager.instance.puckSpeed;
      }
      else
      {
         rb.velocity = Vector2.left * GameManager.instance.puckSpeed;
      }
   }
   private void OnTriggerEnter2D(Collider2D col)
   {
      GameManager.instance.scoreCallbacks.Invoke(col.attachedRigidbody);
   }
}
