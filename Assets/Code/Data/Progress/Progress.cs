using System;
using System.Collections.Generic;
using Code.GameplayLogic.Weapons;

namespace Code.Data.Progress
{
    [Serializable]
    public class Progress
    {
        public Settings Settings = new Settings();
        public bool TutorialPassed;
        public List<WeaponType> CollectedWeapons = new List<WeaponType> { WeaponType.Pistol };
        public WeaponType WeaponType = WeaponType.Pistol;
        public int LevelsPassed = 1;
    }
}