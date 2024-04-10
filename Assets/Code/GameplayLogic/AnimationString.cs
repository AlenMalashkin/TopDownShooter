using System.Collections.Generic;

namespace Code.GameplayLogic
{
    public static class AnimationStrings
    {
        public static Dictionary<WeaponCategory, string> RunWithWeaponAnimationNames = new Dictionary<WeaponCategory, string>()
        {
            [WeaponCategory.Pistol] = "PlayerPistolRun",
            [WeaponCategory.Rifle] = "PlayerRifleRun"
        }; 
        public static Dictionary<WeaponCategory, string> WeaponShootAnimationNames = new Dictionary<WeaponCategory, string>()
        {
            [WeaponCategory.Pistol] = "PistolShoot",
            [WeaponCategory.Rifle] = "RifleShoot"
        };
        
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Attack = "Attack";
        public const string Death = "Death";
        public const string Run = "Run";
        public const string PlayerDetected = "PlayerDetected";
        public const string HitReaction = "HitReaction";
    }
}