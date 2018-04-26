using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex06
{
    public class Board
    {
        public enum BoardDimensions
        {
          MinNumOfRows = 4, MaxNumOfRows = 10, MinNumOfCols = 4, MaxNumOfCols = 10
        }

        public enum eBoardSquare
        {
            Blank = ' ', Player1 = 'O', Player2 = 'X'
        }

        private int m_NumOfRows;
        private int m_NumOfColumns;
        private eBoardSquare[,] m_GameBoard;

        public Board(int i_Rows, int i_Cols)
        {
            m_NumOfRows = i_Rows;
            m_NumOfColumns = i_Cols;
            m_GameBoard = new eBoardSquare[m_NumOfRows, m_NumOfColumns];
        }

        public int NumOfRows
        {
            get { return m_NumOfRows; }
            set { m_NumOfRows = value; }
        }

        public int NumOfColumns
        {
            get
            {
                return m_NumOfColumns;
            }

            set
            {
                m_NumOfColumns = value;
            }
        }

        public eBoardSquare this[int row, int col]
        {
            get
            {
                return m_GameBoard[row, col];
            }

            set
            {
                m_GameBoard[row, col] = value;
            }
        }

        public string this[int row]
        {
            get
            {
                return ConvertBoardSquareArrayToString(row);
            }
        }

        public void InitializeBoard()
        {          
            for (byte i = 0; i < m_NumOfRows; i++)
            {
                for (byte j = 0; j < m_NumOfColumns; j++)
                {
                    m_GameBoard[i, j] = eBoardSquare.Blank;
                }
            }
        }

        private string ConvertBoardSquareArrayToString(int row)
        {
            char[] tempCharArray = new char[m_NumOfColumns];

            for (int i = 0; i < m_NumOfColumns; i++)
            {
                tempCharArray[i] = (char)m_GameBoard[row, i];
            }

            string convertedResult = new string(tempCharArray);

            return convertedResult;
        }

        public bool CheckIfBoardFull()
        {
            bool boardFull = true;

            for (int i = 0; i < NumOfColumns; i++)
            {
                if (this[0, i] == eBoardSquare.Blank)
                {
                    boardFull = false;
                }
            }

            return boardFull;
        }

        public bool CheckIfBoardColumnFull(int col)
        {
            bool colFull = true;

            if (m_GameBoard[0, col] == eBoardSquare.Blank)
                {
                    colFull = false;
                }
           
            return colFull;
        }

        public bool CheckIfValidLocationOnBoard(int row, int col)
        {
            bool validLocation = true;
            int maxRowIndex = NumOfRows - 1;
            int maxColIndex = NumOfColumns - 1;

            if (row > maxRowIndex || row < 0)
            {
                validLocation = false;
            }

            if (col > maxColIndex || col < 0)
            {
                validLocation = false;
            }

            return validLocation;
        }
    }
}
