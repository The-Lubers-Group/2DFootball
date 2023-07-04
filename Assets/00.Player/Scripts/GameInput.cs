using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInput : MonoBehaviour
{
    public event EventHandler<OnEventArgs> OnForceActions;
    public class OnEventArgs : EventArgs
    {
        public float currentTime;
    }

    private PlayerInputAction playerInputActions;
    public float currentTime;
    public float TimeBettweenSpawns = 0.25f;

    private void Awake()
    {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.BallForce.performed += Force_performed;
    }

    void Update()
    {
        // Verificar se o botão de force está precionado
        currentTime += Time.deltaTime;
        bool isForceKeyHeld = playerInputActions.Player.BallForce.ReadValue<float>() > 0.1f;
        if (isForceKeyHeld )
        {
            if (currentTime > TimeBettweenSpawns) currentTime = 0;
        }
        
    }

    //Quando o jogador soltar o botão - envia um evento com o valor do tempo que o botão foi pressionado
    private void Force_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnForceActions?.Invoke(this, new OnEventArgs { currentTime  = currentTime });
        currentTime = 0;
    }

    public Vector2 GetArrowRotationNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.ArrowRotation.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
