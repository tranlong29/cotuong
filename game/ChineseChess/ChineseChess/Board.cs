using System.Drawing;
using System.Windows.Forms;
using ChineseChess.Properties;

namespace ChineseChess
{
    /// <summary>
    /// Define a chessboard
    /// </summary>
    internal static class Board
    {
        public static readonly ChessPoint[,] Position = new ChessPoint[10,9];

        static Board()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Position[i, j] = new ChessPoint();
                    Position[i, j].Row = i;
                    Position[i, j].Col = j;
                    Position[i, j].IsEmpty = true;
                    Position[i, j].Name = "";
                    Position[i, j].Index = -1;
                    Position[i, j].Side = -1;
                    Position[i, j].ValidMove = new PictureBox();
                    Position[i, j].ValidMove.Image = Resources.ValidMove;
                    Position[i, j].ValidMove.Width = 20;
                    Position[i, j].ValidMove.Height = 20;
                    Position[i, j].ValidMove.BackColor = Color.Transparent;
                    Position[i, j].ValidMove.Top = i*48 + 47;
                    Position[i, j].ValidMove.Left = j*48 + 23;
                    Position[i, j].ValidMove.Cursor = Cursors.Hand;
                    Position[i, j].ValidMove.Visible = false;
                }
            }
        }

        public static void ResetValidMove()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Position[i, j].ValidMove.Visible = false;
                }
            }
        }

        /// <summary>
        /// Reset chess board
        /// </summary>
        public static void ResetBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Position[i, j].Row = i;
                    Position[i, j].Col = j;
                    Position[i, j].IsEmpty = true;
                    Position[i, j].Name = "";
                    Position[i, j].Index = -1;
                    Position[i, j].Side = -1;
                    Position[i, j].ValidMove.Visible = false;
                }
            }
        }

        #region Nested type: ChessPoint

        public class ChessPoint // intersections of horizontal lines vs vertical lines in the chess board
        {
            #region Properties

            private int _point;
            public int Row { get; set; }

            public int Col { get; set; }

            public int Side { get; set; }

            public int Index { get; set; }

            public bool IsEmpty { get; set; }

            public string Name { get; set; }

            public PictureBox ValidMove { get; set; }

            public int Value
            {
                get
                {
                    if (Name.Equals("KING")) _point = Engine.PointTable[0, Side, 9*Row + Col];
                    if (Name.Equals("ADVISOR")) _point = Engine.PointTable[1, Side, 9*Row + Col];
                    if (Name.Equals("ELEPHANT")) _point = Engine.PointTable[2, Side, 9*Row + Col];
                    if (Name.Equals("HORSE")) _point = Engine.PointTable[3, Side, 9*Row + Col];
                    if (Name.Equals("CHARIOT")) _point = Engine.PointTable[4, Side, 9*Row + Col];
                    if (Name.Equals("CANNON")) _point = Engine.PointTable[5, Side, 9*Row + Col];
                    if (Name.Equals("SOLDIER")) _point = Engine.PointTable[6, Side, 9*Row + Col];
                    return _point;
                }
            }

            #endregion
        }

        #endregion
    }
}