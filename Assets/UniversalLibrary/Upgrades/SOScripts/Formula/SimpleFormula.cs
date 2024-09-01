using System;
using UnityEngine;

namespace Formula
{
    [CreateAssetMenu(fileName = "Formula", menuName = "Formulas/SimpleFormula")]
    public class SimpleFormula : FormulaParrent
    {
        public float FirstCoeficient;
        public float SecondCoeficient;

        public CostFormulaStruct CostFormula;

        public override float GetValue(int _level)
        {
            var value = FirstCoeficient * Math.Pow(_level, SecondCoeficient);
            return (float)value;
        }

        public override float GetCost(int _level)
        {
            var value = CostFormula.C_coef * (Math.Pow(_level, (CostFormula.I_coef + _level / CostFormula.K_coef)) + _level) + CostFormula.R_coef * Math.Pow(_level, 3 / _level + 1);
            return (float)value;
        }
    }
}