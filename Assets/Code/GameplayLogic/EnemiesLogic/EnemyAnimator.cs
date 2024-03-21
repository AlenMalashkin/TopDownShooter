using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private DetectionZone _detectionZone;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _detectionZone.PlayerDetected += OnPlayerDetected;
            _detectionZone.PlayerLeft += OnPlayerLeft;
        }

        private void OnDisable()
        {
            _detectionZone.PlayerDetected -= OnPlayerDetected;
            _detectionZone.PlayerLeft -= OnPlayerLeft;
        }

        private void OnPlayerDetected()
        {
            _animator.SetBool(AnimationStrings.Attack, true);
        }

        private void OnPlayerLeft()
        {
            _animator.SetBool(AnimationStrings.Attack, false);
        }
    }
}