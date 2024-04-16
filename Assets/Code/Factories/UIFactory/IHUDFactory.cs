using Code.GameplayLogic;
using Code.UI.HUD;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public interface IHUDFactory : IFactory
    {
        HealthBar CreateProgressBar(Transform root);
        AmmoBar CreateAmmoBar(Transform root);
        HealthBar CreateBossHealthBar(Transform root, Damageable bossDamageable);
    }
}