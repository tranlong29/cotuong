namespace ChineseChess.Pieces
{
    class Cannon : Piece
    {
        public override bool Move(int i, int j)
        {
            bool turn = true;
            int count = 0;      // Count how many pieces between the cannon piece and other piece in the same row or column
            if (i < 0 || i > 9 || j < 0 || j > 8) turn = false;
            if (i != Row && j != Col) turn = false;

            if (i >= 0 && i <= 9 && j >= 0 && j <= 8 && (i == Row || j == Col))
            {
                if (Board.Position[i, j].IsEmpty)
                {
                    if (i == Row && j >= 0 && j <= 8)
                    {
                        if (j > Col) // Move right
                        {
                            for (int k = Col + 1; k < j; k++)
                            {
                                if (!Board.Position[i, k].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                        if (j < Col) // Move left
                        {
                            for (int k = j + 1; k < Col; k++)
                            {
                                if (!Board.Position[i, k].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (j == Col && i >= 0 && i <= 9)
                    {
                        if (i > Row) // Move down
                        {
                            for (int k = Row + 1; k < i; k++)
                            {
                                if (!Board.Position[k, j].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                        if (i < Row) // Move up
                        {
                            for (int k = i + 1; k < Row; k++)
                            {
                                if (!Board.Position[k, j].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                else // Not empty
                {
                    if (Board.Position[i, j].Side != Side)
                    {
                        if (i == Row && j >= 0 && j <= 8)
                        {
                            if (j > Col)
                            {
                                for (int k = Col + 1; k < j; k++)
                                {
                                    if (!Board.Position[i, k].IsEmpty) count++;
                                }
                            }
                            if (j < Col)
                            {
                                for (int k = j + 1; k < Col; k++)
                                {
                                    if (!Board.Position[i, k].IsEmpty) count++;
                                }
                            }
                        }
                        if (j == Col && i >= 0 && i <= 9)
                        {
                            if (i > Row)
                            {
                                for (int k = Row + 1; k < i; k++)
                                {
                                    if (!Board.Position[k, j].IsEmpty) count++;
                                }
                            }
                            if (i < Row)
                            {
                                for (int k = i + 1; k < Row; k++)
                                {
                                    if (!Board.Position[k, j].IsEmpty) count++;
                                }
                            }
                        }
                        if (count != 1) turn = false;
                    }
                    if (Board.Position[i, j].Side == Side) turn = false;
                }
            }

            if (Game.Players[0].KingPiece.Col == Game.Players[1].KingPiece.Col && Col == Game.Players[0].KingPiece.Col)
            {
                int cnt = 0;
                if (j != Col)
                {
                    Game.ResetChessPoint(Row, Col);
                    for (int k = Game.Players[0].KingPiece.Row + 1; k < Game.Players[1].KingPiece.Row; k++)
                    {
                        if (!Board.Position[k, Col].IsEmpty) cnt++;
                    }
                    if (cnt == 0) turn = false;
                    Board.Position[Row, Col].IsEmpty = false;
                    Board.Position[Row, Col].Name = Name;
                    Board.Position[Row, Col].Side = Side;
                    Board.Position[Row, Col].Index = Index;
                }
                if (j == Col && (i < Game.Players[0].KingPiece.Row || i > Game.Players[1].KingPiece.Row))
                {
                    Game.ResetChessPoint(Row, Col);
                    for (int k = Game.Players[0].KingPiece.Row + 1; k < Game.Players[1].KingPiece.Row; k++)
                    {
                        if (!Board.Position[k, Col].IsEmpty) cnt++;
                    }
                    if (cnt == 0) turn = false;
                    Board.Position[Row, Col].IsEmpty = false;
                    Board.Position[Row, Col].Name = Name;
                    Board.Position[Row, Col].Side = Side;
                    Board.Position[Row, Col].Index = Index;
                }
            }
            return turn;
        }
    }
}
