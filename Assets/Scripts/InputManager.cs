using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput _playerInput;
    PlayerInput.OnFootActions onFoot;

    PlayerMotor _motor;
    PlayerLook _look;
    void Awake()
    {
        _playerInput = new PlayerInput();
        onFoot = _playerInput.OnFoot;

        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => _motor.Jump();
    }


    void FixedUpdate()
    {
        //Tell the PlayerMotor to move using the value from our movement action
        _motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable(); 
    }
}
