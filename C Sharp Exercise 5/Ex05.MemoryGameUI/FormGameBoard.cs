using System;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Ex05.MemoryGameLogic;
using System.Collections.Generic;

namespace Ex05.MemoryGameUI
{
    public partial class FormGameBoard : Form
    {
        private List<MatrixIndexPair> m_MatrixIndexPairList;
        private GameLogicComponent m_GameLogicComponent;
        private GameBoardDimensions m_CurrentGameBoardDimensions;
        private const int k_ButtonSize = 90;
        private const string k_ImageURL = "https://picsum.photos/80";
        private int m_PlayerStepsCounter;
        private int m_MachineTurnCounter;
        private IndexPictureBox[,] m_IndexPictureBoxesMatrix;
        private System.Windows.Forms.Timer m_CardsRevealTimer;
        private System.Windows.Forms.Timer m_MachineTurnTimer;
        private Player m_PlayerPointer;
        private PictureBox m_ImageGetterFromWeb;

        public FormGameBoard(GameLogicComponent i_GameLogicComponent, GameBoardDimensions i_CurrentGameBoardDimensions)
        {
            this.m_GameLogicComponent = i_GameLogicComponent;
            this.m_CurrentGameBoardDimensions = i_CurrentGameBoardDimensions;
            this.m_IndexPictureBoxesMatrix = new IndexPictureBox[i_CurrentGameBoardDimensions.Height, i_CurrentGameBoardDimensions.Width];
            this.m_ImageGetterFromWeb = new PictureBox();
            this.m_CardsRevealTimer = new System.Windows.Forms.Timer();
            this.m_CardsRevealTimer.Interval = 100;
            this.m_CardsRevealTimer.Tick += m_CardsRevealTimer_Tick;
            if (!this.m_GameLogicComponent.PlayerTwo.IsHuman)
            {
                this.m_MachineTurnTimer = new System.Windows.Forms.Timer();
                this.m_MachineTurnTimer.Interval = 1000;
                this.m_MachineTurnTimer.Tick += m_MachineTurnTimer_Tick;
            }

            InitializeComponent();
            addMemoryCardsToGameBoard();
            setSuitableFormSize();
            setTextInLables();
        }

        private void m_MachineTurnTimer_Tick(object sender, EventArgs e)
        {
            this.m_MachineTurnTimer.Stop();
            this.m_MachineTurnCounter++;
            this.m_PlayerPointer = this.m_GameLogicComponent.CurrentPlayerPointer;

            if (this.m_MachineTurnCounter == 1)
            {
                this.m_PlayerPointer.FirstStep = this.m_GameLogicComponent.GetRandomMachineStep();
                IndexPictureBox machineSelectedButton = this.m_IndexPictureBoxesMatrix[this.m_PlayerPointer.FirstStep.RowIndex, this.m_PlayerPointer.FirstStep.ColumnIndex];
                this.m_GameLogicComponent.SwitchCardStatus(this.m_PlayerPointer.FirstStep);
                //להוציא החוצה?
                showClickedButtonText(machineSelectedButton);
                this.m_MachineTurnTimer.Start();
            }
            else
            {
                this.m_MachineTurnCounter = 0;
                this.m_PlayerPointer.SecondStep = this.m_GameLogicComponent.GetMachineSecondStep(this.m_PlayerPointer.FirstStep);
                IndexPictureBox machineSelectedButton = this.m_IndexPictureBoxesMatrix[this.m_PlayerPointer.SecondStep.RowIndex, this.m_PlayerPointer.SecondStep.ColumnIndex];
                this.m_GameLogicComponent.SwitchCardStatus(this.m_PlayerPointer.SecondStep);
                //להוציא החוצה?
                showClickedButtonText(machineSelectedButton);
                this.m_CardsRevealTimer.Start();
            }
        }

        private void m_CardsRevealTimer_Tick(object sender, EventArgs e)
        {
            checkIfPlayerStepsAreCorrect();
        }

        private void addMemoryCardsToGameBoard()
        {
            int height = this.m_CurrentGameBoardDimensions.Height, width = this.m_CurrentGameBoardDimensions.Width;

            memoryGameLayoutPanel.RowCount = height;
            memoryGameLayoutPanel.ColumnCount = width;
            for (int i = 0; i < height; i++)
            {
                for (int n = 0; n < width; n++)
                {
                    this.m_IndexPictureBoxesMatrix[i, n] = new IndexPictureBox(i, n);
                    this.m_IndexPictureBoxesMatrix[i, n].Name = string.Format("Button{0}_{1}", i, n);
                    this.m_IndexPictureBoxesMatrix[i, n].Width = k_ButtonSize;
                    this.m_IndexPictureBoxesMatrix[i, n].Height = k_ButtonSize;
                    this.m_IndexPictureBoxesMatrix[i, n].Click += IndexPictureBoxInMatrix_Click;
                    this.m_IndexPictureBoxesMatrix[i, n].SizeMode = PictureBoxSizeMode.CenterImage;
                    this.m_IndexPictureBoxesMatrix[i, n].BackColor = Color.FromArgb(0, 109, 240);
                    memoryGameLayoutPanel.Controls.Add(this.m_IndexPictureBoxesMatrix[i, n]);
                }
            }

            setMatchingPicturesInIndexPictureBoxesMatrix();
        }

