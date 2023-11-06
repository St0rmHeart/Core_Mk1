using System;
using System.Collections;
using System.Collections.Generic;

namespace Core_Mk1
{
    /// <summary>
    /// Совокупность всех манипуляций, происходящих с конкренным параметром персонажа, если эффект срабатывает
    /// </summary>
    public class CommonEffect
    {
        //_____________________КОНСТРУКТОР_____________________

        /// <summary>
        /// Стандартный конструктор эффекта
        /// </summary>
        /// <param name="constant">Константа, которую эффект прибавит к текущему значению параметра</param>
        public CommonEffect(double constant) { this.constant = constant; }

        /// <summary>
        /// Конструктор с предустаноской скейла
        /// </summary>
        /// <param name="source">От кого скейлится эффект</param>
        /// <param name="scaleChar">От какой характеристики скейлится эффект</param>
        /// <param name="scaleDer">От какой производной скейлится эффект</param>
        /// <param name="value">Коэффициент с которым происходит скейл</param>
        public CommonEffect(EPlayerType source, ECharacteristic scaleChar, EDerivative scaleDer, double value)
        {
            constant = 0;
            scale.Add(source, new Dictionary<ECharacteristic, Dictionary<EDerivative, double>> { [scaleChar] = new Dictionary<EDerivative, double> { [scaleDer] = value } });
        }
        
        
        
        //_____________________ПОЛЯ_____________________

        //Константа, которую эффект прибавит к текущему значению параметра
        protected double constant;
        //Конструкция позволяющая изменить значение текущещего параметра на % от значения другого параметр
        protected Dictionary<EPlayerType, Dictionary<ECharacteristic, Dictionary<EDerivative, double>>> scale = new Dictionary<EPlayerType, Dictionary<ECharacteristic, Dictionary<EDerivative, double>>>();



        //_____________________GET/SET_____________________

        //get/set constant
        public double ConstanBuff
        {
            get { return constant; }
            set { constant = value; }
        }

        /// <summary>
        /// Установить/изменить изменение эффекта на % от других параметров.
        /// </summary>
        /// <param name="source">От кого скейлится эффект</param>
        /// <param name="scaleChar">От какой характеристики скейлится эффект</param>
        /// <param name="scaleDer">От какой производной скейлится эффект</param>
        /// <param name="value">Коэффициент с которым происходит скейл</param>
        public void SetOrChangeScale(EPlayerType source, ECharacteristic scaleChar, EDerivative scaleDer, double value)
        {
            //проверка эффекта на наличие
            if (scale.ContainsKey(source)) //скейла от source
            {
                //проверка эффекта на наличие
                if (scale[source].ContainsKey(scaleChar)) //скейла от source от характеристики scaleChar
                {
                    //проверка эффекта на наличие
                    if (scale[source][scaleChar].ContainsKey(scaleDer)) //скейла от source от характеристики scaleChar от её производной scaleDer
                    {
                        // задаём новый коэффиент указанного скейла
                        scale[source][scaleChar][scaleDer] = value;
                    }
                    else
                    {
                        //добавляем скейл от производной scaleDer с коэффициентом value
                        scale[source][scaleChar].Add(scaleDer, value);
                    }
                }
                else
                {
                    //добавляем скейл от характеристики scaleChar от производной scaleDer с коэффициентом value
                    scale[source].Add(scaleChar, new Dictionary<EDerivative, double> { [scaleDer] = value });
                }
            }
            else
            {
                //добавляем скейл от source от характеристики scaleChar от производной scaleDer с коэффициентом value
                scale.Add(source, new Dictionary<ECharacteristic, Dictionary<EDerivative, double>> { [scaleChar] = new Dictionary<EDerivative, double> { [scaleDer] = value } });
            }
        }



        //_____________________МЕТОДЫ_____________________

        /// <summary>
        /// Вычислить значение эффекта
        /// </summary>
        /// <param name="ownerStats">Параметры ВЛАДЕЛЬЦА эффекта</param>
        /// <param name="enemyStats">Параметры ПРОТИВНИКА владельца эффекта</param>
        /// <returns>Значение, на которое должен измениться параметр</returns>
        public double CalculateDelta(CharacteristicEnumeration ownerStats, CharacteristicEnumeration enemyStats)
        {
            double resultValue = constant;
            foreach (EPlayerType source in scale.Keys)
            {
                foreach (ECharacteristic scaleChar in scale[source].Keys)
                {
                    foreach (EDerivative scaleDer in scale[source][scaleChar].Keys)
                    {
                        if (source == EPlayerType.self)
                        {
                            resultValue += scale[source][scaleChar][scaleDer] * ownerStats[scaleChar][scaleDer];
                        }
                        else
                        {
                            resultValue += scale[source][scaleChar][scaleDer] * enemyStats[scaleChar][scaleDer];
                        }
                    }
                }
            }
            return resultValue;
        }
    }
}
