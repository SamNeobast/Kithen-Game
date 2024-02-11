using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KithenObjectSO input;
    public KithenObjectSO output;
    public float burningTimerMax;
}
