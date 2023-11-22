using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KithenObjectSO input;
    public KithenObjectSO output;
    public float fryingTimerMax;
}
