using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMulti = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sen")]
    [SerializeField] private float mouseSen = 2.0f;
    [SerializeField] private float upDownRange = 88.0f;

    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerHandleInput inputHandler;
    private Vector3 currentMovement;
    private float verticalRotation;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = GetComponent<PlayerHandleInput>();

    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {

        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMulti : 1f);

        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;

        HandleJumping();
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleJumping()
    {
        if (characterController.isGrounded) 
        { 
            currentMovement.y = -0.5f;

            if (inputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y = gravity * Time.deltaTime;
        }
    }
    void HandleRotation ()
    {
        float mouseXRoration = inputHandler.LookInput.x * mouseSen;
        transform.Rotate(0, mouseXRoration, 0);

        verticalRotation -= inputHandler.LookInput.y * mouseSen;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
