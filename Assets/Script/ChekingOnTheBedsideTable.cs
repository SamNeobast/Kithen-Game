using UnityEngine;

public class ChekingOnTheBedsideTable : MonoBehaviour
{

    [SerializeField] private GameInput gameInput;

    private Vector3 lastInteractDir;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {
        Check();
    }

    private void Check()
    {
        Vector2 inputVector = gameInput.GetMovedVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float maxDistance = 2f;
        
        if (Physics.Raycast(transform.position, lastInteractDir, maxDistance, layerMask))
        {
            Debug.Log("Interact");
        }
        else
        {
            Debug.Log("-");
        }

    }
}
