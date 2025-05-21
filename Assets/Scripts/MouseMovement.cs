using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensivity = 100;

    float _xRotation = 0;
    float _yRotation = 0;

    public float topClamp = -90;
    public float bottomClamp = 90;
    void Start()
    {
        //Bloquea el cursor en el centro de la pantalla y lo hace invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        //Guarda los inputs del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        //Rotación del eje X (Mirar arriba y abajo)
        _xRotation -= mouseY;

        //Limitar la rotación
        _xRotation = Mathf.Clamp(_xRotation, topClamp, bottomClamp);

        //Rotación del eje Y (Mirar izda y dcha)
        _yRotation += mouseX;

        //Aplica rotaciones al transform
        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}
