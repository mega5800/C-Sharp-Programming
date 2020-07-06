using System;

namespace Ex05.MemoryGameLogic
{
    public struct MatrixIndexPair
    {
        private readonly Nullable<MatrixIndex> r_FirstIndex;
        private readonly Nullable<MatrixIndex> r_SecondIndex;

        public MatrixIndexPair(Nullable<MatrixIndex> i_FirstIndex, Nullable<MatrixIndex> i_SecondIndex)
        {
            this.r_FirstIndex = i_FirstIndex;
            this.r_SecondIndex = i_SecondIndex;
        }

        public Nullable<MatrixIndex> FirstIndex
        {
            get { return this.r_FirstIndex; }
        }

        public Nullable<MatrixIndex> SecondIndex
        {
            get { return this.r_SecondIndex; }
        }
    }
}