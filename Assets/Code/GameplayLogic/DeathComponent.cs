using System.Collections;
using UnityEngine;

namespace Code.GameplayLogic
{
    public abstract class DeathComponent : Death
    {
        [SerializeField] private MonoBehaviour[] _componentsToDisalbe;
        [SerializeField] private Collider[] _collidersToDisable;
        [SerializeField] private float _timeToDestroy;
        [SerializeField] private Damageable _damageable;

        public override void OnDeath()
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