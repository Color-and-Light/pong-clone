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
         rb.velocity = new Vector2(-1, 1) * GameManager.instance.puckSpeed;
      }
   }
   private void OnTriggerEnter2D(Collider2D col)
   {
      GameManager.instance.scoreCallbacks.Invoke(col.gameObject);
      Destroy(this.gameObject);
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if (col.gameObject.GetComponent<PlayerController>())
      {
         Vector2 normalized = Vector2.Reflect(rb.velocity, col.contacts[0].normal).normalized;
         rb.velocity = normalized * GameManager.instance.puckSpeed;
      }
      else
      {
         Vector2 cache = Vector2.Reflect(rb.velocity, col.contacts[0].normal).normalized;
         if (Vector2.Angle(cache, ) <= 20)
         {
            
         }
      }
   }
}
