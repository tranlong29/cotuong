using ChineseChess.Pieces;

namespace ChineseChess
{
    class Player
    {
        private King _kingPiece = new King();
        private Advisor[] _advisorPieces = new Advisor[2];
        private Elephant[] _elephantPieces = new Elephant[2];
        private Horse[] _horsePieces = new Horse[2];
        private Chariot[] _chariotPieces = new Chariot[2];
        private Cannon[] _cannonPieces = new Cannon[2];
        private Soldier[] _soldierPieces = new Soldier[5];

        public static int COM = -1;
        public static int MAN = -1;

        #region Properties

        public static int Side { get; set; }          // Red or Black?
        internal King KingPiece
        {
            get { return _kingPiece; }
            set { _kingPiece = value; }
        }
        internal Advisor[] AdvisorPieces
        {
            get { return _advisorPieces; }
            set { _advisorPieces = value; }
        }
        internal Elephant[] ElephantPieces
        {
            get { return _elephantPieces; }
            set { _elephantPieces = value; }
        }
        internal Horse[] HorsePieces
        {
            get { return _horsePieces; }
            set { _horsePieces = value; }
        }
        internal Chariot[] ChariotPieces
        {
            get { return _chariotPieces; }
            set { _chariotPieces = value; }
        }
        internal Cannon[] CannonPieces
        {
            get { return _cannonPieces; }
            set { _cannonPieces = value; }
        }
        internal Soldier[] SoldierPieces
        {
            get { return _soldierPieces; }
            set { _soldierPieces = value; }
        }
        #endregion

        /// <summary>
        /// Constructor with one param: side
        /// side = 0: BLACK
        /// side = 1: RED
        /// </summary>
        /// <param name="side"></param>
        public Player(int side)
        {
            AdvisorPieces[0] = new Advisor();
            AdvisorPieces[1] = new Advisor();
            ElephantPieces[0] = new Elephant();
            ElephantPieces[1] = new Elephant();
            ChariotPieces[0] = new Chariot();
            ChariotPieces[1] = new Chariot();
            CannonPieces[0] = new Cannon();
            CannonPieces[1] = new Cannon();
            HorsePieces[0] = new Horse();
            HorsePieces[1] = new Horse();
            SoldierPieces[0] = new Soldier();
            SoldierPieces[1] = new Soldier();
            SoldierPieces[2] = new Soldier();
            SoldierPieces[3] = new Soldier();
            SoldierPieces[4] = new Soldier();

            if (side == 0) // BLACK
            {
                //Side = 0;
                KingPiece.Init(0, "KING", 0, 1, true, 0, 4);
                AdvisorPieces[0].Init(0, "ADVISOR", 0, 1, true, 0, 3);
                AdvisorPieces[1].Init(0, "ADVISOR", 1, 1, true, 0, 5);
                ElephantPieces[0].Init(0, "ELEPHANT", 0, 1, true, 0, 2);
                ElephantPieces[1].Init(0, "ELEPHANT", 1, 1, true, 0, 6);
                HorsePieces[0].Init(0, "HORSE", 0, 1, true, 0, 1);
                HorsePieces[1].Init(0, "HORSE", 1, 1, true, 0, 7);
                ChariotPieces[0].Init(0, "CHARIOT", 0, 1, true, 0, 0);
                ChariotPieces[1].Init(0, "CHARIOT", 1, 1, true, 0, 8);
                CannonPieces[0].Init(0, "CANNON", 0, 1, true, 2, 1);
                CannonPieces[1].Init(0, "CANNON", 1, 1, true, 2, 7);
                SoldierPieces[0].Init(0, "SOLDIER", 0, 1, true, 3, 0);
                SoldierPieces[1].Init(0, "SOLDIER", 1, 1, true, 3, 2);
                SoldierPieces[2].Init(0, "SOLDIER", 2, 1, true, 3, 4);
                SoldierPieces[3].Init(0, "SOLDIER", 3, 1, true, 3, 6);
                SoldierPieces[4].Init(0, "SOLDIER", 4, 1, true, 3, 8);
            }
            else    // RED
            {
                //Side = 1;
                KingPiece.Init(1, "KING", 0, 1, false, 9, 4);
                AdvisorPieces[0].Init(1, "ADVISOR", 0, 1, false, 9, 3);
                AdvisorPieces[1].Init(1, "ADVISOR", 1, 1, false, 9, 5);
                ElephantPieces[0].Init(1, "ELEPHANT", 0, 1, false, 9, 2);
                ElephantPieces[1].Init(1, "ELEPHANT", 1, 1, false, 9, 6);
                HorsePieces[0].Init(1, "HORSE", 0, 1, false, 9, 1);
                HorsePieces[1].Init(1, "HORSE", 1, 1, false, 9, 7);
                ChariotPieces[0].Init(1, "CHARIOT", 0, 1, false, 9, 0);
                ChariotPieces[1].Init(1, "CHARIOT", 1, 1, false, 9, 8);
                CannonPieces[0].Init(1, "CANNON", 0, 1, false, 7, 1);
                CannonPieces[1].Init(1, "CANNON", 1, 1, false, 7, 7);
                SoldierPieces[0].Init(1, "SOLDIER", 0, 1, false, 6, 0);
                SoldierPieces[1].Init(1, "SOLDIER", 1, 1, false, 6, 2);
                SoldierPieces[2].Init(1, "SOLDIER", 2, 1, false, 6, 4);
                SoldierPieces[3].Init(1, "SOLDIER", 3, 1, false, 6, 6);
                SoldierPieces[4].Init(1, "SOLDIER", 4, 1, false, 6, 8);
            }
        }
    }
}
