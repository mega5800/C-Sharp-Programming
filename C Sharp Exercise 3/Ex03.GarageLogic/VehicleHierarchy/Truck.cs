using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.GarageUtilities;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    internal class Truck : Vehicle
    {
        private const int k_TruckWheelsMaximalAirPressure = 28;
        private const int k_AmountOfTires = 16;
        private const int k_FuelTankVolume = 120;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private readonly float r_TrunkVolume;
        private float m_CurrentAmountOfFuel;
        private readonly bool r_IsTransportingHazardousMaterials;

        public Truck(List<object> i_InputParamsFromUser) : base(i_InputParamsFromUser, k_AmountOfTires, k_TruckWheelsMaximalAirPressure)
        {
            this.r_IsTransportingHazardousMaterials = (bool)i_InputParamsFromUser[4];
            this.r_TrunkVolume = (float)i_InputParamsFromUser[5];
            this.m_CurrentAmountOfFuel = (k_FuelTankVolume * (float)i_InputParamsFromUser[6]) / 100;
            this.m_EnergyType = eEnergyType.FuelPoweredVehicle;
        }

        public eFuelType FuelType
        {
            get { return k_FuelType; }
        }

        public static void GetRequiredInputParamsForTruck(List<VehicleInputParam> i_ParamsListToFill)
        {
            Vehicle.GetRequiredInputParamsForVehicle(i_ParamsListToFill);
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(int), "current tires air pressure", k_TruckWheelsMaximalAirPressure));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(bool), string.Format("(y/n) {0}does the truck transport hazardous materials?", Environment.NewLine), 0));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(float), "trunk volume", 0));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(float), "current amount of fuel in percents", 100));
        }

        public override void CheckEnergyData(object i_InputObject)
        {
            EnergyRefillLogic.Refuel(i_InputObject, k_FuelType, k_FuelTankVolume, ref this.m_CurrentAmountOfFuel, ref this.m_CurrentEnergyPercentage);
        }

        public override string ToString()
        {
            string stringToReturn = string.Empty, tankVolume = string.Empty, fuelType = string.Empty, amountOfFuel = string.Empty, hazardousMaterials = string.Empty, trunkVolume = string.Empty;

            tankVolume = string.Format("Fuel tank volume: {0} liters", k_FuelTankVolume);
            amountOfFuel = string.Format("Current amount of fuel: {0:0.00} liters", this.m_CurrentAmountOfFuel);
            fuelType = string.Format("Fuel type: {0}", k_FuelType);
            trunkVolume = string.Format("Trunk volume is: {0}", this.r_TrunkVolume);
            if (this.r_IsTransportingHazardousMaterials)
            {
                hazardousMaterials = "This truck transports hazardous materials";
            }
            else
            {
                hazardousMaterials = "This truck does not transport hazardous materials";
            }

            stringToReturn = string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}", tankVolume, Environment.NewLine, amountOfFuel, fuelType, trunkVolume, hazardousMaterials);

            return string.Format("{0}{1}{2}", base.ToString(), Environment.NewLine, stringToReturn);
        }
    }
}