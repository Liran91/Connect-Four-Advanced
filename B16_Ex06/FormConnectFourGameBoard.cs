using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B16_Ex06
{
    public class FormConnectFourGameBoard : Form
    {
        public enum GameState
        {
           ContinuePlaying, Tie, Victory
        }

        private const string k_FormTitle = "Connect Four";
        private const string k_PlayerAndScoreString = "{0}: {1}";
        private const string k_TieString = "A Tie!";
        private const string k_WinString = "A Win!";
        private const string k_Player1Coin = "O";
        private const string k_Player2Coin = "X";
        private const string k_Blank = " ";
        private const string k_Computer = "Computer";

        private readonly List<ColButton> r_ColumnButtons;
        private readonly List<List<BoardCell>> r_CoinImageBoard;
        private readonly FormGameSettings r_GameSettingConnectFour;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem howToPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startANewTournirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startANewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripPlayersStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer1Name;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentPlayerName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentPlayer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer1Score;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer2Name;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer2Score;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Timer coinFallTimer = new Timer();
        private System.Windows.Forms.PictureBox fallingCoinPicBox;
        private System.Windows.Forms.Timer timerSwitch = new Timer();
        private System.Windows.Forms.Label labelAbout;

        private int m_Player1Score;
        private int m_Player2Score;
        private string m_Player1Name;
        private string m_Player2Name;
        private Board.eBoardSquare currentPlayerCoin;
        private Board m_GameBoard;
        private ConnectFourGameLogic m_GameLogic;
        private GameState m_GameState;
        private bool matchOver;
        private Label labelPlayer1;
        private Label labelPlayer2;
        private int m_numOfRows;
        private int m_numOfCols;

        public FormConnectFourGameBoard()
        {
            InitializeComponent();
            r_CoinImageBoard = new List<List<BoardCell>>();
            r_ColumnButtons = new List<ColButton>();
            m_GameLogic = new ConnectFourGameLogic();
            r_GameSettingConnectFour = new FormGameSettings();
            r_GameSettingConnectFour.ShowDialog();
            m_numOfRows = r_GameSettingConnectFour.GameSettings.Rows;
            m_numOfCols = r_GameSettingConnectFour.GameSettings.Cols;
            m_Player1Name = r_GameSettingConnectFour.GameSettings.Player1Name;
            m_Player2Name = r_GameSettingConnectFour.GameSettings.Player2Name;
            matchOver = false;
            m_Player1Score = 0;
            m_Player2Score = 0;
            currentPlayerCoin = Board.eBoardSquare.Player1;
            MaximizeBox = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            m_GameBoard = new Board(m_numOfRows, m_numOfCols);
            m_GameBoard.InitializeBoard();
            initlizeControls(m_Player1Name, m_Player2Name, m_numOfRows, m_numOfCols);

            m_GameState = GameState.ContinuePlaying;
        }

        private void InitializeComponent()
        {
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startANewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startANewTournirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStripPlayersStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCurrentPlayer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCurrentPlayerName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer1Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer1Score = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer2Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer2Score = new System.Windows.Forms.ToolStripStatusLabel();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.fallingCoinPicBox = new System.Windows.Forms.PictureBox();
            this.labelAbout = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStripPlayersStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.fallingCoinPicBox).BeginInit();
            this.SuspendLayout();
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startANewGameToolStripMenuItem,
            this.startANewTournirToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.exitToolStripMenuItem1 });
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // startANewGameToolStripMenuItem
            // 
            this.startANewGameToolStripMenuItem.Name = "startANewGameToolStripMenuItem";
            this.startANewGameToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.startANewGameToolStripMenuItem.Text = "Start a New Game";
            this.startANewGameToolStripMenuItem.Click += new System.EventHandler(this.StartANewGameToolStripMenuItem_Click);
            // 
            // startANewTournirToolStripMenuItem
            // 
            this.startANewTournirToolStripMenuItem.Name = "startANewTournirToolStripMenuItem";
            this.startANewTournirToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.startANewTournirToolStripMenuItem.Text = "Start a New Tournir";
            this.startANewTournirToolStripMenuItem.Click += new System.EventHandler(this.StartANewTournirToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToPlayToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem });
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToPlayToolStripMenuItem
            // 
            this.howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            this.howToPlayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.howToPlayToolStripMenuItem.Text = "How to play?";
            this.howToPlayToolStripMenuItem.Click += new System.EventHandler(this.howToPlayToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(467, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            //
            // statusStripPlayersStatus
            // 
            this.statusStripPlayersStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripPlayersStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCurrentPlayer,
            this.toolStripStatusLabelCurrentPlayerName,
            this.toolStripStatusLabelPlayer1Name,
            this.toolStripStatusLabelPlayer1Score,
            this.toolStripStatusLabelPlayer2Name,
            this.toolStripStatusLabelPlayer2Score });
            this.statusStripPlayersStatus.Location = new System.Drawing.Point(0, 447);
            this.statusStripPlayersStatus.Name = "statusStripPlayersStatus";
            this.statusStripPlayersStatus.Size = new System.Drawing.Size(467, 22);
            this.statusStripPlayersStatus.Stretch = false;
            this.statusStripPlayersStatus.TabIndex = 1;
            this.statusStripPlayersStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCurrentPlayer
            // 
            this.toolStripStatusLabelCurrentPlayer.Name = "toolStripStatusLabelCurrentPlayer";
            this.toolStripStatusLabelCurrentPlayer.Size = new System.Drawing.Size(88, 17);
            this.toolStripStatusLabelCurrentPlayer.Text = "Current Player :";
            this.toolStripStatusLabelCurrentPlayer.Visible = false;
            // 
            // toolStripStatusLabelCurrentPlayerName
            // 
            this.toolStripStatusLabelCurrentPlayerName.Name = "toolStripStatusLabelCurrentPlayerName";
            this.toolStripStatusLabelCurrentPlayerName.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabelCurrentPlayerName.Visible = false;
            // 
            // toolStripStatusLabelPlayer1Name
            // 
            this.toolStripStatusLabelPlayer1Name.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.toolStripStatusLabelPlayer1Name.Name = "toolStripStatusLabelPlayer1Name";
            this.toolStripStatusLabelPlayer1Name.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabelPlayer1Name.Visible = false;
            // 
            // toolStripStatusLabelPlayer1Score
            // 
            this.toolStripStatusLabelPlayer1Score.Margin = new System.Windows.Forms.Padding(-5, 3, 0, 2);
            this.toolStripStatusLabelPlayer1Score.Name = "toolStripStatusLabelPlayer1Score";
            this.toolStripStatusLabelPlayer1Score.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelPlayer1Score.Text = "0";
            this.toolStripStatusLabelPlayer1Score.Visible = false;
            // 
            // toolStripStatusLabelPlayer2Name
            // 
            this.toolStripStatusLabelPlayer2Name.Name = "toolStripStatusLabelPlayer2Name";
            this.toolStripStatusLabelPlayer2Name.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabelPlayer2Name.Visible = false;
            // 
            // toolStripStatusLabelPlayer2Score
            // 
            this.toolStripStatusLabelPlayer2Score.Margin = new System.Windows.Forms.Padding(-5, 3, 0, 2);
            this.toolStripStatusLabelPlayer2Score.Name = "toolStripStatusLabelPlayer2Score";
            this.toolStripStatusLabelPlayer2Score.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelPlayer2Score.Text = "0";
            this.toolStripStatusLabelPlayer2Score.Visible = false;
            // 
            // gamePanel
            // 
            this.gamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right));
            this.gamePanel.AutoSize = true;
            this.gamePanel.BackColor = System.Drawing.Color.Plum;
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gamePanel.Location = new System.Drawing.Point(12, 50);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(443, 372);
            this.gamePanel.TabIndex = 2;
            // 
            // fallingCoinPicBox
            // 
            this.fallingCoinPicBox.Location = new System.Drawing.Point(-1, -1);
            this.fallingCoinPicBox.Name = "fallingCoinPicBox";
            this.fallingCoinPicBox.Size = new System.Drawing.Size(67, 67);
            this.fallingCoinPicBox.TabIndex = 0;
            this.fallingCoinPicBox.TabStop = false;

            // 
            // labelAbout
            // 
            this.labelAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAbout.BackColor = System.Drawing.Color.GhostWhite;
            this.labelAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelAbout.Location = new System.Drawing.Point(67, 162);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(333, 145);
            this.labelAbout.TabIndex = 5;
            this.labelAbout.Text = "4 In A Row !! :)";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAbout.Visible = false;
            this.labelAbout.Click += new System.EventHandler(this.labelAbout_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(467, 469);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.statusStripPlayersStatus);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "4 In a Row!!";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStripPlayersStatus.ResumeLayout(false);
            this.statusStripPlayersStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fallingCoinPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void GameOver(Board.eBoardSquare CurrentPlayerCoin, GameState GameState)
        {
            if (GameState == GameState.Victory)
            {
                string playerName;

                m_GameState = GameState.Victory;
                if (currentPlayerCoin == Board.eBoardSquare.Player1)
                {
                    playerName = m_Player1Name;
                    m_Player1Score++;
                    labelPlayer1.Text = string.Format(k_PlayerAndScoreString, m_Player1Name, m_Player1Score);
                }
                else
                {
                    playerName = m_Player2Name;
                    m_Player2Score++;
                    labelPlayer2.Text = string.Format(k_PlayerAndScoreString, m_Player2Name, m_Player2Score);
                }

                DialogResult dialogResult = MessageBox.Show(
                    string.Format(
@"{0} Won!!
Another round?",
               playerName),
                k_WinString,
                MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    ResetBoards();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Close();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(
                    string.Format(@"Tie!!
Another round?"),
               k_TieString,
               MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    ResetBoards();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Close();
                }
            }
        }

        private void ChangeToNextPlayerTurn()
        {
            if (currentPlayerCoin == Board.eBoardSquare.Player1)
            {
                currentPlayerCoin = Board.eBoardSquare.Player2;
            }
            else
            {
                currentPlayerCoin = Board.eBoardSquare.Player1;
            }
        }

        private void RestartGame()
        {
            currentPlayerCoin = Board.eBoardSquare.Player1;
            ClearCoinBoard();
            clearButtonPressed();
            InitColButtons(r_GameSettingConnectFour.GameSettings.Cols);
            initBoard(r_GameSettingConnectFour.GameSettings.Rows, r_GameSettingConnectFour.GameSettings.Cols);
            updatePlayersLabels(r_GameSettingConnectFour.GameSettings.Player1Name, r_GameSettingConnectFour.GameSettings.Player2Name);
            m_GameBoard.InitializeBoard();
            gamePanel.Controls.Remove(fallingCoinPicBox);
            gamePanel.Controls.Add(fallingCoinPicBox);
            fixLabelAboutSize(r_GameSettingConnectFour.GameSettings.Cols, r_GameSettingConnectFour.GameSettings.Rows);
            m_GameState = GameState.ContinuePlaying;
            CenterToParent();
        }

        private void ClearCoinBoard()
        {
            foreach (List<BoardCell> boardColList in r_CoinImageBoard)
            {
                foreach (BoardCell item in boardColList)
                {
                    gamePanel.Controls.Remove(item);
                }
            }

            r_CoinImageBoard.Clear();
        }

        private void clearButtonPressed()
        {
            foreach (ColButton item in r_ColumnButtons)
            {
                Controls.Remove(item);
            }

            r_ColumnButtons.Clear();
        }

        private void ResetBoards()
        {
            m_GameBoard.InitializeBoard();
            m_GameState = GameState.ContinuePlaying;
            currentPlayerCoin = Board.eBoardSquare.Player1;
            matchOver = false;
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
                HowToPlay HowToPlay = new HowToPlay();
                HowToPlay.ShowDialog();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
            gamePanel.Enabled = false;
            menuStrip1.Enabled = false;
            labelAbout.Visible = true;
        }

        private void labelAbout_Click(object sender, EventArgs e)
        {
            gamePanel.Enabled = true;
            menuStrip1.Enabled = true;
            labelAbout.Visible = false;
        }

        public void initlizeControls(string Player1Name, string Player2Name, int Row, int Col)
        {
            initLabel(Player1Name, Player2Name);
            InitColButtons(Col);
            initBoard(Row, Col);
            this.gamePanel.Controls.Add(this.fallingCoinPicBox);
            fixLabelAboutSize(Col, Row);
            CenterToParent();
        }

        private void fixLabelAboutSize(int Col, int Row)
        {
            double getMaxSize = Col > Row ? Col : Row;
            getMaxSize = getMaxSize * 3.5;
            labelAbout.Font = new Font("Calibri", (float)getMaxSize, FontStyle.Bold);
        }

        private void initLabel(string Player1Name, string Player2Name)
        {
            toolStripStatusLabelCurrentPlayerName.Text = Player1Name;
            updatePlayersLabels(Player1Name, Player2Name);
            changeLabelsVisibileProperty(true);
        }

        private void updatePlayersLabels(string Player1Name, string Player2Name)
        {
            string dot = ":";
            toolStripStatusLabelPlayer1Name.Text = Player1Name + dot;
            toolStripStatusLabelPlayer2Name.Text = Player2Name + dot;
            toolStripStatusLabelCurrentPlayerName.Text = Player1Name;
        }

        private void changeLabelsVisibileProperty(bool Visible)
        {
            toolStripStatusLabelCurrentPlayer.Visible = Visible;
            toolStripStatusLabelCurrentPlayerName.Visible = Visible;

            toolStripStatusLabelPlayer1Name.Visible = Visible;
            toolStripStatusLabelPlayer1Score.Visible = Visible;

            toolStripStatusLabelPlayer2Score.Visible = Visible;
            toolStripStatusLabelPlayer2Name.Visible = Visible;
        }

        private void initBoard(int Row, int Col)
        {
            int nextYPosition;
            int nextXPosition = 0;
            Size panelSizeBeforeChngedBoard = gamePanel.MaximumSize;

            r_CoinImageBoard.Capacity = Col;

            for (int i = 0; i < r_CoinImageBoard.Capacity; ++i)
            {
                r_CoinImageBoard.Add(new List<BoardCell>(Row));
                nextYPosition = 67;

                for (int j = 0; j < Row; ++j)
                {
                    BoardCell newBoardCell = new BoardCell(nextYPosition, nextXPosition);
                    newBoardCell.BackgroundImage = Properties.Resources.EmptyCell;
                    r_CoinImageBoard[i].Add(newBoardCell);
                    gamePanel.Controls.Add(newBoardCell);
                    nextYPosition += Properties.Resources.EmptyCell.Size.Height;
                }

                nextXPosition += Properties.Resources.EmptyCell.Size.Width;
            }

            ResizeForm(Col, Row);
        }

        private void InitColButtons(int Col)
        {
            r_ColumnButtons.Capacity = Col;
            int yPosition = 0;
            int nextXPosition = 0;
            for (int i = 0; i < r_ColumnButtons.Capacity; i++)
            {
                ColButton newColButton = new ColButton(nextXPosition, yPosition, i);
                newColButton.Click += ColButton_Click;
                r_ColumnButtons.Add(newColButton);
                nextXPosition += Properties.Resources.EmptyCell.Size.Width;
            }
        }

        private void ResizeForm(int Col, int Row)
        {
            gamePanel.Controls.AddRange(r_ColumnButtons.ToArray());
            gamePanel.MaximumSize = new Size(Col * 67, (Row + 1) * 67);
            Size = new Size(gamePanel.MaximumSize.Width + 48, gamePanel.MaximumSize.Height + menuStrip1.Height + statusStripPlayersStatus.Height + 132);
        }

        private void ColButton_Click(object sender, EventArgs e)
        {
            ColButton buttonPressed = sender as ColButton;
            bool gameWon = false;
            int selectedCol = buttonPressed.Col;
            int lastRowInsertedTo = -1;
            if (coinFallTimer.Enabled == false && buttonPressed != null && !m_GameBoard.CheckIfBoardColumnFull(selectedCol))
            {
                m_GameLogic.MakeMove(m_GameBoard, currentPlayerCoin, selectedCol, ref lastRowInsertedTo, ref gameWon);
                if (gameWon)
                {
                    m_GameState = GameState.Victory;
                    matchOver = true;                  
                }

                if (m_GameBoard.CheckIfBoardFull() && m_GameState == GameState.ContinuePlaying)
                {
                    m_GameState = GameState.Tie;
                    matchOver = true;
                }

                CoinFallingAnimation(buttonPressed.Col, lastRowInsertedTo, m_GameState);
            }    
        }

        private void CoinFallingAnimation(int Col, int Row, GameState Status)
        {
            if (coinFallTimer.Enabled == false)
            {
                fallingCoinPicBox.Image = GetCurrentPlayerCoinImage();
                fallingCoinPicBox.Visible = true;
                fallingCoinPicBox.Location = new Point(67 * Col, 0);
                EventCoinAnimation coinArgs = new EventCoinAnimation(
                fallingCoinPicBox.Location,
                r_CoinImageBoard[Col][Row].Bottom,
                new EventOnCoin(Col, Row, currentPlayerCoin),
                Status);
                EventHandler eventHandlerTickMethod = new EventHandler(delegate(object sender, EventArgs e) { coinFallTimer_Tick(this, coinArgs); });
                coinArgs.EventHandlerMethod = eventHandlerTickMethod;

                coinFallTimer.Tick += eventHandlerTickMethod;
                coinFallTimer.Interval = 25;
                coinFallTimer.Start();
            }
        }

        private Image GetCurrentPlayerCoinImage()
        {
            return currentPlayerCoin == Board.eBoardSquare.Player1 ? Properties.Resources.CoinRed : Properties.Resources.CoinYellow;
        }

        private void coinFallTimer_Tick(object sender, EventArgs e)
        {
            bool Finished = false;
            EventCoinAnimation coinArgsElement = e as EventCoinAnimation;

            for (int i = 0; i < 2 && !Finished; ++i)
            {
                if (fallingCoinPicBox.Bottom >= coinArgsElement.BotoomLimit)
                {
                    Finished = true;
                    CoinFallAnimationDone(coinArgsElement);
                }
                else
                {
                    fallingCoinPicBox.Top += 5;
                }
            }
        }

        private void CoinFallAnimationDone(EventCoinAnimation coinPos)
        {
            coinFallTimer.Stop();

            fallingCoinPicBox.Visible = false;
            r_CoinImageBoard[coinPos.CoinArgs.Col][coinPos.CoinArgs.Row].ResetRegion(); // Change the region now, because the picture is not in empty cell.          

            coinFallTimer.Tick -= coinPos.EventHandlerMethod;
            ChangeCellToFullCell(coinPos.CoinArgs);

            if (matchOver)
            {
                GameOver(coinPos.State, coinPos.CoinArgs);
            }
            else
            {
                ChangeToNextPlayerTurn();
                ChangeCurrentPlayerName();
            }
        }

        private void ChangeCellToFullCell(EventOnCoin coinPos)
        {
            if (currentPlayerCoin == Board.eBoardSquare.Player1)
            {
                r_CoinImageBoard[coinPos.Col][coinPos.Row].BackgroundImage = Properties.Resources.FullCellRed;
            }
            else
            {
                r_CoinImageBoard[coinPos.Col][coinPos.Row].BackgroundImage = Properties.Resources.FullCellYellow;
            }
        }

        private void GameOver(GameState GameState, EventOnCoin Coin)
        {
            if (GameState == GameState.Victory)
            {
                string playerName;

                m_GameState = GameState.Victory;
                if (currentPlayerCoin == Board.eBoardSquare.Player1)
                {
                    playerName = m_Player1Name;
                    m_Player1Score++;
                    toolStripStatusLabelPlayer1Score.Text = m_Player1Score.ToString();
                }
                else
                {
                    playerName = m_Player2Name;
                    m_Player2Score++;
                    toolStripStatusLabelPlayer2Score.Text = m_Player2Score.ToString();
                }
            
                DialogResult dialogResult = MessageBox.Show(
                    string.Format(
@"{0} Won!!
Another round?",
               playerName),
                k_WinString,
                MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    matchOver = false;
                    RestartGame();   
                }
                else if (dialogResult == DialogResult.No)
                {
                    Close();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(
                    string.Format(@"Tie!!
Another round?"),
               k_TieString,
               MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    matchOver = false;
                    RestartGame();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Close();
                }
            }
        }

        private void ChangeCurrentPlayerName()
        {
            string name = GetCurrentPlayerName();
            name = name.Remove(name.Length - 1); // delete the -> ':'
            toolStripStatusLabelCurrentPlayerName.Text = name;
        }

        private string GetCurrentPlayerName()
        {
            return currentPlayerCoin == Board.eBoardSquare.Player1 ? toolStripStatusLabelPlayer1Name.Text : toolStripStatusLabelPlayer2Name.Text;
        }

        private void StartANewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void StartANewTournirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestartGame();
            toolStripStatusLabelPlayer1Score.Text = "0";
            toolStripStatusLabelPlayer2Score.Text = "0";
            m_Player1Score = 0;
            m_Player2Score = 0;
        }
    }
}