using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.GarageUtilities;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    internal class FuelMotorcycle : Motorcycle
    {
        private const int k_FuelTankVolume = 7;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private float m_CurrentAmountOfFuel;

        public FuelMotorcycle(List<object> i_InputParamsFromUser) : base(i_InputParamsFromUser)
        {
            this.m_CurrentAmountOfFuel = (k_FuelTankVolume * (float)i_InputParamsFromUser[6]) / 100;
            this.m_EnergyType = eEnergyType.FuelPoweredVehicle;
        }

        public eFuelType FuelType
        {
            get { return k_FuelType; }
        }

        public static void GetRequiredInputParamsForFuelMotorcycle(List<VehicleInputParam> i_ParamsListToFill)
        {
            Motorcycle.GetRequiredInputParamsForMotorcycle(i_ParamsListToFill);
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(float), "current amount of fuel in percents", 100));
        }

        public override void CheckEnergyData(object i_InputObject)
        {
            EnergyRefillLogic.Refuel(i_InputObject, k_FuelType, k_FuelTankVolume, ref this.m_CurrentAmountOfFuel, ref this.m_CurrentEnergyPercentage);
        }

        public override string ToString()
        {
            string stringToReturn = string.Empty, tankVolume = string.Empty, fuelType = string.Empty, amountOfFuel = string.Empty;

            tankVolume = string.Format("Fuel tank volume: {0} liters", k_FuelTankVolume);
            fuelType = string.Format("Fuel type: {0}", k_FuelType);
            amountOfFuel = string.Format("Current amount of fuel: {0:0.00} liters", this.m_CurrentAmountOfFuel);
            stringToReturn = string.Format("{0}{1}{2}{1}{3}", tankVolume, Environment.NewLine, fuelType, amountOfFuel);

            return string.Format("{0}{1}{2}", base.ToString(), Environment.NewLine, stringToReturn);
        }
    }
}