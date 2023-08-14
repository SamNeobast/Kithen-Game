using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] LayerMask layerCounter;

    private bool isWalking = false;
    private Vector3 lastMoveDir;

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
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
            if (raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }
        private void HandleMovement()
        {
            Vector2 inputVector = gameInput.GetNormalizedMove();

            Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

            float moveDistance = moveSpeed * Time.deltaTime;
            float hightPlayer = 2f;
            float radiusPlayer = 0.7f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * hightPlayer,
                radiusPlayer, moveDir, moveDistance);

            if (!canMove)
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * hightPlayer,
                    radiusPlayer, moveDirX, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirX;
                }
                else
                {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * hightPlayer,
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
        public bool IsWalking()
        {
            return isWalking;
        }
    }
