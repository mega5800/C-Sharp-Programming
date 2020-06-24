using System;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.VehicleHierarchy;

namespace Ex03.GarageLogic.GarageUtilities
{
    public class GarageVehicle
    {
        private readonly VehicleOwner r_VehicleOwner;
        private readonly Vehicle r_StoredVehicle;
        private eVehicleRepairStates m_VehicleRepairState;

        public GarageVehicle(VehicleOwner i_VehicleOwner, Vehicle i_Vehicle)
        {
            this.r_VehicleOwner = i_VehicleOwner;
            this.r_StoredVehicle = i_Vehicle;
            this.m_VehicleRepairState = eVehicleRepairStates.WorkInProgress;
        }

        public Vehicle StoredVehicle
        {
            get { return this.r_StoredVehicle; }
        }

        public eVehicleRepairStates VehicleRepairState
        {
            get { return this.m_VehicleRepairState; }
            set { this.m_VehicleRepairState = value; }
        }

        public override string ToString()
        {
            string stringToReturn = string.Empty, userString = string.Empty, repairState = string.Empty;

            userString = string.Format("Owner name: {0}", this.r_VehicleOwner.OwnerName);
            repairState = string.Format("Repair state: {0}", this.m_VehicleRepairState);
            stringToReturn = string.Format("{0}{1}{2}{1}{3}", userString, Environment.NewLine, repairState, this.r_StoredVehicle.ToString());

            return stringToReturn;
        }
    }
}