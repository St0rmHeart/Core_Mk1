using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// Предмет снаряжения, который можно одеть в соответсвующий ему слот
    /// </summary>
    public class Equipment : BasicDevEntity
    {
        //_____________________КОНСТРУКТОР_____________________

        /// <summary>
        /// Стандартный конструктор предмета снаряжения
        /// </summary>
        /// <param name="bodyPart">Часть тела, на которую можно надеть предмет</param>
        /// <param name="name">Название предмета</param>
        public Equipment(EBodyPart bodyPart, string name) : base(name) { this.bodyPart = bodyPart; }



        //_____________________ПОЛЯ_____________________

        //Часть тела, на которую можно надеть предмет
        protected EBodyPart bodyPart;



        //_____________________GET/SET_____________________

        //get/set bodyPart
        public EBodyPart Bodypart
        {
            get { return bodyPart; }
            set { bodyPart = value; }
        }



        //_____________________МЕТОДЫ_____________________

        //Выяснить может ли указанный персонаж экиперовать данный предмет
        /// <summary>
        /// Выяснить может ли указанный персонаж использовать данный предмет
        /// </summary>
        /// <param name="character">Персонаж, которого требуется проверить</param>
        /// <returns><see cref="bool"/> возможность экиперовки</returns>
        public bool IsPossibleToEquip(Character character)  
        {
            foreach (ECharacteristic characteristic in requirementToUse.Keys)
            {
                if (character[characteristic] < requirementToUse[characteristic])
                {
                    return false;
                }
            }
            return true;
        }
       
    }
}
