using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace B16_Ex06
{
    public partial class HowToPlay : Form
    {
        public HowToPlay()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            InitializeComponent();
            string msg = File.ReadAllText("C:\\FourInARowHelp.txt");
            textBox1.Text = msg;    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
