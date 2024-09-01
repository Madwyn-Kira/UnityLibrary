using System;
using UnityEngine;

namespace Resources
{
    [Serializable]
    public class Resource
    {
        public delegate void ValueChanged();
        public event ValueChanged OnValueChanged;

        public ResourceTypeEnum Type { get; set; }
        public GameObject prefabOnGround { get; set; }
        public Sprite sprite { get; set; }

        public uint Value { get; set; }

        public Resource()
        {

        }

        public Resource(ResourceTypeEnum resourceType)
        {
            Type = resourceType;

            //Load();
        }

        /// <summary>
        /// вернет баланс по ресурсу
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return (int)Value;
        }

        /// <summary>
        /// вернет строку с названием ресурса (по enum)
        /// </summary>
        /// <returns></returns>
        public string GetAssetName()
        {
            return Type.ToString();
        }

        /// <summary>
        /// Добавляет полученно значение к текущему
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(int value)
        {
            Value += (uint)value;
            OnValueChanged?.Invoke();

            //Save();
        }

        /// <summary>
        /// вычитает полученное значение из текущего
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool RemoveValue(int value)
        {
            bool result = false;

            if (IsCanRemove(value))
            {
                Value -= (uint)value;
                OnValueChanged?.Invoke();
                result = true;
            }
            //Save();
            return result;
        }

        /// <summary>
        /// проверяет возможно ли выесть полученное значение из текущего
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsCanRemove(int value)
        {
            bool result = false;

            if (Value >= value)
            {
                result = true;
            }

            return result;
        }

        public static string GetString(ulong value)
        {
            string valueFormatted = "0";
            var tempValue = value;
            ulong fractionPart = 0;
            var index = 0;
            while (tempValue > 1000)
            {
                fractionPart = tempValue % 1000;
                tempValue /= 1000;
                switch (tempValue)
                {
                    case >= 1 and < 10:
                        fractionPart /= 10;
                        break;
                    case < 100:
                        fractionPart /= 100;
                        break;
                    case < 1000:
                        fractionPart = 0;
                        break;
                }
                index++;
            }

            if (index != 0)
            {
                if (fractionPart == 0)
                {
                    valueFormatted = tempValue + " " + (DegreeEnum)index;
                }
                else
                {
                    if (fractionPart < 10 && index == 1)
                    {
                        valueFormatted = tempValue + ".0" + fractionPart + " " + (DegreeEnum)index;
                    }
                    else
                    {
                        valueFormatted = tempValue + "." + fractionPart + " " + (DegreeEnum)index;
                    }
                }
            }
            else
            {
                valueFormatted = value.ToString();
            }

            return valueFormatted;
        }

        private void Save()
        {
            PlayerPrefs.SetString($"resource_{Type}", Value.ToString());
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey($"resource_{Type}"))
            {
                Value = uint.Parse(PlayerPrefs.GetString($"resource_{Type}"));
            }
            else
            {
                Value = 0;
            }
        }
    }

    public enum DegreeEnum
    {
        K = 1,
        M = 2,
        B = 3,
        T = 4,
        Q = 5,
        Sx = 6,
        Sp = 7,
        Oc = 8
    }
}