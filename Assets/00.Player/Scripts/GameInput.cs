using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnForceActions;
    private PlayerInputAction playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();

        //playerInputActions.Player.Force.performed += Force_performed;

        playerInputActions.Player.Force.started += Force_performed;
    }

    private void Force_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnForceActions?.Invoke(this, EventArgs.Empty);
        Debug.Log($"Action triggered!");
        //Debug.Log(obj);
    }

    public Vector2 GetArrowRotationNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.ArrowRotation.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;

    }

}
