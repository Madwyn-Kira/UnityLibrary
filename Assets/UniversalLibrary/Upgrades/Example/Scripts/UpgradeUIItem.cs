using Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrade.Example
{
    public class UpgradeUIItem : MonoBehaviour
    {
        public TMP_Text UpgradeName;
        public TMP_Text UpgradeDescription;
        public Image UpgradeIcon;
        public TMP_Text UpgradeCost;

        private UpgradeParrent _upgradeProperty;
        private ResourceManager _resourceManager;

        public void Initialize(UpgradeParrent upgradeProperty, ResourceManager resourceManager)
        {
            _upgradeProperty = upgradeProperty;
            _resourceManager = resourceManager;
            UpgradeName.text = _upgradeProperty.Parrent.UpgradeName;
            UpgradeIcon.sprite = _upgradeProperty.Parrent.UpgradeIcon;
            updateCostForUI();

            UpgradeDescription.text = $"From {_upgradeProperty.Parrent.GetPropertyValue()} to {_upgradeProperty.Parrent.GetFuturePropertyValue(_upgradeProperty)}";
        }

        public void Upgrade()
        {
            if (_resourceManager.IsCanCharged(_upgradeProperty.GetNextCost()))
            {
                _resourceManager.ChargeResources(_upgradeProperty.GetNextCost());
                _upgradeProperty.Upgrade();

                Refresh();
            }
        }

        public void Refresh()
        {
            updateCostForUI();
            UpgradeDescription.text = $"From {_upgradeProperty.Parrent.GetPropertyValue()} to {_upgradeProperty.Parrent.GetFuturePropertyValue(_upgradeProperty)}";
        }

        private void updateCostForUI()
        {
            if (_upgradeProperty.UpgradeLevel >= _upgradeProperty.MaxUpgradeLevel)
                UpgradeCost.text = "Max";
            else
                UpgradeCost.text = _upgradeProperty.GetNextCost()[0].Cost.ToString();
        }
    }
}