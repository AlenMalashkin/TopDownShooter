using System.Collections;
using UnityEngine;

namespace Code.GameplayLogic
{
    [RequireComponent(typeof(AnimatorComponent))]
    [RequireComponent(typeof(Damageable))]
    public abstract class Death : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _componentsToDisalbe;
        [SerializeField] private Collider[] _collidersToDisable;
        [SerializeField] private float _timeToDestroy;
        [SerializeField] private Damageable _damageable;
        
        public void SubscribeOnDeath()
            => _damageable.Death += OnDeath;

        public void UnsubscribeOnDeath()
            => _damageable.Death -= OnDeath;
        
        public virtual void OnDeath()
        {
            foreach (var component in _componentsToDisalbe)
                component.enabled = false;

            foreach (var col in _collidersToDisable)
                col.enabled = false;
            
            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(_timeToDestroy);
            Destroy(gameObject);
        }
    }
}