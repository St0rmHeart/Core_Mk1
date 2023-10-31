using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// Универсальный класс с полем - двойным словарём и инструментами взаимодействия
    /// </summary>
    public abstract class CharacteristicEnumeration
    {
        /// <summary>
        /// Является структурой: значнеие <see cref="double"/> производной <see cref="Derivative"/> характеристики <see cref="Characteristic"/>
        /// </summary>
        protected Dictionary<Characteristic, Dictionary<Derivative, double>> statList = new Dictionary<Characteristic, Dictionary<Derivative, double>>(); //поле
        
        //_____________________GET_____________________
        /// <summary>
        /// Вернуть весь двойной словарь <see cref="statList"/>
        /// </summary>
        public Dictionary<Characteristic, Dictionary<Derivative, double>> StatList
        {
            get { return statList; }
        }

        //_____________________SET_____________________
        /// <summary>
        /// установить данные <see cref="double"/> <see cref="value"/>
        /// </summary>
        /// /// <param name="characteristic">Характеристка к производной которой обращаемся </param>
        /// /// <param name="derivative">Производная значение которой нужно установить или изменить </param>
        /// /// <param name="value">Аргумент метода Main()</param>
        public void SetOrChangeStat(Characteristic characteristic, Derivative derivative, double value)
        {
            if (statList.ContainsKey(characteristic))
            {
                if (statList[characteristic].ContainsKey(derivative))
                {
                    value += statList[characteristic][derivative];
                    statList[characteristic][derivative] = value;
                }
                else
                {
                    statList[characteristic].Add(derivative, value); 
                }
            }
            else
            {
                statList.Add(characteristic, new Dictionary<Derivative, double> { [derivative] = value });
            }
        }
    }
    
}
