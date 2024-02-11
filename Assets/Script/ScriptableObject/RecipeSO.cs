using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    
    public List<KithenObjectSO> kithenObjectSOList;
    public string recipeName;
}
