using System;
using Code.GameplayLogic;
using Code.GameplayLogic.Weapons;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class WeaponData
    {
        public WeaponType Type;
        public WeaponCategory Category;
        public Bullet Bullet;
        public GameObject Prefab;
    }
}