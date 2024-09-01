using System;
using UnityEngine;

namespace Formula
{
    [CreateAssetMenu(fileName = "Formula", menuName = "Formulas/HardFormula")]
    public class HardFormula : FormulaParrent
    {
        public float C_coef;
        public float I_coef;
        public float R_coef;
        public float K_coef;

        public CostFormulaStruct CostFormula;

        public override float GetValue(int _level)
        {
            var value = C_coef * (Math.Pow(_level, (I_coef + _level / K_coef)) + _level) + R_coef * Math.Pow(_level, 3 / _level + 1);
            return (float)value;
        }

        public override float GetCost(int _level)
        {
            var value = CostFormula.C_coef * (Math.Pow(_level, (CostFormula.I_coef + _level / CostFormula.K_coef)) + _level) + CostFormula.R_coef * Math.Pow(_level, 3 / _level + 1);
            return (float)value;
        }
    }
}