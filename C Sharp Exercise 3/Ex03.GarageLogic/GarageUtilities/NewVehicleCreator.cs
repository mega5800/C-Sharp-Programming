using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.VehicleHierarchy;

namespace Ex03.GarageLogic.GarageUtilities
{
    public class NewVehicleCreator
    {
        private readonly List<string> r_SupportedVehiclesList;
        private readonly List<VehicleInputParam> r_RequiredParams;
        private readonly Garage r_GarageInstance;

        public NewVehicleCreator(Garage i_GarageInstance)
        {
            this.r_GarageInstance = i_GarageInstance;
            this.r_SupportedVehiclesList = new List<string>(5) { "Fuel powered motorcycle", "Electricity powered motorcycle", "Fuel powered car", "Electricity powered car", "Truck" };
            this.r_RequiredParams = new List<VehicleInputParam>();
        }

        public List<string> VehicleBasicInfoList
        {
            get { return this.r_SupportedVehiclesList; }
        }

        public List<VehicleInputParam> GetVehicleInputParamList(int i_ChosenValue)
        {
            this.r_RequiredParams.Clear();
            switch (i_ChosenValue)
            {
                case 1:
                    FuelMotorcycle.GetRequiredInputParamsForFuelMotorcycle(this.r_RequiredParams);
                    break;
                case 2:
                    ElectricMotorcycle.GetRequiredInputParamsForElectricMotorcycle(this.r_RequiredParams);
                    break;
                case 3:
                    FuelCar.GetRequiredInputParamsForFuelCar(this.r_RequiredParams);
                    break;
                case 4:
                    ElectricCar.GetRequiredInputParamsForElectricCar(this.r_RequiredParams);
                    break;
                case 5:
                    Truck.GetRequiredInputParamsForTruck(this.r_RequiredParams);
                    break;
            }

            return this.r_RequiredParams;
        }

        public bool CreateVehicle(int i_OptionSelected, List<object> i_VehicleInfoParams, VehicleOwner i_VehicleOwner)
        {
            bool resultToReturn = true;
            Vehicle vehicleToCreate = null;
            GarageVehicle vehicleToAddToGarage = null;

            resultToReturn = checkIfVehicleIsNotInGarage((string)i_VehicleInfoParams[1]);
            if (resultToReturn)
            {
                switch (i_OptionSelected)
                {
                    case 1:
                        vehicleToCreate = new FuelMotorcycle(i_VehicleInfoParams);
                        break;
                    case 2:
                        vehicleToCreate = new ElectricMotorcycle(i_VehicleInfoParams);
                        break;
                    case 3:
                        vehicleToCreate = new FuelCar(i_VehicleInfoParams);
                        break;
                    case 4:
                        vehicleToCreate = new ElectricCar(i_VehicleInfoParams);
                        break;
                    case 5:
                        vehicleToCreate = new Truck(i_VehicleInfoParams);
                        break;
                }

                vehicleToAddToGarage = new GarageVehicle(i_VehicleOwner, vehicleToCreate);
                this.r_GarageInstance.AddNewVehicleToGarage(vehicleToAddToGarage);
            }

            return resultToReturn;
        }

        private bool checkIfVehicleIsNotInGarage(string i_LicenseNumberToCompare)
        {
            bool resultToReturn = true;
            List<GarageVehicle> vehicleList = this.r_GarageInstance.GarageVehiclesList;

            foreach (GarageVehicle vehicle in vehicleList)
            {
                if (i_LicenseNumberToCompare == vehicle.StoredVehicle.LicenseNumber)
                {
                    resultToReturn = false;
                    vehicle.VehicleRepairState = eVehicleRepairStates.WorkInProgress;
                }
            }

            return resultToReturn;
        }
    }
}