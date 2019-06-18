using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.UI
{
	partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			puzzle = new PuzzleViewModel();
			puzzle.MessageOwner = this;
			puzzle.StateChanged += OnStateChanged;
			puzzle.SourceChanged += OnSourceChanged;
			puzzleView.SetSource(puzzle);
			UpdateState();
		}

		private PuzzleViewModel puzzle;
		private bool isPlaying;
		private Stopwatch playTime = new Stopwatch();

		private void UpdateStatusInfo()
		{
			if (isPlaying)
				statusLabel.Text = "Playing " + playTime.Elapsed.ToString(@"hh\:mm\:ss");
			else
				statusLabel.Text = "Enter puzzle";
		}

        private void statucUpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatusInfo();
        }

        private void UpdatePlayingState(bool force)
		{
			bool wasPlaying = isPlaying;
			isPlaying = puzzle.IsReady;
			if (!force && wasPlaying == isPlaying) return;	
			if (isPlaying)
				playTime.Restart();
			else
				playTime.Stop();
			statusUpdateTimer.Enabled = isPlaying;
			UpdateStatusInfo();
		}

		private void UpdateState(bool force = true)
		{
			Text = "Sudoku - " + puzzle.Name;
			undoMenuItem.Enabled = puzzle.CanUndo;
			redoMenuItem.Enabled = puzzle.CanRedo;
			savePuzzleMenuItem.Enabled = puzzle.CanSave;
			savePuzzleAsMenuItem.Enabled = puzzle.CanSave;
			startGameMenuItem.Enabled = puzzle.CanSetReady;
			solvePuzzleMenuItem.Enabled = puzzle.CanSolve;
			UpdatePlayingState(force);
		}

		private void OnSourceChanged(object sender, EventArgs e)
		{
			UpdateState();
		}

		private void OnStateChanged(object sender, EventArgs e)
		{
			UpdateState(force: false);
		}

		private void undoMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.Undo();
		}

		private void redoMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.Redo();
		}

		private void newBlankPuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.New();
		}

		private void newEasyPuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.New(Difficulty.Easy);
		}

		private void newMediumPuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.New(Difficulty.Medium);
		}

		private void newHardPuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.New(Difficulty.Hard);
		}

		private void newExtremePuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.New(Difficulty.Extreme);
		}

		private void loadPuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.Load();
		}

		private void savePuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.Save();
		}

		private void savePuzzleAsMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.Save(saveAs: true);
		}

		private void startGameMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.SetReady();
		}

		private void solvePuzzleMenuItem_Click(object sender, EventArgs e)
		{
			puzzle.Solve();
		}

		private void exitMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
