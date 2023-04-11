namespace ChineseChess.Pieces
{
    internal class Soldier : Piece
    {
        public override bool Move(int i, int j)
        {
            bool turn = false;
            if (Side == 0)
            {
                if (i >= 0 && i <= 4)
                {
                    if (i == Row + 1 && j == Col)
                    {
                        if (Board.Position[i, j].IsEmpty)
                            turn = true;
                        else
                        {
                            if (Board.Position[i, j].Side != Side) turn = true;
                        }
                    }
                }
                if (i > 4 && i <= 9)
                {
                    if ((i == Row + 1 && j == Col) ||
                        (i == Row && j == Col - 1) ||
                        (i == Row && j == Col + 1)
                        )
                        if (i >= 0 && i <= 9 && j >= 0 && j <= 8)
                        {
                            if (Board.Position[i, j].IsEmpty) turn = true;
                            else
                            {
                                if (Board.Position[i, j].Side != Side) turn = true;
                            }
                        }
                }
            }
            if (Side == 1)
            {
                if (i <= 9 && i >= 5)
                {
                    if ((i == Row - 1) && (j == Col))
                    {
                        if (Board.Position[i, j].IsEmpty)
                            turn = true;
                        else
                        {
                            if (Board.Position[i, j].Side != Side) turn = true;
                        }
                    }
                }
                if (i < 5 && i >= 0)
                {
                    if ((i == Row - 1 && j == Col) ||
                        (i == Row && j == Col - 1) ||
                        (i == Row && j == Col + 1))
                        if (i >= 0 && i <= 9 && j >= 0 && j <= 8)
                        {
                            if (Board.Position[i, j].IsEmpty) turn = true;
                            else
                            {
                                if (Board.Position[i, j].Side != Side) turn = true;
                            }
                        }
                }
            }

            if (Game.Players[0].KingPiece.Col == Game.Players[1].KingPiece.Col && Col == Game.Players[0].KingPiece.Col)
            {
                int ct = 0;
                if (j != Col)
                {
                    Game.ResetChessPoint(Row, Col);
                    for (int t = Game.Players[0].KingPiece.Row + 1; t < Game.Players[1].KingPiece.Row; t++)
                    {
                        if (Board.Position[t, Col].IsEmpty == false) ct++;
                    }
                    if (ct == 0) turn = false;
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