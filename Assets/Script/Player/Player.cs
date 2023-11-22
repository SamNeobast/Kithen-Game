using System;
using UnityEngine;

public class Player : MonoBehaviour, IKithenObjectParent
{
    public static event Action<BaseCounter> OnSelectedCounterChanged;

    [SerializeField] private GameInput gameInput;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] LayerMask layerCounter;
    [SerializeField] private Transform kithenObjectHoldPoint;


    private bool isWalking = false;
    private Vector3 lastMoveDir;
    private BaseCounter selectedCounter;
    private KithenObject kithenObject;

    private void OnEnable()
    {
        gameInput.OnInteractActionE += OnInteractActions;
        gameInput.OnInteractActionF += GameInput_OnInteractActionAlternative;

    }
    private void OnDestroy()
    {
        gameInput.OnInteractActionE -= OnInteractActions;
        gameInput.OnInteractActionF -= GameInput_OnInteractActionAlternative;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    private void GameInput_OnInteractActionAlternative()
    {
        selectedCounter?.InteractAlternative(this);
    }

    private void OnInteractActions()
    {
        selectedCounter?.Interact(this);
    }



    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetNormalizedMove();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHit, interactDistance, layerCounter))
        {
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                SelectedCounterIs(baseCounter);
            }
            else
            {
                SelectedCounterIs(null);
            }
        }
        else
        {
            SelectedCounterIs(null);
        }

    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetNormalizedMove();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float hightPlayer = 2f;
        float radiusPlayer = 0.7f;
        Vector3 topOfThePlayer = transform.position + Vector3.up * hightPlayer;
        bool canMove = !Physics.CapsuleCast(transform.position, topOfThePlayer,
            radiusPlayer, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position,
                topOfThePlayer, radiusPlayer, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, topOfThePlayer,
                    radiusPlayer, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }

            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private void SelectedCounterIs(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(selectedCounter);

    }


    public bool IsWalking()
    {
        return isWalking;
    }


    public Transform GetTopKithenPointFollowTransform()
    {
        return kithenObjectHoldPoint;
    }

    public void SetKithenObjectParent(KithenObject kithenObject)
    {
        this.kithenObject = kithenObject;
    }

    public KithenObject GetKithenObject()
    {
        return kithenObject;
    }

    public void ClearKithenObjectParent()
    {
        kithenObject = null;
    }

    public bool HasKithenObject()
    {
        return kithenObject != null;
    }
}
