using System;
using System.Threading;
using Ex02.ConsoleUtils;

namespace B20_Ex02
{
    public class GameUIComponent
    {
        // MEMBER VARIABLES
        private GameLogicComponent m_LogicComponent;

        // PUBLIC STATIC METHODS
        public static void GreetPlayers(Player i_PlayerOne, Player i_PlayerTwo)
        {
            Console.Write(string.Format("Hello {0}", i_PlayerOne.PlayerName));
            if (i_PlayerTwo.IsHuman)
            {
                Console.Write(string.Format(" and {0}", i_PlayerTwo.PlayerName));
            }

            Console.WriteLine();
        }

        public static string GetValidPlayerName(string i_CurrentPlayer)
        {
            string inputString = string.Empty;

            while (!isPlayerNameValid(inputString))
            {
                Console.Write(string.Format("Please enter {0} player's name: ", i_CurrentPlayer));
                inputString = Console.ReadLine();
                if (!isPlayerNameValid(inputString))
                {
                    Console.WriteLine(string.Format("Player's name can only contain english letters, please try again.{0}", Environment.NewLine));
                }
            }

            Screen.Clear();
            return inputString;
        }

        public static string ChooseOpponent(out bool i_IsHuman)
        {
            int playerOneChoice;
            string playerTwoName = string.Empty;

            Console.WriteLine(string.Format("Choose an opponent to play against:{0}", Environment.NewLine));
            Console.WriteLine("1) Human");
            Console.WriteLine("2) Computer");
            Console.Write(string.Format("{0}Enter 1 or 2 accordingly: ", Environment.NewLine));
            playerOneChoice = getValidOpponentInput();
            if (playerOneChoice == 1)
            {
                playerTwoName = GetValidPlayerName("second");
                i_IsHuman = true;
            }
            else
            {
                playerTwoName = "Computer";
                i_IsHuman = false;
            }

            return playerTwoName;
        }

        public static void GetValidBoardDimensions(out int o_BoardHeight, out int o_BoardWidth)
        {
            bool isValidBoardDimensions = false;
            string heightString = string.Empty, widthString = string.Empty;

            while (!isValidBoardDimensions)
            {
                Console.WriteLine("Please enter the board dimensions");
                Console.WriteLine("=================================");
                Console.Write("Height: ");
                heightString = Console.ReadLine();
                Console.Write("Width: ");
                widthString = Console.ReadLine();
                Console.WriteLine();
                isValidBoardDimensions = isBoardDimensionsValid(heightString, widthString);
            }

            o_BoardHeight = int.Parse(heightString);
            o_BoardWidth = int.Parse(widthString);
            Screen.Clear();
        }

        // PRIVATE STATIC METHODS
        private static int getValidOpponentInput()
        {
            string inputString = string.Empty;

            while (!isOpponentInputValid(inputString))
            {
                inputString = Console.ReadLine();
                if (!isOpponentInputValid(inputString))
                {
                    Console.WriteLine("Your input is invalid");
                    Console.Write(string.Format("{0}Enter 1 or 2 accordingly: ", Environment.NewLine));
                }
            }

            Screen.Clear();
            return int.Parse(inputString);
        }

        private static bool isOpponentInputValid(string i_StringToCheck)
        {
            bool isInputValid = i_StringToCheck == "1" || i_StringToCheck == "2";

            return isInputValid;
        }

        private static bool isPlayerNameValid(string i_StringToCheck)
        {
            bool isInputValid = true;

            if (i_StringToCheck == string.Empty)
            {
                isInputValid = false;
            }
            else
            {
                isInputValid = checkIfStringConsistsOnlyOfEnglishLetters(i_StringToCheck);
            }

            return isInputValid;
        }

        private static bool checkIfStringConsistsOnlyOfEnglishLetters(string i_StringToCheck)
        {
            bool isStringOnlyAlphabet = true;

            for (int i = 0; i < i_StringToCheck.Length; i++)
            {
                if (!(char.IsUpper(i_StringToCheck[i]) || char.IsLower(i_StringToCheck[i])))
                {
                    isStringOnlyAlphabet = false;
                    break;
                }
            }

            return isStringOnlyAlphabet;
        }

        private static bool isBoardDimensionsValid(string i_Height, string i_Width)
        {
            bool isDimensionsValid = false, isErrorMessagePrinted = false;
            int parsedStringHeightToInt, parsedStringWidthToInt;

            if (i_Height != string.Empty && i_Width != string.Empty)
            {
                if (int.TryParse(i_Height, out parsedStringHeightToInt) && int.TryParse(i_Width, out parsedStringWidthToInt))
                {
                    if (GameLogicComponent.CheckValidDimensions(parsedStringHeightToInt, parsedStringWidthToInt))
                    {
                        if (GameLogicComponent.CheckEvenNumberOfCells(parsedStringHeightToInt, parsedStringWidthToInt))
                        {
                            isDimensionsValid = true;
                        }
                        else
                        {
                            Console.WriteLine(string.Format("The board must have an even number of cells{0}", Environment.NewLine));
                            isErrorMessagePrinted = true;
                        }
                    }
                }
            }

            if (!isDimensionsValid && !isErrorMessagePrinted)
            {
                Console.WriteLine(string.Format("The inputs must be between the numeric values {0}-{1}{2}", GameLogicComponent.k_MinimalSize, GameLogicComponent.k_MaximalSize, Environment.NewLine));
            }

            return isDimensionsValid;
        }

