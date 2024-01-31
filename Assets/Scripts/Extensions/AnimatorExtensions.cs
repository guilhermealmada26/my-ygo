using UnityEngine;

public static class AnimatorExtensions
{
    public static bool IsAnimating(this Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
    }
}