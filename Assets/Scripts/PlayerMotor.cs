using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    CharacterController _controller;
    Vector3 _playerVelocity;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    bool _isGrounded;

    bool _crouching = false;
    float _crouchTimer = 1;
    bool _lerpCrouch = false;
    bool _sprinting = false;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = _controller.isGrounded;

        if (_lerpCrouch)
        {
            _crouchTimer += Time.deltaTime;
            float p = _crouchTimer / 1;
            p *= p;
            if (_crouching)
                _controller.height = Mathf.Lerp(_controller.height, 1, p);
            else
                _controller.height = Mathf.Lerp(_controller.height, 2, p);

            if (p > 1)
            {
                _lerpCrouch = false;
                _crouchTimer = 0;
            }
        }
    }

    //Receives the input from the InputManager.cs and apply them to the character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        _controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        _playerVelocity.y += gravity * Time.deltaTime;
        if (_isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;
        _controller.Move(_playerVelocity * Time.deltaTime);
        //Debug.Log(_playerVelocity.y);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
        }
    }

    public void Crouch()
    {
        _crouching = !_crouching;
        _crouchTimer = 0;
        _lerpCrouch = true;
    }

    public void Sprint()
    {
        _sprinting = !_sprinting;
        if (_sprinting )
            speed = 10;
        else
            speed = 5;
    }
}
