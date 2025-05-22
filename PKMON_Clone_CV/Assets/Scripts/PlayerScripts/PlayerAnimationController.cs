using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");

    public void SetPlayerSpeed(float speed)
    {
        animator.SetFloat(MoveSpeedHash, speed);
    }
}
