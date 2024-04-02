using UnityEngine;

namespace Code.GameplayLogic.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Damageable _damageable;
        [SerializeField] private DeathComponent _deathComponent;
        
        private void OnEnable()
        {
            _damageable.Death += _deathComponent.OnDeath;
        }

        private void OnDisable()
        {
            _damageable.Death -= _deathComponent.OnDeath;
        }
    }
}