        // PUBLIC METHODS
        public void StartMatch()
        {
            bool isSuccessfulTurn = false;

            this.m_LogicComponent.ResetGameSettings();
            cleanAndPrint(this.m_LogicComponent.CurrentPlayerPointer);
            while (!this.m_LogicComponent.CheckIfGameIsFinished())
            {
                isSuccessfulTurn = performTurn();
                if (!isSuccessfulTurn)
                {
                    this.m_LogicComponent.SwitchTurn();
                    Thread.Sleep(2000);
                    cleanAndPrint(this.m_LogicComponent.CurrentPlayerPointer);
                }
            }
        }

        public bool NewGameQuestion()
        {
            string newGameInput = string.Empty;

            while (!isNewGameInputValid(newGameInput))
            {
                Console.Write(string.Format("{0}Do you want to play another game? (Y/N): ", Environment.NewLine));
                newGameInput = Console.ReadLine();
            }

            Screen.Clear();
            if (newGameInput == "N")
            {
                Console.WriteLine("Thank you for playing");
            }

            return newGameInput == "Y";
        }

        public void CreateLogicComponent(int i_Height, int i_Width, Player i_PlayerOne, Player i_PlayerTwo)
        {
            this.m_LogicComponent = new GameLogicComponent(i_Height, i_Width, i_PlayerOne, i_PlayerTwo);
        }

        public void AnnounceWinner()
        {
            Player winningPlayerPointer = null;

            Screen.Clear();
            Console.WriteLine(string.Format("{0}, you got {1} points", this.m_LogicComponent.PlayerOne.PlayerName, this.m_LogicComponent.PlayerOne.Score));
            Console.WriteLine(string.Format("{0}, you got {1} points{2}", this.m_LogicComponent.PlayerTwo.PlayerName, this.m_LogicComponent.PlayerTwo.Score, Environment.NewLine));
            this.m_LogicComponent.DecideWinningPlayer(ref winningPlayerPointer);
            if (winningPlayerPointer == null)
            {
                Console.WriteLine("We have a tie here!");
            }
            else
            {
                Console.WriteLine(string.Format("{0}, you are the winner!", winningPlayerPointer.PlayerName));
            }
        }

        // PRIVATE METHODS
        private void makeFirstStep()
        {
            if (this.m_LogicComponent.CurrentPlayerPointer.IsHuman)
            {
                this.m_LogicComponent.CurrentPlayerPointer.FirstStep = getValidStep(this.m_LogicComponent.CurrentPlayerPointer.PlayerName);
            }
            else
            {
                this.m_LogicComponent.CurrentPlayerPointer.FirstStep = this.m_LogicComponent.GetRandomMachineStep();
            }

            this.m_LogicComponent.SwitchCardStatus(this.m_LogicComponent.CurrentPlayerPointer.FirstStep);
            cleanAndPrint(this.m_LogicComponent.CurrentPlayerPointer);
        }

        private void makeSecondStep()
        {
            if (this.m_LogicComponent.CurrentPlayerPointer.IsHuman)
            {
                this.m_LogicComponent.CurrentPlayerPointer.SecondStep = getValidStep(this.m_LogicComponent.CurrentPlayerPointer.PlayerName);
            }
            else
            {
                this.m_LogicComponent.CurrentPlayerPointer.SecondStep = this.m_LogicComponent.GetMachineSecondStep(this.m_LogicComponent.CurrentPlayerPointer.FirstStep);
            }

            this.m_LogicComponent.SwitchCardStatus(this.m_LogicComponent.CurrentPlayerPointer.SecondStep);
            cleanAndPrint(this.m_LogicComponent.CurrentPlayerPointer);
        }

        private void printSeparatorLine(GameLogicComponent i_GameBoardToPrint)
        {
            Console.WriteLine();
            Console.Write("  ==");
            for (int i = 1; i < i_GameBoardToPrint.Width; i++)
            {
                Console.Write("====");
            }

            Console.Write("===");
            Console.WriteLine();
        }

