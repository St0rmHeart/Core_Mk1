using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
/*
    /// <summary>
    /// Содержит ВСЕ конечные значения ВСЕХ характеристик, полученных от Character + производные.
    /// На этапе формирования применяюься "пассивные" эффекты
    /// </summary>
    public class CharacterCard
    {
        protected string name;
        protected Dictionary<SimpleStat, int> simpleStat = new Dictionary<SimpleStat, int>();
        protected Dictionary<ComplexStat, Dictionary<Derivative, double>> complexStat = new Dictionary<ComplexStat, Dictionary<Derivative, double>>();
        public CharacterCard(Character character) 
        {
            name = character.Name;

            SimpleStat currentstat = SimpleStat.endurance;
            simpleStat.Add(currentstat, character[(Characteristic)Enum.Parse(typeof(Characteristic), currentstat.ToString())]);
            
            simpleStat.Add(SimpleStat.maxHealth, CalculateMaxHealth(simpleStat[currentstat]));
            simpleStat.Add(SimpleStat.currentHealth, simpleStat[SimpleStat.maxHealth]);
            
            currentstat= SimpleStat.gold;
            simpleStat.Add(currentstat, character[(Characteristic)Enum.Parse(typeof(Characteristic), currentstat.ToString())]);
            
            currentstat = SimpleStat.xp;
            simpleStat.Add(currentstat, character[(Characteristic)Enum.Parse(typeof(Characteristic), currentstat.ToString())]);
            
            CalculateComplexStatDerivatives(character);
        }
        public int CalculateMaxHealth(int masteryValue)
        {
            return 50 + masteryValue * 4;
        }
        public void CalculateComplexStatDerivatives(Character character)
        {
            foreach (ComplexStat masteryStat in Enum.GetValues(typeof(ComplexStat)))
            {
                int masteryValue = character[(Characteristic)Enum.Parse(typeof(Characteristic), masteryStat.ToString())];
                complexStat.Add(masteryStat, new Dictionary<Derivative, double> { [Derivative.value] = masteryValue });
                switch (masteryStat)
                {
                    case ComplexStat.strength:
                        complexStat[masteryStat].Add(Derivative.terminationMult, CalculateTerminationMult(masteryValue));
                        complexStat[masteryStat].Add(Derivative.addTurnChance, CalculateAddTurnChance(masteryValue));
                        complexStat[masteryStat].Add(Derivative.resistance, CalculateResistance(masteryValue));
                        break;
                    case ComplexStat.dexterity:
                    case ComplexStat.endurance:
                        complexStat[masteryStat].Add(Derivative.terminationMult, CalculateTerminationMult(masteryValue));
                        complexStat[masteryStat].Add(Derivative.addTurnChance, CalculateAddTurnChance(masteryValue));
                        break;
                    case ComplexStat.fire:
                    case ComplexStat.water:
                    case ComplexStat.air:
                    case ComplexStat.earth:
                        complexStat[masteryStat].Add(Derivative.maxMana, CalculateMaxMana(masteryValue));
                        complexStat[masteryStat].Add(Derivative.currentMana, CalculateCurrentMana(masteryValue));
                        complexStat[masteryStat].Add(Derivative.terminationMult, CalculateTerminationMult(masteryValue));
                        complexStat[masteryStat].Add(Derivative.addTurnChance, CalculateAddTurnChance(masteryValue));
                        complexStat[masteryStat].Add(Derivative.resistance, CalculateResistance(masteryValue));
                        break;
                    default:
                        break;
                }
            }
        }
        
        public void ShowAllData()
        {

        }
    }
*/
}
