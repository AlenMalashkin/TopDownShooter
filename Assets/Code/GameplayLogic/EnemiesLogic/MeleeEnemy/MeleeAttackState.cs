using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.MeleeEnemy
{
    public class MeleeAttackState : AIState
    {
        [SerializeField] private float _damage = 20;
        [SerializeField] private MeleeEnemyAnimator _meleeEnemyAnimator;
        [SerializeField] private PlayerDetectionZone _playerDetectionZone;
        [SerializeField] private Collider _fistCollider;
        [SerializeField] private Camera _camera;
        
        private Transform _target;
        private float _rayLength;
        

        public void Init(Transform target)
        {
            _target = target;
        }
        
        private void OnEnable()
        {
            _playerDetectionZone.PlayerDetected += OnAttack;
        }

        private void OnDisable()
        {
            _playerDetectionZone.PlayerDetected -= OnAttack;
        }

        public override void EnterState()
        {
            _meleeEnemyAnimator.PlayAttackAnimation();
        }

        public override void UpdateState()
        {
            Ray cameraRay = _camera.ScreenPointToRay(_target.position);
            Plane plane = new Plane(Vector3.up,
                new Vector2(transform.position.x, transform.position.z));
            if(plane.Raycast(cameraRay, out _rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(_rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        public override void ExitState()
        {
        }

        public void EnableFistCollider()
            => _fistCollider.enabled = true;
        
        public void DisableFistCollider()
            => _fistCollider.enabled = false;

        private void OnAttack(Collider other)
        {
            if (other.TryGetComponent(out Damageable damageable))
                damageable.TakeDamage(_damage);
        }
    }
}