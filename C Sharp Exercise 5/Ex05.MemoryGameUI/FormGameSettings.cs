using System;
using System.Drawing;
using System.Windows.Forms;
using Ex05.MemoryGameLogic;

namespace Ex05.MemoryGameUI
{
    public partial class FormGameSettings : Form
    {
        private GameLogicComponent m_GameLogicComponent;
        private GameBoardDimensions m_CurrentGameBoardDimensions;
        private FormGameBoard m_FormGameBoard;
        private static bool s_IsFormGameBoardShowed;

        public FormGameSettings()
        {
            this.m_CurrentGameBoardDimensions = new GameBoardDimensions(4, 4);
            this.FormClosing += FormGameSettings_FormClosing;
            InitializeComponent();
        }

        private void FormGameSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!s_IsFormGameBoardShowed)
            {
                e.Cancel = true;
                closeSettingsFormAndShowMemoryGameForm();
            }
        }

        private void buttonAgainstOpponent_Click(object sender, EventArgs e)
        {
            Button buttonAgainstOpponent = sender as Button;
            if (buttonAgainstOpponent.Text == "Against a Friend")
            {
                this.buttonAgainstOpponent.Text = "Against Computer";
                this.textBoxSecondPlayerName.Text = "";
                this.textBoxSecondPlayerName.Enabled = true;
            }
            else
            {
                this.buttonAgainstOpponent.Text = "Against a Friend";
                this.textBoxSecondPlayerName.Text = "- computer -";
                this.textBoxSecondPlayerName.Enabled = false;
            }
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            this.m_CurrentGameBoardDimensions = GameLogicComponent.GetNextGameBoardDimensions();
            this.buttonBoardSize.Text = m_CurrentGameBoardDimensions.ToString();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            closeSettingsFormAndShowMemoryGameForm();
        }

        private void closeSettingsFormAndShowMemoryGameForm()
        {
            this.m_FormGameBoard = getFormGameBoard();

            if (this.m_FormGameBoard != null)
            {
                this.Dispose();
                s_IsFormGameBoardShowed = true;
                this.m_FormGameBoard.ShowDialog();
            }
        }

        private FormGameBoard getFormGameBoard()
        {
            FormGameBoard formGameBoard = null;

            if (checkIfEnteredNamesAreValid(textBoxFirstPlayerName.Text, textBoxSecondPlayerName.Text))
            {
                Player playerOne = new Player(textBoxFirstPlayerName.Text, true, Color.FromArgb(0, 192, 0));
                Player playerTwo;

                if (textBoxSecondPlayerName.Enabled)
                {
                    playerTwo = new Player(textBoxSecondPlayerName.Text, true, Color.FromArgb(148, 0, 211));
                }
                else
                {
                    playerTwo = new Player("Computer", false, Color.FromArgb(148, 0, 211));
                }

                this.m_GameLogicComponent = new GameLogicComponent(m_CurrentGameBoardDimensions, playerOne, playerTwo);
                formGameBoard = new FormGameBoard(this.m_GameLogicComponent, this.m_CurrentGameBoardDimensions);
            }

            return formGameBoard;
        }

        private bool checkIfEnteredNamesAreValid(string i_FirstPlayerNameToCheck, string i_SecondPlayerNameToCheck)
        {
            bool checkResult = !string.IsNullOrEmpty(i_FirstPlayerNameToCheck) && !string.IsNullOrEmpty(i_SecondPlayerNameToCheck);

            if (checkResult)
            {
                bool firstPlayerNameValidity = checkIfPlayerNameConsistsOfEnglishLetters(i_FirstPlayerNameToCheck);
                bool secondPlayerNameValidity = checkIfPlayerNameConsistsOfEnglishLetters(i_SecondPlayerNameToCheck);

                checkResult = firstPlayerNameValidity && secondPlayerNameValidity;
                if (!checkResult)
                {
                    string errorMessage = string.Format("One or more name fields are invalid.{0}Please enter names correctly using only english letters.", Environment.NewLine);
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string errorMessage = string.Format("One or more name fields are empty.{0}Please enter names correctly.", Environment.NewLine);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return checkResult;
        }

        private bool checkIfPlayerNameConsistsOfEnglishLetters(string i_PlayerName)
        {
            bool checkResult = true;

            if (this.textBoxSecondPlayerName.Enabled)
            {
                foreach (char stringChar in i_PlayerName)
                {
                    if (!char.IsLetter(stringChar))
                    {
                        checkResult = false;
                        break;
                    }
                }
            }

            return checkResult;
        }
    }
}