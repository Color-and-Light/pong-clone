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
      int initialSlow = 2;
      var _cachedDirection = GameManager.Instance.puckDirection * GameManager.Instance.PuckSpeed;
      
      if (GameManager.Instance.LeftScore > GameManager.Instance.RightScore)
      {
         _rb.velocity = _cachedDirection / initialSlow;
      }
      else if (GameManager.Instance.RightScore > GameManager.Instance.LeftScore)
      {
         _rb.velocity = (new Vector2(-1, 1) * _cachedDirection) / initialSlow;
         return;
      }
      float _direction = Random.Range(0f, 1f);
      if (_direction >= 0.5)
      {
         _rb.velocity = _cachedDirection / initialSlow;
      }
      else
      {
         _rb.velocity = (new Vector2(-1, 1) * _cachedDirection) / initialSlow;
      }
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if (col.gameObject.GetComponent<PlayerController>() != null) //if ball collides with player
      {
         var reference = col.gameObject.GetComponent<PlayerController>();
         GameManager.Instance.BounceCallbacks.Invoke();
         _rb.velocity = new Vector2(_rb.velocity.x * _bounceScalar, _rb.velocity.y);

         if (reference.Direction == PlayerController.PlayerDirection.Left) //slightly different physics applies based on which paddle hits the puck
         {
            if (col.rigidbody.velocity.y < 0) //if paddle is moving downward
            {
               _rb.velocity *= new Vector2(1.2f, 1.2f);
            }
            else //paddle is moving upward
            {
               _rb.velocity *= new Vector2(1.2f, 1.2f);

            }
         }
         else //if not left player, then right player
         {
            if (col.rigidbody.velocity.y < 0) //if paddle is moving downward
            {
               _rb.velocity *= new Vector2(1.2f, 1.2f);
            }
            else //paddle is moving upward
            {
               _rb.velocity *= new Vector2(-1.2f, -1.2f);

            }
         }
      }
      if (col.gameObject.GetComponent<IWall>() != null) //if ball collides with wall
      {
         GameManager.Instance.BounceCallbacks.Invoke();
      }
      if (_rb.velocity.x > GameManager.Instance.MaxHorizontalSpeed)
      {
         _rb.velocity = new Vector2(GameManager.Instance.MaxHorizontalSpeed, _rb.velocity.y);
      }
      else if(col.gameObject.GetComponent<ScoreBoxText>())
      {
         GameManager.Instance.ScoreCallbacks.Invoke(col.gameObject);
         Destroy(this.gameObject);
      }
   }
}
