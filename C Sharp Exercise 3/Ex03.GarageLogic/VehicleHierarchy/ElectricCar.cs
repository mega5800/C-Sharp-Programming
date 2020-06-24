using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.GarageUtilities;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    internal class ElectricCar : Car
    {
        private const float k_MaximalBatteryChargeTime = 2.1f;
        private float m_CurrentBatteryCharge;

        public ElectricCar(List<object> i_InputParamsFromUser) : base(i_InputParamsFromUser)
        {
            this.m_CurrentBatteryCharge = (k_MaximalBatteryChargeTime * (float)i_InputParamsFromUser[6]) / 100;
            this.m_EnergyType = eEnergyType.ElectricPoweredVehicle;
        }

        public static void GetRequiredInputParamsForElectricCar(List<VehicleInputParam> i_ParamsListToFill)
        {
            Car.GetRequiredInputParamsForCar(i_ParamsListToFill);
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(float), "current battery charge in percents", 100));
        }

        public override void CheckEnergyData(object i_InputObject)
        {
            EnergyRefillLogic.Recharge(i_InputObject, k_MaximalBatteryChargeTime, ref this.m_CurrentBatteryCharge, ref this.m_CurrentEnergyPercentage);
        }

        public override string ToString()
        {
            string stringToReturn = string.Empty, currentCharge = string.Empty, maxChargeTime = string.Empty;

            currentCharge = string.Format("Current battery charge: {0:0.00} hours", this.m_CurrentBatteryCharge);
            maxChargeTime = string.Format("Maximal battery charge time: {0} hours", k_MaximalBatteryChargeTime);
            stringToReturn = string.Format("{0}{1}{2}", maxChargeTime, Environment.NewLine, currentCharge);

            return string.Format("{0}{1}{2}", base.ToString(), Environment.NewLine, stringToReturn);
        }
    }
}