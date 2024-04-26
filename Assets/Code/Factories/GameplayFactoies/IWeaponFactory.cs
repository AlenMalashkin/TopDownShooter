using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IWeaponFactory : IFactory
    {
        Weapon CreateWeapon(WeaponType type);
        Weapon CreateEnemyWeapon(EnemyWeaponType type);
        Bullet CreateBullet(Vector3 spawnPosition, Bullet bulletPrefab);
    }
}