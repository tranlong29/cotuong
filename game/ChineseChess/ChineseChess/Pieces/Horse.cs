namespace ChineseChess.Pieces
{
    class Horse : Piece
    {
        /// <summary>
        /// Check valid move for the Horse piece
        /// </summary>
        /// <param name="i">row number the piece move to</param>
        /// <param name="j">column number the piece move to</param>
        /// <returns></returns>
        public override bool Move(int i, int j)
        {
            bool turn = false;
            if (i == Row - 2 && (j == Col -1 || j == Col + 1))
            {
                if (Row - 2 >= 0 && Row - 2 <= 9 && 
                    ((Col - 1 >= 0 && Col - 1 <= 8) || (Col + 1 >= 0 && Col + 1 <= 8)))
                {
                    if (Board.Position[Row - 1, Col].IsEmpty)
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
            }
            if (i == Row + 2 && (j == Col - 1 || j == Col + 1))
            {
                if (Row + 2 >= 0 && Row + 2 <= 9 &&
                    ((Col - 1 >= 0 && Col - 1 <= 8) || (Col + 1 >= 0 && Col + 1 <= 8)))
                {
                    if (Board.Position[Row + 1, Col].IsEmpty)
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
            }
            if (j == Col - 2 && (i == Row - 1 || i == Row + 1))
            {
                if (Col - 2 >= 0 && Col - 2 <= 8 &&
                    (Row - 1 >= 0 && Row - 1 <= 9) || (Row + 1 >= 0 && Row + 1 <= 9))
                {
                    if (Board.Position[Row, Col - 1].IsEmpty)
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
            }
            if (j == Col + 2 && (i == Row - 1 || i == Row + 1))
            {
                if (Col + 2 >= 0 && Col + 2 <= 8 &&
                    (Row - 1 >= 0 && Row - 1 <= 9) || (Row + 1 >= 0 && Row + 1 <= 9))
                {
                    if (Board.Position[Row, Col + 1].IsEmpty)
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
