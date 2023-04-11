using ChineseChess.Pieces;

namespace ChineseChess
{
    class Move
    {
        internal Piece PieceFrom { get; set; }

        internal Piece PieceDest { get; set; }

        public int RowFrom { get; set; }

        public int RowDest { get; set; }

        public int ColFrom { get; set; }

        public int ColDest { get; set; }
    }
}
