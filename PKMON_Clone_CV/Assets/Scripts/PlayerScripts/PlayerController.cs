using DG.Tweening;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerAnimationController playerAnimationController;
    public Transform cameraTarget; 
    private PlayerInputActions playerInputActions;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float rotationDuration = 0.15f; // Smooth rotation speed

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 moveDirection;
    private bool isGrounded;
    private bool isRunning;
    private float verticalVelocity;

    private const float RUN_ANIMATION_SPEED = 1f;
    private const float WALK_ANIMATION_SPEED = 0.5f;
    private const float IDLE_ANIMATION_SPEED = 0f;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Overworld.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInputActions.Overworld.Move.canceled += ctx => moveInput = Vector2.zero;
        playerInputActions.Overworld.Jump.performed += ctx => Jump();
        playerInputActions.Overworld.Run.performed += ctx => isRunning = true;
        playerInputActions.Overworld.Run.canceled += ctx => isRunning = false;
    }


    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();
        MovePlayer();
        UpdatePlayerMoveAnimation(moveInput.magnitude * (isRunning ? RUN_ANIMATION_SPEED : WALK_ANIMATION_SPEED));
    }

    private void MovePlayer()
    {
        // Calculate movement direction relative to camera's forward
        Vector3 cameraForward = cameraTarget.forward;
        cameraForward.y = 0f; // Keep movement horizontal
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTarget.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        // Transform input relative to camera
        Vector3 move = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        // Apply movement
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        moveDirection = move * currentSpeed;
        verticalVelocity += gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        // Rotate character to face movement direction (camera-aligned)
        if (move.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.DORotateQuaternion(targetRotation, 0.15f);
        }

        // Move with CharacterController
        controller.Move(moveDirection * Time.deltaTime);
    }
    private void UpdatePlayerMoveAnimation(float targetSpeed)
    {
        // Set animation speed, default to IDLE_ANIMATION_SPEED if no movement
        float animSpeed = moveInput.magnitude > 0 ? targetSpeed : IDLE_ANIMATION_SPEED;
        playerAnimationController.SetPlayerSpeed(animSpeed);

        // Smooth rotation with DOTween
        if (moveInput.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveInput.x, 0, moveInput.y));
            transform.DORotateQuaternion(targetRotation, 0.2f);
        }
    }
    private void CheckIsGrounded()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }
}
