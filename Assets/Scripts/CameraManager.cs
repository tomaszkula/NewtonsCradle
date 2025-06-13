using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 1f;

    [Header("References")]
    [SerializeField] private PlayerInput playerInput = null;

    private bool isRotating = false;

    private InputAction secondAttackAction = null;

    private void Awake()
    {
        secondAttackAction = playerInput.actions["SecondAttack"];
    }

    private void OnEnable()
    {
        secondAttackAction.started += ctx => OnSecondAttackActionStarted();
        secondAttackAction.canceled += ctx => OnSecondAttackActionCanceled();
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void OnDisable()
    {
        secondAttackAction.started -= ctx => OnSecondAttackActionStarted();
        secondAttackAction.canceled -= ctx => OnSecondAttackActionCanceled();
    }

    private void OnSecondAttackActionStarted()
    {
        isRotating = true;
    }

    private void OnSecondAttackActionCanceled()
    {
        isRotating = false;
    }

    private void RotateCamera()
    {
        if (!isRotating)
            return;

        float xRotation = speed * Input.mousePositionDelta.x;
        transform.Rotate(Vector3.up, xRotation);
    }
}
