using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    internal abstract class Car : Vehicle
    {
        private const int k_CarWheelsMaximalAirPressure = 32;
        private const int k_AmountOfTires = 4;
        private readonly eCarColor r_CarColor;
        private readonly eAmountOfDoors r_AmountOfDoors;

        public Car(List<object> i_InputParamsFromUser) : base(i_InputParamsFromUser, k_AmountOfTires, k_CarWheelsMaximalAirPressure)
        {
            this.r_CarColor = (eCarColor)i_InputParamsFromUser[4];
            this.r_AmountOfDoors = (eAmountOfDoors)i_InputParamsFromUser[5];
        }

        protected static void GetRequiredInputParamsForCar(List<VehicleInputParam> i_ParamsListToFill)
        {
            Vehicle.GetRequiredInputParamsForVehicle(i_ParamsListToFill);
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(int), "current tires air pressure", k_CarWheelsMaximalAirPressure));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(eCarColor), "car color", 0));
            i_ParamsListToFill.Add(new VehicleInputParam(typeof(eAmountOfDoors), "amount of doors", 0));
        }

        public abstract override void CheckEnergyData(object i_InputObject);

        public override string ToString()
        {
            string stringToReturn = string.Empty, carColor = string.Empty, amountOfDoors = string.Empty;

            carColor = string.Format("Car color: {0}", this.r_CarColor);
            amountOfDoors = string.Format("Amount of doors: {0}", this.r_AmountOfDoors);
            stringToReturn = string.Format("{0}{1}{2}", carColor, Environment.NewLine, amountOfDoors);

            return string.Format("{0}{1}{2}", base.ToString(), Environment.NewLine, stringToReturn);
        }
    }
}