using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.GameplayLogic
{
    public class DeathComponent : Death
    {
        [SerializeField] private Collider[] _collidersToDisable;
        [SerializeField] private float _timeToDestroy;

        public override void OnDeath(Damageable damageable)
        {
            foreach (var component in gameObject.GetComponents<MonoBehaviour>())
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