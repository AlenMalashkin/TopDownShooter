using UnityEngine;

namespace Code.GameplayLogic
{
    public abstract class Enemy : MonoBehaviour
    {
        private Transform _followTarget;
        public Transform FollowTarget => _followTarget;
        
        public void Init(Transform followTarget)
        {
            _followTarget = followTarget;
        }
    }
}