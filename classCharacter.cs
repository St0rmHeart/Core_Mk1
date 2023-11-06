using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// Класс, в хором хранится вся информация, определющая степень развития персонажа
    /// </summary>
    public class Character
    {
        //_____________________КОНСТРУКТОР_____________________
        /// <summary>
        /// Базовый конструктор персонажа.
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="level">Уровень</param>
        /// <param name="xp">Текущее количество опыта</param>
        /// <param name="gold">Текущее количество золота</param>
        public Character(string name, int level = 0, int xp = 0, int gold = 0)
        {
            this.name = name;
            this.level = level;
            this.xp = xp;
            this.gold = gold;

            foreach (EBodyPart bodyPart in Enum.GetValues(typeof(EBodyPart)))
            {
                usedEquipment.Add(bodyPart, null);
            }

        }



        //_____________________ПОЛЯ_____________________

        //имя 
        protected string name;
        //уровень 
        protected int level;
        //накопленный опыт на уровне 
        protected int xp;
        //накопленное золото
        protected int gold;
        //базовые характеристики
        protected Dictionary<ECharacteristic, int> characteristics = new Dictionary<ECharacteristic, int>();
        //используемые перки
        protected List<Perk> usedPerks = new List<Perk>();
        //носимое снаряжение
        protected Dictionary<EBodyPart, Equipment> usedEquipment = new Dictionary<EBodyPart, Equipment>();
        //используемые заклинания
        protected List<Spell> usedSpells = new List<Spell>();



        //_____________________GET/SET_____________________

        //get/set имя персонажа
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //get уровень персонажа
        public int Level
        {
            get { return level; }
        }

        //get/set опыт на текущем уровне 
        public int Xp
        {
            get { return xp; }
            set { xp = value; }
        }

        //get/set количество золота 
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        /// <summary>
        /// get/set значение характеристики
        /// </summary>
        /// <param name="characteristic">Характеристика, к которой обращаемся</param>
        /// <returns></returns>
        public int this[ECharacteristic characteristic] 
        {
            get { return characteristics[characteristic];  }
            set { characteristics[characteristic] = value; }
        }

        //get список перков
        public List<Perk> UsedPerks
        {
            get { return usedPerks; }
        }
        //get словарь снаряжения
        public Dictionary<EBodyPart, Equipment> UsedEquipment
        {
            get { return usedEquipment; }
        }



        //_____________________МЕТОДЫ_____________________

        /// <summary>
        /// Отображение базовых характеристик, экиперованных предметов, перков и заклинаний.
        /// </summary>
        public void Showstats() 
        {
            Console.WriteLine("Персонаж: " + name);
            Console.WriteLine("Список характеристик:");
            foreach (var stat in characteristics.Keys)
            {
                Console.WriteLine($"\t{stat} = {characteristics[stat]}");
            }
            Console.WriteLine("Экиперованное снаряжение:");
            foreach (var slot in usedEquipment.Keys)
            {
                if (usedEquipment[slot] == null)
                {
                    Console.WriteLine($"\t{slot} = {usedEquipment[slot]}");
                }
                else
                {
                    Console.WriteLine($"\t{slot} = {usedEquipment[slot].Name}");
                }   
            }
            Console.WriteLine("Используемые перки:");
            foreach (Perk slot in usedPerks)
            {
                Console.WriteLine($"\t{slot.Name}");
            }
            Console.WriteLine("Используемые заклинания:");
            foreach (Spell slot in usedSpells)
            {
                Console.WriteLine($"\t{slot.Name}");
            }
        }

        /// <summary>
        /// Попытаться одеть на персонажа предмет или заменить уже одетый на другой.
        /// </summary>
        /// <param name="thing">Предмет, который мы пытаемся экиперовать</param>
        /// <returns></returns>
        public bool TryEquip(Equipment thing)
        {
            if (thing.IsPossibleToEquip(this))
            {
                usedEquipment[thing.Bodypart] = thing;
                return true;
            }
            return false;
        }
    }
}   
