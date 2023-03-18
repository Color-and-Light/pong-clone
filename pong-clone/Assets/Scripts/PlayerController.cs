using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    [SerializeField] private playerDirection direction;
    private InputMap map;
    private Rigidbody2D rb;
    private Vector2 moveVector;
    private enum playerDirection
    {
        left,
        right
    }
    private void Awake()
    {
        moveVector = new Vector2(0, GameManager._Instance.MoveSpeed);
        map = new InputMap();
        
        rb = GetComponent<Rigidbody2D>();
        
        if (direction == playerDirection.left)
        {
            map.LeftPaddle.Move.started += OnMove;
            map.LeftPaddle.Move.canceled += OnMoveCancel;
        }

        else
        {
            map.RightPaddle.Move.started += OnMove;
            map.RightPaddle.Move.canceled += OnMoveCancel;
        }

        map.Enable();
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        rb.velocity = moveVector * (int)obj.ReadValue<float>();
    }
    private void OnMoveCancel(InputAction.CallbackContext obj)
    {
        rb.velocity = Vector2.zero;
    }


}
