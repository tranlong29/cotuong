using System;
using System.Drawing;
using System.Windows.Forms;
using ChineseChess.Properties;

namespace ChineseChess.Pieces
{
    /// <summary>
    /// Define a piece
    /// </summary>
    class Piece
    {
        #region Properties

        public int Row { get; set; }

        public int Col { get; set; }

        public string Name { get; private set; }

        public int Side { get; set; }

        public int Index { get; private set; }

        public int State { get; set; }

        public PictureBox PbPiece
        {
            get { return _pbPiece; }
        }

        public bool Lock { private get; set; }

        #endregion

        private readonly PictureBox _pbPiece = new PictureBox();

        public Piece()
        {
            Row = Col = 10;
            Name = "";
            Side = -1;
            Index = -1;
            State = -1;
            Lock = true;
        }

        /// <summary>
        /// Initialize piece
        /// </summary>
        public void Init(int side, string name, int index, int state, bool _lock, int row, int col)
        {
            Row = row;
            Col = col;
            Name = name;
            Side = side;
            Index = index;
            State = state;
            Lock = _lock;
            Board.Position[row, col].Row = row;
            Board.Position[row, col].Col = col;
            Board.Position[row, col].Name = name;
            Board.Position[row, col].Index = index;
            Board.Position[row, col].Side = side;
            Board.Position[row, col].IsEmpty = false;
            _pbPiece.MouseClick += pbPieces_MouseClick;
        }

        /// <summary>
        /// Draw Pieces
        /// </summary>
        public void Draw()
        {
            if (Side == 0) // BLACK
            {
                if (Name.Equals("KING")) _pbPiece.Image = Resources.B_K;
                if (Name.Equals("ADVISOR")) _pbPiece.Image = Resources.B_A;
                if (Name.Equals("ELEPHANT")) _pbPiece.Image = Resources.B_E;
                if (Name.Equals("HORSE")) _pbPiece.Image = Resources.B_H;
                if (Name.Equals("CHARIOT")) _pbPiece.Image = Resources.B_R;
                if (Name.Equals("CANNON")) _pbPiece.Image = Resources.B_C;
                if (Name.Equals("SOLDIER")) _pbPiece.Image = Resources.B_S;
            }
            if (Side == 1) // RED
            {
                if (Name.Equals("KING")) _pbPiece.Image = Resources.R_K;
                if (Name.Equals("ADVISOR")) _pbPiece.Image = Resources.R_A;
                if (Name.Equals("ELEPHANT")) _pbPiece.Image = Resources.R_E;
                if (Name.Equals("HORSE")) _pbPiece.Image = Resources.R_H;
                if (Name.Equals("CHARIOT")) _pbPiece.Image = Resources.R_R;
                if (Name.Equals("CANNON")) _pbPiece.Image = Resources.R_C;
                if (Name.Equals("SOLDIER")) _pbPiece.Image = Resources.R_S;
            }
            //Draw piece
            _pbPiece.Width = 39;
            _pbPiece.Height = 39;
            _pbPiece.Cursor = Cursors.Hand;
            _pbPiece.Top = Row*48 + 38;
            _pbPiece.Left = Col*48 + 13;
            _pbPiece.BackColor = Color.Transparent;
            // Initialize piece in chess board
            Board.Position[Row, Col].Row = Row;
            Board.Position[Row, Col].Col = Col;
            Board.Position[Row, Col].IsEmpty = false;
            Board.Position[Row, Col].Name = Name;
            Board.Position[Row, Col].Index = Index;
            Board.Position[Row, Col].Side = Side;
        }

        public virtual bool Move(int i, int j)
        {
            return false;
        }

