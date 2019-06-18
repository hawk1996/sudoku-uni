namespace Sudoku.UI
{
	partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.gameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newPuzzleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newBlankPuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEasyPuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMediumPuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newHardPuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newExtremePuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.savePuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePuzzleAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solvePuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.statusUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.puzzleView = new Sudoku.UI.PuzzleView();
            this.statusBar.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 482);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(585, 22);
            this.statusBar.TabIndex = 5;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameMenu,
            this.editMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(585, 28);
            this.mainMenu.TabIndex = 6;
            this.mainMenu.Text = "menuStrip1";
            // 
            // gameMenu
            // 
            this.gameMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPuzzleMenu,
            this.loadPuzzleMenuItem,
            this.toolStripSeparator,
            this.savePuzzleMenuItem,
            this.savePuzzleAsMenuItem,
            this.toolStripSeparator1,
            this.startGameMenuItem,
            this.solvePuzzleMenuItem,
            this.toolStripSeparator2,
            this.exitMenuItem});
            this.gameMenu.Name = "gameMenu";
            this.gameMenu.Size = new System.Drawing.Size(60, 24);
            this.gameMenu.Text = "&Game";
            // 
            // newPuzzleMenu
            // 
            this.newPuzzleMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBlankPuzzleMenuItem,
            this.newEasyPuzzleMenuItem,
            this.newMediumPuzzleMenuItem,
            this.newHardPuzzleMenuItem,
            this.newExtremePuzzleMenuItem});
            this.newPuzzleMenu.Image = ((System.Drawing.Image)(resources.GetObject("newPuzzleMenu.Image")));
            this.newPuzzleMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newPuzzleMenu.Name = "newPuzzleMenu";
            this.newPuzzleMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newPuzzleMenu.Size = new System.Drawing.Size(167, 26);
            this.newPuzzleMenu.Text = "&New";
            // 
            // newBlankPuzzleMenuItem
            // 
            this.newBlankPuzzleMenuItem.Name = "newBlankPuzzleMenuItem";
            this.newBlankPuzzleMenuItem.Size = new System.Drawing.Size(139, 26);
            this.newBlankPuzzleMenuItem.Text = "&Blank";
            this.newBlankPuzzleMenuItem.Click += new System.EventHandler(this.newBlankPuzzleMenuItem_Click);
            // 
            // newEasyPuzzleMenuItem
            // 
            this.newEasyPuzzleMenuItem.Name = "newEasyPuzzleMenuItem";
            this.newEasyPuzzleMenuItem.Size = new System.Drawing.Size(139, 26);
            this.newEasyPuzzleMenuItem.Text = "&Easy";
            this.newEasyPuzzleMenuItem.Click += new System.EventHandler(this.newEasyPuzzleMenuItem_Click);
            // 
            // newMediumPuzzleMenuItem
            // 
            this.newMediumPuzzleMenuItem.Name = "newMediumPuzzleMenuItem";
            this.newMediumPuzzleMenuItem.Size = new System.Drawing.Size(139, 26);
            this.newMediumPuzzleMenuItem.Text = "&Medium";
            this.newMediumPuzzleMenuItem.Click += new System.EventHandler(this.newMediumPuzzleMenuItem_Click);
            // 
            // newHardPuzzleMenuItem
            // 
            this.newHardPuzzleMenuItem.Name = "newHardPuzzleMenuItem";
            this.newHardPuzzleMenuItem.Size = new System.Drawing.Size(139, 26);
            this.newHardPuzzleMenuItem.Text = "&Hard";
            this.newHardPuzzleMenuItem.Click += new System.EventHandler(this.newHardPuzzleMenuItem_Click);
            // 
            // newExtremePuzzleMenuItem
            // 
            this.newExtremePuzzleMenuItem.Name = "newExtremePuzzleMenuItem";
            this.newExtremePuzzleMenuItem.Size = new System.Drawing.Size(139, 26);
            this.newExtremePuzzleMenuItem.Text = "&Extreme";
            this.newExtremePuzzleMenuItem.Click += new System.EventHandler(this.newExtremePuzzleMenuItem_Click);
            // 
            // loadPuzzleMenuItem
            // 
            this.loadPuzzleMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadPuzzleMenuItem.Image")));
            this.loadPuzzleMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadPuzzleMenuItem.Name = "loadPuzzleMenuItem";
            this.loadPuzzleMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadPuzzleMenuItem.Size = new System.Drawing.Size(167, 26);
            this.loadPuzzleMenuItem.Text = "&Load";
            this.loadPuzzleMenuItem.Click += new System.EventHandler(this.loadPuzzleMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(164, 6);
            // 
            // savePuzzleMenuItem
            // 
            this.savePuzzleMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("savePuzzleMenuItem.Image")));
            this.savePuzzleMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savePuzzleMenuItem.Name = "savePuzzleMenuItem";
            this.savePuzzleMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.savePuzzleMenuItem.Size = new System.Drawing.Size(167, 26);
            this.savePuzzleMenuItem.Text = "&Save";
            this.savePuzzleMenuItem.Click += new System.EventHandler(this.savePuzzleMenuItem_Click);
            // 
            // savePuzzleAsMenuItem
            // 
            this.savePuzzleAsMenuItem.Name = "savePuzzleAsMenuItem";
            this.savePuzzleAsMenuItem.Size = new System.Drawing.Size(167, 26);
            this.savePuzzleAsMenuItem.Text = "Save &As";
            this.savePuzzleAsMenuItem.Click += new System.EventHandler(this.savePuzzleAsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // startGameMenuItem
            // 
            this.startGameMenuItem.Name = "startGameMenuItem";
            this.startGameMenuItem.Size = new System.Drawing.Size(167, 26);
            this.startGameMenuItem.Text = "S&tart";
            this.startGameMenuItem.Click += new System.EventHandler(this.startGameMenuItem_Click);
            // 
            // solvePuzzleMenuItem
            // 
            this.solvePuzzleMenuItem.Name = "solvePuzzleMenuItem";
            this.solvePuzzleMenuItem.Size = new System.Drawing.Size(167, 26);
            this.solvePuzzleMenuItem.Text = "Sol&ve";
            this.solvePuzzleMenuItem.Click += new System.EventHandler(this.solvePuzzleMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(167, 26);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem,
            this.toolStripSeparator3});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(47, 24);
            this.editMenu.Text = "&Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoMenuItem.Size = new System.Drawing.Size(171, 26);
            this.undoMenuItem.Text = "&Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
            // 
            // redoMenuItem
            // 
            this.redoMenuItem.Name = "redoMenuItem";
            this.redoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoMenuItem.Size = new System.Drawing.Size(171, 26);
            this.redoMenuItem.Text = "&Redo";
            this.redoMenuItem.Click += new System.EventHandler(this.redoMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // statusUpdateTimer
            // 
            this.statusUpdateTimer.Interval = 1000;
            this.statusUpdateTimer.Tick += new System.EventHandler(this.statucUpdateTimer_Tick);
            // 
            // puzzleView
            // 
            this.puzzleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.puzzleView.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.puzzleView.Location = new System.Drawing.Point(0, 28);
            this.puzzleView.Margin = new System.Windows.Forms.Padding(4);
            this.puzzleView.Name = "puzzleView";
            this.puzzleView.Size = new System.Drawing.Size(585, 454);
            this.puzzleView.TabIndex = 0;
            this.puzzleView.Text = "gridView1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(585, 504);
            this.Controls.Add(this.puzzleView);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Sudoku";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private PuzzleView puzzleView;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem gameMenu;
		private System.Windows.Forms.ToolStripMenuItem newPuzzleMenu;
		private System.Windows.Forms.ToolStripMenuItem newEasyPuzzleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newMediumPuzzleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newHardPuzzleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newExtremePuzzleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadPuzzleMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem savePuzzleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem savePuzzleAsMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editMenu;
		private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem solvePuzzleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newBlankPuzzleMenuItem;
		private System.Windows.Forms.Timer statusUpdateTimer;
		private System.Windows.Forms.ToolStripMenuItem startGameMenuItem;
	}
}

