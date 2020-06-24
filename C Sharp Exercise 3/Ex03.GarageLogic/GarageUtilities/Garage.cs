using System.Collections.Generic;

namespace Ex03.GarageLogic.GarageUtilities
{
    public class Garage
    {
        private readonly List<GarageVehicle> r_GarageVehiclesList;

        public Garage()
        {
            this.r_GarageVehiclesList = new List<GarageVehicle>();
        }

        public List<GarageVehicle> GarageVehiclesList
        {
            get { return this.r_GarageVehiclesList; }
        }

        public void AddNewVehicleToGarage(GarageVehicle i_VehicleToAdd)
        {
            this.r_GarageVehiclesList.Add(i_VehicleToAdd);
        }
    }
}