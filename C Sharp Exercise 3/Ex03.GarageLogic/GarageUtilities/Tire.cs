using System;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.GarageUtilities
{
    public class Tire
    {
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public Tire(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            this.m_ManufacturerName = i_ManufacturerName;
            this.m_CurrentAirPressure = i_CurrentAirPressure;
            this.r_MaxAirPressure = i_MaxAirPressure;
        }

        public void PumpAirToMaxAirPressure()
        {
            this.m_CurrentAirPressure = this.r_MaxAirPressure;
        }

        public void PumpTireIfPossible(float i_AirAmount)
        {
            if (this.m_CurrentAirPressure + i_AirAmount > this.r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(1, this.r_MaxAirPressure - this.m_CurrentAirPressure);
            }
            else
            {
                this.m_CurrentAirPressure += i_AirAmount;
            }
        }

        public override string ToString()
        {
            string stringToReturn = string.Empty, manufacturerName = string.Empty, currentAirPressure = string.Empty, maxAirPressure = string.Empty;

            manufacturerName = string.Format("Tire Manufacturer: {0}", this.m_ManufacturerName);
            currentAirPressure = string.Format("Current air pressure in all tires: {0}", this.m_CurrentAirPressure);
            maxAirPressure = string.Format("Tire max air pressure: {0}", this.r_MaxAirPressure);
            stringToReturn = string.Format("{0}{1}{0}{2}{0}{3}{0}", Environment.NewLine, manufacturerName, currentAirPressure, maxAirPressure);

            return stringToReturn;
        }
    }
}