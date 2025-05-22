using DG.Tweening.Core.Easing;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonLook : MonoBehaviour
{
    PlayerInputActions playerInput;
    [SerializeField] private float rotationalSpeedMouse = 10f;
    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private float bottomClamp = -40f;
    [SerializeField] private float topClamp = 70f;

    [SerializeField] Transform followTarget;

    private CinemachineCamera vCam;
    private CinemachineThirdPersonFollow thirdPersonFollowCam;
    private float cinemachineTargetPitch;
    private float cinemachineTargetYaw;

    private Vector2 lookInput;

    private void Awake()
    {
        // Get the Cinemachine components
        vCam = GetComponent<CinemachineCamera>();
        thirdPersonFollowCam = GetComponent<CinemachineThirdPersonFollow>();
        Cursor.lockState = CursorLockMode.Locked;
        playerInput = new PlayerInputActions();

        playerInput.Overworld.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInput.Overworld.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInput.Overworld.Enable();
    }

    private void OnDisable()
    {
        playerInput.Overworld.Disable();
    }

    private void LateUpdate()
    {
        //if (!GameManager.Instance.IsGameRunning)
        //{
        //    return;
        //}
        CameraLogic();
        ToggleCursorMode();
    }

    private void ToggleCursorMode()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    private void CameraLogic()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {

            return;
        }
        if (lookInput.sqrMagnitude >= 0.01f)
        {
            lookInput *= sensitivity * Time.deltaTime;
            cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, lookInput.y, bottomClamp, topClamp, true);
            cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, lookInput.x, float.MinValue, float.MaxValue, false);
        }
        ApplyRotation(cinemachineTargetPitch, cinemachineTargetYaw);
    }

    private void ApplyRotation(float pitch, float yaw)
    {
        //float parentYaw = followTarget.root.eulerAngles.y;
        //float desiredRelativeYaw = Mathf.DeltaAngle(parentYaw, yaw);
        ////float clampedRelativeYaw = Mathf.Clamp(desiredRelativeYaw, -headTurnLimit, headTurnLimit);
        ////float extraYaw = desiredRelativeYaw - clampedRelativeYaw;
        //followTarget.root.Rotate(0, desiredRelativeYaw, 0);
        //parentYaw = followTarget.root.eulerAngles.y;
        //followTarget.rotation = Quaternion.Euler(pitch, parentYaw, 0f);
        followTarget.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);

    }
}
