using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject counterGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = counterGameObject.GetComponent<IHasProgress>();

        hasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;

        Hide();
    }

    private void IHasProgress_OnProgressChanged(float progressNormalized)
    {
        barImage.fillAmount = progressNormalized;

        if (progressNormalized == 0 || progressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
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
