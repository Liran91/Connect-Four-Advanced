using System;

namespace B16_Ex06
{
    public class EventOnCoin : EventArgs
    {
        private readonly int r_Col;
        private readonly int r_Row;
        private readonly Board.eBoardSquare r_PlayerCoin;

        public EventOnCoin(int col, int row, Board.eBoardSquare playerCoin)
        {
            r_Col = col;
            r_Row = row;
            r_PlayerCoin = playerCoin;
        }

        public int Col
        {
            get { return r_Col; }
        }

        public int Row
        {
            get { return r_Row; }
        }

        public Board.eBoardSquare PlayerCoin
        {
            get { return r_PlayerCoin; }
        }
    }
}
