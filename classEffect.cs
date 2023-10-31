using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    public class Effect : CharacteristicEnumeration
    {
        /// <summary>
        /// Значение эффекта по умолчанию
        /// </summary>
        protected int value;
        /// <summary>
        /// Установить/получить значение эффекта по умолчанию
        /// </summary>
        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public Effect(int value) //Конструктор
        {
            this.value = value;
        }
        /// <summary>
        /// Установка скейла значения эффекта от какой-либо характеристики.
        /// </summary>
        public void AddScale(Characteristic characteristic, Derivative derivative, double value)
        {
            SetOrChangeStat(characteristic, derivative, value);
        }

    }
}
