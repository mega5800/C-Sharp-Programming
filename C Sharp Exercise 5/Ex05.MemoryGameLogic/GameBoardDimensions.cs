namespace Ex05.MemoryGameLogic
{
    public struct GameBoardDimensions
    {
        private readonly int r_Height;
        private readonly int r_Width;

        public GameBoardDimensions(int i_Height, int i_Width)
        {
            this.r_Height = i_Height;
            this.r_Width = i_Width;
        }

        public int Height
        {
            get { return this.r_Height; }
        }

        public int Width
        {
            get { return this.r_Width; }
        }

        public override string ToString()
        {
            string stringToReturn = string.Format("{0} x {1}", this.r_Height, this.r_Width);

            return stringToReturn;
        }
    }
}