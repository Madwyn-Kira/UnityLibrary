using Resources;
using System.Collections.Generic;
using UnityEngine;

namespace Upgrade.Example
{
    public class UIUpgradeFiller : MonoBehaviour
    {
        public Transform Content;
        public UpgradeUIItem UpgradeCellPrefab;
        public List<UpgradeParrent> upgradeUIItems = new();
        public ResourceManager resourceManager;

        private void Start()
        {
            foreach (var item in upgradeUIItems)
            {
                var buff = Instantiate(UpgradeCellPrefab, Content);
                buff.Initialize(item, resourceManager);
            }
        }
    }
}