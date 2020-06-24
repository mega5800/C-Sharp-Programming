using System;
using System.Collections.Generic;

namespace B20_Ex02
{
    public class GameLogicComponent
    {
        // MEMBER VARIABLES
        private readonly List<int> r_RandomPlacementList = null;
        private readonly List<PlayerStep> r_ArtificialPlayerStepMemoryList = null;
        private int m_Width;
        private int m_Height;
        private int m_TotalCardsAmount;
        private int m_ShownCardsAmount;
        private MemoryCard[,] m_MemoryBoard;
        private Random m_randomNumGenerator;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayerPointer;
        public const int k_MinimalSize = 4; // this value can be changed here if desired
        public const int k_MaximalSize = 6; // this value can be changed here if desired 

        // PROPERTIES
        public int Width
        {
            get { return this.m_Width; }
            set { this.m_Width = value; }
        }

        public int Height
        {
            get { return this.m_Height; }
            set { this.m_Height = value; }
        }

        public int ShownCardsAmount
        {
            get { return this.m_ShownCardsAmount; }
            set { this.m_ShownCardsAmount = value; }
        }

        public Player PlayerOne
        {
            get { return this.m_PlayerOne; }
        }

        public Player PlayerTwo
        {
            get { return this.m_PlayerTwo; }
        }

        public Player CurrentPlayerPointer
        {
            get { return this.m_CurrentPlayerPointer; }
        }

        // CTOR
        public GameLogicComponent(int i_Height, int i_Width, Player i_PlayerOne, Player i_PlayerTwo)
        {
            this.m_Width = i_Width;
            this.m_Height = i_Height;
            this.m_TotalCardsAmount = i_Width * i_Height;
            this.m_ShownCardsAmount = 0;
            this.r_RandomPlacementList = new List<int>(i_Width * i_Height);
            this.m_randomNumGenerator = new Random();
            createAndFillMemoryBoard(this.m_Height, this.m_Width);
            this.m_PlayerOne = i_PlayerOne;
            this.m_PlayerTwo = i_PlayerTwo;
            this.m_CurrentPlayerPointer = this.m_PlayerOne;
            if (!i_PlayerTwo.IsHuman)
            {
                this.r_ArtificialPlayerStepMemoryList = new List<PlayerStep>((i_Height * i_Width) / 2);
            }
        }

        // PUBLIC STATIC METHOD
        public static bool CheckValidDimensions(int i_Height, int i_Width)
        {
            bool isHeightValid = i_Height >= k_MinimalSize && i_Height <= k_MaximalSize;
            bool isWidthValid = i_Width >= k_MinimalSize && i_Width <= k_MaximalSize;

            return isHeightValid && isWidthValid;
        }

        public static bool CheckEvenNumberOfCells(int i_Height, int i_Width)
        {
            bool isEvenNumberOfCells = false;

            if ((i_Height * i_Width) % 2 == 0)
            {
                isEvenNumberOfCells = true;
            }

            return isEvenNumberOfCells;
        }

        // PUBLIC METHODS
        public PlayerStep GetRandomMachineStep()
        {
            int rowIndex, columnIndex, randomNum;

            this.r_RandomPlacementList.Clear();
            for (int i = 0; i < m_Height; i++)
            {
                for (int n = 0; n < m_Width; n++)
                {
                    if (!this.m_MemoryBoard[i, n].IsShown)
                    {
                        this.r_RandomPlacementList.Add((i * this.m_Width) + n);
                    }
                }
            }

            randomNum = this.m_randomNumGenerator.Next(0, this.r_RandomPlacementList.Count);
            convertRandomNumToMatrixIndexes(this.r_RandomPlacementList[randomNum], out rowIndex, out columnIndex);
            PlayerStep machinePlayerStep = new PlayerStep(rowIndex, columnIndex);

            return machinePlayerStep;
        }

        public void SwitchTurn()
        {
            if (this.m_CurrentPlayerPointer == this.m_PlayerOne)
            {
                this.m_CurrentPlayerPointer = this.m_PlayerTwo;
            }
            else
            {
                this.m_CurrentPlayerPointer = this.m_PlayerOne;
            }
        }

        public bool CheckIfCardIsNotShown(int i_RowIndex, int i_ColumnIndex)
        {
            bool isPlayerStepValid = false;

            if (!GetShownStatus(i_RowIndex, i_ColumnIndex))
            {
                isPlayerStepValid = true;
            }

            return isPlayerStepValid;
        }

        public void ResetGameSettings()
        {
            this.m_PlayerOne.Score = 0;
            this.m_PlayerTwo.Score = 0;
            this.m_ShownCardsAmount = 0;
            if (!this.m_PlayerTwo.IsHuman)
            {
                this.r_ArtificialPlayerStepMemoryList.Clear();
            }
        }

        public void DecideWinningPlayer(ref Player io_WinningPlayerPointer)
        {
            if (this.PlayerOne.Score != this.PlayerTwo.Score)
            {
                if (this.PlayerOne.Score > this.PlayerTwo.Score)
                {
                    io_WinningPlayerPointer = this.PlayerOne;
                }
                else
                {
                    io_WinningPlayerPointer = this.PlayerTwo;
                }
            }
        }

        public bool IsInBounds(int i_RowIndex, int i_ColumnIndex)
        {
            bool isRowValid = i_RowIndex >= 0 && i_RowIndex <= this.m_Height;
            bool isColumnValid = i_ColumnIndex >= 0 && i_ColumnIndex <= this.m_Width;

            return isRowValid && isColumnValid;
        }

