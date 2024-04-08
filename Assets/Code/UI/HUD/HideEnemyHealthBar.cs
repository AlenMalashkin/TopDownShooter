using System.Collections;
using Code.GameplayLogic;
using UnityEngine;

namespace Code.UI.HUD
{
    public class HideEnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Damageable _damageable;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        private void OnEnable()
        {
            _damageable.HealthChanged += OnHealthChanged;
        }

        private void Start()
        {
            StartHideRoutine();
        }

        private void OnDisable()
        {
            _damageable.HealthChanged -= OnHealthChanged;
        }

        private void StartHideRoutine()
            => StartCoroutine(HideRoutine());

        private IEnumerator HideRoutine()
        {
            yield return new WaitForSeconds(3f);
            if (_canvasGroup.alpha > 0.01f)
            {
                StartCoroutine(SetCanvasAlphaToZeroRoutine());
            }
        }

        private IEnumerator SetCanvasAlphaToZeroRoutine()
        {
            while (_canvasGroup.alpha > 0.01f)
            {
                yield return new WaitForSeconds(0.1f);
                _canvasGroup.alpha -= 0.2f;
            }
        }
        
        private void OnHealthChanged(float prikol)
        {
            _canvasGroup.alpha = 1f;
            StartHideRoutine();
        }
    }
}