        private bool performTurn()
        {
            bool isSuccessfulTurn = false;

            makeFirstStep();
            cleanAndPrint(this.m_LogicComponent.CurrentPlayerPointer);
            makeSecondStep();
            if (this.m_LogicComponent.CheckCardMatch(this.m_LogicComponent.CurrentPlayerPointer.FirstStep, this.m_LogicComponent.CurrentPlayerPointer.SecondStep))
            {
                this.m_LogicComponent.ShownCardsAmount += 2;
                this.m_LogicComponent.AddPoint(this.m_LogicComponent.CurrentPlayerPointer);
                cleanAndPrint(this.m_LogicComponent.CurrentPlayerPointer);
                isSuccessfulTurn = true;
                Console.WriteLine(string.Format("{0}, you got a match!{1}", this.m_LogicComponent.CurrentPlayerPointer.PlayerName, Environment.NewLine));
            }
            else
            {
                if (!this.m_LogicComponent.CurrentPlayerPointer.IsHuman)
                {
                    this.m_LogicComponent.SaveMachineStepInList(this.m_LogicComponent.CurrentPlayerPointer.FirstStep);
                    this.m_LogicComponent.SaveMachineStepInList(this.m_LogicComponent.CurrentPlayerPointer.SecondStep);
                }

                this.m_LogicComponent.SwitchCardStatus(this.m_LogicComponent.CurrentPlayerPointer.FirstStep);
                this.m_LogicComponent.SwitchCardStatus(this.m_LogicComponent.CurrentPlayerPointer.SecondStep);
            }

            return isSuccessfulTurn;
        }

        private PlayerStep getValidStep(string i_PlayerName)
        {
            int rowIndex = 0, columnIndex = 0;
            string playerInput = string.Empty;
            bool isCurrentStepValidFlag = false;

            while (!isCurrentStepValidFlag)
            {
                Console.Write(string.Format("{0}'s step: ", i_PlayerName));
                playerInput = Console.ReadLine();
                isCurrentStepValidFlag = isCurrentStepValid(playerInput);
            }

            rowIndex = (int)char.GetNumericValue(playerInput[1]) - 1;
            columnIndex = playerInput[0] - 'A';
            PlayerStep playerStep = new PlayerStep(rowIndex, columnIndex);
            return playerStep;
        }

        private bool isCurrentStepValid(string i_StepToCheck)
        {
            int parsedNumberIndexToInt, parsedCharIndexToInt;
            bool isStepValid = false;

            if (i_StepToCheck == "Q")
            {
                Screen.Clear();
                Console.WriteLine("Thank you for playing");
                Environment.Exit(0);
            }

            if ((i_StepToCheck.Length == 2) && (i_StepToCheck != string.Empty))
            {
                if (char.IsUpper(i_StepToCheck[0]) && char.IsDigit(i_StepToCheck[1]))
                {
                    parsedNumberIndexToInt = (int)char.GetNumericValue(i_StepToCheck[1]) - 1;
                    parsedCharIndexToInt = i_StepToCheck[0] - 'A';
                    if (this.m_LogicComponent.IsInBounds(parsedNumberIndexToInt, parsedCharIndexToInt))
                    {
                        if (this.m_LogicComponent.CheckIfCardIsNotShown(parsedNumberIndexToInt, parsedCharIndexToInt))
                        {
                            isStepValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Your step was not valid");
                            Console.WriteLine("You need to choose a hidden card");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your step was out of bounds");
                        Console.WriteLine(string.Format("You need to pick a letter between {0}-{1}, and then a number between {2}-{3}", 'A', (char)('A' + this.m_LogicComponent.Height - 1), 1, this.m_LogicComponent.Width));
                    }
                }
                else
                {
                    Console.WriteLine("Your step was not valid");
                    Console.WriteLine("Switch the order of your step characters");
                }
            }
            else
            {
                Console.WriteLine("Your step was not valid");
                Console.WriteLine(string.Format("You need to pick a letter between {0}-{1}, and then a number between {2}-{3}", 'A', (char)('A' + this.m_LogicComponent.Height - 1), 1, this.m_LogicComponent.Width));
            }

            if (!isStepValid)
            {
                Console.WriteLine();
            }

            return isStepValid;
        }

        private bool isNewGameInputValid(string i_StringToCheck)
        {
            bool validInput = false;

            if (i_StringToCheck != string.Empty)
            {
                if ((i_StringToCheck == "Y") || (i_StringToCheck == "N"))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Please enter Y or N");
                }
            }

            return validInput;
        }

        private void printGameBoard()
        {
            char charToPrint = 'A';

            Screen.Clear();
            Console.Write("    ");
            for (int i = 0; i < this.m_LogicComponent.Width; i++)
            {
                Console.Write(string.Format("{0}   ", charToPrint));
                charToPrint++;
            }

            printSeparatorLine(this.m_LogicComponent);
            for (int i = 0; i < this.m_LogicComponent.Height; i++)
            {
                Console.Write(string.Format("{0} |", i + 1));
                for (int n = 0; n < this.m_LogicComponent.Width; n++)
                {
                    if (this.m_LogicComponent.GetShownStatus(i, n))
                    {
                        Console.Write(string.Format(" {0} ", this.m_LogicComponent.GetSymbol(i, n)));
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.Write("|");
                }

                printSeparatorLine(this.m_LogicComponent);
            }
        }

        private void cleanAndPrint(Player i_CurrentPlayer)
        {
            if (!i_CurrentPlayer.IsHuman)
            {
                Thread.Sleep(500);
            }

            printGameBoard();
            Console.WriteLine(string.Format("{0}{1}'s score: {2}", Environment.NewLine, i_CurrentPlayer.PlayerName, i_CurrentPlayer.Score));
        }
    }
}