using Cinemachine;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using Code.Services;
using UnityEngine;

namespace Code.Factories.GameplayFactoies
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 position);
        GameObject CreateEnemy(Vector3 position);
 RangeEnemyAnimations/MAIN-T-63
        GameObject CreateRangeEnemy(Vector3 position);
  

        Weapon CreatePlayerWeapon();
 master
        Bullet CreateBullet(Vector3 spawnPosition, int damage, Vector3 direction);
        CinemachineVirtualCamera CreatePlayerCamera();
    }
}