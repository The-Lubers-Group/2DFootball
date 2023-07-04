using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnForceStarted;
    public event EventHandler OnForcePerformed;

    private PlayerInputAction playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
    }

    void Update()
    {
        if (playerInputActions.Player.BallForce.IsPressed())
        {
            Force_started();
        }
        if (playerInputActions.Player.BallForce.WasReleasedThisFrame() )
        {
            Force_performed();
        }
    }

    //Quando o jogador clicar no bot�o 
    private void Force_started()
    {
        OnForceStarted?.Invoke(this, EventArgs.Empty);
    }

    //Quando o jogador soltar o bot�o - Envia o tempo que o bot�o ficou pressionado
    private void Force_performed()
    {
        OnForcePerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetArrowRotationNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.ArrowRotation.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
