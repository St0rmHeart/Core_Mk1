using Core_Mk1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    public abstract class UniCharacteristic
    {
        protected double value;
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public UniCharacteristic(double value)
        {
            this.value = value;
        }
        public abstract Dictionary<Derivative, double> CalculateDerivatives();
        public virtual double CalculateTerminationMult()
        {
            double result = 1;

            return result;
        }
        public virtual double CalculateAddTurnChance()
        {
            double result = 1;

            return result;
        }

    }
    public class Strength : UniCharacteristic
    {
        public Strength(double value) : base(value)
        {
        }
        public override Dictionary<Derivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<Derivative, double>
            {
                { Derivative.value, value },
                { Derivative.terminationMult, CalculateTerminationMult() },
                { Derivative.addTurnChance, CalculateAddTurnChance() },
                { Derivative.resistance, CalculateResistance() },
            };
            return derivatives;
        }
        public override double CalculateTerminationMult()
        {
            double result;
            result = value/4;
            return result;
        }
        public override double CalculateAddTurnChance()
        {
            double result;
            result = value/5;
            return result;
        }
        public double CalculateResistance()
        {
            double result;
            result = value/5;
            return result;
        }
    }
    public class Dexterity : UniCharacteristic
    {
        protected double gold;
        public Dexterity(double value, double gold) : base(value)
        {
            this.gold = gold;
        }
        public override Dictionary<Derivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<Derivative, double>
            {
                { Derivative.value, value },
                { Derivative.terminationMult, CalculateTerminationMult() },
                { Derivative.addTurnChance, CalculateAddTurnChance() }
            };
            return derivatives;
        }
        public override double CalculateTerminationMult()
        {
            double result;
            result = value/3;
            return result;
        }
        public override double CalculateAddTurnChance()
        {
            double result;
            result = value/3;
            return result;
        }
    }
    public class Endurance : UniCharacteristic
    {
        protected double xp;
        public Endurance(double value, double xp) : base(value)
        {
            this.xp = xp;
        }
        public override Dictionary<Derivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<Derivative, double>
            {
                { Derivative.value, value },
                { Derivative.terminationMult, CalculateTerminationMult() },
                { Derivative.addTurnChance, CalculateAddTurnChance() },
                { Derivative.maxHealth, CalculateMaxHealth()},
                { Derivative.currentHealth, CalculateMaxHealth()}
            };
            return derivatives;
        }
        public override double CalculateTerminationMult()
        {
            double result;
            result = value/3;
            return result;
        }
        public override double CalculateAddTurnChance()
        {
            double result;
            result = value/3;
            return result;
        }
        public double CalculateMaxHealth()
        {
            double result;
            result = 100 + value*2;
            return result;
        }

    }
    public class ElementalUniCharacteristic : UniCharacteristic
    {
        public ElementalUniCharacteristic(double value) : base(value)
        {
        }
        public override Dictionary<Derivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<Derivative, double>
            {
                { Derivative.value, value },
                { Derivative.terminationMult, CalculateTerminationMult() },
                { Derivative.addTurnChance, CalculateAddTurnChance() },
                { Derivative.maxMana, CalculateMaxMana() },
                { Derivative.currentMana, CalculateCurrentMana() }
            };
            return derivatives;
        }
        public override double CalculateTerminationMult()
        {
            double result;
            result = value/4;
            return result;
        }
        public override double CalculateAddTurnChance()
        {
            double result;
            result = value/5;
            return result;
        }
        public virtual double CalculateMaxMana()
        {
            double result;
            result = 10 + value/2;
            return result;
        }
        public virtual double CalculateCurrentMana()
        {
            double result;
            result = value/5;
            return result;
        }
    }
    public class Fire : ElementalUniCharacteristic
    {
        public Fire(double value) : base(value)
        {
        }
    }
    public class Water : ElementalUniCharacteristic
    {
        public Water(double value) : base(value)
        {
        }
    }
    public class Air : ElementalUniCharacteristic
    {
        public Air(double value) : base(value)
        {
        }
    }
    public class Earth : ElementalUniCharacteristic
    {
        public Earth(double value) : base(value)
        {
        }
    }
}