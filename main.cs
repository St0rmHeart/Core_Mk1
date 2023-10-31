using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    internal class main
    {
        /// <summary>
        /// Этот метод передаёт привет ХабраХабру столько раз, сколько скажите.
        /// </summary>
        /// <param name="repeat">Сколько раз передать привет</param>
        /// <returns>Сама строка с приветами</returns>
        public string HelloHabr(int repeat)
        {
            string result = "";
            for (int i = 0; i < repeat; i++)
            {
                result += "Hello, HabraHabr!\n";
            }
            return result;
        }
        static void Main()
        {
            Console.WriteLine("Тест создания персонажа:");
            Character Troll = new Character("Тролль");
            Console.WriteLine("\nТест изменения множества характеристик :");
            Troll[Characteristic.strength] = 70;
            Troll[Characteristic.dexterity] = 5;
            Troll[Characteristic.endurance] = 60;
            Troll[Characteristic.fire] = 10;
            Troll[Characteristic.water] = 10;
            Troll[Characteristic.air] = 15;
            Troll[Characteristic.earth] = 25;
            Troll[Characteristic.level] = 30;


            Console.WriteLine("\nТест создания предмета-пуштышки:");
            Equipment sword = new Equipment(BodyPart.weapon, "Пелельный клинок", false);
            Console.WriteLine("\tОписание предмета: " + sword.Bodypart + " " +sword.Name);

            Console.WriteLine("\nТест создания полноценного предмета 1 ");
            sword.Description = "Старинный меч, зачарованный утраченной пепельной магией.\nПри нанесении 6 или более урона вы:\n\tнаносите дополнительно 5 урона +1 за каждые 10 очков навыка \"сила\";\n\tполучаете +1 к красной мане за каджые 2 очка навыка \"Мастерство огня\"";
            //установка требований по характеристикам
            sword.SetRequirementToUse(Characteristic.strength, 10);
            sword.SetRequirementToUse(Characteristic.fire, 2);
            //Установка триггера - "если нанесено 6 или более урона"
            sword.SetTrigger(TriggerType.skull, 6, '+');
            //Установка эффекта 1 - "Нанести 5 едениц урона"
            sword.SetEffectValue(Characteristic.endurance, Derivative.currentHealth, -5);
            //установка скейла для эффекта 1 с помощью индексатора - "+1 ед. за каждые 10 очков навыка "сила""
            sword[Characteristic.endurance, Derivative.currentHealth].AddScale(characteristic: Characteristic.strength, derivative: Derivative.value, value: -0.1);
            //Установка эффекта 2 - "Получить 0 красной маны "
            sword.SetEffectValue(Characteristic.fire, Derivative.currentMana, 0);
            //установка скейла для эффекта 1 с помощью метода - "+1 ед. за каждые 2 очка "мастерства огня""
            sword.GetEffect(Characteristic.fire, Derivative.currentMana).AddScale(Characteristic.fire, Derivative.value, value: 0.5);

            Console.WriteLine("\nТест создания полноценного предмета 2 ");
            //Установка "слота", названия и типа "пассивный/по условию"
            Equipment crownOfWisdom = new Equipment(BodyPart.head, "корона мудрости", isPassive: true);
            //Установка описания
            crownOfWisdom.Description = "Уникальный магический артефакт.\nУвеличивает выносливость 10, мастерство огня и воды на 5;\nУвеличивает максимальные запасы маны огня и воды на 1 за каждые 5 едениц соответствующего мастерства";
            //установка требований по характеристикам
            crownOfWisdom.SetRequirementToUse(Characteristic.endurance, 15);
            crownOfWisdom.SetRequirementToUse(Characteristic.fire, 5);
            crownOfWisdom.SetRequirementToUse(Characteristic.water, 5);
            //Установка триггера
            //Так как предмет пассивный - триггер не требуется - предмет срабатывает всегда 
            //Установка эффекта 1 - "Увеличение выносливости 10"
            crownOfWisdom.SetEffectValue(Characteristic.endurance, Derivative.value, 10);
            //Установка эффекта 2 - "Увеличение мастерства огня на 5"
            crownOfWisdom.SetEffectValue(Characteristic.fire, Derivative.value, 5);
            //Установка скейла для эффекта 2 - "+1 макс. красной маны за каждые 5 ед. мастерства огня"
            crownOfWisdom.SetEffectValue(Characteristic.fire, Derivative.maxMana, 0);
            crownOfWisdom.GetEffect(Characteristic.fire,Derivative.maxMana).AddScale(Characteristic.fire,Derivative.value, value: 0.2);
            //Установка эффекта 3 - "Увеличение мастерства воды на 5"
            crownOfWisdom.SetEffectValue(Characteristic.water, Derivative.value, 5);
            //Установка скейла для эффекта 3 - "+1 макс. синей маны за каждые 5 ед. мастерства воды"
            crownOfWisdom.SetEffectValue(Characteristic.water, Derivative.maxMana, 0);
            crownOfWisdom.GetEffect(Characteristic.water, Derivative.maxMana).AddScale(Characteristic.water, Derivative.value, value: 0.2);

            Console.WriteLine("\nТест попытки экиперовки полноценного предмета 1:" + Troll.TryEquip(sword));
            Console.WriteLine("\nТест попытки экиперовки полноценного предмета 2:" + Troll.TryEquip(crownOfWisdom));

            Console.WriteLine("\nТест изменения отображения базовых характеристик и экиперованного снаряжения:");
            Console.WriteLine("_____НАЧАЛО_____");
            Troll.Showstats();
            Console.WriteLine("_____КОНЕЦ_____");
            Console.WriteLine("\nТест инициализации ВСЕХ параметров персонажа");
            Console.WriteLine("_____НАЧАЛО_____");
            Troll.InitStats();
            Console.WriteLine("_____КОНЕЦ_____");
            //не относящиеся к системе тесты
            /*Console.WriteLine(Characteristic.fire);
            Console.WriteLine(ComplexStat.fire);
            Console.WriteLine(Characteristic.fire.ToString() == ComplexStat.fire.ToString());
            Console.WriteLine(Enum.Parse(typeof(Characteristic), SimpleStat.endurance.ToString())); //найти более адекватный способ перехода от одного enum к другому
            
            Console.WriteLine("Characteristic.fire.GetType() " + Characteristic.fire.GetType());
            Console.WriteLine("Characteristic.fire.ToString().GetType() " + Characteristic.fire.ToString().GetType());
            Console.WriteLine("Enum.Parse(typeof(Characteristic), SimpleStat.endurance.ToString()) " + ((Characteristic)Enum.Parse(typeof(Characteristic), SimpleStat.endurance.ToString())).GetType());
            Console.WriteLine("Enum.Parse(typeof(Characteristic), SimpleStat.endurance.ToString()) " + Enum.Parse(typeof(Characteristic), SimpleStat.endurance.ToString()).GetType().GetType());
            int a = 5;
            double b = 4.3;
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(a + b);
            b = a;
            Console.WriteLine(b);
            Console.WriteLine(b.GetType());
            Console.WriteLine((a + b).GetType());*/

            /// <summary>
            /// Этот метод передаёт привет ХабраХабру столько раз, сколько скажите.
            /// </summary>
            /// <param name="repeat">Сколько раз передать привет</param>
            /// <returns>Сама строка с приветами</returns>
            

            Console.ReadLine();
        }
    }
}
