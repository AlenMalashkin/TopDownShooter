using Code.Factories.GameplayFactoies;
using Code.UI.HUD;
using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic.Bosses
{
    public class BossDeath : DeathComponent
    {
        [SerializeField] private WeaponType _dropType;
        [SerializeField] private AnimatorComponent _animator;
        
        private HealthBar _healthBar;
        private IPickupFactory _pickupFactory;
        
        public void Init(HealthBar healthBar, IPickupFactory pickupFactory)
        {
            _healthBar = healthBar;
            _pickupFactory = pickupFactory;
        }
        
        public override void OnDeath(Damageable damageable)
        {
            base.OnDeath(damageable);

            _animator.PlayAnimationByName(AnimationStrings.Death);
            _pickupFactory.CreateWeaponPickup(transform.position, _dropType);
            Destroy(_healthBar.gameObject);
        }
    }
}