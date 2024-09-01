using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Upgrade.Property
{
    [CreateAssetMenu(fileName = "PropertyValue", menuName = "Properties/PropertyValue")]
    public class MainProperty : ScriptableObject
    {
        [Header("Data")]
        public Sprite UpgradeIcon;
        public string UpgradeName;
        public float BaseValue;

        [Header("Upgrades")]
        public List<UpgradeParrent> Upgrades = new();

        public float PropertyValue
        {
            get
            {
                if (!PlayerPrefs.HasKey(UpgradeName + "Value"))
                    return BaseValue;
                else
                    return PlayerPrefs.GetFloat(UpgradeName + "Value");
            }
            private set
            {
                PlayerPrefs.SetFloat(UpgradeName + "Value", value);
            }
        }

        public void SetPropertyValue()
        {
            (float, float) increments = GetIncrements();
            PropertyValue = (BaseValue + increments.Item1) * (1 + increments.Item2 / 100);

            Debug.Log(increments.Item1);
            Debug.Log(increments.Item2);
            Debug.Log(PropertyValue);
        }

        public float GetPropertyValue()
        {
            return PropertyValue;
        }

        public float GetFuturePropertyValue(UpgradeParrent parrent)
        {
            (float, float) increments = GetFurureIncrementByUpgradeType(parrent);
            return (BaseValue + increments.Item1) * (1 + increments.Item2 / 100);
        }

        private (float, float) GetIncrements(bool future = false)
        {
            var additive = Upgrades.Where(item => !item.isMultiplier).Sum(item => item.GetIncrementValue(future));
            var multiplier = Upgrades.Where(item => item.isMultiplier).Sum(item => item.GetIncrementValue(future));
            return (additive, multiplier);
        }

        private (float, float) GetFurureIncrementByUpgradeType(UpgradeParrent parrent)
        {
            (float, float) increment = (0, 0);

            if (parrent.isMultiplier)
                increment = (0, parrent.GetIncrementValue(true));
            else
                increment = (parrent.GetIncrementValue(true), 0);

            return increment;
        }
    }
}