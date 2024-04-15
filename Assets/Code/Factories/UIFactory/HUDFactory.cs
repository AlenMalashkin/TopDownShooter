using Code.GameplayLogic;
using Code.Services.AssetProvider;
using Code.UI.HUD;
using UnityEngine;

namespace Code.Factories.UIFactory
{
    public class HUDFactory : IHUDFactory
    {
        private IAssetProvider _assetProvider;
        
        public HUDFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public HealthBar CreateProgressBar(Transform root)
        {
            HealthBar healthBar = _assetProvider.LoadAsset<HealthBar>("Prefabs/HealthBar");
            return Object.Instantiate(healthBar, root);
        }

        public AmmoBar CreateAmmoBar(Transform root)
        {
            AmmoBar ammoBar = _assetProvider.LoadAsset<AmmoBar>("Prefabs/AmmoBar");
            return Object.Instantiate(ammoBar, root);
        }

        public HealthBar CreateBossHealthBar(Transform root, Damageable bossDamageable)
        {
            HealthBar healthBarPrefab = _assetProvider.LoadAsset<HealthBar>("Prefabs/BossHealthBar");
            HealthBar healthBar = Object.Instantiate(healthBarPrefab, root);
            healthBar.Init(bossDamageable);
            return healthBar;
        }
    }
}