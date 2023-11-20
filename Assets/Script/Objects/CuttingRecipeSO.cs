using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KithenObjectSO input;
    public KithenObjectSO output;
    public int cuttingProgressCountMax;

}
