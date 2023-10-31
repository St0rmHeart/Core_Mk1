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
        
        //поля
        protected BodyPart bodyPart;
        public BodyPart Bodypart
        {
            get { return bodyPart; }
            set { bodyPart = value; }
        }
        protected bool isPassive;
        public bool IsPassive
        {
            get { return isPassive; }
            set { isPassive = value; }
        }
        //методы
        public Equipment(BodyPart bodyPart, string name, bool isPassive) //конструктор
        {
            this.bodyPart = bodyPart;
            this.name = name;
            this.isPassive = isPassive;
        }
        /*
                public void ShowAllData()//Вывод информации всей информации предмета
                {
                    Console.WriteLine("Название:" + name);
                    Console.WriteLine("Описание: " + description);
                    Console.WriteLine("Слот: " + bodyPart);
                    Console.WriteLine("Требования по характеристикам: ");
                    foreach (var item in requirementToUse)
                    {
                        Console.WriteLine("\tХарактеристика: " + item.Key + ", не менее " + item.Value);
                    }
                    Console.WriteLine("Условия срабатывания: ");
                    foreach (var item in trigger)
                    {
                        Console.WriteLine("\tКамень: " + item.Key + ", Количество: " + item.Value.value + ", Отношение: " + item.Value.comparison);
                    }
                    Console.WriteLine("Эффект срабатывания: ");
                    foreach (var item in simpleStatEffect)
                    {
                        Console.WriteLine("\tХарактеристика: " + item.Key + ", изменяется на: " + item.Value);
                    }
                    foreach (var item in StatEffect)
                    {
                        foreach (var deriv in item.Value)
                        {
                            Console.WriteLine("\tХарактеристика: " + item.Key.ToString() + deriv.Key.ToString() + ", изменяется на: " + deriv.Value);
                        }
                    }

                }
        */


        /// <summary>
        /// проверка возможности одеть данное снаряжение 
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool IsPossibleToEquip(Character character)  
        {
            foreach (var item in requirementToUse.Keys)
            {
                if (character[item] < requirementToUse[item])
                {
                    return false;
                }
            }
            return true;
        }
        public void ApplyEffect(CharacteristicEnumeration source, CharacteristicEnumeration purpose)
        {
            foreach (var effChar in effect.Keys)
            {
                foreach(var effDer in effect[effChar].Keys)
                {
                    double resultEffect = effect[effChar][effDer].Value;//возможно имеет место множественное создание переменной

                    foreach (var scaleChar in effect[effChar][effDer].StatList.Keys)
                    {
                        foreach (var scaleDer in effect[effChar][effDer].StatList[scaleChar].Keys)
                        {
                            resultEffect += source.StatList[scaleChar][scaleDer] * effect[effChar][effDer].StatList[scaleChar][scaleDer];
                        }
                    }

                    purpose.SetOrChangeStat(effChar, effDer, Math.Floor(resultEffect));
                }
            }
        }
    }
}
