using System;
using Code.GameplayLogic.Weapons;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class EnemyWeaponData
    {
        public EnemyWeaponType Type;
        public Bullet Bullet;
        public GameObject Prefab;
    }
}