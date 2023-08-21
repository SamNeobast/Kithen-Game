using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public Action OnInteractActionE;

    private PlayerInputActions playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interacts.performed += Interacts_performed;
        
    }

    private void Interacts_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractActionE?.Invoke();
    }

    public Vector2 GetNormalizedMove()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
