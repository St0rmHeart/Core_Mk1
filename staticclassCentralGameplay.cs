using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    public class CharacterSlot
    {
        public CharacterSlot(Character character) { this.character = character; }
        private Character character;
        private CharacteristicEnumeration characterData = new CharacteristicEnumeration();
        private CharacteristicEnumeration characterDataDataBuffer = new CharacteristicEnumeration();
        public Character Character { get { return character; } }
        public CharacteristicEnumeration CharacterData { get {  return characterData; } }
        public CharacteristicEnumeration CharacterDataDataBuffer { get {  return characterDataDataBuffer; } }
    }
    public static class СentralGameplay
    {
        public static CharacterSlot leftPlayer;
        public static CharacterSlot rightPlayer;
        /// <summary>
        /// Пересчитывает параметры указанного персонажа:
        /// </summary>
        /// <param name="currentCharacter"></param>
        /// <param name="isInitGame"></param>
        private static void UpdateData(CharacterSlot currentCharacter, bool isInitGame = false)
        {
            CharacteristicEnumeration data = currentCharacter.CharacterData;
            CharacteristicEnumeration dataBuffer = currentCharacter.CharacterDataDataBuffer;
            int basevalue;
            double delta;

            foreach (ECharacteristic characteristic in Enum.GetValues(typeof(ECharacteristic)))
            {
                basevalue = currentCharacter.Character[characteristic];
                if (dataBuffer.ContainsKey(characteristic) && dataBuffer[characteristic].ContainsKey(EDerivative.value))
                {
                    delta = dataBuffer[characteristic][EDerivative.value];
                }
                else
                {
                    delta = 0;
                }
                data.SetDerivatives(characteristic, DerivativesCalculator.GetDerivatives(characteristic, basevalue + (int)delta, isInitGame));
            }

            foreach (ECharacteristic characteristic in dataBuffer.Keys)
            {
                foreach (EDerivative derivative in dataBuffer[characteristic].Keys)
                {
                    if (derivative != EDerivative.value) { data.SetOrIncreaseStat(characteristic, derivative, dataBuffer[characteristic][derivative]); }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        private static void CalculateEntityImpact(BasicDevEntity entity, CharacterSlot owner, CharacterSlot enemy)
        {
            CommonEffect currentEffect;
            foreach (EPlayerType target in entity.Effect.Keys)
            {
                foreach (ECharacteristic targetChar in entity.Effect[target].Keys)
                {
                    foreach (EDerivative targetDer in entity.Effect[target][targetChar].Keys)
                    {
                        currentEffect = entity.Effect[target][targetChar][targetDer];
                        if (target == EPlayerType.self)
                        {
                            owner.CharacterDataDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(owner.CharacterData, enemy.CharacterData));
                        }
                        else
                        {
                            enemy.CharacterDataDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(owner.CharacterData, enemy.CharacterData));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        private static void CalculatePerksImpact(CharacterSlot owner, CharacterSlot enemy)
        {
            foreach (Perk perk in owner.Character.UsedPerks) { CalculateEntityImpact(perk, owner, enemy); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="enemy"></param>
        private static void CalculateEquipmentImpact(CharacterSlot owner, CharacterSlot enemy)
        {
            foreach (Equipment equipment in owner.Character.UsedEquipment) { CalculateEntityImpact(equipment, owner, enemy); }
        }

        /// <summary>
        /// Инициализация начала сражения - установвка всех статов и состояний игрока и его противника
        /// </summary>
        public static void InitGame()
        {
            //пункт 1 - расчёт базовых параметров
            //пункт 1.2 Высчитываем все производные от базовых значений характеристик для каждого из персонажей
            UpdateData(leftPlayer);
            UpdateData(rightPlayer);
            //пункт 2. Получаем промежуточный набор данных, от которого отталкиваемся дальше

            //пункт 2.1 Применяем все эффекты всех перков. Параметры скейлов - данные из 1.2. Результаты - значение НА СКОЛЬКО дожен изменится тот или иной параметр записываются в буффер.
            CalculatePerksImpact(leftPlayer, rightPlayer);
            CalculatePerksImpact(rightPlayer, leftPlayer);
            //пункт 2.2 Прибавляем к значениям характеристик данные из буффера. Обновляем значения производных.
            UpdateData(leftPlayer);
            UpdateData(rightPlayer);
            //пункт 3. Получаем промежуточный набор данных, от которого отталкиваемся дальше

            //пункт 3.1 Применяем все эффекты всего снаряжения. Параметры скейлов - данные из 2.3. Результаты - значение НА СКОЛЬКО дожен изменится тот или иной показатель записываются в буффер.
            CalculateEquipmentImpact(leftPlayer, rightPlayer);
            CalculateEquipmentImpact(rightPlayer, leftPlayer);
            //пункт 3.2 Прибавляем к значениям характеристик данные из буффера. Обновляем значения производных.
            UpdateData(leftPlayer);
            UpdateData(rightPlayer);
            //пункт 4. Получаем все конечные значения параметров обоих персонажей
            leftPlayer.CharacterDataDataBuffer.Clear();
            rightPlayer.CharacterDataDataBuffer.Clear();
        }

    }
    
}
