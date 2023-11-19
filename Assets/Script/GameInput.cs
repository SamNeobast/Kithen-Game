using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event Action OnInteractActionE;
    public event Action OnInteractActionF;

    private PlayerInputActions playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interacts.performed += Interacts_performed;
        playerInputAction.Player.InteractAlternative.performed += InteractAlternative_performed;
        
    }

    private void InteractAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractActionF?.Invoke();
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
