using System;

namespace B20_Ex01_5
{
    public class Program
    {
        // ENTERY POINT
        public static void Main()
        {
            runExercise5();
        }

        // RUN METHOD
        private static void runExercise5()
        {
            int maxDigit, minDigit, dividedByThreeAmount, greaterThanUnitsDigitAmount;
            string inputString = getInputString();

            findBiggestAndSmallestDigits(inputString, out maxDigit, out minDigit);
            dividedByThreeAmount = checkDivisionsByThree(inputString);
            greaterThanUnitsDigitAmount = findGreaterThanUnitsDigit(inputString);
            Console.WriteLine(string.Format("The biggest digit is: {0}", maxDigit));
            Console.WriteLine(string.Format("The smallest digit is: {0}", minDigit));
            Console.WriteLine(string.Format("The amount of digits that can be divided by 3 is: {0}", dividedByThreeAmount));
            Console.WriteLine(string.Format("The amount of digits greater than the units digit is: {0}", greaterThanUnitsDigitAmount));
        }

        // INPUT STRING METHOD
        private static string getInputString()
        {
            int tempStringToIntValue;
            bool isInputValid = false;
            string inputString = string.Empty;

            while (!isInputValid)
            {
                Console.Write("Enter a number with 9 digits: ");
                inputString = Console.ReadLine();
                isInputValid = int.TryParse(inputString, out tempStringToIntValue) && (tempStringToIntValue > 0) && (inputString.Length == 9);
                if (!isInputValid)
                {
                    Console.WriteLine("The input you entered is invalid. Please try again.\n");
                }
            }

            return inputString;
        }

        // STATISTICS
        private static int checkDivisionsByThree(string i_StringToCheck)
        {
            int dividingByThreeAmount = 0;

            for (int i = 0; i < i_StringToCheck.Length; i++)
            {
                if ((i_StringToCheck[i] - 48) % 3 == 0)
                {
                    dividingByThreeAmount++;
                }
            }

            return dividingByThreeAmount;
        }

        private static void findBiggestAndSmallestDigits(string i_StringToCheck, out int o_MaxDigit, out int o_MinDigit)
        {
            o_MaxDigit = o_MinDigit = i_StringToCheck[0] - 48;

            for (int i = 1; i < i_StringToCheck.Length; i++)
            {
                o_MaxDigit = Math.Max(o_MaxDigit, i_StringToCheck[i] - 48);
                o_MinDigit = Math.Min(o_MinDigit, i_StringToCheck[i] - 48);
            }
        }

        private static int findGreaterThanUnitsDigit(string i_StringToCheck)
        {
            int greaterThanUnitsAmount = 0, unitsDigit = i_StringToCheck[i_StringToCheck.Length - 1] - 48;

            for (int i = 0; i < i_StringToCheck.Length - 1; i++)
            {
                if (i_StringToCheck[i] - 48 > unitsDigit)
                {
                    greaterThanUnitsAmount++;
                }
            }

            return greaterThanUnitsAmount;
        }
    }
}