using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    public static class СentralGameplay
    {
        public static CharacteristicEnumeration leftPlayerData = new CharacteristicEnumeration();
        public static CharacteristicEnumeration rightPlayerData = new CharacteristicEnumeration();

        public static CharacteristicEnumeration leftPlayerDataBuffer = new CharacteristicEnumeration();
        public static CharacteristicEnumeration rightPlayerDataBuffer = new CharacteristicEnumeration();
        //инициируем начало сражения - задаём все стартовые параметры персонажа - игрока и персонажа - противника
        public static void InitGame(Character player, Character computer)
        {
            //пункт 1.2 Высчитываем все производные от базовых значений характеристик
            DerivativesCalculator derivativeCalculator;
            foreach (ECharacteristic characteristic in Enum.GetValues(typeof(ECharacteristic)))
            {
                derivativeCalculator = new DerivativesCalculator(characteristic, player[characteristic]);
                leftPlayerData.SetDerivatives(characteristic, derivativeCalculator.CalculateDerivatives());
                derivativeCalculator = new DerivativesCalculator(characteristic, computer[characteristic]);
                rightPlayerData.SetDerivatives(characteristic, derivativeCalculator.CalculateDerivatives());
            }
            //пункт 2. Получаем промежуточный набор данных, от которого отталкиваемся дальше


            //пункт 2.1 Применяем все эффекты всех перков. Параметры скейлов - данные из 1.2. Результаты - значение НА СКОЛЬКО дожен изменится тот или иной параметр записываются в буффер.
            CommonEffect currentEffect;
            foreach (Perk perk in player.UsedPerks)
            {
                foreach (EPlayerType target in perk.Effect.Keys)
                {
                    foreach (ECharacteristic targetChar in perk.Effect[target].Keys)
                    {
                        foreach (EDerivative targetDer in perk.Effect[target][targetChar].Keys)
                        {
                            currentEffect = perk.Effect[target][targetChar][targetDer];
                            if (target == EPlayerType.self)
                            {
                                leftPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(leftPlayerData, rightPlayerData));
                            }
                            else
                            {
                                rightPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(leftPlayerData, rightPlayerData));
                            }
                        }
                    }
                }
            }
            foreach (Perk perk in computer.UsedPerks)
            {
                foreach (EPlayerType target in perk.Effect.Keys)
                {
                    foreach (ECharacteristic targetChar in perk.Effect[target].Keys)
                    {
                        foreach (EDerivative targetDer in perk.Effect[target][targetChar].Keys)
                        {
                            currentEffect = perk.Effect[target][targetChar][targetDer];
                            if (target == EPlayerType.self)
                            {
                                rightPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(rightPlayerData, leftPlayerData));
                            }
                            else
                            {
                                leftPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(rightPlayerData, leftPlayerData));
                            }
                        }
                    }
                }
            }
            
            
            //пункт 2.2 Прибавляем к значениям характеристик данные из буффера. Обновляем значения производных.
            foreach (ECharacteristic characteristic in Enum.GetValues(typeof(ECharacteristic)))
            {
                if (leftPlayerDataBuffer.StatList.ContainsKey(characteristic) && leftPlayerDataBuffer.StatList[characteristic].ContainsKey(EDerivative.value))
                {
                    derivativeCalculator = new DerivativesCalculator(characteristic, Convert.ToInt32(leftPlayerData[characteristic][EDerivative.value] + leftPlayerDataBuffer[characteristic][EDerivative.value]));
                    leftPlayerData.SetDerivatives(characteristic, derivativeCalculator.CalculateDerivatives());
                    leftPlayerDataBuffer[characteristic].Remove(EDerivative.value);
                }
                if (rightPlayerDataBuffer.StatList.ContainsKey(characteristic) && rightPlayerDataBuffer.StatList[characteristic].ContainsKey(EDerivative.value))
                {
                    derivativeCalculator = new DerivativesCalculator(characteristic, Convert.ToInt32(rightPlayerData[characteristic][EDerivative.value] + rightPlayerDataBuffer[characteristic][EDerivative.value]));
                    rightPlayerData.SetDerivatives(characteristic, derivativeCalculator.CalculateDerivatives());
                    rightPlayerData[characteristic].Remove(EDerivative.value);
                }
            }


            //пункт 2.3 Прибавляем к значениям производных данные из буффера.
            foreach (ECharacteristic characteristic in leftPlayerDataBuffer.StatList.Keys)
            {
                foreach (EDerivative derivative in leftPlayerDataBuffer[characteristic].Keys)
                {
                    leftPlayerData[characteristic][derivative] = leftPlayerData[characteristic][derivative] + leftPlayerDataBuffer[characteristic][derivative];
                }
            }
            //leftPlayerDataBuffer.StatList.Clear();

            foreach (ECharacteristic characteristic in rightPlayerDataBuffer.StatList.Keys)
            {
                foreach (EDerivative derivative in rightPlayerDataBuffer[characteristic].Keys)
                {
                    rightPlayerData[characteristic][derivative] = rightPlayerData[characteristic][derivative] + rightPlayerDataBuffer[characteristic][derivative];
                }
            }
            //rightPlayerDataBuffer.StatList.Clear();
            //пункт 3. Получаем промежуточный набор данных, от которого отталкиваемся дальше


            //пункт 3.1 Применяем все эффекты всего снаряжения. Параметры скейлов - данные из 2.3. Результаты - значение НА СКОЛЬКО дожен изменится тот или иной показатель записываются в буффер.
            foreach (Equipment equipment in player.UsedEquipment.Values)
            {
                if (equipment != null)
                {
                    foreach (EPlayerType target in equipment.Effect.Keys)
                    {
                        foreach (ECharacteristic targetChar in equipment.Effect[target].Keys)
                        {
                            foreach (EDerivative targetDer in equipment.Effect[target][targetChar].Keys)
                            {
                                currentEffect = equipment.Effect[target][targetChar][targetDer];
                                if (target == EPlayerType.self)
                                {
                                    leftPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(leftPlayerData, rightPlayerData));
                                }
                                else
                                {
                                    rightPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(leftPlayerData, rightPlayerData));
                                }
                            }
                        }
                    }
                }
            }
            foreach (Equipment equipment in computer.UsedEquipment.Values)
            {
                if (equipment != null)
                {
                    foreach (EPlayerType target in equipment.Effect.Keys)
                    {
                        foreach (ECharacteristic targetChar in equipment.Effect[target].Keys)
                        {
                            foreach (EDerivative targetDer in equipment.Effect[target][targetChar].Keys)
                            {
                                currentEffect = equipment.Effect[target][targetChar][targetDer];
                                if (target == EPlayerType.self)
                                {
                                    rightPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(rightPlayerData, leftPlayerData));
                                }
                                else
                                {
                                    leftPlayerDataBuffer.SetOrIncreaseStat(targetChar, targetDer, currentEffect.CalculateDelta(rightPlayerData, leftPlayerData));
                                }
                            }
                        }
                    }
                }
            }


            //пункт 3.2 Прибавляем к значениям характеристик данные из буффера. Обновляем значения производных.
            foreach (ECharacteristic characteristic in Enum.GetValues(typeof(ECharacteristic)))
            {
                if (leftPlayerDataBuffer.StatList.ContainsKey(characteristic) && leftPlayerDataBuffer.StatList[characteristic].ContainsKey(EDerivative.value))
                {
                    derivativeCalculator = new DerivativesCalculator(characteristic, Convert.ToInt32(leftPlayerData[characteristic][EDerivative.value] + leftPlayerDataBuffer[characteristic][EDerivative.value]));
                    leftPlayerData.SetDerivatives(characteristic, derivativeCalculator.CalculateDerivatives());
                    leftPlayerDataBuffer[characteristic].Remove(EDerivative.value);
                }
                if (rightPlayerDataBuffer.StatList.ContainsKey(characteristic) && rightPlayerDataBuffer.StatList[characteristic].ContainsKey(EDerivative.value))
                {
                    derivativeCalculator = new DerivativesCalculator(characteristic, Convert.ToInt32(rightPlayerData[characteristic][EDerivative.value] + rightPlayerDataBuffer[characteristic][EDerivative.value]));
                    rightPlayerData.SetDerivatives(characteristic, derivativeCalculator.CalculateDerivatives());
                    rightPlayerData[characteristic].Remove(EDerivative.value);
                }
            }


            //пункт 3.3 прибавляем к значениям производных данные из буффера.
            foreach (ECharacteristic characteristic in leftPlayerDataBuffer.StatList.Keys)
            {
                foreach (EDerivative derivative in leftPlayerDataBuffer[characteristic].Keys)
                {
                    leftPlayerData[characteristic][derivative] = leftPlayerData[characteristic][derivative] + leftPlayerDataBuffer[characteristic][derivative];
                }
            }
            leftPlayerDataBuffer.StatList.Clear();

            foreach (ECharacteristic characteristic in rightPlayerDataBuffer.StatList.Keys)
            {
                foreach (EDerivative derivative in rightPlayerDataBuffer[characteristic].Keys)
                {
                    rightPlayerData[characteristic][derivative] = rightPlayerData[characteristic][derivative] + rightPlayerDataBuffer[characteristic][derivative];
                }
            }
            rightPlayerDataBuffer.StatList.Clear();
            //пункт 4. Получаем все конечные значения сложной формы представления
        }

    }
    
}
