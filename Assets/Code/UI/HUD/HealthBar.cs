using System;
using Code.GameplayLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HUD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Damageable _damageable;
        
        private Slider _healthSlider;
            
        public void Init(Damageable damageable)
        {
            _damageable = damageable;
            _damageable.HealthChanged += OnHealthChanged;
        }

        public virtual void Awake()
        {
            _healthSlider = GetComponent<Slider>();
            
        }

        private void Start()
        {
            _healthSlider.maxValue = _damageable.MaxHealth;
            _healthSlider.value = _damageable.MaxHealth;
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