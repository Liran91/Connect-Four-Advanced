using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B16_Ex06
{
    public class FormGameSettings : Form
    {
        private const string k_ComputerName = "[Computer]";
        private Button m_ButtonPlay;
        private Label labelPlayers;
        private Label labelPlayer1;
        private Label labelBoardSize;
        private Label labelRows;
        private Label labelCols;
        private Label m_CheckBoxPlayer2;
        private TextBox m_TextBoxPlayer1;
        private TextBox m_TextBoxPlayer2;
        private NumericUpDown m_RowsNumericUD;
        private NumericUpDown m_ColsNumericUD;

        private GameSettings m_GameSettings;

        public GameSettings GameSettings
        {
            get
            {
                return m_GameSettings;
            }
        }

        public FormGameSettings()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Game Settings";
            m_GameSettings = new GameSettings();
            InitializeComponent();
        }

        public void InitializeComponent()
        {
          m_ButtonPlay = new Button();
          labelPlayers = new Label();
          labelPlayer1 = new Label();
          labelBoardSize = new Label();
          labelRows = new Label();
          labelCols = new Label();
          m_CheckBoxPlayer2 = new Label();
          m_TextBoxPlayer1 = new TextBox();
          m_TextBoxPlayer2 = new TextBox();
          m_RowsNumericUD = new NumericUpDown();
          m_ColsNumericUD = new NumericUpDown();

          // LabelPlayers
          labelPlayers.AutoSize = true;
          labelPlayers.Text = "Players:";
          labelPlayers.Name = "LabelPlayers";
          labelPlayers.Location = new System.Drawing.Point(12, 19);
          labelPlayers.Size = new System.Drawing.Size(35, 13);

          // LabelPlayer1 init
          labelPlayer1.AutoSize = true;
          labelPlayer1.Text = "Player1:";
          labelPlayer1.Name = "abelPlayer1";
          labelPlayer1.Size = new System.Drawing.Size(35, 13);
          labelPlayer1.Location = new System.Drawing.Point(30, 48);

          // LabelBoardSize init
          labelBoardSize.AutoSize = true;
          labelBoardSize.Location = new System.Drawing.Point(30, 135);
          labelBoardSize.Name = "LabelBoardSize";
          labelBoardSize.Size = new System.Drawing.Size(35, 13);
          labelBoardSize.Text = "Board Size:";

          // LabelRows init
          labelRows.AutoSize = true;
          new System.Drawing.Point(41, 165);
          labelRows.Location = new System.Drawing.Point(41, 165);
          labelRows.Name = "LabelRows";
          labelRows.Size = new System.Drawing.Size(35, 13);
          labelRows.TabIndex = 4;
          labelRows.Text = "Rows:";

          // LabelCols init
          labelCols.AutoSize = true;
          labelCols.Location = new System.Drawing.Point(158, 165);
          labelCols.Name = "LabelCols";
          labelCols.Size = new System.Drawing.Size(35, 13);
          labelCols.TabIndex = 5;
          labelCols.Text = "Cols:";

          // TextBoxPlayer1 init
          m_TextBoxPlayer1.Location = new System.Drawing.Point(108, 45);
          m_TextBoxPlayer1.Name = "TextBoxPlayer1";
          m_TextBoxPlayer1.Size = new System.Drawing.Size(100, 20);

          // TextBoxPlayer2 init
          m_TextBoxPlayer2.Location = new System.Drawing.Point(108, 71);
          m_TextBoxPlayer2.Name = "TextBoxPlayer2";
          m_TextBoxPlayer2.Size = new System.Drawing.Size(100, 20);

          // CheckBoxPlayer2 init
          m_CheckBoxPlayer2.AutoSize = true;
          m_CheckBoxPlayer2.Location = new System.Drawing.Point(33, 74);
          m_CheckBoxPlayer2.Name = "CheckBoxPlayer2";
          m_CheckBoxPlayer2.Size = new System.Drawing.Size(80, 17);
          m_CheckBoxPlayer2.Text = "Player 2:";

          // RowsNumericUD init
          m_RowsNumericUD.Location = new System.Drawing.Point(82, 163);
          m_RowsNumericUD.Name = "RowsNumericUD";
          m_RowsNumericUD.Size = new System.Drawing.Size(32, 20);
          m_RowsNumericUD.Minimum = (int)Board.BoardDimensions.MinNumOfRows;
          m_RowsNumericUD.Maximum = (int)Board.BoardDimensions.MaxNumOfRows;

          // ColsNumericUD init
          m_ColsNumericUD.Location = new System.Drawing.Point(199, 163);
          m_ColsNumericUD.Name = "ColsNumericUD";
          m_ColsNumericUD.Size = new System.Drawing.Size(32, 20);
          m_ColsNumericUD.Minimum = (int)Board.BoardDimensions.MinNumOfCols;
          m_ColsNumericUD.Maximum = (int)Board.BoardDimensions.MaxNumOfCols;

          // ButtonPlay init
          m_ButtonPlay.Location = new System.Drawing.Point(44, 208);
          m_ButtonPlay.Name = "m_ButtonPlay";
          m_ButtonPlay.Size = new System.Drawing.Size(187, 31);
          m_ButtonPlay.TabIndex = 11;
          m_ButtonPlay.Text = "Play!";
          m_ButtonPlay.UseVisualStyleBackColor = true;
          m_ButtonPlay.Click += new System.EventHandler(ButtonPlay_Click);

          // Game Settings Form init
          AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          ClientSize = new System.Drawing.Size(284, 262);
          Controls.Add(labelPlayers);
          Controls.Add(labelPlayer1);
          Controls.Add(labelBoardSize);
          Controls.Add(labelRows);
          Controls.Add(labelCols);
          Controls.Add(m_TextBoxPlayer1);
          Controls.Add(m_TextBoxPlayer2);
          Controls.Add(m_CheckBoxPlayer2);
          Controls.Add(m_RowsNumericUD);
          Controls.Add(m_ColsNumericUD);
          Controls.Add(m_ButtonPlay);
          Name = "GameSettings";
          Text = "GameSettings";
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            bool valid = true;

            m_GameSettings.Rows = (int)m_RowsNumericUD.Value;
            m_GameSettings.Cols = (int)m_ColsNumericUD.Value;

            if (m_TextBoxPlayer1.Text == string.Empty)
            {
                valid = false;
                MessageBox.Show("Please enter a name for Player 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                m_GameSettings.Player1Name = m_TextBoxPlayer1.Text;
            }

                if (m_TextBoxPlayer2.Text == string.Empty)
                {
                    valid = false;
                    MessageBox.Show("Please enter a name for Player 2");
                }
                else
                {
                    m_GameSettings.Player2Name = m_TextBoxPlayer2.Text;
                }
            
            if (valid)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }
    }
}
