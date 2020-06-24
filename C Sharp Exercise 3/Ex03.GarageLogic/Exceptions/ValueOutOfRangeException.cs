using System;

namespace Ex03.GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base(string.Format("Please enter a value between {0}-{1}", i_MinValue, i_MaxValue))
        {
            this.r_MinValue = i_MinValue;
            this.r_MaxValue = i_MaxValue;
        }
    }
}