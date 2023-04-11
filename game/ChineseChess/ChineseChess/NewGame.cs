using System;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class frmNewGame : Form
    {
        public frmNewGame()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Game.NewGame();
            Close();
        }
    }
}
