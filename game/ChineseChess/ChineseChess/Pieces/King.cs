namespace ChineseChess.Pieces
{
    class King : Piece
    {
        private void SetTempPiece(ref Piece tmp, int i, int j)
        {
            if (Board.Position[i, j].Name.Equals("KING")) tmp = Game.Players[Board.Position[i, j].Side].KingPiece;
            if (Board.Position[i, j].Name.Equals("ADVISOR")) tmp = Game.Players[Board.Position[i, j].Side].AdvisorPieces[Board.Position[i, j].Index];
            if (Board.Position[i, j].Name.Equals("ELEPHANT")) tmp = Game.Players[Board.Position[i, j].Side].ElephantPieces[Board.Position[i, j].Index];
            if (Board.Position[i, j].Name.Equals("HORSE")) tmp = Game.Players[Board.Position[i, j].Side].HorsePieces[Board.Position[i, j].Index];
            if (Board.Position[i, j].Name.Equals("CHARIOT")) tmp = Game.Players[Board.Position[i, j].Side].ChariotPieces[Board.Position[i, j].Index];
            if (Board.Position[i, j].Name.Equals("CANNON")) tmp = Game.Players[Board.Position[i, j].Side].CannonPieces[Board.Position[i, j].Index];
            if (Board.Position[i, j].Name.Equals("SOLDIER")) tmp = Game.Players[Board.Position[i, j].Side].SoldierPieces[Board.Position[i, j].Index];
        }

        /// <summary>
        /// Check valid move for the King piece
        /// </summary>
        /// <param name="i">row number the piece move to</param>
        /// <param name="j">column number the piece move to</param>
        /// <returns></returns>
        public override bool Move(int i, int j)
        {
            bool turn = false;
            int _row, _col;
            Piece tmp = new Piece();

            if ((i >= 0 && i <= 2 && j >= 3 && j <= 5) || (i >= 7 && i <= 9 && j >= 3 && j <= 5))
            {
                if ((i == Row + 1 && j == Col) ||
                    (i == Row - 1 && j == Col) ||
                    (i == Row && j == Col + 1) ||
                    (i == Row && j == Col - 1))
                {
                    if (Board.Position[i, j].IsEmpty)
                        turn = true;
                    else
                    {
                        if (Board.Position[i, j].Side != Side)
                        {
                            turn = true;
                        }
                    }
                }
            }

            if (Side == 0)     // BLACK
            {
                int cnt = 0;
                if (j == Game.Players[1].KingPiece.Col)
                {
                    if (Board.Position[i, j].IsEmpty)
                    {
                        for (int k = Row + 1; k < Game.Players[1].KingPiece.Row; k++)
                        {
                            if (Board.Position[k, j].IsEmpty) cnt++;
                        }
                        if (cnt == 0) turn = false;
                    }
                    else
                    {
                        SetTempPiece(ref tmp, i, j);
                        _row = Row;
                        _col = Col;
                        Game.ResetChessPoint(Row, Col);
                        Board.Position[i, j].IsEmpty = false;
                        Board.Position[i, j].Side = Side;
                        Board.Position[i, j].Name = Name;
                        Board.Position[i, j].Index = Index;
                        Row = i;
                        Col = j;
                        tmp.State = 0;
                        tmp.PbPiece.Visible = false;

                        for (int k = Row + 1; k < Game.Players[1].KingPiece.Row; k++)
                        {
                            if (!Board.Position[i, j].IsEmpty) cnt++;
                        }
                        if (cnt == 0) turn = false;

                        Row = _row;
                        Col = _col;
                        Game.ResetChessPoint(i, j);
                        Board.Position[_row, _col].IsEmpty = false;
                        Board.Position[_row, _col].Side = Side;
                        Board.Position[_row, _col].Name = Name;
                        Board.Position[_row, _col].Index = Index;
                        if (tmp.Side != -1)
                        {
                            tmp.State = 1;
                            tmp.PbPiece.Visible = true;
                            Board.Position[i, j].IsEmpty = false;
                            Board.Position[i, j].Name = tmp.Name;
                            Board.Position[i, j].Side = tmp.Side;
                            Board.Position[i, j].Index = tmp.Index;
                        }
                    }
                }
            }
            // RED
            else
            {
                int cnt = 0;
                if (j == Game.Players[0].KingPiece.Col)
                {
                    if (Board.Position[i, j].IsEmpty)
                    {
                        for (int k = Row - 1; k > Game.Players[0].KingPiece.Row; k--)
                        {
                            if (!Board.Position[k, j].IsEmpty) cnt++;
                        }
                        if (cnt == 0) turn = false;
                    }
                    else
                    {
                        SetTempPiece(ref tmp, i, j);
                        _row = Row;
                        _col = Col;
                        Game.ResetChessPoint(Row, Col);
                        Board.Position[i, j].IsEmpty = false;
                        Board.Position[i, j].Side = Side;
                        Board.Position[i, j].Name = Name;
                        Board.Position[i, j].Index = Index;
                        Row = i;
                        Col = j;
                        tmp.State = 0;
                        tmp.PbPiece.Visible = false;

                        for (int k = Row - 1; k > Game.Players[0].KingPiece.Col; k--)
                        {
                            if (!Board.Position[k, j].IsEmpty) cnt++;
                        }
                        if (cnt == 0) turn = false;

                        Row = _row;
                        Col = _col;
                        Game.ResetChessPoint(i, j);
                        Board.Position[i, j].IsEmpty = false;
                        Board.Position[i, j].Side = Side;
                        Board.Position[i, j].Name = Name;
                        Board.Position[i, j].Index = Index;
                        if (tmp.Side != -1)
                        {
                            tmp.State = 1;
                            tmp.PbPiece.Visible = true;
                            Board.Position[i, j].IsEmpty = false;
                            Board.Position[i, j].Name = tmp.Name;
                            Board.Position[i, j].Side = tmp.Side;
                            Board.Position[i, j].Index = tmp.Index;
                        }
                    }
                }
            }

            return turn;
        }
    }
}
