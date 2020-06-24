using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    internal abstract class Motorcycle : Vehicle
    {
        private const int k_MotorcycleWheelsMaximalAirPressure = 30;
        private const int k_AmountOfTires = 2;
        private readonly int r_EngineVolume;
        private readonly eMotorcycleLicenseType r_LicenseType;

        public Motorcycle(List<object> i_InputParamsFromUser) : base(i_InputParamsFromUser, k_AmountOfTires, k_MotorcycleWheelsMaximalAirPressure)
        {
            this.r_EngineVolume = (int)i_InputParamsFromUser[4];
            this.r_LicenseType = (eMotorcycleLicenseType)i_InputParamsFromUser[5];
        }

        protected static void GetRequiredInputParamsForMotorcycle(List<VehicleInputParam> i_ParamsListToFill)
        {
            Vehicle.GetRequiredInputParamsForVehicle(i_ParamsListToFill);
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(int), "current tires air pressure", k_MotorcycleWheelsMaximalAirPressure));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(int), "engine volume", 0));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(eMotorcycleLicenseType), "license type", 0));
        }

        public override abstract void CheckEnergyData(object i_InputObject);

        public override string ToString()
        {
            string stringToReturn = string.Empty, engineVolume = string.Empty, licenseType = string.Empty;

            engineVolume = string.Format("Engine volume: {0}", this.r_EngineVolume);
            licenseType = string.Format("License type: {0}", this.r_LicenseType);
            stringToReturn = string.Format("{0}{1}{2}", engineVolume, Environment.NewLine, licenseType);

            return string.Format("{0}{1}{2}", base.ToString(), Environment.NewLine, stringToReturn);
        }
    }
}