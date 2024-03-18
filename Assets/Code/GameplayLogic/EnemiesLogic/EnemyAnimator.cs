using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private bool _hasTarget;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            DetectionZone.PlayerDetected += SetAttackAnimation;
        }

        private void OnDisable()
        {
            DetectionZone.PlayerDetected -= SetAttackAnimation;
        }

        private void SetAttackAnimation()
        {
            _animator.SetTrigger(AnimationStrings.Attack);
        }
    }
}