using UnityEngine;

namespace Formula
{
    public abstract class FormulaParrent : ScriptableObject
    {
        public abstract float GetValue(int _level);

        public abstract float GetCost(int _level);
    }
}