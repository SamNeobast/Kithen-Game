using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternativeText;
    [SerializeField] private TextMeshProUGUI keyPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebing += GameInput_OnBindingRebing;
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;

        UpdateVisual();

        Show();
    }

    private void GameManager_OnStateChange()
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void OnDisable()
    {
        GameInput.Instance.OnBindingRebing -= GameInput_OnBindingRebing;
        GameManager.Instance.OnStateChange -= GameManager_OnStateChange;
    }


    private void GameInput_OnBindingRebing()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternativeText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternative);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
