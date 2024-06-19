using Code.Factories.GameplayFactoies;
using Code.GameplayLogic;
using Code.GameplayLogic.EnemiesLogic;
using Code.GameplayLogic.PlayerLogic;
using Code.Services.EnemiesProvider;
using Code.StaticData.TutorialStaticData;
using Code.Tutorial.TutorialWindows;
using UnityEngine;

namespace Code.Tutorial
{
    public class TutorialTriggerZone : TriggerObserver
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Collider _collider;

        private DialogWindow _dialogWindow;
        private IEnemiesProvider _enemiesProvider;
        private IEnemyFactory _enemyFactory;
        private TutorialStaticData _tutorialStaticData;

        public void Init(DialogWindow dialogWindow, IEnemiesProvider enemiesProvider, IEnemyFactory enemyFactory,
            TutorialStaticData tutorialStaticData)
        {
            _dialogWindow = dialogWindow;
            _enemiesProvider = enemiesProvider;
            _enemyFactory = enemyFactory;
            _tutorialStaticData = tutorialStaticData;
        }

        private void OnEnable()
        {
            _triggerObserver.TriggerEntered += OnTriggerEntered;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEntered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _dialogWindow.ShowNextWindow();
                Enemy enemy = _enemyFactory.CreateTutorialEnemy(
                    _tutorialStaticData.TutorialLevel.EnemySpawnMarker.transform.position,
                    _dialogWindow);
                _enemiesProvider.AddEnemy(enemy);
                _collider.enabled = false;
            }
        }
    }
}