        public bool CheckIfGameIsFinished()
        {
            bool isGameFinished = false;

            if (this.m_TotalCardsAmount == this.m_ShownCardsAmount)
            {
                isGameFinished = true;
            }

            return isGameFinished;
        }

        public char GetSymbol(int i_RowIndex, int i_ColumnIndex)
        {
            return this.m_MemoryBoard[i_RowIndex, i_ColumnIndex].Symbol;
        }

        public bool GetShownStatus(int i_RowIndex, int i_ColumnIndex)
        {
            return this.m_MemoryBoard[i_RowIndex, i_ColumnIndex].IsShown;
        }

        public void SwitchCardStatus(PlayerStep i_PlayerStep)
        {
            bool currentCardStatus = this.m_MemoryBoard[i_PlayerStep.RowIndex, i_PlayerStep.ColumnIndex].IsShown;
            this.m_MemoryBoard[i_PlayerStep.RowIndex, i_PlayerStep.ColumnIndex].IsShown = !currentCardStatus;
        }

        public PlayerStep GetMachineSecondStep(PlayerStep i_MachineFirstStep)
        {
            PlayerStep currentMachineStep = checkIfSuitablePlayerStepSavedInList(i_MachineFirstStep);

            return currentMachineStep;
        }

        public void AddPoint(Player i_CurrentPlayer)
        {
            i_CurrentPlayer.Score++;
        }

        public void SaveMachineStepInList(PlayerStep i_PlayerStep)
        {
            if (checkIfCurrentPlayerStepIsNotInList(i_PlayerStep))
            {
                this.r_ArtificialPlayerStepMemoryList.Add(i_PlayerStep);
            }
        }
           
        public bool CheckCardMatch(PlayerStep i_FirstStep, PlayerStep i_SecondStep)
        {
            char firstCardSymbol, secondCardSymbol;

            firstCardSymbol = GetSymbol(i_FirstStep.RowIndex, i_FirstStep.ColumnIndex);
            secondCardSymbol = GetSymbol(i_SecondStep.RowIndex, i_SecondStep.ColumnIndex);

            return firstCardSymbol == secondCardSymbol;
        }

        // PRIVATE METHODS
        private PlayerStep checkIfSuitablePlayerStepSavedInList(PlayerStep i_PlayerStep)
        {
            int suitablePlayerStepIndex = 0;
            bool isPlayerStepFound = false;
            PlayerStep stepPlayerToReturn;

            for (int i = 0; i < this.r_ArtificialPlayerStepMemoryList.Count; i++)
            {
                if (!i_PlayerStep.Equals(this.r_ArtificialPlayerStepMemoryList[i]))
                {
                    if (CheckCardMatch(i_PlayerStep, this.r_ArtificialPlayerStepMemoryList[i]))
                    {
                        suitablePlayerStepIndex = i;
                        isPlayerStepFound = true;
                        break;
                    }
                }
            }

            if (isPlayerStepFound)
            {
                stepPlayerToReturn = this.r_ArtificialPlayerStepMemoryList[suitablePlayerStepIndex];
                this.r_ArtificialPlayerStepMemoryList.RemoveAt(suitablePlayerStepIndex);
            }
            else
            {
                stepPlayerToReturn = GetRandomMachineStep();
            }

            return stepPlayerToReturn;
        }

        private bool checkIfCurrentPlayerStepIsNotInList(PlayerStep i_PlayerStepToCheck)
        {
            bool resultToReturn = true;

            for (int i = 0; i < this.r_ArtificialPlayerStepMemoryList.Count; i++)
            {
                if (i_PlayerStepToCheck.Equals(this.r_ArtificialPlayerStepMemoryList[i]))
                {
                    resultToReturn = false;
                }
            }

            return resultToReturn;
        }

        private void createAndFillMemoryBoard(int i_Height, int i_Width)
        {
            char charToSet = 'A';
            int rowIndex, columnIndex, upperLimit = i_Width * i_Height, randomNum;
            this.m_MemoryBoard = new MemoryCard[i_Height, i_Width];

            fillRandomPlacementList(upperLimit);
            for (int i = 0; i < this.m_TotalCardsAmount / 2; i++)
            {
                for (int n = 0; n < 2; n++)
                {
                    randomNum = this.m_randomNumGenerator.Next(0, upperLimit);
                    convertRandomNumToMatrixIndexes(this.r_RandomPlacementList[randomNum], out rowIndex, out columnIndex);
                    this.m_MemoryBoard[rowIndex, columnIndex] = new MemoryCard(charToSet, false);
                    this.r_RandomPlacementList.RemoveAt(randomNum);
                    upperLimit--;
                }

                charToSet++;
            }
        }

        private void fillRandomPlacementList(int i_ListSize)
        {
            this.r_RandomPlacementList.Clear();

            for (int i = 0; i < i_ListSize; i++)
            {
                this.r_RandomPlacementList.Add(i);
            }
        }

        private void convertRandomNumToMatrixIndexes(int i_RandomNum, out int o_RowIndex, out int o_ColumnIndex)
        {
            o_RowIndex = i_RandomNum / this.m_Width;
            o_ColumnIndex = i_RandomNum - (o_RowIndex * this.m_Width);
        }
    }
}