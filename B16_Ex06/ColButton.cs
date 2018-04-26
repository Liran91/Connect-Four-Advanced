using System;
using System.Windows.Forms;
using System.Drawing;

namespace B16_Ex06
{
    public class ColButton : Label
    {
        private readonly int r_Col;

        public ColButton(int i_XLocation, int i_YLocation, int i_Col)
        {
            Location = new Point(i_XLocation, i_YLocation);
            r_Col = i_Col;
            Size = new Size(Properties.Resources.FullCellRed.Width, Properties.Resources.FullCellRed.Height);
        }

        public bool Enabeld
        {
            get { return Enabeld; }
            set { Enabeld = value; }
        }

        public int Col
        {
            get { return r_Col; }
        }
    }
}
