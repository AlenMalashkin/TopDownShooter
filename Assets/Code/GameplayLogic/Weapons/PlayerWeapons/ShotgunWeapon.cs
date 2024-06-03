using UnityEngine;

namespace Code.GameplayLogic.Weapons.PlayerWeapons
{
    public class ShotgunWeapon : PlayerWeapon
    {
        [SerializeField] private float _spread;
        [SerializeField] private int _maxBulletsCount;
        [SerializeField] private int _minBulletsCount;

        public override void ShootBullets(Vector3 direction, int damage)
        {
            for (int i = 0; i < Random.Range(_minBulletsCount, _maxBulletsCount); i++)
            {
                Bullet bullet = WeaponFactory.CreateBullet(ShootPoint.position, BulletPrefab);
                bullet.Init(damage,
                    transform.forward + new Vector3(
                        Random.Range(-_spread, _spread), 
                        Random.Range(-_spread, _spread),
                        Random.Range(-_spread, _spread)));
            }
        }
    }
}