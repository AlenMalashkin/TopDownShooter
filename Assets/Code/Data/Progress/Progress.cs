using System;
using Code.GameplayLogic.Weapons;

namespace Code.Data.Progress
{
    [Serializable]
    public class Progress
    {
        public WeaponType WeaponType = WeaponType.Pistol;
        public int LevelsPassed;
    }
}