        private void setMatchingPicturesInIndexPictureBoxesMatrix()
        {
            MatrixIndex firstMatrixIndex, secondMatrixIndex;
            this.m_MatrixIndexPairList = this.m_GameLogicComponent.MatrixIndexPairList;

            foreach (MatrixIndexPair matrixIndexPair in this.m_MatrixIndexPairList)
            {
                try
                {
                    firstMatrixIndex = matrixIndexPair.FirstIndex.Value;
                    secondMatrixIndex = matrixIndexPair.SecondIndex.Value;
                    this.m_ImageGetterFromWeb.Load(k_ImageURL);
                    this.m_IndexPictureBoxesMatrix[firstMatrixIndex.MatrixRowIndex, firstMatrixIndex.MatrixColumnIndex].IndexPictureBoxImage = this.m_ImageGetterFromWeb.Image;
                    this.m_IndexPictureBoxesMatrix[secondMatrixIndex.MatrixRowIndex, secondMatrixIndex.MatrixColumnIndex].IndexPictureBoxImage = this.m_ImageGetterFromWeb.Image;
                }
                catch (Exception)
                {
                    string errorMessage = string.Format("An error has occurred while loading the memory game pictures.{0}Please check your internet connection and try again later.", Environment.NewLine);
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }

        private void enableIndexButtonMatrix(bool i_EnabledIndexButton)
        {
            int height = this.m_CurrentGameBoardDimensions.Height, width = this.m_CurrentGameBoardDimensions.Width;

            for (int i = 0; i < height; i++)
            {
                for (int n = 0; n < width; n++)
                {
                    this.m_IndexPictureBoxesMatrix[i, n].Enabled = i_EnabledIndexButton;
                }
            }
        }

        private void setSuitableFormSize()
        {
            int height = 0, width = 0;

            width = k_ButtonSize * this.m_CurrentGameBoardDimensions.Width;
            width += (this.m_CurrentGameBoardDimensions.Width - 1) * 7;
            height = k_ButtonSize * this.m_CurrentGameBoardDimensions.Height;
            height += (this.m_CurrentGameBoardDimensions.Height - 1) * 7;
            memoryGameLayoutPanel.Size = new Size(width, height);
            //perfect size
            //this.Size = new Size(width + 28, height + 50);
            this.Size = new Size(width + 28, height + 120);
            this.playersInfoPanel.Location = new Point(10, height + 10);
        }

        private void IndexPictureBoxInMatrix_Click(object sender, EventArgs e)
        {
            preformATurn(sender as IndexPictureBox);
        }

        private void preformATurn(IndexPictureBox i_IndexButton)
        {
            if (this.m_GameLogicComponent.CheckIfCardIsNotShown(i_IndexButton.HeightIndex, i_IndexButton.WidthIndex))
            {
                this.m_PlayerStepsCounter++;
                this.m_PlayerPointer = this.m_GameLogicComponent.CurrentPlayerPointer;
                showClickedButtonText(i_IndexButton);
                if (this.m_PlayerStepsCounter == 1)
                {
                    this.m_PlayerPointer.FirstStep = performHumanStep(i_IndexButton);
                }
                else
                {
                    this.m_PlayerPointer.SecondStep = performHumanStep(i_IndexButton);
                    this.m_CardsRevealTimer.Start();
                }
            }
        }

        private void showClickedButtonText(IndexPictureBox i_IndexPictureBox)
        {
            i_IndexPictureBox.SetIndexPictureBoxImage();
            i_IndexPictureBox.BackColor = this.m_GameLogicComponent.CurrentPlayerPointer.PlayerColor;
        }

        private PlayerStep performHumanStep(IndexPictureBox i_IndexButtonSender)
        {
            PlayerStep playerStepToReturn = new PlayerStep(i_IndexButtonSender.HeightIndex, i_IndexButtonSender.WidthIndex);

            this.m_GameLogicComponent.SwitchCardStatus(playerStepToReturn);
            return playerStepToReturn;
        }

        private string getWinnerMessage()
        {
            string stringToReturn = string.Empty;
            Player winningPlayerPointer = null;

            this.m_GameLogicComponent.DecideWinningPlayer(ref winningPlayerPointer);
            if (winningPlayerPointer == null)
            {
                stringToReturn = "We have a tie here!";
            }
            else
            {
                stringToReturn = string.Format("{0}, you are the winner!", winningPlayerPointer.PlayerName);
            }

            return stringToReturn;
        }

        private void checkIfPlayerStepsAreCorrect()
        {
            this.m_PlayerPointer = this.m_GameLogicComponent.CurrentPlayerPointer;

            this.m_CardsRevealTimer.Stop();
            if (this.m_GameLogicComponent.CheckCardMatch(this.m_PlayerPointer.FirstStep, this.m_PlayerPointer.SecondStep))
            {
                this.m_GameLogicComponent.AddPoint();
            }
            else
            {
                if (!this.m_GameLogicComponent.PlayerTwo.IsHuman)
                {
                    this.m_GameLogicComponent.SaveMachineStepInList(this.m_GameLogicComponent.CurrentPlayerPointer.FirstStep);
                    this.m_GameLogicComponent.SaveMachineStepInList(this.m_GameLogicComponent.CurrentPlayerPointer.SecondStep);
                }

                Thread.Sleep(1000);
                coverUnmatchingCardsAndSwitchTurn();
                enableIndexButtonMatrix(true);
            }

            this.m_PlayerStepsCounter = 0;
            setTextInLables();
            if (!checkIfGameFinished() && !this.m_GameLogicComponent.CurrentPlayerPointer.IsHuman)
            {
                enableIndexButtonMatrix(false);
                this.m_MachineTurnTimer.Start();
            }
        }

        private bool checkIfGameFinished()
        {
            bool isGameFinished = this.m_GameLogicComponent.CheckIfGameIsFinished();

            if (isGameFinished)
            {
                string messageToShow = getWinnerMessage();
                messageToShow = string.Format("{0}{1}{2}", messageToShow, Environment.NewLine, "Would you like to play another game?");
                DialogResult dialogResult = MessageBox.Show(messageToShow, "Game Over", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    this.m_GameLogicComponent.ResetGameSettings();
                    this.m_GameLogicComponent.CreateAndFillMemoryBoard();
                    setMatchingPicturesInIndexPictureBoxesMatrix();
                    resetGameBoard();
                    setTextInLables();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Dispose();
                    this.Close();
                }
            }

            return isGameFinished;
        }

        private void setTextInLables()
        {
            this.currentPlayerInfoLabel.Text = string.Format("Current Player: {0}", this.m_GameLogicComponent.CurrentPlayerPointer.PlayerName);
            this.currentPlayerInfoLabel.BackColor = this.m_GameLogicComponent.CurrentPlayerPointer.PlayerColor;
            this.firstPlayerInfoLabel.Text = getPlayerInfoMessage(this.m_GameLogicComponent.PlayerOne);
            this.firstPlayerInfoLabel.BackColor = this.m_GameLogicComponent.PlayerOne.PlayerColor;
            this.secondPlayerInfoLabel.Text = getPlayerInfoMessage(this.m_GameLogicComponent.PlayerTwo);
            this.secondPlayerInfoLabel.BackColor = this.m_GameLogicComponent.PlayerTwo.PlayerColor;
        }

        private void coverUnmatchingCardsAndSwitchTurn()
        {
            this.m_PlayerPointer = this.m_GameLogicComponent.CurrentPlayerPointer;

            this.m_IndexPictureBoxesMatrix[this.m_PlayerPointer.FirstStep.RowIndex, this.m_PlayerPointer.FirstStep.ColumnIndex].BackColor = Color.FromArgb(0, 109, 240);
            this.m_IndexPictureBoxesMatrix[this.m_PlayerPointer.SecondStep.RowIndex, this.m_PlayerPointer.SecondStep.ColumnIndex].BackColor = Color.FromArgb(0, 109, 240);
            this.m_IndexPictureBoxesMatrix[this.m_PlayerPointer.FirstStep.RowIndex, this.m_PlayerPointer.FirstStep.ColumnIndex].SetQuestionMarkImage();
            this.m_IndexPictureBoxesMatrix[this.m_PlayerPointer.SecondStep.RowIndex, this.m_PlayerPointer.SecondStep.ColumnIndex].SetQuestionMarkImage();
            this.m_GameLogicComponent.SwitchCardStatus(this.m_PlayerPointer.FirstStep);
            this.m_GameLogicComponent.SwitchCardStatus(this.m_PlayerPointer.SecondStep);
            this.m_GameLogicComponent.SwitchTurn();
        }

        private void resetGameBoard()
        {
            if (!this.m_GameLogicComponent.PlayerTwo.IsHuman)
            {
                enableIndexButtonMatrix(true);
            }

            for (int i = 0; i < this.m_CurrentGameBoardDimensions.Height; i++)
            {
                for (int n = 0; n < this.m_CurrentGameBoardDimensions.Width; n++)
                {
                    this.m_IndexPictureBoxesMatrix[i, n].BackColor = Color.FromArgb(0, 109, 240);
                    this.m_IndexPictureBoxesMatrix[i, n].SetQuestionMarkImage();
                }
            }
        }

        private string getPlayerInfoMessage(Player i_CurrentPlayer)
        {
            string stringToReturn = string.Empty, pairString = string.Empty;

            if (i_CurrentPlayer.Score == 1)
            {
                pairString = "Pair";
            }
            else
            {
                pairString = "Pairs";
            }

            stringToReturn = string.Format("{0}: {1} {2}", i_CurrentPlayer.PlayerName, i_CurrentPlayer.Score, pairString);

            return stringToReturn;
        }
    }
}