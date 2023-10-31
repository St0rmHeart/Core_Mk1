using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core_Mk1
{
    /// <summary>
    /// Абстрактынй класс содержащий общие для перков, эффектов, снаряжения и заклинаний свойства
    /// </summary>
    public abstract class BasicDevEntity
    {
        //_____________________НАЗВАНИЕ_____________________

        /// <summary>
        /// Название сущности
        /// </summary>
        protected String name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        

        //_____________________ОПИСАНИЕ_____________________

        /// <summary>
        /// Подробное описание сущности
        /// </summary>
        protected String description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //_____________________ТРЕБОВАНИЯ ДЛЯ ИСПОЛЬЗОВАНИЯ_____________________

        /// <summary>
        /// Минимальные значение характеристик, которыми должен обладать персонаж, для использования сущности
        /// </summary>
        protected Dictionary<Characteristic, int> requirementToUse = new Dictionary<Characteristic, int>();
        public void SetRequirementToUse(Characteristic characteristic, int requirement)
        {
            if (requirementToUse.ContainsKey(characteristic)) 
            {
                requirementToUse[characteristic] = requirement;
            }
            else
            {
                requirementToUse.Add(characteristic, requirement);
            }
        }
        public int GetRequirementToUse(Characteristic characteristic)
        {
            return requirementToUse[characteristic];
        }
        public int this[Characteristic param]
        {
            get
            {
                return requirementToUse[param];
            }
            set
            {
                requirementToUse[param] = value;
            }
        }




        //_____________________УСЛОВИЯ СРАБАТЫВАНИЯ_____________________

        /// <summary>
        /// Условия, связанны с уничтожением определённых камней определённого типа на доске
        /// </summary>
        protected Dictionary<TriggerType, (int value, char comparison) > trigger = new Dictionary<TriggerType, (int value, char comparison) >();
        public void SetTrigger(TriggerType stonetype, int value, char comparison)
        {
            if (trigger.ContainsKey(stonetype))
            {
                trigger[stonetype] = (value, comparison);
            }
            else
            {
                trigger.Add(stonetype, (value, comparison));
            }
        }
        public (int, char) GetTrigger(TriggerType stonetype)
        {
            return trigger[stonetype];
        }
        public (int, char) this[TriggerType param]
        {
            get
            {
                return trigger[param];
            }
            set
            {
                trigger[param] = value;
            }
        }

        //_____________________ЭФФЕКТ СРАБАТЫВАНИЯ_____________________
        /// <summary>
        /// Эффекты срабатывания со всеми параметрами 
        /// Как читать:
        /// </summary>
        protected Dictionary<Characteristic, Dictionary<Derivative, Effect>> effect = new Dictionary<Characteristic, Dictionary<Derivative, Effect>>();
        public void SetEffectValue(Characteristic characteristic, Derivative derivative, int value)
        {
            if (effect.ContainsKey(characteristic))
            {
                if (effect[characteristic].ContainsKey(derivative))
                {
                    effect[characteristic][derivative].Value = value;
                }
                else
                {
                    effect[characteristic].Add(derivative, new Effect(value));
                }
            }
            else
            {
                effect.Add(characteristic, new Dictionary<Derivative, Effect> { [derivative] = new Effect(value) });
            }
        }
        public Effect GetEffect(Characteristic characteristic, Derivative derivative)
        {
            return effect[characteristic][derivative];
        }
        public Effect this[Characteristic characteristic, Derivative derivative]
        {
            get
            {
                return effect[characteristic][derivative];
            }
        }
    }
}
