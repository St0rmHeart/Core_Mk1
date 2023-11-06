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

        //get структуру
        public Dictionary<ECharacteristic, Dictionary<EDerivative, double>> StatList
        {
            get { return statList; }
        }

        /// <summary>
        /// Установить все производные, какой-либо характеристики
        /// </summary>
        /// <param name="characteristic"> Характеристика, куда устанавливаем набор производных</param>
        /// <param name="value">Набор производных</param>
        public void SetDerivatives(ECharacteristic characteristic, Dictionary<EDerivative, double> value)
        {
            if (statList.ContainsKey(characteristic))
            {
                statList[characteristic] = value;
            }
            else
            {
                statList.Add(characteristic, value);
            }
        }

        //get набор производных указанной характеристики
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
            if (StatList.ContainsKey(characteristic)) //
            {
                //
                if (StatList[characteristic].ContainsKey(derivative)) //
                {
                    //
                    StatList[characteristic][derivative] = value;
                }
                else
                {
                    //
                    StatList[characteristic].Add(derivative, value);
                }
            }
            else
            {
                //
                StatList.Add(characteristic, new Dictionary<EDerivative, double> { [derivative] = value });
            }
        }

        /// <summary>
        /// Установить новое/увеличить существующее значение конкретной производной конкретной характеристики
        /// </summary>
        /// <param name="characteristic">Характеристика</param>
        /// <param name="derivative">Производная указанной характеристики</param>
        /// <param name="value">Насколько увеличить значение</param>
        public void SetOrIncreaseStat(ECharacteristic characteristic, EDerivative derivative, double value)
        {
            //
            if (StatList.ContainsKey(characteristic)) //
            {
                //
                if (StatList[characteristic].ContainsKey(derivative)) //
                {
                    //
                    StatList[characteristic][derivative] = StatList[characteristic][derivative] + value;
                }
                else
                {
                    //
                    StatList[characteristic].Add(derivative, value);
                }
            }
            else
            {
                //
                StatList.Add(characteristic, new Dictionary<EDerivative, double> { [derivative] = value });
            }
        }


    }
}
