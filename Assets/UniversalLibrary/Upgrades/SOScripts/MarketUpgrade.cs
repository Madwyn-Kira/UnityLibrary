using Resources;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade.Child
{
    [CreateAssetMenu(fileName = "MarketUpgrade", menuName = "Upgrades/MarketUpgrade")]
    public class MarketUpgrade : UpgradeParrent
    {
        public delegate void OnChange();
        public event OnChange OnChanged;

        public override void Upgrade()
        {
            if (UpgradeLevel >= MaxUpgradeLevel)
                return;

            UpgradeLevel++;
            Parrent.SetPropertyValue();

            OnChanged?.Invoke();
        }

        public override float GetIncrementValue(bool future = false)
        {
            if (future)
                return Formula.GetValue(UpgradeLevel + 1);
            else
                return Formula.GetValue(UpgradeLevel);
        }

        public override float GetPropertyValue(bool future = false)
        {
            if (future)
                return 0;
            else
                return 0;
        }
    }
}