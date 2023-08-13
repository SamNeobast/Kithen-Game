using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 10f;
    private bool isWalking = false;

    [SerializeField] private GameInput gameInput;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputVector = gameInput.GetMovedVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //Не може рухатись по moveDir



            //Спроба руху по Х
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
           playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Може рухатись по X
                moveDir = moveDirX;
            }
            else
        {
                //Не може рухатись по Х


                //Спроба по Z
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
           playerRadius, moveDirZ, moveDistance);
            if (canMove)
            {
                    //Може рухатись по Z
                moveDir = moveDirZ;
            }else
                {
                    //Не може рухатись в дані напрямки
                }
        }
        }
        
        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        IsWalking();
        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);//Поворот в сторону напрямку

    }

    public bool IsWalking()
    {
        return isWalking;
    }


}