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
    public class Character : CharacteristicEnumeration
    {
        //имя персонажа
        protected string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        protected int level;
        protected int xp;
        public int Xp
        {
            get { return xp; }
            set { xp = value; }
        }
        protected int gold;
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        //Основные характеристики
        protected Dictionary<Characteristic, UniCharacteristic> characteristics = new Dictionary<Characteristic, UniCharacteristic>();
        //носимое снаряжение
        protected Dictionary<BodyPart, Equipment> usedEquipment = new Dictionary<BodyPart, Equipment>();
        //используемые перки
        protected Dictionary<int, Perk> usedPerks = new Dictionary<int, Perk>();
        //используемые заклинания
        protected Dictionary<int, Spell> usedSpells = new Dictionary<int, Spell>();
        

        //методы
        public Character(string name) //конструктор
        {
            this.name = name;
            level = 0;
            xp = 0;
            gold = 0;
            characteristics.Add(Characteristic.strength,  new Strength(0));
            characteristics.Add(Characteristic.dexterity, new Dexterity(0,gold));
            characteristics.Add(Characteristic.endurance, new Endurance(0,xp));
            characteristics.Add(Characteristic.fire,      new Fire(0));
            characteristics.Add(Characteristic.water,     new Water(0));
            characteristics.Add(Characteristic.air,       new Air(0));
            characteristics.Add(Characteristic.earth,     new Earth(0));


            foreach (BodyPart bodyPart in Enum.GetValues(typeof(BodyPart)))
            {
                usedEquipment.Add(bodyPart, null);
            }
            for (int i = 0; i < 5; i++)
            {
                usedPerks.Add(i+1, null);
            }
            for (int i = 0; i < 8; i++)
            {
                usedSpells.Add(i+1, null);
            }

        }
        public double this[Characteristic param] //получение/запись значений характеристики
        {
            get
            {
                return characteristics[param].Value;
            }
            set
            {
                characteristics[param].Value = value;
            }
        }
        
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
            foreach (var slot in usedPerks.Keys)
            {
                if (usedPerks[slot] == null)
                {
                    Console.WriteLine($"\t{slot} = {usedPerks[slot]}");
                }
                else
                {
                    Console.WriteLine($"\t{slot} = {usedPerks[slot].Name}");
                }
            }
            Console.WriteLine("Используемые заклинания:");
            foreach (var slot in usedSpells.Keys)
            {
                if (usedSpells[slot] == null)
                {
                    Console.WriteLine($"\t{slot} = {usedSpells[slot]}");
                }
                else
                {
                    Console.WriteLine($"\t{slot} = {usedSpells[slot].Name}");
                }
            }
        }
        
        /// <summary>
        /// Попытаться одеть на персонажа предмет или заменить уже одетый на другой с тем же слотом.
        /// </summary>
        public bool TryEquip(Equipment thing)
        {
            if (thing.IsPossibleToEquip(this))
            {
                usedEquipment[thing.Bodypart] = thing;
                return true;
            }
            return false;
        }

        public void InitStats()
        {
            //пименяем ВСЕ пассивные эффекты ВСЕГО носимого снаряжения
            foreach (BodyPart bodyPart in usedEquipment.Keys)
            {
                if (usedEquipment[bodyPart] != null && usedEquipment[bodyPart].IsPassive)
                {
                    usedEquipment[bodyPart].ApplyEffect(source: this, purpose: this);
                }
            }
            //Высчитываем ВСЕ производные параметры характеристик и записываем или прибавляем их
            foreach (var masteryStat in statList.Keys)
            {
                statList[masteryStat] = characteristics[masteryStat].CalculateDerivatives();
            }
        }
    }
}
