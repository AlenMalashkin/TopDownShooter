using System;
using Code.GameplayLogic;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class WeaponData
    {
        public WeaponType Type;
        public WeaponCategory Category;
        public GameObject Prefab;
    }
}