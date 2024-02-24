using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event Action OnPauseAction;
    public event Action OnInteractActionE;
    public event Action OnInteractActionF;


    private PlayerInputActions playerInputAction;

    private void Awake()
    {
        Instance = this;

        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();        
    }

    private void OnEnable()
    {
        playerInputAction.Player.Interacts.performed += Interacts_performed;
        playerInputAction.Player.InteractAlternative.performed += InteractAlternative_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }


    private void OnDisable()
    {
        playerInputAction.Player.Interacts.performed -= Interacts_performed;
        playerInputAction.Player.InteractAlternative.performed -= InteractAlternative_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke();
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
