using UnityEngine;

namespace Code.GameplayLogic.EnemiesLogic
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;
        public int Damage => _damage;

        public abstract void ActivateAttack();
        public abstract void DisableAttack();
    }
}