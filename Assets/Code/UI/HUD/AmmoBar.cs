using System;
using Code.GameplayLogic.Weapons;
using TMPro;
using UnityEngine;

namespace Code.UI.HUD
{
    public class AmmoBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private Weapon _playerWeapon;
        
        public void Init(Weapon playerWeapon)
        {
            _playerWeapon = playerWeapon;
            _playerWeapon.AmmoChanged += SetAmmoValue;
        }

        private void OnDisable()
        {
            _playerWeapon.AmmoChanged-= SetAmmoValue;
        }

        private void SetAmmoValue(int ammoValue)
            => _text.SetText(ammoValue.ToString());
    }
}