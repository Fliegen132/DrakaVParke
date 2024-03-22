using UnityEngine;

public class AnimationController
{
    private string _currentAnimation;
    private Animator _anim;
    public AnimationController(Animator animator)
    {
        _anim = animator;
    }

    public void SetAnimation(string newAnimation)
    {
        _currentAnimation = newAnimation;
        _anim.Play(_currentAnimation);
        _anim.SetTrigger("Idle");

    }

    public Animator GetAnimator() => _anim;
}
