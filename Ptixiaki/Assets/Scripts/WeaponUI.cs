using Demo.Scripts.Runtime.Character;
using Demo.Scripts.Runtime.Item;
using TMPro;
using UnityEngine;

namespace Demo.Scripts.Runtime.UI
{
    public class WeaponUI : MonoBehaviour
    {
        public TextMeshProUGUI ammoText;
        private FPSController playerController;

        void Start()
        {
            playerController = FindObjectOfType<FPSController>();
        }

        void Update()
        {
            if (playerController == null || playerController.GetActiveItem() == null)
            {
                ammoText.text = "- / -";
                return;
            }

            var weapon = playerController.GetActiveItem();

            if (weapon is Weapon gun) // Assuming your weapon class inherits from FPSItem and is called 'Weapon'
            {
                ammoText.text = $"{gun.currentAmmo} / {gun.magazineSize}";
            }
            else
            {
                ammoText.text = "- / -";
            }
        }
    }
}
