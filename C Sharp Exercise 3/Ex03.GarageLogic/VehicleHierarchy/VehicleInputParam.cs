using System;

namespace Ex03.GarageLogic.VehicleHierarchy
{
    public class VehicleInputParam
    {
        private readonly Type r_ParamType;
        private readonly int r_UpperLimit;
        private readonly string r_Description;

        public VehicleInputParam(Type i_ParamType, string i_Description, int i_UpperLimit)
        {
            this.r_ParamType = i_ParamType;
            this.r_Description = i_Description;
            this.r_UpperLimit = i_UpperLimit;
        }

        public Type ParamType
        {
            get { return this.r_ParamType; }
        }

        public string Description
        {
            get { return this.r_Description; }
        }

        public int UpperLimit
        {
            get { return this.r_UpperLimit; }
        }
    }
}