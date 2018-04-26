using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex06
{
    public class ConnectFourGameLogic
    {
        public void MakeMove(Board connectFourBoard, Board.eBoardSquare playerCoin, int selectedColumn, ref int lastRowInsertedTo, ref bool gameWon)
        {
            int rowToInsertTo = GetFirstOpenSpotInColumn(connectFourBoard, selectedColumn);
            connectFourBoard[rowToInsertTo, selectedColumn] = playerCoin;
            lastRowInsertedTo = rowToInsertTo;
            gameWon = CheckIfGameWon(connectFourBoard, rowToInsertTo, selectedColumn, playerCoin);
        }

        public int GetFirstOpenSpotInColumn(Board connectFourBoard, int column)
        {
            int bottomRow = connectFourBoard.NumOfRows - 1;
            bool openSpotFound = false;
            int openSpotRowNum = 0;

            for (int i = bottomRow; i >= 0 && !openSpotFound; i--)
            {
                if (connectFourBoard[i, column] == Board.eBoardSquare.Blank)
                {
                    openSpotFound = true;
                    openSpotRowNum = i;
                }
            }

            return openSpotRowNum;
        }

        private bool CheckIfGameWon(Board connectFourBoard, int lastInsertedRow, int lastInsertedCol, Board.eBoardSquare playerCoin)
        {
            bool gameWon = false;

            gameWon = CheckRowForConnectedFour(connectFourBoard, lastInsertedRow, playerCoin);

            if (!gameWon)
            {
                gameWon = CheckColForConnectedFour(connectFourBoard, lastInsertedCol, playerCoin);

                if (!gameWon)
                {
                    gameWon = CheckDiagonalsForConnectedFour(connectFourBoard, lastInsertedRow, lastInsertedCol, playerCoin);
                }
            }

            return gameWon;
        }

        private bool CheckRowForConnectedFour(Board connectFourBoard, int lastInsertedRow, Board.eBoardSquare playerCoin)
        {
            int connectedFourFound = 0;
            bool connectedFourFoundInRow = false;

            string rowString = connectFourBoard[lastInsertedRow];

            if (playerCoin == Board.eBoardSquare.Player1)
            {
                connectedFourFound = rowString.IndexOf("OOOO");
            }
            else
            {
                connectedFourFound = rowString.IndexOf("XXXX");
            }

            if (connectedFourFound != -1)
            {
                connectedFourFoundInRow = true;
            }

            return connectedFourFoundInRow;
        }

        private bool CheckColForConnectedFour(Board connectFourBoard, int lastInsertedCol, Board.eBoardSquare playerCoin)
        {
            bool connectedFourFoundInCol = false;
            int numOfRows = connectFourBoard.NumOfRows;
            int coinCount = 0;
            for (int i = 0; i < numOfRows; i++)
            {
                if (connectFourBoard[i, lastInsertedCol] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInCol = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }
            }

            return connectedFourFoundInCol;
        }

        private bool CheckDiagonalsForConnectedFour(Board connectFourBoard, int lastInsertedRow, int lastInsertedCol, Board.eBoardSquare playerCoin)
        {
            bool gameWon = false;

            gameWon = CheckFullLeftDiagonalForConnectedFour(connectFourBoard, lastInsertedRow, lastInsertedCol, playerCoin);

            if (!gameWon)
            {
                gameWon = CheckFullRightDiagonalForConnectedFour(connectFourBoard, lastInsertedRow, lastInsertedCol, playerCoin);
            }

            return gameWon;
        }

        private bool CheckFullLeftDiagonalForConnectedFour(Board connectFourBoard, int row, int col, Board.eBoardSquare playerCoin)
        {
            bool validLocation = false;
            int coinCount = 0;
            bool connectedFourFoundInRightDiagonal = false;
            int minRow = 0;
            int minCol = 0;
            GetLowestBottomLeftSquare(connectFourBoard, ref minRow, ref minCol, row, col);

            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(minRow, minCol);

            while (validLocation)
            {
                if (connectFourBoard[minRow, minCol] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInRightDiagonal = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }

                minCol++;
                minRow--;
                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(minRow, minCol);
            }

            return connectedFourFoundInRightDiagonal;
        }

        private bool CheckFullRightDiagonalForConnectedFour(Board connectFourBoard, int row, int col, Board.eBoardSquare playerCoin)
        {
            bool validLocation = false;
            int coinCount = 0;
            bool connectedFourFoundInRightDiagonal = false;
            int minRow = 0;
            int minCol = 0;
            GetLowestBottomRightSquare(connectFourBoard, ref minRow, ref minCol, row, col);

            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(minRow, minCol);

            while (validLocation)
            {
                if (connectFourBoard[minRow, minCol] == playerCoin)
                {
                    coinCount++;

                    if (coinCount == 4)
                    {
                        connectedFourFoundInRightDiagonal = true;
                    }
                }
                else
                {
                    coinCount = 0;
                }

                minRow--;
                minCol--;
                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(minRow, minCol);
            }

            return connectedFourFoundInRightDiagonal;
        }

        private void GetLowestBottomLeftSquare(Board connectFourBoard, ref int row, ref int col, int currRow, int currCol)
        {
            bool validLocation = false;
            int resRow = currRow;
            int resCol = currCol;
            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(currRow, currCol);

            while (validLocation)
            {
                currRow++;
                currCol--;
                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(currRow, currCol);
                if (validLocation)
                {
                    resRow++;
                    resCol--;
                }
            }

            row = resRow;
            col = resCol;
        }

        private void GetLowestBottomRightSquare(Board connectFourBoard, ref int row, ref int col, int currRow, int currCol)
        {
            bool validLocation = false;
            int resRow = currRow;
            int resCol = currCol;
            validLocation = connectFourBoard.CheckIfValidLocationOnBoard(currRow, currCol);

            while (validLocation)
            {
                currRow++;
                currCol++;
                validLocation = connectFourBoard.CheckIfValidLocationOnBoard(currRow, currCol);
                if (validLocation)
                {
                    resRow++;
                    resCol++;
                }
            }

            row = resRow;
            col = resCol;
        }
    }
}
