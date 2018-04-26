using System.Windows.Forms;
using System.Drawing;
using System;
using System.Drawing.Imaging;

namespace B16_Ex06
{
    public class BoardCell : PictureBox
    {
        private readonly int cellWidth;
        private readonly int cellHeight;
        private readonly Region defaultRegion;

        public BoardCell(int xPos, int yPos)
        {
            cellWidth = Properties.Resources.EmptyCell.Width;
            cellHeight = Properties.Resources.EmptyCell.Height;
            Location = new Point(yPos, xPos);
            Size = new Size(cellWidth, cellHeight);
            BackgroundImage = Properties.Resources.EmptyCell;
            defaultRegion = Region;
            BitmapRegion.CreateControlRegion(this);
        }

        public void ResetRegion()
        {
            Region = defaultRegion;
        }
    }   
}