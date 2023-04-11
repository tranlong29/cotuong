using System;
using System.Drawing;
using System.Windows.Forms;
using ChineseChess.Pieces;
using ChineseChess.Properties;

namespace ChineseChess
{
    public partial class ChessForm : Form
    {
        private readonly frmNewGame _frmNewGame = new frmNewGame();
        private readonly Timer _tmTotalTime = new Timer();
        private readonly Timer tm_BlackTime = new Timer();
        private readonly Timer tm_RedTime = new Timer();
        private int _totalTimeHour = 0;
        private int _totalTimeMin = 0;
        private int _totalTimeSec = 0;

        public ChessForm()
        {
            InitializeComponent();
            _tmTotalTime.Tick += _tmGame_Tick;
            _tmTotalTime.Interval = 1000;
            _tmTotalTime.Enabled = false;
            Game.NewGame();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Controls.Add(Board.Position[i, j].ValidMove);
                    Board.Position[i, j].ValidMove.MouseClick += ValidMove_MouseClick;
                }
            }
            AddPieces();
        }

        #region NewGame, Undo, Save, Load

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Game.Marked)
            {
                switch (Game.Playing)
                {
                    case true:
                        LockChessBoard();
                        _tmTotalTime.Enabled = false;
                        break;
                    case false:
                        _frmNewGame.ShowDialog(this);
                        break;
                }
                if (Game.Playing)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Controls.Add(Board.Position[i, j].ValidMove);
                            Board.Position[i, j].ValidMove.MouseClick += ValidMove_MouseClick;
                        }
                    }
                    AddPieces();
                    // start timer
                    _tmTotalTime.Enabled = true;
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pbUndo_Click(sender, e);
        }
        // TO DO: Write code to save game
        private void SavetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Game.Playing && !Game.Marked)
            {
                tm_BlackTime.Enabled = false;
                tm_RedTime.Enabled = false;
                _tmTotalTime.Enabled = false;
                LockChessBoard();
            }
        }

        private void pbUndo_Click(object sender, EventArgs e)
        {
            if (!Game.Marked)
            {
                Game.Undo();
            }
        }

        private void btnTurnAgain_Click(object sender, EventArgs e)
        {
            Game.Undo();
            Game.Undo();
            Game.PbGameOver.Visible = false;
            Game.Winner = -1;
        }

        private void btnGameOver_Click(object sender, EventArgs e)
        {
            Game.PbGameOver.Visible = false;
            LockChessBoard();
            if (Game.Winner == 0) // BLACK
            {
                MessageBox.Show("Quân đen thắng");
            }
            if (Game.Winner == 1) // RED
            {
                MessageBox.Show("Quân đỏ thắng");
            }
            Game.btnTurnAgain.Visible = false;
            Game.btnGameOver.Visible = false;
        }

        #endregion

        #region Event for pieces

        private void ChessBoard_MouseClick(Object sender, MouseEventArgs e)
        {
            var tmp = new Piece();
            switch (Game.Marked)
            {
                case true:
                    Game.Marked = false;
                    if (Game.MarkedPiece.Name.Equals("KING")) tmp = Game.Players[Game.MarkedPiece.Side].KingPiece;
                    if (Game.MarkedPiece.Name.Equals("ADVISOR"))
                        tmp = Game.Players[Game.MarkedPiece.Side].AdvisorPieces[Game.MarkedPiece.Index];
                    if (Game.MarkedPiece.Name.Equals("ELEPHANT"))
                        tmp = Game.Players[Game.MarkedPiece.Side].ElephantPieces[Game.MarkedPiece.Index];
                    if (Game.MarkedPiece.Name.Equals("HORSE"))
                        tmp = Game.Players[Game.MarkedPiece.Side].HorsePieces[Game.MarkedPiece.Index];
                    if (Game.MarkedPiece.Name.Equals("CHARIOT"))
                        tmp = Game.Players[Game.MarkedPiece.Side].ChariotPieces[Game.MarkedPiece.Index];
                    if (Game.MarkedPiece.Name.Equals("CANNON"))
                        tmp = Game.Players[Game.MarkedPiece.Side].CannonPieces[Game.MarkedPiece.Index];
                    if (Game.MarkedPiece.Name.Equals("SOLDIER"))
                        tmp = Game.Players[Game.MarkedPiece.Side].SoldierPieces[Game.MarkedPiece.Index];

                    if (tmp.Side == 0) // BLACK
                    {
                        if (tmp.Name.Equals("KING")) tmp.PbPiece.Image = Resources.B_K;
                        if (tmp.Name.Equals("ADVISOR")) tmp.PbPiece.Image = Resources.B_A;
                        if (tmp.Name.Equals("ELEPHANT")) tmp.PbPiece.Image = Resources.B_E;
                        if (tmp.Name.Equals("HORSE")) tmp.PbPiece.Image = Resources.B_H;
                        if (tmp.Name.Equals("CHARIOT")) tmp.PbPiece.Image = Resources.B_R;
                        if (tmp.Name.Equals("CANNON")) tmp.PbPiece.Image = Resources.B_C;
                        if (tmp.Name.Equals("SOLDIER")) tmp.PbPiece.Image = Resources.B_S;
                    }
                    else // RED
                    {
                        if (tmp.Name.Equals("KING")) tmp.PbPiece.Image = Resources.R_K;
                        if (tmp.Name.Equals("ADVISOR")) tmp.PbPiece.Image = Resources.R_A;
                        if (tmp.Name.Equals("ELEPHANT")) tmp.PbPiece.Image = Resources.R_E;
                        if (tmp.Name.Equals("HORSE")) tmp.PbPiece.Image = Resources.R_H;
                        if (tmp.Name.Equals("CHARIOT")) tmp.PbPiece.Image = Resources.R_R;
                        if (tmp.Name.Equals("CANNON")) tmp.PbPiece.Image = Resources.R_C;
                        if (tmp.Name.Equals("SOLDIER")) tmp.PbPiece.Image = Resources.R_S;
                    }
                    Board.ResetValidMove();
                    break;
                case false:
                    break;
            }
        }

        private void ValidMove_MouseClick(Object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sender.Equals(Board.Position[i, j].ValidMove))
                    {
                        if (Game.Marked)
                        {
                            Game.OldPosition.Row = Game.MarkedPiece.Row;
                            Game.OldPosition.Col = Game.MarkedPiece.Col;
                            Game.OldPosition.Side = Game.MarkedPiece.Side;
                            Game.OldPosition.PbPiece.BackColor = Color.Transparent;
                            Game.OldPosition.PbPiece.Image = Resources.OldPosition;
                            Game.OldPosition.PbPiece.Height = Game.OldPosition.PbPiece.Width = 40;
                            Game.OldPosition.PbPiece.Top = Game.MarkedPiece.Row * 48 + 37;
                            Game.OldPosition.PbPiece.Left = Game.MarkedPiece.Col * 48 + 12;
                            Controls.Add(Game.OldPosition.PbPiece);
                            switch (Board.Position[i, j].IsEmpty)
                            {
                                case true:
                                    if (Game.MarkedPiece.Side == 0) // BLACK
                                    {
                                        if (Game.MarkedPiece.Name.Equals("KING"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_K;
                                        if (Game.MarkedPiece.Name.Equals("ADVISOR"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_A;
                                        if (Game.MarkedPiece.Name.Equals("ELEPHANT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_E;
                                        if (Game.MarkedPiece.Name.Equals("HORSE"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_H;
                                        if (Game.MarkedPiece.Name.Equals("CHARIOT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_R;
                                        if (Game.MarkedPiece.Name.Equals("CANNON"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_C;
                                        if (Game.MarkedPiece.Name.Equals("SOLDIER"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_S;
                                    }
                                    else // RED
                                    {
                                        if (Game.MarkedPiece.Name.Equals("KING"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_K;
                                        if (Game.MarkedPiece.Name.Equals("ADVISOR"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_A;
                                        if (Game.MarkedPiece.Name.Equals("ELEPHANT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_E;
                                        if (Game.MarkedPiece.Name.Equals("HORSE"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_H;
                                        if (Game.MarkedPiece.Name.Equals("CHARIOT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_R;
                                        if (Game.MarkedPiece.Name.Equals("CANNON"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_C;
                                        if (Game.MarkedPiece.Name.Equals("SOLDIER"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_S;
                                    }
                                    Game.Marked = false;
                                    Game.SaveGameLog(this, Game.MarkedPiece);
                                    Game.ResetChessPoint(Game.MarkedPiece.Row, Game.MarkedPiece.Col);
                                    Game.SetPiece(Game.MarkedPiece, i, j);

                                    // Check capture king
                                    Game.CaptureKing();
                                    Game.ChangeTurn();
                                    // check gameover
                                    Game.GameOver();
                                    if (Game.Winner != -1)
                                    {
                                        Game.PbCaptureKing.Visible = false;
                                        Game.PbGameOver.Visible = true;
                                        Game.btnTurnAgain.Visible = true;
                                        Game.btnGameOver.Visible = true;
                                    }
                                    else
                                    {
                                        Game.PbGameOver.Visible = false;
                                        Game.btnTurnAgain.Visible = false;
                                        Game.btnGameOver.Visible = false;
                                    }
                                    Board.ResetValidMove();
                                    break;
                                case false: // not empty
                                    if (Game.MarkedPiece.Side == 0) // BLACK
                                    {
                                        if (Game.MarkedPiece.Name.Equals("KING"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_K;
                                        if (Game.MarkedPiece.Name.Equals("ADVISOR"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_A;
                                        if (Game.MarkedPiece.Name.Equals("ELEPHANT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_E;
                                        if (Game.MarkedPiece.Name.Equals("HORSE"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_H;
                                        if (Game.MarkedPiece.Name.Equals("CHARIOT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_R;
                                        if (Game.MarkedPiece.Name.Equals("CANNON"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_C;
                                        if (Game.MarkedPiece.Name.Equals("SOLDIER"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.B_S;
                                    }
                                    else // RED
                                    {
                                        if (Game.MarkedPiece.Name.Equals("KING"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_K;
                                        if (Game.MarkedPiece.Name.Equals("ADVISOR"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_A;
                                        if (Game.MarkedPiece.Name.Equals("ELEPHANT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_E;
                                        if (Game.MarkedPiece.Name.Equals("HORSE"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_H;
                                        if (Game.MarkedPiece.Name.Equals("CHARIOT"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_R;
                                        if (Game.MarkedPiece.Name.Equals("CANNON"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_C;
                                        if (Game.MarkedPiece.Name.Equals("SOLDIER"))
                                            Game.MarkedPiece.PbPiece.Image = Resources.R_S;
                                    }

                                    int otherSide;
                                    if (Game.MarkedPiece.Side == 0) otherSide = 1;
                                    else otherSide = 0;

                                    var tmp = new Piece();

                                    if (Board.Position[i, j].Name.Equals("KING"))
                                        tmp = Game.Players[otherSide].KingPiece;
                                    if (Board.Position[i, j].Name.Equals("ADVISOR"))
                                        tmp = Game.Players[otherSide].AdvisorPieces[Board.Position[i, j].Index];
                                    if (Board.Position[i, j].Name.Equals("ELEPHANT"))
                                        tmp = Game.Players[otherSide].ElephantPieces[Board.Position[i, j].Index];
                                    if (Board.Position[i, j].Name.Equals("HORSE"))
                                        tmp = Game.Players[otherSide].HorsePieces[Board.Position[i, j].Index];
                                    if (Board.Position[i, j].Name.Equals("CHARIOT"))
                                        tmp = Game.Players[otherSide].ChariotPieces[Board.Position[i, j].Index];
                                    if (Board.Position[i, j].Name.Equals("CANNON"))
                                        tmp = Game.Players[otherSide].CannonPieces[Board.Position[i, j].Index];
                                    if (Board.Position[i, j].Name.Equals("SOLDIER"))
                                        tmp = Game.Players[otherSide].SoldierPieces[Board.Position[i, j].Index];

                                    Game.Marked = false;
                                    Game.SaveGameLog(sender, tmp);
                                    Game.CapturePiece(tmp);
                                    Game.ResetChessPoint(Game.MarkedPiece.Row, Game.MarkedPiece.Col);
                                    Game.SetPiece(Game.MarkedPiece, i, j);
                                    Game.CaptureKing();
                                    Game.ChangeTurn();
                                    // check gameover
                                    Game.GameOver();
                                    if (Game.Winner != -1)
                                    {
                                        Game.PbCaptureKing.Visible = false;
                                        Game.PbGameOver.Visible = true;
                                        Game.btnTurnAgain.Visible = true;
                                        Game.btnGameOver.Visible = true;
                                    }
                                    else
                                    {
                                        Game.PbGameOver.Visible = false;
                                        Game.btnTurnAgain.Visible = false;
                                        Game.btnGameOver.Visible = false;
                                    }
                                    Board.ResetValidMove();
                                    break;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Action in ChessBoard

        private void _tmGame_Tick(object sender, EventArgs e)
        {
            string str = "";
            _totalTimeSec++;
            if (_totalTimeSec > 59)
            {
                _totalTimeMin++;
                if (_totalTimeMin > 59)
                {
                    _totalTimeHour++;
                    _totalTimeMin = 0;
                }
                _totalTimeSec = 0;
            }
            if (_totalTimeHour < 10)
            {
                if (_totalTimeMin < 10)
                {
                    if (_totalTimeSec < 10)
                    {
                        str = "0" + _totalTimeHour + " : 0" + _totalTimeMin + " : 0" + _totalTimeSec;
                    }
                    else
                    {
                        str = "0" + _totalTimeHour + " : 0" + _totalTimeMin + " : " + _totalTimeSec;
                    }
                }
                else
                {
                    str = "0" + _totalTimeHour + " : " + _totalTimeMin + " : " + _totalTimeSec;
                }
            }
            else
            {
                str = _totalTimeHour + " : " + _totalTimeMin + " : " + _totalTimeSec;
            }
            lblTotalTime.Text = str;
        }

        private void OpenChessBoard()
        {
            if (Game.Turn == 0) // Black
            {
                Game.Turn = 1;
                Game.ChangeTurn();
            }
            else
            {
                Game.Turn = 0;
                Game.ChangeTurn();
            }
        }

        private void LockChessBoard()
        {
            Game.Players[0].KingPiece.Lock = true;
            Game.Players[0].AdvisorPieces[0].Lock = true;
            Game.Players[0].AdvisorPieces[1].Lock = true;
            Game.Players[0].ElephantPieces[0].Lock = true;
            Game.Players[0].ElephantPieces[1].Lock = true;
            Game.Players[0].ChariotPieces[0].Lock = true;
            Game.Players[0].ChariotPieces[1].Lock = true;
            Game.Players[0].CannonPieces[0].Lock = true;
            Game.Players[0].CannonPieces[1].Lock = true;
            Game.Players[0].HorsePieces[0].Lock = true;
            Game.Players[0].HorsePieces[1].Lock = true;
            Game.Players[0].SoldierPieces[0].Lock = true;
            Game.Players[0].SoldierPieces[1].Lock = true;
            Game.Players[0].SoldierPieces[2].Lock = true;
            Game.Players[0].SoldierPieces[3].Lock = true;
            Game.Players[0].SoldierPieces[4].Lock = true;

            Game.Players[1].KingPiece.Lock = true;
            Game.Players[1].AdvisorPieces[0].Lock = true;
            Game.Players[1].AdvisorPieces[1].Lock = true;
            Game.Players[1].ElephantPieces[0].Lock = true;
            Game.Players[1].ElephantPieces[1].Lock = true;
            Game.Players[1].ChariotPieces[0].Lock = true;
            Game.Players[1].ChariotPieces[1].Lock = true;
            Game.Players[1].CannonPieces[0].Lock = true;
            Game.Players[1].CannonPieces[1].Lock = true;
            Game.Players[1].HorsePieces[0].Lock = true;
            Game.Players[1].HorsePieces[1].Lock = true;
            Game.Players[1].SoldierPieces[0].Lock = true;
            Game.Players[1].SoldierPieces[1].Lock = true;
            Game.Players[1].SoldierPieces[2].Lock = true;
            Game.Players[1].SoldierPieces[3].Lock = true;
            Game.Players[1].SoldierPieces[4].Lock = true;
        }

        private void AddPieces()
        {
            // BLACK
            Controls.Add(Game.Players[0].KingPiece.PbPiece);
            Controls.Add(Game.Players[0].AdvisorPieces[0].PbPiece);
            Controls.Add(Game.Players[0].AdvisorPieces[1].PbPiece);
            Controls.Add(Game.Players[0].ElephantPieces[0].PbPiece);
            Controls.Add(Game.Players[0].ElephantPieces[1].PbPiece);
            Controls.Add(Game.Players[0].ChariotPieces[0].PbPiece);
            Controls.Add(Game.Players[0].ChariotPieces[1].PbPiece);
            Controls.Add(Game.Players[0].CannonPieces[0].PbPiece);
            Controls.Add(Game.Players[0].CannonPieces[1].PbPiece);
            Controls.Add(Game.Players[0].HorsePieces[0].PbPiece);
            Controls.Add(Game.Players[0].HorsePieces[1].PbPiece);
            Controls.Add(Game.Players[0].SoldierPieces[0].PbPiece);
            Controls.Add(Game.Players[0].SoldierPieces[1].PbPiece);
            Controls.Add(Game.Players[0].SoldierPieces[2].PbPiece);
            Controls.Add(Game.Players[0].SoldierPieces[3].PbPiece);
            Controls.Add(Game.Players[0].SoldierPieces[4].PbPiece);

            //RED
            Controls.Add(Game.Players[1].KingPiece.PbPiece);
            Controls.Add(Game.Players[1].AdvisorPieces[0].PbPiece);
            Controls.Add(Game.Players[1].AdvisorPieces[1].PbPiece);
            Controls.Add(Game.Players[1].ElephantPieces[0].PbPiece);
            Controls.Add(Game.Players[1].ElephantPieces[1].PbPiece);
            Controls.Add(Game.Players[1].ChariotPieces[0].PbPiece);
            Controls.Add(Game.Players[1].ChariotPieces[1].PbPiece);
            Controls.Add(Game.Players[1].CannonPieces[0].PbPiece);
            Controls.Add(Game.Players[1].CannonPieces[1].PbPiece);
            Controls.Add(Game.Players[1].HorsePieces[0].PbPiece);
            Controls.Add(Game.Players[1].HorsePieces[1].PbPiece);
            Controls.Add(Game.Players[1].SoldierPieces[0].PbPiece);
            Controls.Add(Game.Players[1].SoldierPieces[1].PbPiece);
            Controls.Add(Game.Players[1].SoldierPieces[2].PbPiece);
            Controls.Add(Game.Players[1].SoldierPieces[3].PbPiece);
            Controls.Add(Game.Players[1].SoldierPieces[4].PbPiece);

            Controls.Add(Game.PbCaptureKing);
            Controls.Add(Game.PbGameOver);
            Controls.Add(Game.RedTurn);
            Controls.Add(Game.BlackTurn);
            Controls.Add(Game.btnGameOver);
            Controls.Add(Game.btnTurnAgain);
            Game.btnGameOver.Click += new System.EventHandler(btnGameOver_Click);
            Game.btnTurnAgain.Click += new System.EventHandler(btnTurnAgain_Click);
        }

        #endregion
    }
}