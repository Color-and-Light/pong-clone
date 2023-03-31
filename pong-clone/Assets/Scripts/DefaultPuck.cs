using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class DefaultPuck : MonoBehaviour, IPuck
{
   [SerializeField, Range(0, 5)] private float _bounceScalar, _horizontalForce, _verticalForce, 
      _horizontalScalarForce, _verticalScalarForce;
   private Rigidbody2D _rb;
 
   public void Init()
   {
      _rb = GetComponent<Rigidbody2D>();
   }

   public void Punch()
   {
      var _cachedDirection = GameManager.Instance.puckDirection * GameManager.Instance.PuckSpeed;
      
      if (GameManager.Instance.LeftScore > GameManager.Instance.RightScore)
      {
         _rb.velocity = _cachedDirection;
      }
      else if (GameManager.Instance.RightScore > GameManager.Instance.LeftScore)
      {
         _rb.velocity = new Vector2(-1, 1) * _cachedDirection;
         return;
      }
      float _direction = Random.Range(0f, 1f);
      if (_direction >= 0.5)
      {
         _rb.velocity = _cachedDirection;
      }
      else
      {
         _rb.velocity = new Vector2(-1, 1) * _cachedDirection;
      }
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if(col.gameObject.GetComponent<IWall>() != null || col.gameObject.GetComponent<PlayerController>() != null)
      {
         /*GameManager.Instance.BounceCallbacks.Invoke();
         _rb.velocity = new Vector2(_rb.velocity.x * _bounceScalar, _rb.velocity.y);
         if (_rb.velocity.x > GameManager.Instance.MaxHorizontalSpeed)
         {
            _rb.velocity = new Vector2(GameManager.Instance.MaxHorizontalSpeed, _rb.velocity.y);
         }*/
         GameManager.Instance.BounceCallbacks.Invoke();
         int randomForce = Random.Range(0, 2);
         if (randomForce == 0)
         {
            _rb.AddForce(new Vector2(_horizontalForce / _horizontalScalarForce, _verticalForce / _verticalScalarForce));
         }
         else
         {
            _rb.AddForce(new Vector2(_horizontalForce / _horizontalScalarForce, -_verticalForce / _horizontalScalarForce));
         }

      }
      else if(col.gameObject.GetComponent<ScoreBoxText>())
      {
         GameManager.Instance.ScoreCallbacks.Invoke(col.gameObject);
         Destroy(this.gameObject);
      }
   }

   
}
