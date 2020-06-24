namespace Ex03.GarageLogic.GarageUtilities
{
    public struct VehicleOwner
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;

        public VehicleOwner(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            this.r_OwnerName = i_OwnerName;
            this.r_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public string OwnerName
        {
            get { return this.r_OwnerName; }
        }
    }
}