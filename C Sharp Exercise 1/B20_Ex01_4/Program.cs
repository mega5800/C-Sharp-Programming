using System;

namespace B20_Ex01_4
{
    // ENUM INPUT STATES
    public enum eInputState
    {
        InvalidInput,
        ValidInputLength,
        NumericString,
        EnglishAlphabetString
    }

    public class Program
    {
        // ENTERY POINT
        public static void Main()
        {
            runExercise4();
        }

        // RUN METHOD
        private static void runExercise4()
        {
            string inputString;
            eInputState inputState;
            
            inputString = getInputString(out inputState);
            runPalindromeCheck(inputString);
            if (inputState == eInputState.NumericString)
            {
                checkDivisionByFive(inputString);
            }
            else
            {
                printUpperCaseAmount(inputString);
            }
        }

        // INPUT STRING METHODS
        private static string getInputString(out eInputState o_InputState)
        {
            o_InputState = eInputState.InvalidInput;
            string inputString = string.Empty;

            while (o_InputState == eInputState.InvalidInput)
            {
                Console.Write("Please enter a string with 8 characters: ");
                inputString = Console.ReadLine();
                o_InputState = checkStringValidityState(inputString);
                if (o_InputState == eInputState.InvalidInput)
                {
                    Console.WriteLine("The input you entered is invalid. Please try again.\n");
                }
            }

            return inputString;
        }

        private static eInputState checkStringValidityState(string i_StringToCheck)
        {
            eInputState inputStringState = checkStringLengthValidity(i_StringToCheck);

            if (inputStringState == eInputState.ValidInputLength)
            {
                inputStringState = checkIfStringConsistsOnlyOfDigits(i_StringToCheck);
                if (inputStringState == eInputState.InvalidInput)
                {
                    inputStringState = checkIfStringConsistsOnlyOfEnglishLetters(i_StringToCheck);
                }
            }

            return inputStringState;
        }

        private static eInputState checkStringLengthValidity(string i_StringToCheck)
        {
            eInputState stringLengthValidity = eInputState.InvalidInput;

            if (i_StringToCheck.Length == 8)
            {
                stringLengthValidity = eInputState.ValidInputLength;
            }

            return stringLengthValidity;
        }

        private static eInputState checkIfStringConsistsOnlyOfDigits(string i_StringToCheck)
        {
            int tempStringToIntValue;
            bool isInputValid = int.TryParse(i_StringToCheck, out tempStringToIntValue) && (tempStringToIntValue >= 0);
            eInputState currentStringState = eInputState.InvalidInput;

            if (isInputValid)
            {
                currentStringState = eInputState.NumericString;
            }

            return currentStringState;
        }

        private static eInputState checkIfStringConsistsOnlyOfEnglishLetters(string i_StringToCheck)
        {
            eInputState currentStringState = eInputState.EnglishAlphabetString;

            for (int i = 0; i < i_StringToCheck.Length; i++)
            {
                if (!(char.IsUpper(i_StringToCheck[i]) || char.IsLower(i_StringToCheck[i])))
                {
                    currentStringState = eInputState.InvalidInput;
                    break;
                }
            }

            return currentStringState;
        }

        // EXERCISE CHECK METHODS
        private static void runPalindromeCheck(string i_StringToCheck)
        {
            if (palindromeRecursiveCheck(i_StringToCheck))
            {
                Console.WriteLine("The string is a palindrome");
            }
            else
            {
                Console.WriteLine("The string is not a palindrome");
            }
        }

        private static bool palindromeRecursiveCheck(string i_StringToCheck)
        {
            bool recursiveCheckResult = false;

            if (i_StringToCheck.Length <= 1)
            {
                recursiveCheckResult = true;
            }
            else
            {
                if (i_StringToCheck[0] != i_StringToCheck[i_StringToCheck.Length - 1])
                {
                    recursiveCheckResult = false;
                }
                else
                {
                    recursiveCheckResult = palindromeRecursiveCheck(i_StringToCheck.Substring(1, i_StringToCheck.Length - 2));
                }
            }

            return recursiveCheckResult;
        }

        private static void checkDivisionByFive(string i_NumberToCheck)
        {
            if (i_NumberToCheck[i_NumberToCheck.Length - 1] == '0' || i_NumberToCheck[i_NumberToCheck.Length - 1] == '5')
            {
                Console.WriteLine("The number can be divided by 5!");
            }
            else
            {
                Console.WriteLine("The number can't be divided by 5!");
            }
        }

        private static void printUpperCaseAmount(string i_StringToCheck)
        {
            int upperCaseCounter = 0;

            for (int i = 0; i < i_StringToCheck.Length; i++)
            {
                if (char.IsUpper(i_StringToCheck[i]))
                {
                    upperCaseCounter++;
                }
            }

            Console.WriteLine("The amount of uppercase letters in your string is: " + upperCaseCounter);
        }
    }
}