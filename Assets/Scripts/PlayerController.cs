using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController _cc;
    
    public float speed = 12;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool _isGrounded;
    bool _isMoving;

    Vector3 lastPosition = new Vector3(0, 0, 0);
    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        //Detecta si esta en el suelo
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Resetea la velocidad por defecto
        if(_isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        //Guarda los inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Crea el vector de movimiento
        Vector3 move = transform.right * x + transform.forward * z;

        //Movimineto actual del player
        _cc.Move(move * speed * Time.deltaTime);

        //Comprueba si el player puede saltar
        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            //Saltando 
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Callendo
        velocity.y += gravity * Time.deltaTime;

        //Ejecutando el salto
        _cc.Move(velocity * Time.deltaTime);

        if(lastPosition != gameObject.transform.position && _isGrounded)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
}
