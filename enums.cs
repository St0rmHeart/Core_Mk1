using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// Все характеристики, необходимые для описания параметров персонажа, снаряжения, перков, заклинаний, эффектов и всего прочего
    /// </summary>
    public enum ECharacteristic
    {
        strength,       //сила
        dexterity,      //ловкость
        endurance,      //выносливость
        fire,           //мастерство огня
        water,          //мастерство земли
        air,            //мастерство воздуха
        earth,          //мастерство земли
    }
    /// <summary>
    /// Указатель для эфектов/скейлов, настоятся/обращаются они по/к владельцу или противнику
    /// </summary>
    public enum EPlayerType
    {
        self,           //обращаемся к владельцу
        enemy           //обращаемся к простивнику
    }
    /// <summary>
    /// Всевозможное производные параметры от каждой <see cref="ECharacteristic"/>.
    /// у КАЖДОЙ <see cref="ECharacteristic"/> может быть СВОЙ набор из <see cref="EDerivative"/>
    /// </summary>
    public enum EDerivative
    {
        value,          //значение характеристики
        maxMana,        //максимальный запас маны
        currentMana,    //текущий(стартовый) запас маны
        terminationMult,//мультипликатор эффекта уничтожения камня, связанного с характеристикой производной 
        addTurnChance,  //шанс доп хода при уничтожении камня, связанного с характеристикой производной
        resistance,     //сопротивления урону, связанному с этой характеристикой
        maxHealth,      //максимальный запас здоровья
        currentHealth,  //текущий(стартовый) запас здоровья
    }
    /// <summary>
    /// 6 "слотов" в в которые можно одевать снаряжение соответсвующего типа
    /// </summary>
    public enum EBodyPart
    {
        head,           //голова
        body,           //тело
        hands,          //руки
        feet,           //ноги
        weapon,         //оружие
        extra,          //экста
    }
}
