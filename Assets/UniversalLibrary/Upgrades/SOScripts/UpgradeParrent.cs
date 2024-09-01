using Formula;
using Resources;
using System;
using System.Collections.Generic;
using UnityEngine;
using Upgrade.Property;

namespace Upgrade
{
    public abstract class UpgradeParrent : ScriptableObject
    {
        [Header("Data")]
        public int MaxUpgradeLevel;
        public FormulaParrent Formula;
        public string Id;
        public bool isMultiplier;
        public MainProperty Parrent;
        public List<Payment> Cost = new();

        public int UpgradeLevel
        {
            get
            {
                if (!PlayerPrefs.HasKey(Id + "Level"))
                    return 1;
                else
                    return PlayerPrefs.GetInt(Id + "Level");
            }
            set
            {
                PlayerPrefs.SetInt(Id + "Level", value);
            }
        }

        public abstract void Upgrade();

        #region Getters
        public virtual float GetIncrementValue(bool future = false)
        {
            return 1;
        }

        public abstract float GetPropertyValue(bool future = false);

        public virtual List<Payment> GetNextCost()
        {
            List<Payment> payCost = new();

            foreach(var item in Cost)
            {
                payCost.Add(new Payment {Type = item.Type, Cost = Convert.ToUInt32(Formula.GetCost(UpgradeLevel + 1))});
            }

            return payCost;
        }
        #endregion
    }
}