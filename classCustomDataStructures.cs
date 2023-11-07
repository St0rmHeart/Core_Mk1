using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// Универсальный класс с полем - двойным словарём - набором производных характеристик - и инструментами взаимодействия
    /// </summary>
    public class CharacteristicEnumeration
    {
        //_____________________ПОЛЯ_____________________

        /// <summary>
        /// Структура: Характеристика <see cref="ECharacteristic"/>, её производная <see cref="EDerivative"/>, её значение <see cref="double"/>
        /// </summary>
        protected Dictionary<ECharacteristic, Dictionary<EDerivative, double>> statList = new Dictionary<ECharacteristic, Dictionary<EDerivative, double>>();



        //_____________________GET/SET_____________________

        /// <summary>
        /// Установить указанный набор значений производных какой-либо характеристики. Сохраняет значения уже существующих, неуказанных производных
        /// </summary>
        /// <param name="characteristic"> Характеристика, куда устанавливаем набор производных</param>
        /// <param name="value">Набор производных</param>
        public void SetDerivatives(ECharacteristic characteristic, Dictionary<EDerivative, double> value)
        {
            if (statList.ContainsKey(characteristic))
            {
                foreach (EDerivative derivative in value.Keys)
                {
                    if (statList[characteristic].ContainsKey(derivative)) { statList[characteristic][derivative] = value[derivative]; }
                    else { statList[characteristic].Add(derivative, value[derivative]); }
                }
            }
            else
            {
                statList.Add(characteristic, value);
            }
        }

        /// <summary>
        /// Вернуть набор производных указанной характеристики
        /// </summary>
        /// <param name="characteristic"></param>
        /// <returns> Словарь: Ключ - производная <see cref="EDerivative"/>, значение - её значение <see cref="double"/> </returns>
        public Dictionary<EDerivative, double> this[ECharacteristic characteristic]
        {
            get
            {
                return statList[characteristic];
            }
        }

        /// <summary>
        /// Установить новое/перезаписать существующее значение конкретной производной конкретной характеристики
        /// </summary>
        /// <param name="characteristic">Характеристика</param>
        /// <param name="derivative">Производная указанной характеристики</param>
        /// <param name="value">Новое значение указанной производной</param>
        public void SetOrOverwriteStat(ECharacteristic characteristic, EDerivative derivative, double value)
        {
            //
            if (statList.ContainsKey(characteristic)) //
            {
                //
                if (statList[characteristic].ContainsKey(derivative)) //
                {
                    //
                    statList[characteristic][derivative] = value;
                }
                else
                {
                    //
                    statList[characteristic].Add(derivative, value);
                }
            }
            else
            {
                //
                statList.Add(characteristic, new Dictionary<EDerivative, double> { [derivative] = value });
            }
        }

        /// <summary>
        /// Установить новое/увеличить существующее значение конкретной производной конкретной характеристики на указанную величину
        /// </summary>
        /// <param name="characteristic">Характеристика</param>
        /// <param name="derivative">Производная указанной характеристики</param>
        /// <param name="value">Насколько увеличить значение</param>
        public void SetOrIncreaseStat(ECharacteristic characteristic, EDerivative derivative, double value)
        {
            //
            if (statList.ContainsKey(characteristic)) //
            {
                //
                if (statList[characteristic].ContainsKey(derivative)) //
                {
                    //
                    statList[characteristic][derivative] = statList[characteristic][derivative] + value;
                }
                else
                {
                    //
                    statList[characteristic].Add(derivative, value);
                }
            }
            else
            {
                //
                statList.Add(characteristic, new Dictionary<EDerivative, double> { [derivative] = value });
            }
        }
        /// <summary>
        /// Вернуть список всех ключей - характеристик
        /// </summary>
        public List<ECharacteristic> Keys { get { return statList.Keys.ToList(); } }



        //_____________________МЕТОДЫ_____________________

        /// <summary>
        /// Проверить словарь на существование производных для указанной характеристики
        /// </summary>
        /// <param name="characteristic">Характеристика</param>
        /// <returns><see cref="bool"/> наличие производных для данной характеристики.</returns>
        public bool ContainsKey(ECharacteristic characteristic) { return statList.ContainsKey(characteristic); }
        /// <summary>
        /// Очистить содержимое структры
        /// </summary>
        public void Clear() { statList.Clear(); }
        
    }
}