        /// <summary>
        /// Protect the King prevent the King's capture (called "checkmate")
        /// The piece and the King piece are the same side
        /// </summary>
        /// <param name="i">row number the piece move to</param>
        /// <param name="j">columns number the piece move to</param>
        /// <returns></returns>
        public bool ProtectTheKing(int i, int j)
        {
            bool turn = true;
            int row = Row;
            int col = Col;
            var tmp = new Piece();

            if (!Board.Position[i, j].IsEmpty)
            {
                if (Board.Position[i, j].Name.Equals("KING")) tmp = Game.Players[Board.Position[i, j].Side].KingPiece;
                if (Board.Position[i, j].Name.Equals("ADVISOR"))
                    tmp = Game.Players[Board.Position[i, j].Side].AdvisorPieces[Board.Position[i, j].Index];
                if (Board.Position[i, j].Name.Equals("ELEPHANT"))
                    tmp = Game.Players[Board.Position[i, j].Side].ElephantPieces[Board.Position[i, j].Index];
                if (Board.Position[i, j].Name.Equals("HORSE"))
                    tmp = Game.Players[Board.Position[i, j].Side].HorsePieces[Board.Position[i, j].Index];
                if (Board.Position[i, j].Name.Equals("CHARIOT"))
                    tmp = Game.Players[Board.Position[i, j].Side].ChariotPieces[Board.Position[i, j].Index];
                if (Board.Position[i, j].Name.Equals("CANNON"))
                    tmp = Game.Players[Board.Position[i, j].Side].CannonPieces[Board.Position[i, j].Index];
                if (Board.Position[i, j].Name.Equals("SOLDIER"))
                    tmp = Game.Players[Board.Position[i, j].Side].SoldierPieces[Board.Position[i, j].Index];
            }

            Game.ResetChessPoint(Row, Col);
            Board.Position[i, j].IsEmpty = false;
            Board.Position[i, j].Side = Side;
            Board.Position[i, j].Name = Name;
            Board.Position[i, j].Index = Index;
            Row = i;
            Col = j;
            if (tmp.Side != 2)
            {
                tmp.State = 0;
                tmp._pbPiece.Visible = false;
            }

            // Test
            if (Game.TestCaptureKing(Game.Players[Side].KingPiece)) turn = false;

            Row = row;
            Col = col;
            Game.ResetChessPoint(i, j);
            Board.Position[row, col].IsEmpty = false;
            Board.Position[row, col].Side = Side;
            Board.Position[row, col].Name = Name;
            Board.Position[row, col].Index = Index;
            if (tmp.Side != -1)
            {
                tmp.State = 1;
                tmp.PbPiece.Visible = true;
                Board.Position[i, j].IsEmpty = false;
                Board.Position[i, j].Name = tmp.Name;
                Board.Position[i, j].Side = tmp.Side;
                Board.Position[i, j].Index = tmp.Index;
            }
            return turn;
        }

        private void pbPieces_MouseClick(Object sender, MouseEventArgs e)
        {
            //MessageBox.Show("Row : " + Row + "; Col : " + Col + "; Value : " + Board.Position[Row, Col].Value);
            if (Game.OldPosition.PbPiece.Image != null && Game.OldPosition.Side != Side)
            {
                Game.OldPosition.PbPiece.Image = null;
            }
            switch (Game.Marked)
            {
                case true: // If a piece is selected
                    if (State == 1) // the piece is still living
                    {
                        if (Side == Game.MarkedPiece.Side)
                        {
                            Game.Marked = false;
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
                            Board.ResetValidMove();
                        }
                        else //Side != Game.MarkedPiece.Side
                        {
                            if (Game.MarkedPiece.Move(Row, Col) && Game.MarkedPiece.ProtectTheKing(Row, Col))
                            {
                                //  Save to game log
                                Game.SaveGameLog(sender, this);
                                //  Capture the opponent's piece
                                Game.CapturePiece(this);
                                //  Reset chess point
                                Game.ResetChessPoint(Game.MarkedPiece.Row, Game.MarkedPiece.Col);
                                //  Set new piece to chess board
                                Game.SetPiece(Game.MarkedPiece, Row, Col);
                                //  Check capture the king
                                Game.CaptureKing();
                                //  Change turn
                                Game.ChangeTurn();
                                //  GameOver
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
                            }
                            Game.Marked = false;

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
                            Board.ResetValidMove();
                        }
                    }
                    break;
                case false:
                    if (State == 1)
                    {
                        if (!Lock)
                        {
                            Game.Marked = true;
                            Game.MarkedPiece = new Piece();
                            Game.MarkedPiece = this;

                            if (Side == 0) // BLACK
                            {
                                if (Game.MarkedPiece.Name.Equals("KING"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_B_K;
                                if (Game.MarkedPiece.Name.Equals("ADVISOR"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_B_A;
                                if (Game.MarkedPiece.Name.Equals("ELEPHANT"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_B_E;
                                if (Game.MarkedPiece.Name.Equals("HORSE"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_B_H;
                                if (Game.MarkedPiece.Name.Equals("CHARIOT"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_B_R;
                                if (Game.MarkedPiece.Name.Equals("CANNON"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_B_C;
                                if (Game.MarkedPiece.Name.Equals("SOLDIER"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_B_S;
                            }
                            else // RED
                            {
                                if (Game.MarkedPiece.Name.Equals("KING"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_R_K;
                                if (Game.MarkedPiece.Name.Equals("ADVISOR"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_R_A;
                                if (Game.MarkedPiece.Name.Equals("ELEPHANT"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_R_E;
                                if (Game.MarkedPiece.Name.Equals("HORSE"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_R_H;
                                if (Game.MarkedPiece.Name.Equals("CHARIOT"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_R_R;
                                if (Game.MarkedPiece.Name.Equals("CANNON"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_R_C;
                                if (Game.MarkedPiece.Name.Equals("SOLDIER"))
                                    Game.MarkedPiece.PbPiece.Image = Resources.Select_R_S;
                            }
                            for (int i = 0; i < 10; i++)
                            {
                                for (int j = 0; j < 9; j++)
                                {
                                    if (i != Row || j != Col)
                                    {
                                        if (Move(i, j) && ProtectTheKing(i, j))
                                        {
                                            Board.Position[i, j].ValidMove.Visible = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}