using System;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;

namespace Ex03.GarageLogic.GarageUtilities
{
    internal class EnergyRefillLogic
    {
        public static void Refuel(object i_InputObject, eFuelType i_FuelType, int i_FuelTankVolume, ref float io_CurrentAmountOfFuel, ref float io_CurrentEnergyPercentage)
        {
            if (i_InputObject.GetType() == typeof(eFuelType))
            {
                if ((eFuelType)i_InputObject != i_FuelType)
                {
                    throw new ArgumentException(string.Format("The type of fuel you chose ({0}) is not suitable to your vehicle{1}", i_InputObject, Environment.NewLine));
                }
            }

            if (i_InputObject.GetType() == typeof(float))
            {
                if (io_CurrentAmountOfFuel + (float)i_InputObject > i_FuelTankVolume)
                {
                    throw new ValueOutOfRangeException(0, i_FuelTankVolume - io_CurrentAmountOfFuel);
                }
                else
                {
                    io_CurrentAmountOfFuel += (float)i_InputObject;
                    io_CurrentEnergyPercentage = (io_CurrentAmountOfFuel * 100) / i_FuelTankVolume;
                }
            }
        }

        public static void Recharge(object i_InputObject, float i_MaxBatteryChargeTime, ref float io_CurrentBatteryCharge, ref float io_CurrentEnergyPercentage)
        {
            if (i_InputObject.GetType() == typeof(float))
            {
                if (io_CurrentBatteryCharge + ((float)i_InputObject / 60) > i_MaxBatteryChargeTime)
                {
                    throw new ValueOutOfRangeException(0, (i_MaxBatteryChargeTime - io_CurrentBatteryCharge) * 60);
                }
                else
                {
                    io_CurrentBatteryCharge += ((float)i_InputObject) / 60;
                    io_CurrentEnergyPercentage = (io_CurrentBatteryCharge * 100) / i_MaxBatteryChargeTime;
                }
            }
        }
    }
}