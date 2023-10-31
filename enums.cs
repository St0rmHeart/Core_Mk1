using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// виды камней, которые могут быть на игровой доске
    /// </summary>
    public enum TriggerType
    {
        passive,
        skull,
        fire,
        water,
        air,
        earth,
        exp,
        gold
    }
    /// <summary>
    /// Основные характеристики персонажа, определяющие его развитие
    /// </summary>
    public enum Characteristic
    {
        strength,
        dexterity,
        endurance,
        fire,
        water,
        air,
        earth,
        level,
    }

    /// <summary>
    /// Производные от <see cref="Characteristic"/> характеристики, 
    /// </summary>
    public enum Derivative
    {
        value,
        maxMana,
        currentMana,
        terminationMult,
        addTurnChance,
        resistance,
        maxHealth,      
        currentHealth,  
    }
    /// <summary>
    /// 6 "слотов" в в которые можно одевать снаряжение соответсвующего типа
    /// </summary>
    public enum BodyPart
    {
        head,
        body,
        hands,
        feet,
        weapon,
        extra,
    }
}
