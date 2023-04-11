using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ChineseChess.Pieces;
using ChineseChess.Properties;

namespace ChineseChess
{
    internal class Game
    {
        public static Player[] Players = new Player[2]; // 0: BLACK - 1: RED
        public static bool Playing; // game is playing
        public static int Turn = 1;
        public static int Winner = -1;

        //Select a piece
        public static bool Marked;
        public static Piece MarkedPiece;
        public static readonly Piece OldPosition;

        // Movement (using for Undo)
        public static List<Move> GameLog;
        public static int TurnCount; // Total turn in a game

        // Captured pieces
        public static List<CapturedPiece> RCapturedPiece;
        public static List<CapturedPiece> BCapturedPiece;
        public static int RedCount;
        public static int BlackCount;

        // Capture king panel
        public static PictureBox PbCaptureKing = new PictureBox();
        //-------------------------------------------
        public static PictureBox PbGameOver = new PictureBox();
        public static Button btnGameOver = new Button();
        public static Button btnTurnAgain = new Button();

        // Turn picturebox
        public static PictureBox RedTurn = new PictureBox();
        public static PictureBox BlackTurn = new PictureBox();

        static Game()
        {
            Playing = false;
            Marked = false;
            RedCount = 0;
            BlackCount = 0;
            OldPosition = new Piece();
            GameLog = new List<Move>();
            RCapturedPiece = new List<CapturedPiece>();
            BCapturedPiece = new List<CapturedPiece>();
            Players[0] = new Player(0);
            Players[1] = new Player(1);
            //
            // Capture king alert
            //
            PbCaptureKing.BackColor = Color.Transparent;
            PbCaptureKing.Image = Resources.CaptureKingAlert;
            PbCaptureKing.Width = 160;
            PbCaptureKing.Height = 70;
            PbCaptureKing.Top = 60;
            PbCaptureKing.Left = 450;
            PbCaptureKing.Visible = false;
            //
            // GameOver Panel
            //
            PbGameOver.BackColor = Color.Transparent;
            PbGameOver.Image = Resources.Gameover;
            PbGameOver.Size = new Size(160, 70);
            //PbGameOver.Controls.Add();
            //PbGameOver.Controls.Add();
            PbGameOver.Top = 60;
            PbGameOver.Left = 450;
            PbGameOver.Visible = false;
            //
            // RedTurn
            //
            RedTurn.SizeMode = PictureBoxSizeMode.Zoom;
            RedTurn.BackColor = Color.Transparent;
            RedTurn.Width = 30;
            RedTurn.Height = 30;
            RedTurn.Top = 340;
            RedTurn.Left = 600;
            RedTurn.Image = Resources.Turning;
            //
            // BlackTurn
            //
            BlackTurn.SizeMode = PictureBoxSizeMode.Zoom;
            BlackTurn.BackColor = Color.Transparent;
            BlackTurn.Width = 30;
            BlackTurn.Height = 30;
            BlackTurn.Top = 140;
            BlackTurn.Left = 600;
            BlackTurn.Image = Resources.NotTurn;
            // 
            // btnGameOver
            // 
            btnGameOver.BackColor = Color.NavajoWhite;
            btnGameOver.FlatAppearance.BorderColor = Color.DarkKhaki;
            btnGameOver.FlatStyle = FlatStyle.Flat;
            btnGameOver.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((163)));
            btnGameOver.Location = new Point(567, 34);
            btnGameOver.Size = new Size(69, 23);
            btnGameOver.TabIndex = 9;
            btnGameOver.Text = "Chịu thua";
            btnGameOver.UseVisualStyleBackColor = false;
            btnGameOver.Visible = false;
            // 
            // btnTurnAgain
            // 
            btnTurnAgain.BackColor = Color.NavajoWhite;
            btnTurnAgain.FlatAppearance.BorderColor = Color.DarkKhaki;
            btnTurnAgain.FlatStyle = FlatStyle.Flat;
            btnTurnAgain.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((163)));
            btnTurnAgain.Location = new Point(491, 34);
            btnTurnAgain.Size = new Size(69, 23);
            btnTurnAgain.TabIndex = 10;
            btnTurnAgain.Text = "Đi lại";
            btnTurnAgain.UseVisualStyleBackColor = false;
            btnTurnAgain.Visible = false;
        }

        public static void NewGame()
        {
            switch (Playing)
            {
                case true:
                    // Delete all pieces in chessboard
                    Players[0].KingPiece.PbPiece.Visible = false;
                    Players[0].AdvisorPieces[0].PbPiece.Visible = false;
                    Players[0].AdvisorPieces[1].PbPiece.Visible = false;
                    Players[0].ElephantPieces[0].PbPiece.Visible = false;
                    Players[0].ElephantPieces[1].PbPiece.Visible = false;
                    Players[0].ChariotPieces[0].PbPiece.Visible = false;
                    Players[0].ChariotPieces[1].PbPiece.Visible = false;
                    Players[0].CannonPieces[0].PbPiece.Visible = false;
                    Players[0].CannonPieces[1].PbPiece.Visible = false;
                    Players[0].HorsePieces[0].PbPiece.Visible = false;
                    Players[0].HorsePieces[1].PbPiece.Visible = false;
                    Players[0].SoldierPieces[0].PbPiece.Visible = false;
                    Players[0].SoldierPieces[1].PbPiece.Visible = false;
                    Players[0].SoldierPieces[2].PbPiece.Visible = false;
                    Players[0].SoldierPieces[3].PbPiece.Visible = false;
                    Players[0].SoldierPieces[4].PbPiece.Visible = false;
                    Players[1].KingPiece.PbPiece.Visible = false;
                    Players[1].AdvisorPieces[0].PbPiece.Visible = false;
                    Players[1].AdvisorPieces[1].PbPiece.Visible = false;
                    Players[1].ElephantPieces[0].PbPiece.Visible = false;
                    Players[1].ElephantPieces[1].PbPiece.Visible = false;
                    Players[1].ChariotPieces[0].PbPiece.Visible = false;
                    Players[1].ChariotPieces[1].PbPiece.Visible = false;
                    Players[1].CannonPieces[0].PbPiece.Visible = false;
                    Players[1].CannonPieces[1].PbPiece.Visible = false;
                    Players[1].HorsePieces[0].PbPiece.Visible = false;
                    Players[1].HorsePieces[1].PbPiece.Visible = false;
                    Players[1].SoldierPieces[0].PbPiece.Visible = false;
                    Players[1].SoldierPieces[1].PbPiece.Visible = false;
                    Players[1].SoldierPieces[2].PbPiece.Visible = false;
                    Players[1].SoldierPieces[3].PbPiece.Visible = false;
                    Players[1].SoldierPieces[4].PbPiece.Visible = false;

                    Array.Resize(ref Players, 0);

                    // Create new 2 players
                    Array.Resize(ref Players, 2);
                    Players[0] = new Player(0);
                    Players[1] = new Player(1);

                    // Create empty chess board
                    Board.ResetBoard();
                    Turn = 1;
                    Winner = -1;
                    TurnCount = 0;
                    BlackCount = 0;
                    RedCount = 0;
                    PbCaptureKing.Visible = false;
                    PbGameOver.Visible = false;
                    RedTurn.Image = Resources.Turning;
                    BlackTurn.Image = Resources.NotTurn;

                    // Draw pieces
                    Players[0].KingPiece.Draw();
                    Players[0].AdvisorPieces[0].Draw();
                    Players[0].AdvisorPieces[1].Draw();
                    Players[0].ElephantPieces[0].Draw();
                    Players[0].ElephantPieces[1].Draw();
                    Players[0].ChariotPieces[0].Draw();
                    Players[0].ChariotPieces[1].Draw();
                    Players[0].CannonPieces[0].Draw();
                    Players[0].CannonPieces[1].Draw();
                    Players[0].HorsePieces[0].Draw();
                    Players[0].HorsePieces[1].Draw();
                    Players[0].SoldierPieces[0].Draw();
                    Players[0].SoldierPieces[1].Draw();
                    Players[0].SoldierPieces[2].Draw();
                    Players[0].SoldierPieces[3].Draw();
                    Players[0].SoldierPieces[4].Draw();
                    Players[1].KingPiece.Draw();
                    Players[1].AdvisorPieces[0].Draw();
                    Players[1].AdvisorPieces[1].Draw();
                    Players[1].ElephantPieces[0].Draw();
                    Players[1].ElephantPieces[1].Draw();
                    Players[1].ChariotPieces[0].Draw();
                    Players[1].ChariotPieces[1].Draw();
                    Players[1].CannonPieces[0].Draw();
                    Players[1].CannonPieces[1].Draw();
                    Players[1].HorsePieces[0].Draw();
                    Players[1].HorsePieces[1].Draw();
                    Players[1].SoldierPieces[0].Draw();
                    Players[1].SoldierPieces[1].Draw();
                    Players[1].SoldierPieces[2].Draw();
                    Players[1].SoldierPieces[3].Draw();
                    Players[1].SoldierPieces[4].Draw();
                    break;
                case false: // new game
                    // Create empty chess board
                    Board.ResetBoard();
                    Playing = true;

                    // Draw pieces
                    Players[0].KingPiece.Draw();
                    Players[0].AdvisorPieces[0].Draw();
                    Players[0].AdvisorPieces[1].Draw();
                    Players[0].ElephantPieces[0].Draw();
                    Players[0].ElephantPieces[1].Draw();
                    Players[0].ChariotPieces[0].Draw();
                    Players[0].ChariotPieces[1].Draw();
                    Players[0].CannonPieces[0].Draw();
                    Players[0].CannonPieces[1].Draw();
                    Players[0].HorsePieces[0].Draw();
                    Players[0].HorsePieces[1].Draw();
                    Players[0].SoldierPieces[0].Draw();
                    Players[0].SoldierPieces[1].Draw();
                    Players[0].SoldierPieces[2].Draw();
                    Players[0].SoldierPieces[3].Draw();
                    Players[0].SoldierPieces[4].Draw();
                    Players[1].KingPiece.Draw();
                    Players[1].AdvisorPieces[0].Draw();
                    Players[1].AdvisorPieces[1].Draw();
                    Players[1].ElephantPieces[0].Draw();
                    Players[1].ElephantPieces[1].Draw();
                    Players[1].ChariotPieces[0].Draw();
                    Players[1].ChariotPieces[1].Draw();
                    Players[1].CannonPieces[0].Draw();
                    Players[1].CannonPieces[1].Draw();
                    Players[1].HorsePieces[0].Draw();
                    Players[1].HorsePieces[1].Draw();
                    Players[1].SoldierPieces[0].Draw();
                    Players[1].SoldierPieces[1].Draw();
                    Players[1].SoldierPieces[2].Draw();
                    Players[1].SoldierPieces[3].Draw();
                    Players[1].SoldierPieces[4].Draw();
                    RedTurn.Image = Resources.Turning;
                    BlackTurn.Image = Resources.NotTurn;
                    break;
            }
        }

        private static void LockPieces(bool @lock)
        {
            Players[0].KingPiece.Lock = @lock;
            Players[0].AdvisorPieces[0].Lock = @lock;
            Players[0].AdvisorPieces[1].Lock = @lock;
            Players[0].ElephantPieces[0].Lock = @lock;
            Players[0].ElephantPieces[1].Lock = @lock;
            Players[0].ChariotPieces[0].Lock = @lock;
            Players[0].ChariotPieces[1].Lock = @lock;
            Players[0].CannonPieces[0].Lock = @lock;
            Players[0].CannonPieces[1].Lock = @lock;
            Players[0].HorsePieces[0].Lock = @lock;
            Players[0].HorsePieces[1].Lock = @lock;
            Players[0].SoldierPieces[0].Lock = @lock;
            Players[0].SoldierPieces[1].Lock = @lock;
            Players[0].SoldierPieces[2].Lock = @lock;
            Players[0].SoldierPieces[3].Lock = @lock;
            Players[0].SoldierPieces[4].Lock = @lock;

            Players[1].KingPiece.Lock = !@lock;
            Players[1].AdvisorPieces[0].Lock = !@lock;
            Players[1].AdvisorPieces[1].Lock = !@lock;
            Players[1].ElephantPieces[0].Lock = !@lock;
            Players[1].ElephantPieces[1].Lock = !@lock;
            Players[1].ChariotPieces[0].Lock = !@lock;
            Players[1].ChariotPieces[1].Lock = !@lock;
            Players[1].CannonPieces[0].Lock = !@lock;
            Players[1].CannonPieces[1].Lock = !@lock;
            Players[1].HorsePieces[0].Lock = !@lock;
            Players[1].HorsePieces[1].Lock = !@lock;
            Players[1].SoldierPieces[0].Lock = !@lock;
            Players[1].SoldierPieces[1].Lock = !@lock;
            Players[1].SoldierPieces[2].Lock = !@lock;
            Players[1].SoldierPieces[3].Lock = !@lock;
            Players[1].SoldierPieces[4].Lock = !@lock;
        }

        public static void ChangeTurn()
        {
            Turn = Turn == 0 ? 1 : 0;
            if (Turn == 0) // BLACK
            {
                LockPieces(false);
                BlackTurn.Image = Resources.Turning;
                RedTurn.Image = Resources.NotTurn;
            }
            else
            {
                LockPieces(true);
                BlackTurn.Image = Resources.NotTurn;
                RedTurn.Image = Resources.Turning;
            }
        }

        public static void Undo()
        {
            if (OldPosition.PbPiece.Image != null)
            {
                OldPosition.PbPiece.Image = null;
            }
            var tmp1 = new Piece();
            var tmp2 = new Piece();

            if (Marked) return;

            if (TurnCount > 0)
            {
                if (GameLog[TurnCount - 1].PieceFrom.Name.Equals("KING"))
                    tmp1 = Players[GameLog[TurnCount - 1].PieceFrom.Side].KingPiece;
                if (GameLog[TurnCount - 1].PieceFrom.Name.Equals("ADVISOR"))
                    tmp1 =
                        Players[GameLog[TurnCount - 1].PieceFrom.Side].AdvisorPieces[
                            GameLog[TurnCount - 1].PieceFrom.Index];
                if (GameLog[TurnCount - 1].PieceFrom.Name.Equals("ELEPHANT"))
                    tmp1 =
                        Players[GameLog[TurnCount - 1].PieceFrom.Side].ElephantPieces[
                            GameLog[TurnCount - 1].PieceFrom.Index];
                if (GameLog[TurnCount - 1].PieceFrom.Name.Equals("HORSE"))
                    tmp1 =
                        Players[GameLog[TurnCount - 1].PieceFrom.Side].HorsePieces[
                            GameLog[TurnCount - 1].PieceFrom.Index];
                if (GameLog[TurnCount - 1].PieceFrom.Name.Equals("CHARIOT"))
                    tmp1 =
                        Players[GameLog[TurnCount - 1].PieceFrom.Side].ChariotPieces[
                            GameLog[TurnCount - 1].PieceFrom.Index];
                if (GameLog[TurnCount - 1].PieceFrom.Name.Equals("CANNON"))
                    tmp1 =
                        Players[GameLog[TurnCount - 1].PieceFrom.Side].CannonPieces[
                            GameLog[TurnCount - 1].PieceFrom.Index];
                if (GameLog[TurnCount - 1].PieceFrom.Name.Equals("SOLDIER"))
                    tmp1 =
                        Players[GameLog[TurnCount - 1].PieceFrom.Side].SoldierPieces[
                            GameLog[TurnCount - 1].PieceFrom.Index];

                int capturedmove;
                capturedmove = GameLog[TurnCount - 1].PieceDest == null ? 0 : 1;

                switch (capturedmove)
                {
                    case 0: // last move doesn't eat any pieces
                        Board.Position[GameLog[TurnCount - 1].PieceFrom.Row, GameLog[TurnCount - 1].PieceFrom.Col].
                            IsEmpty = true;
                        Board.Position[
                            GameLog[TurnCount - 1].PieceFrom.Row,
                            GameLog[TurnCount - 1].PieceFrom.Col].Side = -1;
                        Board.Position[
                            GameLog[TurnCount - 1].PieceFrom.Row,
                            GameLog[TurnCount - 1].PieceFrom.Col].Name = "";
                        Board.Position[
                            GameLog[TurnCount - 1].PieceFrom.Row,
                            GameLog[TurnCount - 1].PieceFrom.Col].Index = -1;

                        // Reset to old position
                        tmp1.Row = GameLog[TurnCount - 1].RowFrom;
                        tmp1.Col = GameLog[TurnCount - 1].ColFrom;
                        tmp1.PbPiece.Width = tmp1.PbPiece.Height = 39;
                        tmp1.PbPiece.Top = tmp1.Row*48 + 38;
                        tmp1.PbPiece.Left = tmp1.Col*48 + 13;
                        Board.Position[tmp1.Row, tmp1.Col].IsEmpty = false;
                        Board.Position[tmp1.Row, tmp1.Col].Side = tmp1.Side;
                        Board.Position[tmp1.Row, tmp1.Col].Index = tmp1.Index;
                        Board.Position[tmp1.Row, tmp1.Col].Name = tmp1.Name;

                        // delete the last move from Log
                        if (TurnCount >= 1) TurnCount--;
                        GameLog.RemoveAt(TurnCount);
                        CaptureKing();
                        if (PbGameOver.Visible)
                        {
                            PbGameOver.Visible = false;
                        }
                        // rollback turn
                        ChangeTurn();
                        break;
                    case 1: // last move eat a other's piece
                        if (GameLog[TurnCount - 1].PieceDest.Name.Equals("KING"))
                            tmp2 = Players[GameLog[TurnCount - 1].PieceDest.Side].KingPiece;
                        if (GameLog[TurnCount - 1].PieceDest.Name.Equals("ADVISOR"))
                            tmp2 =
                                Players[GameLog[TurnCount - 1].PieceDest.Side].AdvisorPieces[
                                    GameLog[TurnCount - 1].PieceDest.Index];
                        if (GameLog[TurnCount - 1].PieceDest.Name.Equals("ELEPHANT"))
                            tmp2 =
                                Players[GameLog[TurnCount - 1].PieceDest.Side].ElephantPieces[
                                    GameLog[TurnCount - 1].PieceDest.Index];
                        if (GameLog[TurnCount - 1].PieceDest.Name.Equals("HORSE"))
                            tmp2 =
                                Players[GameLog[TurnCount - 1].PieceDest.Side].HorsePieces[
                                    GameLog[TurnCount - 1].PieceDest.Index];
                        if (GameLog[TurnCount - 1].PieceDest.Name.Equals("CHARIOT"))
                            tmp2 =
                                Players[GameLog[TurnCount - 1].PieceDest.Side].ChariotPieces[
                                    GameLog[TurnCount - 1].PieceDest.Index];
                        if (GameLog[TurnCount - 1].PieceDest.Name.Equals("CANNON"))
                            tmp2 =
                                Players[GameLog[TurnCount - 1].PieceDest.Side].CannonPieces[
                                    GameLog[TurnCount - 1].PieceDest.Index];
                        if (GameLog[TurnCount - 1].PieceDest.Name.Equals("SOLDIER"))
                            tmp2 =
                                Players[GameLog[TurnCount - 1].PieceDest.Side].SoldierPieces[
                                    GameLog[TurnCount - 1].PieceDest.Index];

                        //reset to old position
                        Board.Position[GameLog[TurnCount - 1].PieceFrom.Row, GameLog[TurnCount - 1].PieceFrom.Col].
                            IsEmpty = false;
                        Board.Position[GameLog[TurnCount - 1].PieceFrom.Row, GameLog[TurnCount - 1].PieceFrom.Col].
                            Side = GameLog[TurnCount - 1].PieceDest.Side;
                        Board.Position[GameLog[TurnCount - 1].PieceFrom.Row, GameLog[TurnCount - 1].PieceFrom.Col].
                            Name = GameLog[TurnCount - 1].PieceDest.Name;
                        Board.Position[GameLog[TurnCount - 1].PieceFrom.Row, GameLog[TurnCount - 1].PieceFrom.Col].
                            Index = GameLog[TurnCount - 1].PieceDest.Index;

                        tmp2.State = 1;
                        tmp2.PbPiece.Width = tmp2.PbPiece.Height = 39;
                        tmp2.PbPiece.Top = tmp2.Row*48 + 38;
                        tmp2.PbPiece.Left = tmp2.Col*48 + 13;
                        tmp2.PbPiece.Cursor = Cursors.Hand;

                        if (tmp2.Side == 0) // BLACK
                        {
                            if (tmp2.Name.Equals("KING")) tmp2.PbPiece.Image = Resources.B_K;
                            if (tmp2.Name.Equals("ADVISOR")) tmp2.PbPiece.Image = Resources.B_A;
                            if (tmp2.Name.Equals("ELEPHANT")) tmp2.PbPiece.Image = Resources.B_E;
                            if (tmp2.Name.Equals("HORSE")) tmp2.PbPiece.Image = Resources.B_H;
                            if (tmp2.Name.Equals("CHARIOT")) tmp2.PbPiece.Image = Resources.B_R;
                            if (tmp2.Name.Equals("CANNON")) tmp2.PbPiece.Image = Resources.B_C;
                            if (tmp2.Name.Equals("SOLDIER")) tmp2.PbPiece.Image = Resources.B_S;
                            BlackCount--;
                        }
                        else // RED
                        {
                            if (tmp2.Name.Equals("KING")) tmp2.PbPiece.Image = Resources.R_K;
                            if (tmp2.Name.Equals("ADVISOR")) tmp2.PbPiece.Image = Resources.R_A;
                            if (tmp2.Name.Equals("ELEPHANT")) tmp2.PbPiece.Image = Resources.R_E;
                            if (tmp2.Name.Equals("HORSE")) tmp2.PbPiece.Image = Resources.R_H;
                            if (tmp2.Name.Equals("CHARIOT")) tmp2.PbPiece.Image = Resources.R_R;
                            if (tmp2.Name.Equals("CANNON")) tmp2.PbPiece.Image = Resources.R_C;
                            if (tmp2.Name.Equals("SOLDIER")) tmp2.PbPiece.Image = Resources.R_S;
                            RedCount--;
                        }

                        tmp1.Row = GameLog[TurnCount - 1].RowFrom;
                        tmp1.Col = GameLog[TurnCount - 1].ColFrom;
                        tmp1.PbPiece.Width = tmp1.PbPiece.Height = 39;
                        tmp1.PbPiece.Top = tmp1.Row*48 + 38;
                        tmp1.PbPiece.Left = tmp1.Col*48 + 13;
                        Board.Position[tmp1.Row, tmp1.Col].IsEmpty = false;
                        Board.Position[tmp1.Row, tmp1.Col].Side = tmp1.Side;
                        Board.Position[tmp1.Row, tmp1.Col].Index = tmp1.Index;
                        Board.Position[tmp1.Row, tmp1.Col].Name = tmp1.Name;

                        if (TurnCount >= 1) TurnCount--;
                        GameLog.RemoveAt(TurnCount);
                        if (Winner != -1) Winner = -1;
                        CaptureKing();
                        ChangeTurn();
                        break;
                }
            }
        }

        public static void SaveGameLog(Object sender, Piece p)
        {
            if (sender.GetType() == typeof (ChessForm))
            {
                TurnCount++;
                GameLog.Add(new Move());
                GameLog[TurnCount - 1].PieceFrom = MarkedPiece;
                GameLog[TurnCount - 1].RowFrom = p.Row;
                GameLog[TurnCount - 1].ColFrom = p.Col;
            }

            if (sender.GetType() == typeof (PictureBox))
            {
                TurnCount++;
                GameLog.Add(new Move());
                GameLog[TurnCount - 1].PieceFrom = MarkedPiece;
                GameLog[TurnCount - 1].RowFrom = MarkedPiece.Row;
                GameLog[TurnCount - 1].ColFrom = MarkedPiece.Col;
                GameLog[TurnCount - 1].PieceDest = p;
                GameLog[TurnCount - 1].RowDest = p.Row;
                GameLog[TurnCount - 1].ColDest = p.Col;
            }
        }

        public static void ResetChessPoint(int row, int col)
        {
            Board.Position[row, col].IsEmpty = true;
            Board.Position[row, col].Name = "";
            Board.Position[row, col].Index = -1;
            Board.Position[row, col].Side = -1;
        }

        /// <summary>
        /// Set a piece to new position
        /// </summary>
        /// <param name="p"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void SetPiece(Piece p, int i, int j)
        {
            Board.Position[i, j].IsEmpty = false;
            Board.Position[i, j].Side = MarkedPiece.Side;
            Board.Position[i, j].Name = MarkedPiece.Name;
            Board.Position[i, j].Index = MarkedPiece.Index;
            MarkedPiece.Row = i;
            MarkedPiece.Col = j;
            MarkedPiece.PbPiece.Width = MarkedPiece.PbPiece.Height = 39;
            MarkedPiece.PbPiece.Top = i*48 + 38;
            MarkedPiece.PbPiece.Left = j*48 + 13;
        }

        /// <summary>
        /// If the piece can move to the king's position => the king is captured
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        public static bool TestCaptureKing(King king)
        {
            bool capture;
            if (king.Side == 0) // BLACK
            {
                int chariot0 = 0, chariot1 = 0;
                int cannon0 = 0, cannon1 = 0;
                int horse0 = 0, horse1 = 0;
                int soldier0 = 0, soldier1 = 0, soldier2 = 0, soldier3 = 0, soldier4 = 0;

                if (Players[1].ChariotPieces[0].State == 1)
                    if (Players[1].ChariotPieces[0].Move(king.Row, king.Col)) chariot0 = 1;
                if (Players[1].ChariotPieces[1].State == 1)
                    if (Players[1].ChariotPieces[1].Move(king.Row, king.Col)) chariot1 = 1;
                if (Players[1].CannonPieces[0].State == 1)
                    if (Players[1].CannonPieces[0].Move(king.Row, king.Col)) cannon0 = 1;
                if (Players[1].CannonPieces[1].State == 1)
                    if (Players[1].CannonPieces[1].Move(king.Row, king.Col)) cannon1 = 1;
                if (Players[1].HorsePieces[0].State == 1)
                    if (Players[1].HorsePieces[0].Move(king.Row, king.Col)) horse0 = 1;
                if (Players[1].HorsePieces[1].State == 1)
                    if (Players[1].HorsePieces[1].Move(king.Row, king.Col)) horse1 = 1;
                if (Players[1].SoldierPieces[0].State == 1)
                    if (Players[1].SoldierPieces[0].Move(king.Row, king.Col)) soldier0 = 1;
                if (Players[1].SoldierPieces[1].State == 1)
                    if (Players[1].SoldierPieces[1].Move(king.Row, king.Col)) soldier1 = 1;
                if (Players[1].SoldierPieces[2].State == 1)
                    if (Players[1].SoldierPieces[2].Move(king.Row, king.Col)) soldier2 = 1;
                if (Players[1].SoldierPieces[3].State == 1)
                    if (Players[1].SoldierPieces[3].Move(king.Row, king.Col)) soldier3 = 1;
                if (Players[1].SoldierPieces[4].State == 1)
                    if (Players[1].SoldierPieces[4].Move(king.Row, king.Col)) soldier4 = 1;

                if (chariot0 != 1 && chariot1 != 1 &&
                    cannon0 != 1 && cannon1 != 1 &&
                    horse0 != 1 && horse1 != 1 &&
                    soldier0 != 1 && soldier1 != 1 && soldier2 != 1 && soldier3 != 1 && soldier4 != 1)
                {
                    capture = false;
                }
                else
                {
                    capture = true;
                }
            }
            else // RED
            {
                int chariot0 = 0, chariot1 = 0;
                int cannon0 = 0, cannon1 = 0;
                int horse0 = 0, horse1 = 0;
                int soldier0 = 0, soldier1 = 0, soldier2 = 0, soldier3 = 0, soldier4 = 0;

                if (Players[0].ChariotPieces[0].State == 1)
                    if (Players[0].ChariotPieces[0].Move(king.Row, king.Col)) chariot0 = 1;
                if (Players[0].ChariotPieces[1].State == 1)
                    if (Players[0].ChariotPieces[1].Move(king.Row, king.Col)) chariot1 = 1;
                if (Players[0].CannonPieces[0].State == 1)
                    if (Players[0].CannonPieces[0].Move(king.Row, king.Col)) cannon0 = 1;
                if (Players[0].CannonPieces[1].State == 1)
                    if (Players[0].CannonPieces[1].Move(king.Row, king.Col)) cannon1 = 1;
                if (Players[0].HorsePieces[0].State == 1)
                    if (Players[0].HorsePieces[0].Move(king.Row, king.Col)) horse0 = 1;
                if (Players[0].HorsePieces[1].State == 1)
                    if (Players[0].HorsePieces[1].Move(king.Row, king.Col)) horse1 = 1;
                if (Players[0].SoldierPieces[0].State == 1)
                    if (Players[0].SoldierPieces[0].Move(king.Row, king.Col)) soldier0 = 1;
                if (Players[0].SoldierPieces[1].State == 1)
                    if (Players[0].SoldierPieces[1].Move(king.Row, king.Col)) soldier1 = 1;
                if (Players[0].SoldierPieces[2].State == 1)
                    if (Players[0].SoldierPieces[2].Move(king.Row, king.Col)) soldier2 = 1;
                if (Players[0].SoldierPieces[3].State == 1)
                    if (Players[0].SoldierPieces[3].Move(king.Row, king.Col)) soldier3 = 1;
                if (Players[0].SoldierPieces[4].State == 1)
                    if (Players[0].SoldierPieces[4].Move(king.Row, king.Col)) soldier4 = 1;

                if (chariot0 != 1 && chariot1 != 1 &&
                    cannon0 != 1 && cannon1 != 1 &&
                    horse0 != 1 && horse1 != 1 &&
                    soldier0 != 1 && soldier1 != 1 && soldier2 != 1 && soldier3 != 1 && soldier4 != 1)
                {
                    capture = false;
                }
                else
                {
                    capture = true;
                }
            }
            return capture;
        }

        public static void CaptureKing()
        {
            int t = 0;
            if (TestCaptureKing(Players[0].KingPiece) && !TestCaptureKing(Players[1].KingPiece)) t = 0;
            if (!TestCaptureKing(Players[0].KingPiece) && TestCaptureKing(Players[1].KingPiece)) t = 1;
            if (!TestCaptureKing(Players[0].KingPiece) && !TestCaptureKing(Players[1].KingPiece)) t = -1;
            switch (t)
            {
                case -1:
                    Players[0].KingPiece.PbPiece.Image = Resources.B_K;
                    Players[1].KingPiece.PbPiece.Image = Resources.R_K;
                    PbCaptureKing.Visible = false;
                    break;
                case 0: // BLACK
                    Players[0].KingPiece.PbPiece.Image = Resources.Checkmate_B_K;
                    Players[1].KingPiece.PbPiece.Image = Resources.R_K;
                    PbCaptureKing.Visible = true;
                    break;
                case 1: // RED
                    Players[0].KingPiece.PbPiece.Image = Resources.B_K;
                    Players[1].KingPiece.PbPiece.Image = Resources.Checkmate_R_K;
                    PbCaptureKing.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// GameOver: The king can't make any move to prevent the king's capture
        /// </summary>
        public static void GameOver()
        {
            bool prevent = false;
            int king = 0;
            int advisor0 = 0, advisor1 = 0;
            int elephant0 = 0, elephant1 = 0;
            int chariot0 = 0, chariot1 = 0;
            int cannon0 = 0, cannon1 = 0;
            int horse0 = 0, horse1 = 0;
            int soldier0 = 0, soldier1 = 0, soldier2 = 0, soldier3 = 0, soldier4 = 0;

            if (Turn == 0) // BLACK
            {
                if (TestCaptureKing(Players[0].KingPiece))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (Players[0].KingPiece.State == 1)
                            {
                                if (Players[0].KingPiece.Move(i, j) && Players[0].KingPiece.ProtectTheKing(i, j))
                                    king = 1;
                            }
                            if (Players[0].AdvisorPieces[0].State == 1)
                            {
                                if (Players[0].AdvisorPieces[0].Move(i, j) &&
                                    Players[0].AdvisorPieces[0].ProtectTheKing(i, j)) advisor0 = 1;
                            }
                            if (Players[0].AdvisorPieces[1].State == 1)
                            {
                                if (Players[0].AdvisorPieces[1].Move(i, j) &&
                                    Players[0].AdvisorPieces[1].ProtectTheKing(i, j)) advisor1 = 1;
                            }
                            if (Players[0].ElephantPieces[0].State == 1)
                            {
                                if (Players[0].ElephantPieces[0].Move(i, j) &&
                                    Players[0].ElephantPieces[0].ProtectTheKing(i, j)) elephant0 = 1;
                            }
                            if (Players[0].ElephantPieces[1].State == 1)
                            {
                                if (Players[0].ElephantPieces[1].Move(i, j) &&
                                    Players[0].ElephantPieces[1].ProtectTheKing(i, j)) elephant1 = 1;
                            }
                            if (Players[0].ChariotPieces[0].State == 1)
                            {
                                if (Players[0].ChariotPieces[0].Move(i, j) &&
                                    Players[0].ChariotPieces[0].ProtectTheKing(i, j)) chariot0 = 1;
                            }
                            if (Players[0].ChariotPieces[1].State == 1)
                            {
                                if (Players[0].ChariotPieces[1].Move(i, j) &&
                                    Players[0].ChariotPieces[1].ProtectTheKing(i, j)) chariot1 = 1;
                            }
                            if (Players[0].HorsePieces[0].State == 1)
                            {
                                if (Players[0].HorsePieces[0].Move(i, j) &&
                                    Players[0].HorsePieces[0].ProtectTheKing(i, j)) horse0 = 1;
                            }
                            if (Players[0].HorsePieces[1].State == 1)
                            {
                                if (Players[0].HorsePieces[1].Move(i, j) &&
                                    Players[0].HorsePieces[1].ProtectTheKing(i, j)) horse1 = 1;
                            }
                            if (Players[0].CannonPieces[0].State == 1)
                            {
                                if (Players[0].CannonPieces[0].Move(i, j) &&
                                    Players[0].CannonPieces[0].ProtectTheKing(i, j)) cannon0 = 1;
                            }
                            if (Players[0].CannonPieces[1].State == 1)
                            {
                                if (Players[0].CannonPieces[1].Move(i, j) &&
                                    Players[0].CannonPieces[1].ProtectTheKing(i, j)) cannon1 = 1;
                            }
                            if (Players[0].SoldierPieces[0].State == 1)
                            {
                                if (Players[0].SoldierPieces[0].Move(i, j) &&
                                    Players[0].SoldierPieces[0].ProtectTheKing(i, j)) soldier0 = 1;
                            }
                            if (Players[0].SoldierPieces[1].State == 1)
                            {
                                if (Players[0].SoldierPieces[1].Move(i, j) &&
                                    Players[0].SoldierPieces[1].ProtectTheKing(i, j)) soldier1 = 1;
                            }
                            if (Players[0].SoldierPieces[2].State == 1)
                            {
                                if (Players[0].SoldierPieces[2].Move(i, j) &&
                                    Players[0].SoldierPieces[2].ProtectTheKing(i, j)) soldier2 = 1;
                            }
                            if (Players[0].SoldierPieces[3].State == 1)
                            {
                                if (Players[0].SoldierPieces[3].Move(i, j) &&
                                    Players[0].SoldierPieces[3].ProtectTheKing(i, j)) soldier3 = 1;
                            }
                            if (Players[0].SoldierPieces[4].State == 1)
                            {
                                if (Players[0].SoldierPieces[4].Move(i, j) &&
                                    Players[0].SoldierPieces[4].ProtectTheKing(i, j)) soldier4 = 1;
                            }

                            if ((king == 1) ||
                                (advisor0 == 1) || (advisor1 == 1) ||
                                (elephant0 == 1) || (elephant1 == 1) ||
                                (chariot0 == 1) || (chariot1 == 1) ||
                                (cannon0 == 1) || (cannon1 == 1) ||
                                (horse0 == 1) || (horse1 == 1) ||
                                (soldier0 == 1) || (soldier1 == 1) || (soldier2 == 1) || (soldier3 == 1) ||
                                (soldier4 == 1)
                                )
                            {
                                prevent = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    prevent = true;
                }
                if (!prevent) Winner = 1; // Winner is RED
            }
            else // RED
            {
                if (TestCaptureKing(Players[1].KingPiece))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (Players[1].KingPiece.State == 1)
                            {
                                if (Players[1].KingPiece.Move(i, j) && Players[1].KingPiece.ProtectTheKing(i, j))
                                    king = 1;
                            }
                            if (Players[1].AdvisorPieces[0].State == 1)
                            {
                                if (Players[1].AdvisorPieces[0].Move(i, j) &&
                                    Players[1].AdvisorPieces[0].ProtectTheKing(i, j)) advisor0 = 1;
                            }
                            if (Players[1].AdvisorPieces[1].State == 1)
                            {
                                if (Players[1].AdvisorPieces[1].Move(i, j) &&
                                    Players[1].AdvisorPieces[1].ProtectTheKing(i, j)) advisor1 = 1;
                            }
                            if (Players[1].ElephantPieces[0].State == 1)
                            {
                                if (Players[1].ElephantPieces[0].Move(i, j) &&
                                    Players[1].ElephantPieces[0].ProtectTheKing(i, j)) elephant0 = 1;
                            }
                            if (Players[1].ElephantPieces[1].State == 1)
                            {
                                if (Players[1].ElephantPieces[1].Move(i, j) &&
                                    Players[1].ElephantPieces[1].ProtectTheKing(i, j)) elephant1 = 1;
                            }
                            if (Players[1].ChariotPieces[0].State == 1)
                            {
                                if (Players[1].ChariotPieces[0].Move(i, j) &&
                                    Players[1].ChariotPieces[0].ProtectTheKing(i, j)) chariot0 = 1;
                            }
                            if (Players[1].ChariotPieces[1].State == 1)
                            {
                                if (Players[1].ChariotPieces[1].Move(i, j) &&
                                    Players[1].ChariotPieces[1].ProtectTheKing(i, j)) chariot1 = 1;
                            }
                            if (Players[1].HorsePieces[0].State == 1)
                            {
                                if (Players[1].HorsePieces[0].Move(i, j) &&
                                    Players[1].HorsePieces[0].ProtectTheKing(i, j)) horse0 = 1;
                            }
                            if (Players[1].HorsePieces[1].State == 1)
                            {
                                if (Players[1].HorsePieces[1].Move(i, j) &&
                                    Players[1].HorsePieces[1].ProtectTheKing(i, j)) horse1 = 1;
                            }
                            if (Players[1].CannonPieces[0].State == 1)
                            {
                                if (Players[1].CannonPieces[0].Move(i, j) &&
                                    Players[1].CannonPieces[0].ProtectTheKing(i, j)) cannon0 = 1;
                            }
                            if (Players[1].CannonPieces[1].State == 1)
                            {
                                if (Players[1].CannonPieces[1].Move(i, j) &&
                                    Players[1].CannonPieces[1].ProtectTheKing(i, j)) cannon1 = 1;
                            }
                            if (Players[1].SoldierPieces[0].State == 1)
                            {
                                if (Players[1].SoldierPieces[0].Move(i, j) &&
                                    Players[1].SoldierPieces[0].ProtectTheKing(i, j)) soldier0 = 1;
                            }
                            if (Players[1].SoldierPieces[1].State == 1)
                            {
                                if (Players[1].SoldierPieces[1].Move(i, j) &&
                                    Players[1].SoldierPieces[1].ProtectTheKing(i, j)) soldier1 = 1;
                            }
                            if (Players[1].SoldierPieces[2].State == 1)
                            {
                                if (Players[1].SoldierPieces[2].Move(i, j) &&
                                    Players[1].SoldierPieces[2].ProtectTheKing(i, j)) soldier2 = 1;
                            }
                            if (Players[1].SoldierPieces[3].State == 1)
                            {
                                if (Players[1].SoldierPieces[3].Move(i, j) &&
                                    Players[1].SoldierPieces[3].ProtectTheKing(i, j)) soldier3 = 1;
                            }
                            if (Players[1].SoldierPieces[4].State == 1)
                            {
                                if (Players[1].SoldierPieces[4].Move(i, j) &&
                                    Players[1].SoldierPieces[4].ProtectTheKing(i, j)) soldier4 = 1;
                            }

                            if ((king == 1) ||
                                (advisor0 == 1) || (advisor1 == 1) ||
                                (elephant0 == 1) || (elephant1 == 1) ||
                                (chariot0 == 1) || (chariot1 == 1) ||
                                (cannon0 == 1) || (cannon1 == 1) ||
                                (horse0 == 1) || (horse1 == 1) ||
                                (soldier0 == 1) || (soldier1 == 1) || (soldier2 == 1) || (soldier3 == 1) ||
                                (soldier4 == 1)
                                )
                            {
                                prevent = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    prevent = true;
                }
                if (!prevent) Winner = 0; // Winner is BLACK
            }
        }

        public static void CapturePiece(Piece p)
        {
            int row = 0;
            int col = 0;
            p.State = 0;

            if (p.Side == 0) // BLACK
            {
                if (BlackCount >= 0 && BlackCount < 5)
                {
                    row = 0;
                    col = BlackCount;
                }
                if (BlackCount >= 5 && BlackCount < 11)
                {
                    row = 1;
                    col = BlackCount - 5;
                }
                if (BlackCount >= 10 && BlackCount < 15)
                {
                    row = 2;
                    col = BlackCount - 10;
                }
                BlackCount++;
                BCapturedPiece.Add(new CapturedPiece());
                BCapturedPiece[BlackCount - 1].Row = row;
                BCapturedPiece[BlackCount - 1].Col = col;
                BCapturedPiece[BlackCount - 1].PbPiece = p.PbPiece;
                if (p.Name.Equals("KING")) BCapturedPiece[BlackCount - 1].PbPiece.Image = Resources.Captured_B_K;
                if (p.Name.Equals("ADVISOR")) BCapturedPiece[BlackCount - 1].PbPiece.Image = Resources.Captured_B_A;
                if (p.Name.Equals("ELEPHANT")) BCapturedPiece[BlackCount - 1].PbPiece.Image = Resources.Captured_B_E;
                if (p.Name.Equals("HORSE")) BCapturedPiece[BlackCount - 1].PbPiece.Image = Resources.Captured_B_H;
                if (p.Name.Equals("CHARIOT")) BCapturedPiece[BlackCount - 1].PbPiece.Image = Resources.Captured_B_R;
                if (p.Name.Equals("CANNON")) BCapturedPiece[BlackCount - 1].PbPiece.Image = Resources.Captured_B_C;
                if (p.Name.Equals("SOLDIER")) BCapturedPiece[BlackCount - 1].PbPiece.Image = Resources.Captured_B_S;
                BCapturedPiece[BlackCount - 1].PbPiece.Width = 25;
                BCapturedPiece[BlackCount - 1].PbPiece.Height = 25;
                BCapturedPiece[BlackCount - 1].PbPiece.BackColor = Color.Transparent;
                BCapturedPiece[BlackCount - 1].PbPiece.Cursor = Cursors.Arrow;
                BCapturedPiece[BlackCount - 1].PbPiece.Top = BCapturedPiece[BlackCount - 1].Row*30 + 385;
                BCapturedPiece[BlackCount - 1].PbPiece.Left = BCapturedPiece[BlackCount - 1].Col*30 + 465;
            }
            else // RED
            {
                if (RedCount >= 0 && RedCount < 5)
                {
                    row = 0;
                    col = RedCount;
                }
                if (RedCount >= 5 && RedCount < 11)
                {
                    row = 1;
                    col = RedCount - 5;
                }
                if (RedCount >= 10 && RedCount < 15)
                {
                    row = 2;
                    col = RedCount - 10;
                }
                RedCount++;
                RCapturedPiece.Add(new CapturedPiece());
                RCapturedPiece[RedCount - 1].Row = row;
                RCapturedPiece[RedCount - 1].Col = col;
                RCapturedPiece[RedCount - 1].PbPiece = p.PbPiece;
                if (p.Name.Equals("KING")) RCapturedPiece[RedCount - 1].PbPiece.Image = Resources.Captured_R_K;
                if (p.Name.Equals("ADVISOR")) RCapturedPiece[RedCount - 1].PbPiece.Image = Resources.Captured_R_A;
                if (p.Name.Equals("ELEPHANT")) RCapturedPiece[RedCount - 1].PbPiece.Image = Resources.Captured_R_E;
                if (p.Name.Equals("HORSE")) RCapturedPiece[RedCount - 1].PbPiece.Image = Resources.Captured_R_H;
                if (p.Name.Equals("CHARIOT")) RCapturedPiece[RedCount - 1].PbPiece.Image = Resources.Captured_R_R;
                if (p.Name.Equals("CANNON")) RCapturedPiece[RedCount - 1].PbPiece.Image = Resources.Captured_R_C;
                if (p.Name.Equals("SOLDIER")) RCapturedPiece[RedCount - 1].PbPiece.Image = Resources.Captured_R_S;
                RCapturedPiece[RedCount - 1].PbPiece.Width = 25;
                RCapturedPiece[RedCount - 1].PbPiece.Height = 25;
                RCapturedPiece[RedCount - 1].PbPiece.BackColor = Color.Transparent;
                RCapturedPiece[RedCount - 1].PbPiece.Cursor = Cursors.Arrow;
                RCapturedPiece[RedCount - 1].PbPiece.Top = RCapturedPiece[RedCount - 1].Row*30 + 185;
                RCapturedPiece[RedCount - 1].PbPiece.Left = RCapturedPiece[RedCount - 1].Col*30 + 465;
            }
        }

        #region Nested type: CapturedPiece

        public class CapturedPiece
        {
            #region Properties

            public int Row { get; set; }

            public int Col { get; set; }

            public PictureBox PbPiece { get; set; }

            #endregion
        }

        #endregion
    }
}