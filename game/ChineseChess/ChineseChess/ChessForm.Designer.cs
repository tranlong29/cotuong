namespace ChineseChess
{
    partial class ChessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessForm));
            this.lblComputerTime = new System.Windows.Forms.Label();
            this.lblPlayerTime = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pbx_Expand = new System.Windows.Forms.PictureBox();
            this.pbUndo = new System.Windows.Forms.PictureBox();
            this.SavetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Expand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUndo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblComputerTime
            // 
            this.lblComputerTime.AutoSize = true;
            this.lblComputerTime.BackColor = System.Drawing.Color.Transparent;
            this.lblComputerTime.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComputerTime.Location = new System.Drawing.Point(500, 139);
            this.lblComputerTime.Name = "lblComputerTime";
            this.lblComputerTime.Size = new System.Drawing.Size(94, 27);
            this.lblComputerTime.TabIndex = 3;
            this.lblComputerTime.Text = "00:00:00";
            // 
            // lblPlayerTime
            // 
            this.lblPlayerTime.AutoSize = true;
            this.lblPlayerTime.BackColor = System.Drawing.Color.Transparent;
            this.lblPlayerTime.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerTime.Location = new System.Drawing.Point(500, 341);
            this.lblPlayerTime.Name = "lblPlayerTime";
            this.lblPlayerTime.Size = new System.Drawing.Size(94, 27);
            this.lblPlayerTime.TabIndex = 4;
            this.lblPlayerTime.Text = "00:00:00";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalTime.Font = new System.Drawing.Font("Trebuchet MS", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTime.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTotalTime.Location = new System.Drawing.Point(451, 279);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(0, 46);
            this.lblTotalTime.TabIndex = 5;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(677, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.SavetoolStripMenuItem,
            this.undoToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fileToolStripMenuItem.Text = "&Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.newGameToolStripMenuItem.Text = "&Game mới";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.undoToolStripMenuItem.Text = "Đ&i lại";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(220, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exitToolStripMenuItem.Text = "&Thoát game";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.helpToolStripMenuItem.Text = "&Thông tin";
            // 
            // pbx_Expand
            // 
            this.pbx_Expand.BackColor = System.Drawing.Color.Transparent;
            this.pbx_Expand.Cursor = System.Windows.Forms.Cursors.PanEast;
            this.pbx_Expand.Image = global::ChineseChess.Properties.Resources.Expand;
            this.pbx_Expand.Location = new System.Drawing.Point(644, 25);
            this.pbx_Expand.Name = "pbx_Expand";
            this.pbx_Expand.Size = new System.Drawing.Size(32, 32);
            this.pbx_Expand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_Expand.TabIndex = 7;
            this.pbx_Expand.TabStop = false;
            this.toolTip.SetToolTip(this.pbx_Expand, "Xem nước đi");
            // 
            // pbUndo
            // 
            this.pbUndo.BackColor = System.Drawing.Color.Transparent;
            this.pbUndo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbUndo.Image = global::ChineseChess.Properties.Resources.Undo;
            this.pbUndo.Location = new System.Drawing.Point(453, 25);
            this.pbUndo.Name = "pbUndo";
            this.pbUndo.Size = new System.Drawing.Size(32, 32);
            this.pbUndo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUndo.TabIndex = 8;
            this.pbUndo.TabStop = false;
            this.toolTip.SetToolTip(this.pbUndo, "Đi lại");
            this.pbUndo.Click += new System.EventHandler(this.pbUndo_Click);
            // 
            // SavetoolStripMenuItem
            // 
            this.SavetoolStripMenuItem.Name = "SavetoolStripMenuItem";
            this.SavetoolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SavetoolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.SavetoolStripMenuItem.Text = "&Lưu game đang chơi";
            this.SavetoolStripMenuItem.Click += new System.EventHandler(this.SavetoolStripMenuItem_Click);
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::ChineseChess.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(677, 521);
            this.Controls.Add(this.lblPlayerTime);
            this.Controls.Add(this.lblComputerTime);
            this.Controls.Add(this.pbUndo);
            this.Controls.Add(this.pbx_Expand);
            this.Controls.Add(this.lblTotalTime);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "ChessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chinese Chess";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChessBoard_MouseClick);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Expand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUndo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblComputerTime;
        private System.Windows.Forms.Label lblPlayerTime;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.PictureBox pbx_Expand;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbUndo;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SavetoolStripMenuItem;
    }
}

