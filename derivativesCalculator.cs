using Core_Mk1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Core_Mk1
{
    /// <summary>
    /// ����������� �����������
    /// </summary>
    public abstract class DerivativesCalculator
    {
        //_____________________�����������_____________________

        /// <summary>
        /// ���������� ����������� ��� �������� ��������
        /// </summary>
        /// <param name="value"> �������� ������������� ��������������</param>
        protected DerivativesCalculator(int value, bool isInitGame) { this.value = value; this.isInitGame = isInitGame; }

        //_____________________����_____________________

        //������ ��� �������� ��������������
        protected double value;
        //������, ����������, �������� �� ������ ����������� � ������ ������������� ��������
        protected bool isInitGame;

        //_____________________������_____________________

        /// <summary>
        /// ������� ����� ���� ����������� ��� ���������� ��������������, ����������� �� � ��������
        /// </summary>
        /// <param name="characteristic">��������������, ����� ����������� ������� ����� ����������</param>
        /// <param name="value">�������� ��������������</param>
        /// <returns>������� ����������� �����������</returns>
        public static Dictionary<EDerivative, double> GetDerivatives(ECharacteristic characteristic, int value, bool isInitGame)
        {
            switch (characteristic)
            {
                case ECharacteristic.strength:
                    return new StrengthModule(value, isInitGame).CalculateDerivatives();
                case ECharacteristic.dexterity:
                    return new DexterityModule(value, isInitGame).CalculateDerivatives();
                case ECharacteristic.endurance:
                    return new EnduranceModule(value, isInitGame).CalculateDerivatives();
                case ECharacteristic.fire:
                    return new FireModule(value, isInitGame).CalculateDerivatives();
                case ECharacteristic.water:
                    return new WaterModule(value, isInitGame).CalculateDerivatives();
                case ECharacteristic.air:
                    return new AirModule(value, isInitGame).CalculateDerivatives();
                case ECharacteristic.earth:
                    return new EarthModule(value, isInitGame).CalculateDerivatives();
                default: return null;
            }
        }

        /// <summary>
        ///������ �����������, ������� ��������� ������ �������
        /// </summary>
        /// <returns>������� ����������� �����������</returns>
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
    public class StrengthModule : DerivativesCalculator
    {
        public StrengthModule(int value, bool isInitGame) : base(value, isInitGame) { }
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
    public class DexterityModule : DerivativesCalculator
    {
        public DexterityModule(int value, bool isInitGame) : base(value, isInitGame) { }
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
    public class EnduranceModule : DerivativesCalculator
    {
        public EnduranceModule(int value, bool isInitGame) : base(value, isInitGame) { }
        public override Dictionary<EDerivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<EDerivative, double>
            {
                { EDerivative.value, value },
                { EDerivative.terminationMult, CalculateTerminationMult() },
                { EDerivative.addTurnChance, CalculateAddTurnChance() },
                { EDerivative.maxHealth, CalculateMaxHealth()},
            };
            if (isInitGame) { derivatives.Add(EDerivative.currentHealth, derivatives[EDerivative.maxHealth]); }
            
            return derivatives;
        }
        public double CalculateMaxHealth()
        {
            double result;
            result = 1;
            return result;
        }
    }
    public abstract class UniversalElementalModule : DerivativesCalculator
    {
        public UniversalElementalModule(int value, bool isInitGame) : base(value, isInitGame) { }
        public override Dictionary<EDerivative, double> CalculateDerivatives()
        {
            var derivatives = new Dictionary<EDerivative, double>
            {
                { EDerivative.value, value },
                { EDerivative.terminationMult, CalculateTerminationMult() },
                { EDerivative.addTurnChance, CalculateAddTurnChance() },
                { EDerivative.maxMana, CalculateMaxMana() },
            };
            if (isInitGame) { derivatives.Add(EDerivative.currentMana, CalculateStartMana()); }
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
    public class FireModule : UniversalElementalModule
    {
        public FireModule(int value, bool isInitGame) : base(value, isInitGame) { }
    }
    public class WaterModule : UniversalElementalModule
    {
        public WaterModule(int value, bool isInitGame) : base(value, isInitGame) { }
    }
    public class AirModule : UniversalElementalModule
    {
        public AirModule(int value, bool isInitGame) : base(value, isInitGame) { }
    }
    public class EarthModule : UniversalElementalModule
    {
        public EarthModule(int value, bool isInitGame) : base(value, isInitGame) { }
    }
}
