using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core_Mk1
{
    /// <summary>
    /// Абстрактынй класс содержащий общие для перков, снаряжения, заклинаний и статусов свойства
    /// </summary>
    public abstract class BasicDevEntity
    {
        //_____________________КОНСТРУКТОР_____________________

        /// <summary>
        /// Стандартный конструктор игровой сущности
        /// </summary>
        /// <param name="name">Название сущности</param>
        public BasicDevEntity(string name) { this.name = name; }



        //_____________________ПОЛЯ_____________________

        //Название 
        protected string name;
        //Подробное описание 
        protected string description;
        //Минимальные значение характеристик, которыми должен обладать персонаж, для использования сущности
        protected Dictionary<ECharacteristic, int> requirementToUse = new Dictionary<ECharacteristic, int>();
        //Словарь всех срабатывающих эффектов сущности при её активации
        protected Dictionary<EPlayerType, Dictionary<ECharacteristic, Dictionary<EDerivative, CommonEffect>>> effect = new Dictionary<EPlayerType, Dictionary<ECharacteristic, Dictionary<EDerivative, CommonEffect>>>();



        //_____________________GET/SET_____________________

        //get/set name
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //get/set description
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //get словарь всех эффектов
        public Dictionary<EPlayerType, Dictionary<ECharacteristic, Dictionary<EDerivative, CommonEffect>>> Effect
        {
            get { return effect; }
        }

        /// <summary>
        /// get/set требование для использования
        /// </summary>
        /// <param name="characteristic">Установить порог, не меньше которого должна быть базовая характеристика персонажа</param>
        /// <returns></returns>
        public int this[ECharacteristic characteristic]
        {
            get
            {
                return requirementToUse[characteristic];
            }
            set
            {
                requirementToUse[characteristic] = value;
            }
        }

        /// <summary>
        /// Установка/изменение эффекта, который бдует срабатывать при активации сущности.
        /// </summary>
        /// <param name="target">На кого воздействеут эффект</param>
        /// <param name="targetChar">На какую характеристику воздействеут эффект</param>
        /// <param name="targetDer">На какую производную воздействеут эффект</param>
        /// <param name="сonstanBuff">Константа, которую эффект прибавит к указанной производной</param>
        public void SetOrChangeEffect(EPlayerType target, ECharacteristic targetChar, EDerivative targetDer, double сonstanBuff = 0)
        {
            //
            if (effect.ContainsKey(target)) //
            {
                //
                if (effect[target].ContainsKey(targetChar)) //
                {
                    //
                    if (effect[target][targetChar].ContainsKey(targetDer)) //
                    {
                        //
                        effect[target][targetChar][targetDer].ConstanBuff = сonstanBuff;
                    }
                    else
                    {
                        //
                        effect[target][targetChar].Add(targetDer, new CommonEffect(сonstanBuff));
                    }
                }
                else
                {
                    //
                    effect[target].Add(targetChar, new Dictionary<EDerivative, CommonEffect> { [targetDer] = new CommonEffect(сonstanBuff) });
                }
            }
            else
            {
                //
                effect.Add(target, new Dictionary<ECharacteristic, Dictionary<EDerivative, CommonEffect>> { [targetChar] = new Dictionary<EDerivative, CommonEffect> { [targetDer] = new CommonEffect(сonstanBuff) } });
            }
        }
        /// <summary>
        /// Установка/изменение эффекта, который будет расчитываться как % от указанного параметра
        /// </summary>
        /// <param name="target">На кого воздействеут эффект</param>
        /// <param name="targetChar">На какую характеристику воздействеут эффект</param>
        /// <param name="targetDer">На какую производную воздействеут эффект</param>
        /// <param name="source">От кого скейлится эффект</param>
        /// <param name="scaleChar">От какой характеристики скейлится эффект</param>
        /// <param name="scaleDer">От какой производной скейлится эффект</param>
        /// <param name="value">Коэффициент с которым происходит скейл</param>
        public void SetOrChangeEffectScale(EPlayerType target, ECharacteristic targetChar, EDerivative targetDer, EPlayerType source, ECharacteristic scaleChar, EDerivative scaleDer, double value)
        {
            //
            if (effect.ContainsKey(target)) //
            {
                //
                if (effect[target].ContainsKey(targetChar)) //
                {
                    //
                    if (effect[target][targetChar].ContainsKey(targetDer)) //
                    {
                        //
                        effect[target][targetChar][targetDer].SetOrChangeScale(source, scaleChar, scaleDer, value);
                    }
                    else
                    {
                        //
                        effect[target][targetChar].Add(targetDer, new CommonEffect(source, scaleChar, scaleDer, value));
                    }
                }
                else
                {
                    //
                    effect[target].Add(targetChar, new Dictionary<EDerivative, CommonEffect> { [targetDer] = new CommonEffect(source, scaleChar, scaleDer, value) });
                }
            }
            else
            {
                //
                effect.Add(target, new Dictionary<ECharacteristic, Dictionary<EDerivative, CommonEffect>> { [targetChar] = new Dictionary<EDerivative, CommonEffect> { [targetDer] = new CommonEffect(source, scaleChar, scaleDer, value) } });
            }
        }



        //_____________________МЕТОДЫ_____________________




    }
}
