using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    internal class main
    {
        static void Main()
        {
            Console.WriteLine("Тест создания персонажа 1:");
            Character Troll = new Character("Тролль");
            Console.WriteLine("\nТест изменения множества характеристик персонажа 1:");
            Troll[ECharacteristic.strength] = 70;
            Troll[ECharacteristic.dexterity] = 5;
            Troll[ECharacteristic.endurance] = 60;
            Troll[ECharacteristic.fire] = 10;
            Troll[ECharacteristic.water] = 10;
            Troll[ECharacteristic.air] = 15;
            Troll[ECharacteristic.earth] = 25;

            Perk perk1 = new Perk("перк +50% хп");
            perk1.SetOrChangeEffectScale(EPlayerType.self, ECharacteristic.endurance, EDerivative.maxHealth, EPlayerType.self, ECharacteristic.endurance, EDerivative.maxHealth, 0.5);
            Equipment equipment1 = new Equipment(EBodyPart.head, "Шапка на +50% хп");
            equipment1.SetOrChangeEffectScale(EPlayerType.self, ECharacteristic.endurance, EDerivative.maxHealth, EPlayerType.self, ECharacteristic.endurance, EDerivative.maxHealth, 0.5);

            Troll.TryEquip(equipment1);
            Troll.UsedPerks.Add(perk1);

            Console.WriteLine("Тест создания персонажа 2:");
            Character LakeBeast = new Character("Владычица озера");
            Console.WriteLine("\nТест изменения множества характеристик персонажа 2:");
            LakeBeast[ECharacteristic.strength] = 15;
            LakeBeast[ECharacteristic.dexterity] = 35;
            LakeBeast[ECharacteristic.endurance] = 90;
            LakeBeast[ECharacteristic.fire] = 0;
            LakeBeast[ECharacteristic.water] = 150;
            LakeBeast[ECharacteristic.air] = 25;
            LakeBeast[ECharacteristic.earth] = 15;


            /*Console.WriteLine("\nТест создания предмета-пуштышки:");
            Equipment sword = new Equipment(EBodyPart.weapon, "Пелельный клинок", false);
            Console.WriteLine("\tОписание предмета: " + sword.Bodypart + " " +sword.Name);

            Console.WriteLine("\nТест создания полноценного предмета 1 ");
            sword.Description = "Старинный меч, зачарованный утраченной пепельной магией.\nПри нанесении 6 или более урона вы:\n\tнаносите дополнительно 5 урона +1 за каждые 10 очков навыка \"сила\";\n\tполучаете +1 к красной мане за каджые 2 очка навыка \"Мастерство огня\"";
            //установка требований по характеристикам
            sword.SetRequirementToUse(ECharacteristic.strength, 10);
            sword.SetRequirementToUse(ECharacteristic.fire, 2);
            //Установка триггера - "если нанесено 6 или более урона"
            sword.SetTrigger(ETriggerType.Skull, 6, '+');
            //Установка эффекта 1 - "Нанести 5 едениц урона"
            sword.SetOrChangeEffect(ECharacteristic.endurance, EDerivative.currentHealth, -5);
            //установка скейла для эффекта 1 с помощью индексатора - "+1 ед. за каждые 10 очков навыка "сила""
            sword[ECharacteristic.endurance, EDerivative.currentHealth].AddScale(characteristic: ECharacteristic.strength, derivative: EDerivative.value, value: -0.1);
            //Установка эффекта 2 - "Получить 0 красной маны "
            sword.SetOrChangeEffect(ECharacteristic.fire, EDerivative.currentMana, 0);
            //установка скейла для эффекта 1 с помощью метода - "+1 ед. за каждые 2 очка "мастерства огня""
            sword.GetEffect(ECharacteristic.fire, EDerivative.currentMana).AddScale(ECharacteristic.fire, EDerivative.value, value: 0.5);

            Console.WriteLine("\nТест создания полноценного предмета 2 ");
            //Установка "слота", названия и типа "пассивный/по условию"
            Equipment crownOfWisdom = new Equipment(EBodyPart.head, "корона мудрости", isPassive: true);
            //Установка описания
            crownOfWisdom.Description = "Уникальный магический артефакт.\nУвеличивает выносливость 10, мастерство огня и воды на 5;\nУвеличивает максимальные запасы маны огня и воды на 1 за каждые 5 едениц соответствующего мастерства";
            //установка требований по характеристикам
            crownOfWisdom.SetRequirementToUse(ECharacteristic.endurance, 15);
            crownOfWisdom.SetRequirementToUse(ECharacteristic.fire, 5);
            crownOfWisdom.SetRequirementToUse(ECharacteristic.water, 5);
            //Установка триггера
            //Так как предмет пассивный - триггер не требуется - предмет срабатывает всегда 
            //Установка эффекта 1 - "Увеличение выносливости 10"
            crownOfWisdom.SetOrChangeEffect(ECharacteristic.endurance, EDerivative.value, 10);
            //Установка эффекта 2 - "Увеличение мастерства огня на 5"
            crownOfWisdom.SetOrChangeEffect(ECharacteristic.fire, EDerivative.value, 5);
            //Установка скейла для эффекта 2 - "+1 макс. красной маны за каждые 5 ед. мастерства огня"
            crownOfWisdom.SetOrChangeEffect(ECharacteristic.fire, EDerivative.maxMana, 0);
            crownOfWisdom.GetEffect(ECharacteristic.fire,EDerivative.maxMana).AddScale(ECharacteristic.fire,EDerivative.value, value: 0.2);
            //Установка эффекта 3 - "Увеличение мастерства воды на 5"
            crownOfWisdom.SetOrChangeEffect(ECharacteristic.water, EDerivative.value, 5);
            //Установка скейла для эффекта 3 - "+1 макс. синей маны за каждые 5 ед. мастерства воды"
            crownOfWisdom.SetOrChangeEffect(ECharacteristic.water, EDerivative.maxMana, 0);
            crownOfWisdom.GetEffect(ECharacteristic.water, EDerivative.maxMana).AddScale(ECharacteristic.water, EDerivative.value, value: 0.2);

            Console.WriteLine("\nТест попытки экиперовки полноценного предмета 1:" + Troll.TryEquip(sword));
            Console.WriteLine("\nТест попытки экиперовки полноценного предмета 2:" + Troll.TryEquip(crownOfWisdom));

            Console.WriteLine("\nТест изменения отображения базовых характеристик и экиперованного снаряжения:");
            Console.WriteLine("_____НАЧАЛО_____");
            Troll.Showstats();
            Console.WriteLine("_____КОНЕЦ_____");*/
            Console.WriteLine("\nТест инициализации начала сражения");
            Console.WriteLine("_____НАЧАЛО_____");
            СentralGameplay.InitGame(Troll, LakeBeast);
            Console.WriteLine("_____КОНЕЦ_____");
            /*//не относящиеся к системе тесты
            Console.WriteLine(Characteristic.fire);
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




            Console.ReadLine();
        }
    }
}
