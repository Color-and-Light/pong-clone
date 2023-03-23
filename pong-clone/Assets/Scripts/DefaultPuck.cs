using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class DefaultPuck : MonoBehaviour, IPuck
{
   [SerializeField, Range(0, 5)] private float bounceScalar; 
   private Rigidbody2D rb;
 
   public void Init()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   public void Punch()
   {
      float direction = Random.Range(0f, 1f);
      if (direction >= 0.5 || GameManager.instance.leftScore > GameManager.instance.rightScore)
      {
         rb.velocity = GameManager.instance.puckDirection * GameManager.instance.puckSpeedScalar;
      }
      else
      {
         rb.velocity = new Vector2(-1, 1) * GameManager.instance.puckDirection * GameManager.instance.puckSpeedScalar;
      }
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if(col.gameObject.GetComponent<IWall>() != null || col.gameObject.GetComponent<PlayerController>() != null)
      {
         GameManager.instance.bounceCallbacks.Invoke();
         rb.velocity = new Vector2(rb.velocity.x * bounceScalar, rb.velocity.y); 
      }
      else if(col.gameObject.GetComponent<ScoreBoxText>())
      {
         GameManager.instance.scoreCallbacks.Invoke(col.gameObject);
         Destroy(this.gameObject);
      }
   }

   
}
