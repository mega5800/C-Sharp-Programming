using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.GarageUtilities;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    public abstract class Vehicle
    {
        private readonly List<Tire> r_TireList;
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        protected float m_CurrentEnergyPercentage;
        protected eEnergyType m_EnergyType;

        public Vehicle(List<object> i_InputParamsFromUser, int i_AmountOfTires, int i_MaxAirPressure)
        {
            this.r_ModelName = (string)i_InputParamsFromUser[0];
            this.r_LicenseNumber = (string)i_InputParamsFromUser[1];
            this.m_CurrentEnergyPercentage = (float)i_InputParamsFromUser[6];
            this.r_TireList = new List<Tire>(i_AmountOfTires);
            for (int i = 0; i < i_AmountOfTires; i++)
            {
                this.r_TireList.Add(new Tire((string)i_InputParamsFromUser[2], (int)i_InputParamsFromUser[3], i_MaxAirPressure));
            }
        }

        public abstract void CheckEnergyData(object i_InputObject);

        public eEnergyType EnergyType
        {
            get { return this.m_EnergyType; }
        }

        public string LicenseNumber
        {
            get { return this.r_LicenseNumber; }
        }

        protected static void GetRequiredInputParamsForVehicle(List<VehicleInputParam> i_ParamsListToFill)
        {
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(string), "model name", 0));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(string), "license number", 0));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(string), "tires manufacturer", 0));
        }

        public void PumpAllTires(float i_AirAmount, bool i_PumpToMax)
        {
            foreach (Tire tire in this.r_TireList)
            {
                if (i_PumpToMax)
                {
                    tire.PumpAirToMaxAirPressure();
                }
                else
                {
                    tire.PumpTireIfPossible(i_AirAmount);
                }
            }
        }

        public override string ToString()
        {
            string stringToReturn = string.Empty, modelName = string.Empty, licenseNumber = string.Empty, currentEnergyLevel = string.Empty, amountOfTires = string.Empty;

            modelName = string.Format("Model name: {0}", this.r_ModelName);
            licenseNumber = string.Format("License number: {0}", this.r_LicenseNumber);
            currentEnergyLevel = string.Format("Current energy level: {0:0.00}%", this.m_CurrentEnergyPercentage);
            amountOfTires = string.Format("Amount of tires: {0}", this.r_TireList.Count);
            stringToReturn = string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}", modelName, Environment.NewLine, licenseNumber, currentEnergyLevel, amountOfTires, this.r_TireList[0].ToString());

            return stringToReturn;
        }
    }
}