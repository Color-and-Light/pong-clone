using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour

{
    [SerializeField] private PlayerDirection _direction;
    private InputMap _map;
    private Rigidbody2D _rb;
    private Vector2 _moveVector;
    private enum PlayerDirection
    {
        Left,
        right
    }
    private void Awake()
    {
        _moveVector = new Vector2(0, GameManager.Instance.MoveSpeed);
        _map = new InputMap();
        
        _rb = GetComponent<Rigidbody2D>();
        
        if (_direction == PlayerDirection.Left)
        {
            _map.LeftPaddle.Move.started += OnMove;
            _map.LeftPaddle.Move.canceled += OnMoveCancel;
        }
        else
        {
            _map.RightPaddle.Move.started += OnMove;
            _map.RightPaddle.Move.canceled += OnMoveCancel;
        }

        _map.LeftPaddle.Pause.started += OnPause;
        _map.Enable();
        Cursor.visible = false;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        _rb.velocity = _moveVector * (int)obj.ReadValue<float>();
    }
    private void OnMoveCancel(InputAction.CallbackContext obj) => _rb.velocity = Vector2.zero;

    private void OnPause(InputAction.CallbackContext context) => GameManager.Instance.OnGamePause();
}
