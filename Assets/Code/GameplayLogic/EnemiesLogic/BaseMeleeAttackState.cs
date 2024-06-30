using System;
using Code.Audio;
using Code.GameplayLogic.PlayerLogic;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class BaseMeleeAttackState : AIState
    {
        [SerializeField] private float _damage = 20;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private AudioClip _punchSound;
        [SerializeField] private AnimatorComponent _meleeEnemyAnimator;
        [SerializeField] private TriggerObserver _fistTriggerObserver;
        [SerializeField] private Collider _fistCollider;
        
        private Transform _target;
        private float _rayLength;

        public void Init(Transform target)
        {
            _target = target;
        }

        public override void EnterState()
        {
            _fistTriggerObserver.TriggerEntered += OnAttack;
            _meleeEnemyAnimator.PlayAnimationByName(AnimationStrings.Attack);
        }

        public override void UpdateState()
        {
            var targetPosition = _target.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

        public override void ExitState()
        {
            _fistTriggerObserver.TriggerEntered -= OnAttack;
        }

        public void EnableFistCollider()
            => _fistCollider.enabled = true;
        
        private void OnAttack(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable) && other.TryGetComponent(out Player player))
            {
                _soundPlayer.PlaySoundEffect(_punchSound);
                damageable.TakeDamage(_damage);
                _fistCollider.enabled = false;
            }
        }
    }
}