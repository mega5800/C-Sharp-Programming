using System;

namespace B20_Ex01_1
{
    public class Program
    {
        // ENTERY POINT
        public static void Main()
        {
            runExercise1();
        }

        // RUN METHOD
        private static void runExercise1()
        {
            string firstInputNumber, secondInputNumber, thirdInputNumber;

            Console.WriteLine("Please enter 3 binary numbers with 9 digits each:");
            firstInputNumber = getInputString();
            secondInputNumber = getInputString();
            thirdInputNumber = getInputString();
            showStatistics(firstInputNumber, secondInputNumber, thirdInputNumber);
        }

        // INPUT STRING METHODS
        private static string getInputString()
        {
            bool isInputValid = false;
            string inputString = string.Empty;

            while (!isInputValid)
            {
                inputString = Console.ReadLine();
                isInputValid = checkStringValidity(inputString);
                if (!isInputValid)
                {
                    Console.WriteLine("The input you entered is invalid. Please try again.");
                }
            }

            return inputString;
        }

        private static bool checkStringValidity(string i_StringToCheck)
        {
            bool inputStringValidity = false;

            if ((i_StringToCheck.Length == 9) && checkIfStringConsistsOnlyOfBinaryDigits(i_StringToCheck))
            {
                inputStringValidity = true;
            }

            return inputStringValidity;
        }

        private static bool checkIfStringConsistsOnlyOfBinaryDigits(string i_StringToCheck)
        {
            bool binaryCheckResult = true;

            for (int i = 0; i < i_StringToCheck.Length; i++)
            {
                if (i_StringToCheck[i] != '0' && i_StringToCheck[i] != '1')
                {
                    binaryCheckResult = false;
                    break;
                }
            }

            return binaryCheckResult;
        }

        // CONVERT BIN TO DEC
        private static int convertBinaryToDecimal(string i_StringToConvert)
        {
            int decimalResult = 0, currentPowerOfTwo = 1, convertedStringToInt;
            int.TryParse(i_StringToConvert, out convertedStringToInt);

            while (convertedStringToInt >= 1)
            {
                decimalResult += currentPowerOfTwo * (convertedStringToInt % 10);
                convertedStringToInt /= 10;
                currentPowerOfTwo *= 2;
            }

            return decimalResult;
        }

        // STATISTICS
        private static void showStatistics(string i_FirstNumber, string i_SecondNumber, string i_ThirdNumber)
        {
            int powerOfTwoAmount, ascendingDigitsAmount;
            int firstConvertedDecimalNumber = convertBinaryToDecimal(i_FirstNumber);
            int secondConvertedDecimalNumber = convertBinaryToDecimal(i_SecondNumber);
            int thirdConvertedDecimalNumber = convertBinaryToDecimal(i_ThirdNumber);

            powerOfTwoAmount = checkIfPowerOfTwo(i_FirstNumber) + checkIfPowerOfTwo(i_SecondNumber) + checkIfPowerOfTwo(i_ThirdNumber);
            ascendingDigitsAmount = checkIfAscendingDigits(firstConvertedDecimalNumber) + checkIfAscendingDigits(secondConvertedDecimalNumber) + checkIfAscendingDigits(thirdConvertedDecimalNumber);
            Console.WriteLine("\nThe decimal numbers are:");
            Console.WriteLine(string.Format("{0} = {1}", i_FirstNumber, firstConvertedDecimalNumber));
            Console.WriteLine(string.Format("{0} = {1}", i_SecondNumber, secondConvertedDecimalNumber));
            Console.WriteLine(string.Format("{0} = {1}", i_ThirdNumber, thirdConvertedDecimalNumber));
            Console.WriteLine(string.Format("\nThe amount of powers of 2 inputs is: {0}", powerOfTwoAmount));
            Console.WriteLine(string.Format("The amount of ascending numbers is: {0}", ascendingDigitsAmount));
            printGreatestAndSmallestNumbers(firstConvertedDecimalNumber, secondConvertedDecimalNumber, thirdConvertedDecimalNumber);
            printCalculatedAverageAmount(i_FirstNumber, i_SecondNumber, i_ThirdNumber);
        }

        private static int checkIfPowerOfTwo(string i_StringToCheck)
        {
            int powerOfTwoResult = 0;

            if (countOneInstances(i_StringToCheck) <= 1)
            {
                powerOfTwoResult = 1;
            }

            return powerOfTwoResult;
        }

        private static int countOneInstances(string i_StringToCount)
        {
            int onesCounter = 0;

            for (int i = 0; i < i_StringToCount.Length; i++)
            {
                if (i_StringToCount[i] == '1')
                {
                    onesCounter++;
                }
            }

            return onesCounter;
        }

        private static void printCalculatedAverageAmount(string i_firstNumber, string i_secondNumber, string i_thirdNumber)
        {
            int amountOfOneDigits, amountOfZeroDigits;
            string messageToPrint = string.Empty;

            amountOfOneDigits = countOneInstances(i_firstNumber) + countOneInstances(i_secondNumber) + countOneInstances(i_thirdNumber);
            amountOfZeroDigits = 27 - amountOfOneDigits; // 3 valid inputs will have 27 digits in total
            messageToPrint = string.Format("The average amount of ones is: {0:F2}\nThe average amount of zeros is: {1:F2}", amountOfOneDigits / 3f, amountOfZeroDigits / 3f);
            Console.WriteLine(messageToPrint);
        }

        private static void printGreatestAndSmallestNumbers(int i_firstNumber, int i_secondNumber, int i_thirdNumber)
        {
            int localMax, localMin;

            localMax = Math.Max(i_firstNumber, i_secondNumber);
            localMax = Math.Max(localMax, i_thirdNumber);
            localMin = Math.Min(i_firstNumber, i_secondNumber);
            localMin = Math.Min(localMin, i_thirdNumber);
            Console.WriteLine("The greatest number is: " + localMax);
            Console.WriteLine("The smallest number is: " + localMin);
        }

        private static int checkIfAscendingDigits(int i_DecimalInput)
        {
            int isAscendingInput = 1;

            while (i_DecimalInput > 9)
            {
                if (i_DecimalInput % 10 <= (i_DecimalInput / 10) % 10)
                {
                    isAscendingInput = 0;
                    break;
                }

                i_DecimalInput /= 10;
            }

            return isAscendingInput;
        }
    }
}