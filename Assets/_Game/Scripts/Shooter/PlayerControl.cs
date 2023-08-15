using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    public Transform camera;
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;
    //public float gravity = -9.81f;
    
    
    private float xRotation = 0f;
    //private Vector3 velocity;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        ControlCamera();
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = movementSpeed * Time.deltaTime * (transform.forward * verticalInput + transform.right * horizontalInput);

        characterController.Move(movement);
    }

    public void ControlCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(0f, mouseX, 0f);
    }

    public void GravityControl()
    {
        //velocity.y += gravity * Time.deltaTime;

        //characterController.Move(velocity * Time.deltaTime);
    }


}