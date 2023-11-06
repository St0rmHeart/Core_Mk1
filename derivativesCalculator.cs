using Core_Mk1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    public interface IDerCalculator
    {
        Dictionary<EDerivative, double> CalculateDerivatives();
    }
    public abstract class UniversalCharacteristicDerivativescCalculatorModule : IDerCalculator
    {
        protected UniversalCharacteristicDerivativescCalculatorModule(int value) { this.value = value; }
        protected double value;
        public abstract Dictionary<EDerivative, double> CalculateDerivatives();
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
    public class StrengthDerivativesCalculatorModule : UniversalCharacteristicDerivativescCalculatorModule
    {
        public StrengthDerivativesCalculatorModule(int value) : base(value) { }
        public double CalculateResistance()
        {
            double result;
            result = 1;
            return result;
        }
        public override Dictionary<EDerivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<EDerivative, double>
            {
                { EDerivative.value, value },
                { EDerivative.terminationMult, CalculateTerminationMult() },
                { EDerivative.addTurnChance, CalculateAddTurnChance() },
                { EDerivative.resistance, CalculateResistance() },
            };
            return derivatives;
        }
    }
    public class DexterityDerivativesCalculatorModule : UniversalCharacteristicDerivativescCalculatorModule
    {
        public DexterityDerivativesCalculatorModule(int value) : base(value) { }
        public override Dictionary<EDerivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<EDerivative, double>
            {
                { EDerivative.value, value },
                { EDerivative.terminationMult, CalculateTerminationMult() },
                { EDerivative.addTurnChance, CalculateAddTurnChance() }
            };
            return derivatives;
        }
    }
    public class EnduranceDerivativesCalculatorModule : UniversalCharacteristicDerivativescCalculatorModule
    {
        public EnduranceDerivativesCalculatorModule(int value) : base(value) { }
        public override Dictionary<EDerivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<EDerivative, double>
            {
                { EDerivative.value, value },
                { EDerivative.terminationMult, CalculateTerminationMult() },
                { EDerivative.addTurnChance, CalculateAddTurnChance() },
                { EDerivative.maxHealth, CalculateMaxHealth()},
                { EDerivative.currentHealth, CalculateMaxHealth()}
            };
            return derivatives;
        }
        public double CalculateMaxHealth()
        {
            double result;
            result = 1;
            return result;
        }
    }
    public abstract class UniversalElementalCharacteristicDerivativesCalculatorModule : UniversalCharacteristicDerivativescCalculatorModule
    {
        public UniversalElementalCharacteristicDerivativesCalculatorModule(int value) : base(value) { }
        public override Dictionary<EDerivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<EDerivative, double>
            {
                { EDerivative.value, value },
                { EDerivative.terminationMult, CalculateTerminationMult() },
                { EDerivative.addTurnChance, CalculateAddTurnChance() },
                { EDerivative.maxMana, CalculateMaxMana() },
                { EDerivative.currentMana, CalculateStartMana() }
            };
            return derivatives;
        }
        public virtual double CalculateMaxMana()
        {
            double result;
            result = 1;
            return result;
        }
        public virtual double CalculateStartMana()
        {
            double result;
            result = 1;
            return result;
        }
    }
    public class FireDerivativesCalculatorModule : UniversalElementalCharacteristicDerivativesCalculatorModule
    {
        public FireDerivativesCalculatorModule(int value) : base(value) { }
    }
    public class WaterDerivativesCalculatorModule : UniversalElementalCharacteristicDerivativesCalculatorModule
    {
        public WaterDerivativesCalculatorModule(int value) : base(value) { }
    }
    public class AirDerivativesCalculatorModule : UniversalElementalCharacteristicDerivativesCalculatorModule
    {
        public AirDerivativesCalculatorModule(int value) : base(value) { }
    }
    public class EarthDerivativesCalculatorModule : UniversalElementalCharacteristicDerivativesCalculatorModule
    {
        public EarthDerivativesCalculatorModule(int value) : base(value) { }
    }
    public class DerivativesCalculator
    {
        public IDerCalculator IDerCalculatorModule;
        public DerivativesCalculator(ECharacteristic characteristic, int value)
        {
            switch (characteristic)
            {
                case ECharacteristic.strength:
                    IDerCalculatorModule = new StrengthDerivativesCalculatorModule(value);
                    break;
                case ECharacteristic.dexterity:
                    IDerCalculatorModule = new DexterityDerivativesCalculatorModule(value);
                    break;
                case ECharacteristic.endurance:
                    IDerCalculatorModule = new EnduranceDerivativesCalculatorModule(value);
                    break;
                case ECharacteristic.fire:
                    IDerCalculatorModule = new FireDerivativesCalculatorModule(value);
                    break;
                case ECharacteristic.water:
                    IDerCalculatorModule = new WaterDerivativesCalculatorModule(value);
                    break;
                case ECharacteristic.air:
                    IDerCalculatorModule = new AirDerivativesCalculatorModule(value);
                    break;
                case ECharacteristic.earth:
                    IDerCalculatorModule = new EarthDerivativesCalculatorModule(value);
                    break;
            }
        }
        public Dictionary<EDerivative, double> CalculateDerivatives()
        {
        return IDerCalculatorModule.CalculateDerivatives();
        }
    }
}
