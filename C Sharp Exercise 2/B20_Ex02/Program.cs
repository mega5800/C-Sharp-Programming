namespace B20_Ex02
{
    public class Program
    {
        // MAIN METHOD
        public static void Main()
        {
            runExercise2();
        }

        // # RUN METHOD # 
        private static void runExercise2()
        {
            string playerOneName = string.Empty, playerTwoName = string.Empty;
            GameUIComponent gameUIComponent = null;
            Player playerOne = null, playerTwo = null;
            int boardHeight, boardWidth;
            bool isHuman, isGameActive = true;

            playerOneName = GameUIComponent.GetValidPlayerName("first");
            playerOne = new Player(playerOneName, true);
            playerTwoName = GameUIComponent.ChooseOpponent(out isHuman);
            playerTwo = new Player(playerTwoName, isHuman);
            GameUIComponent.GreetPlayers(playerOne, playerTwo);
            gameUIComponent = new GameUIComponent();
            while (isGameActive)
            {
                GameUIComponent.GetValidBoardDimensions(out boardHeight, out boardWidth);
                gameUIComponent.CreateLogicComponent(boardHeight, boardWidth, playerOne, playerTwo);
                gameUIComponent.StartMatch();
                gameUIComponent.AnnounceWinner();
                isGameActive = gameUIComponent.NewGameQuestion();
            }
        }
    }
}