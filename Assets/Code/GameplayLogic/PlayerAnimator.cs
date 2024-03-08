using UnityEngine;

namespace Code.GameplayLogic
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        
        private bool _isMoving;
        private bool _isMovingBackwards;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();
        }

        public bool IsMoving
        {
            get => _isMoving;
            private set
            {
                _isMoving = value;
            }
        }

        public bool IsMovingBackwards
        {
            get => _isMovingBackwards;
            private set
            {
                _isMovingBackwards = value;
            }
        }
        
        private void Update()
        {
            IsMoving = _playerMovement.MoveDirection != Vector3.zero;
            IsMovingBackwards = _playerMovement.MoveDirection.z < 0;
            _animator.SetBool(AnimationStrings.IsMoving, IsMoving);
            _animator.SetBool(AnimationStrings.IsMovingBackwards, IsMovingBackwards);
        }
    }
}