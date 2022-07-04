using UnityEngine;

public class CharacterAnimation : MonoBehaviour, ICharacterAnimation
{
    private Animator _animator;

    private void Awake() => _animator = GetComponentInChildren<Animator>();

    public void PlayIdleAnimation() => _animator.SetTrigger(AnimationNames.Idle);

    public void PlayAttackAnimation() => _animator.SetTrigger(AnimationNames.Attack);

    public void PlayRunAnimation() => _animator.SetTrigger(AnimationNames.Run);
}