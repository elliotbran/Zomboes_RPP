using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnim;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool _grounded;

    public Transform orientation;

    float _horizontalInput;
    float _verticalInput;

    public int maxHealth = 60;
    public int currentHealth;

    Vector3 _moveDirection;
    public GameObject deadscreen;

    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        readyToJump = true;

        currentHealth = maxHealth;
    }

    
    void Update()
    {
        // ground check
        _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedControl();

        // handle drag
        if ( _grounded )
            _rb.drag = groundDrag;
        else
            _rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && _grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown); // salto tipo minecraft. creo ¿?
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player muerto");
        Time.timeScale = 0;
        deadscreen.SetActive(true);
    }

    void MovePlayer()
    {
        //calculate movement direction
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        playerAnim.SetFloat("Speed", _moveDirection.magnitude);

        // on ground
        if (_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        // reset Y velocity
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}
