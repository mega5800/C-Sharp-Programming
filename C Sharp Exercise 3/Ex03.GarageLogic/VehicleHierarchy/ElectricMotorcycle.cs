using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.GarageUtilities;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaximalBatteryChargeTime = 1.2f;
        private float m_CurrentBatteryCharge;

        public ElectricMotorcycle(List<object> i_InputParamsFromUser) : base(i_InputParamsFromUser)
        {
            this.m_CurrentBatteryCharge = (k_MaximalBatteryChargeTime * (float)i_InputParamsFromUser[6]) / 100;
            this.m_EnergyType = eEnergyType.ElectricPoweredVehicle;
        }

        public static void GetRequiredInputParamsForElectricMotorcycle(List<VehicleInputParam> i_ParamsListToFill)
        {
            Motorcycle.GetRequiredInputParamsForMotorcycle(i_ParamsListToFill);
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(float), "current battery charge in percents", 100));
        }

        public override void CheckEnergyData(object i_InputObject)
        {
            EnergyRefillLogic.Recharge(i_InputObject, k_MaximalBatteryChargeTime, ref this.m_CurrentBatteryCharge, ref this.m_CurrentEnergyPercentage);
        }

        public override string ToString()
        {
            string stringToReturn = string.Empty, currentChargeTime = string.Empty, maxChargeTime = string.Empty;

            maxChargeTime = string.Format("Maximal battery charge: {0} hours", k_MaximalBatteryChargeTime);
            currentChargeTime = string.Format("Current battery charge time: {0:0.00} hours", this.m_CurrentBatteryCharge);
            stringToReturn = string.Format("{0}{1}{2}", maxChargeTime, Environment.NewLine, currentChargeTime);

            return string.Format("{0}{1}{2}", base.ToString(), Environment.NewLine, stringToReturn);
        }
    }
}