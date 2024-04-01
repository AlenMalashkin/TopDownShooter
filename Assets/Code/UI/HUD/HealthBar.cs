using Code.GameplayLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HUD
{
    public class HealthBar : MonoBehaviour
    {
        private Slider _healthSlider;
        private Damageable _damageable;

        public void Init(Damageable damageable)
        {
            _damageable = damageable;
            _damageable.HealthChanged += OnHealthChanged;
        }

        private void Awake()
        {
            _healthSlider = GetComponent<Slider>();
        }

        private void Start()
        {
            // _damageable.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _damageable.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float newHealth)
        {
            _healthSlider.value = newHealth;
        }
    }
}