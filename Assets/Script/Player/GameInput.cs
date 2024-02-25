using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";



    public static GameInput Instance { get; private set; }

    public event Action OnPauseAction;
    public event Action OnInteractActionE;
    public event Action OnInteractActionF;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternative,
        Pause
    }

    private PlayerInputActions playerInputAction;

    private void Awake()
    {
        Instance = this;

        playerInputAction = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }



        playerInputAction.Player.Enable();
    }

    private void OnEnable()
    {
        playerInputAction.Player.Interact.performed += Interacts_performed;
        playerInputAction.Player.InteractAlternative.performed += InteractAlternative_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }


    private void OnDisable()
    {
        playerInputAction.Player.Interact.performed -= Interacts_performed;
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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternative:
                return playerInputAction.Player.InteractAlternative.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        }
    }


    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        playerInputAction.Disable();

        InputAction inputAction;
        int bindingIndex;


        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputAction.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternative:
                inputAction = playerInputAction.Player.InteractAlternative;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputAction.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputAction.Enable();
                OnActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputAction.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            }).Start();

    }
}
