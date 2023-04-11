namespace ChineseChess.Pieces
{
    class Advisor : Piece
    {
        /// <summary>
        /// Check valid move for the Advisor piece
        /// </summary>
        /// <param name="i">row number the piece move to</param>
        /// <param name="j">column number the piece move to</param>
        /// <returns></returns>
        public override bool Move(int i, int j)
        {
            bool turn = false;
            if ((i >= 0 && i <= 2 && j >= 3 && j <= 5) || (i >= 7 && i <= 9 && j >= 3 && j <= 5))
            {
                if ((i == Row + 1 && j == Col + 1) ||
                    (i == Row + 1 && j == Col - 1) ||
                    (i == Row - 1 && j == Col + 1) ||
                    (i == Row - 1 && j == Col - 1))
                {
                    if (Board.Position[i, j].IsEmpty)
                        turn = true;
                    else
                    {
                        if (Board.Position[i, j].Side != Side)
                            turn = true;
                    }
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
            }
            return turn;
        }
    }
}
