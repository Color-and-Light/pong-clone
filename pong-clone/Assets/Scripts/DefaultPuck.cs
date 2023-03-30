using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class DefaultPuck : MonoBehaviour, IPuck
{
   [SerializeField, Range(0, 5)] private float _bounceScalar; 
   private Rigidbody2D _rb;
 
   public void Init()
   {
      _rb = GetComponent<Rigidbody2D>();
   }

   public void Punch()
   {
      var _cachedDirection = GameManager.Instance.puckDirection * GameManager.Instance.PuckSpeedScalar;
      
      if (GameManager.Instance.LeftScore > GameManager.Instance.RightScore)
      {
         _rb.velocity = new Vector2(-1, 1) * _cachedDirection;
      }
      else if (GameManager.Instance.RightScore > GameManager.Instance.LeftScore)
      {
         _rb.velocity = _cachedDirection;
         return;
      }
      float _direction = Random.Range(0f, 1f);
      if (_direction >= 0.5)
      {
         _rb.velocity = _cachedDirection;
      }
      else
      {
         _rb.velocity = new Vector2(-1, 1) * GameManager.Instance.puckDirection * GameManager.Instance.PuckSpeedScalar;
      }
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if(col.gameObject.GetComponent<IWall>() != null || col.gameObject.GetComponent<PlayerController>() != null)
      {
         GameManager.Instance.BounceCallbacks.Invoke();
         _rb.velocity = new Vector2(_rb.velocity.x * _bounceScalar, _rb.velocity.y); 
      }
      else if(col.gameObject.GetComponent<ScoreBoxText>())
      {
         GameManager.Instance.ScoreCallbacks.Invoke(col.gameObject);
         Destroy(this.gameObject);
      }
   }

   
}
