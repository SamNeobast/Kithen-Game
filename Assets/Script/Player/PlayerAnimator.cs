using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player; 

    private Animator playerAnimator;
    private const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        playerAnimator.SetBool(IS_WALKING, player.IsWalking());
    }


}
