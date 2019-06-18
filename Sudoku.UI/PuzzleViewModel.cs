using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sudoku.Model;
using System.IO;

namespace Sudoku.UI
{
	class PuzzleViewModel
	{
		private Puzzle source;
		private HashSet<Cell> fixedCells = new HashSet<Cell>();
		private UndoManager undoManager = new UndoManager();

		public PuzzleViewModel()
		{
			New();
		}

		public string Name { get; private set; }

		public string FileName { get; private set; }

		public bool IsReady { get; private set; }

		public bool IsEmpty => source == null || source.IsEmpty();

		public bool CanSetReady => !IsReady && !IsEmpty;

		public bool SetReady()
		{
			if (!CanSetReady) return false;
			FixNonEmptyCells();
			undoManager.Clear();
			IsReady = true;
			RaiseStateChanged();
			return true;
		}

		private void ClearSource()
		{
			if (source == null) return;
			source.ContentChanged -= OnSourceContentChanged;
			fixedCells.Clear();
			undoManager.Clear();
		}

		private void SetSource(Puzzle puzzle, string name, string fileName = null)
		{
			ClearSource();
			source = puzzle;
			Name = name;
			FileName = fileName;
			IsReady = !IsEmpty;
			FixNonEmptyCells();
			source.ContentChanged += OnSourceContentChanged;
			RaiseSourceChanged();
			RaiseContentChanged();
			RaiseStateChanged();
		}

		private void FixNonEmptyCells()
		{
			fixedCells.Clear();
			foreach (var cell in source.NonEmptyCells)
				fixedCells.Add(cell);
		}

		private void OnSourceContentChanged(object sender, EventArgs e) => RaiseContentChanged();

		#region Notifications

		public event EventHandler SourceChanged;

		private void RaiseSourceChanged()
		{
			if (SourceChanged != null)
				SourceChanged(this, EventArgs.Empty);
		}

		public event EventHandler ContentChanged;

		private void RaiseContentChanged()
		{
			if (ContentChanged != null)
				ContentChanged(this, EventArgs.Empty);
		}

		public event EventHandler StateChanged;

		private void RaiseStateChanged()
		{
			if (StateChanged != null)
				StateChanged(this, EventArgs.Empty);
		}

		#endregion

		public bool IsCellFixed(int row, int column) => IsCellFixed(source.Cell(row, column));
		public bool IsCellFixed(Cell cell) => fixedCells.Contains(cell);

		public int? GetCellValue(int row, int column) => source.Cell(row, column).Value;

		public bool SetCellValue(int row, int column, int? value) => Execute(new CellEditCommand(source.Cell(row, column), value));

		public bool CanUndo => undoManager.CanUndo;

		public bool CanRedo => undoManager.CanRedo;

		public bool Undo()
		{
			if (!undoManager.Undo()) return false;
			RaiseStateChanged();
			return true;
		}

		public bool Redo()
		{
			if (!undoManager.Redo()) return false;
			RaiseStateChanged();
			return true;
		}

		public void New()
		{
			SetSource(new Puzzle(), GenerateNewName("Untitled"));
		}

		public void New(Difficulty difficulty)
		{
			SetSource(Generator.NewPuzzle(difficulty), GenerateNewName(difficulty.ToString()));
		}

		private Dictionary<string, int> nextNameIndex = new Dictionary<string, int>();

		private string GenerateNewName(string baseName)
		{
			int nameIndex;
			if (!nextNameIndex.TryGetValue(baseName, out nameIndex))
				nameIndex = 1;
			nextNameIndex[baseName] = nameIndex + 1;
			return baseName + nameIndex;
		}

		public bool Load()
		{
			var fileName = GetLoadFileName();
			if (fileName == null) return false;
			Puzzle puzzle;
			try
			{
				var data = File.ReadAllLines(fileName);
				puzzle = Puzzle.FromContent(data);
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return false;
			}
			SetSource(puzzle, Path.GetFileNameWithoutExtension(fileName), fileName);
			return true;
		}

		public bool CanSave => !IsEmpty;

		public bool Save(bool saveAs = false)
		{
			if (!CanSave) return false;
			var fileName = FileName;
			if (!saveAs && fileName != null && File.Exists(fileName))
			{
				var message = string.Format("File '{0}' already exists. Do you wish to overwrite it?", fileName);
				var response = MessageBox.Show(MessageOwner, message, MessageTitle,
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if (response == DialogResult.Cancel) return false;
				if (response == DialogResult.No) saveAs = true;
			}
			if (saveAs || fileName == null)
			{
				fileName = GetSaveFileName();
				if (fileName == null) return false;
			}
			try
			{
				var data = source.ExportContent();
				File.WriteAllText(fileName, data);
			}
			catch (Exception ex)
			{
				ShowErrorMessage(ex.Message);
				return false;
			}
			if (FileName != fileName)
			{
				FileName = fileName;
				Name = Path.GetFileNameWithoutExtension(fileName);
			}
			if (!SetReady())
				RaiseStateChanged();
			return true;
		}

		const string FileFilter = "Sudoku files (*.sdx)|*.sdx|All files (*.*)|*.*";

		private string GetLoadFileName()
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.Filter = FileFilter;
				return dialog.ShowDialog(MessageOwner) == DialogResult.OK ? dialog.FileName : null;
			}
		}

		private string GetSaveFileName()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.Filter = FileFilter;
				dialog.FileName = FileName ?? Name;
				return dialog.ShowDialog(MessageOwner) == DialogResult.OK ? dialog.FileName : null;
			}
		}

		public bool CanSolve => IsReady && !source.IsSolved();

		public bool Solve() => Execute(new SolveCommand(source));

		#region Message services

		const string MessageTitle = "Sudoku";

		public Control MessageOwner { get; set; }

		private void ShowErrorMessage(string text)
		{
			MessageBox.Show(MessageOwner, text, MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void ShowInfoMessage(string text)
		{
			MessageBox.Show(MessageOwner, text, MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		#endregion

		#region Undoable commands

		abstract class UndoableCommand : UndoCommand
		{
			public abstract bool CanExecute(PuzzleViewModel target);
			public abstract bool Execute(PuzzleViewModel target);
		}

		private bool Execute(UndoableCommand command)
		{
			if (!command.CanExecute(this)) return false;
			bool success = false;
			try { success = command.Execute(this); }
			catch (Exception ex) { ShowErrorMessage(ex.Message); }
			if (success)
			{
				undoManager.Add(command);
				RaiseStateChanged();
			}
			return true;
		}

		class CellEditCommand : UndoableCommand
		{
			private readonly Cell cell;
			private readonly int? oldValue;
			private readonly int? newValue;
			public CellEditCommand(Cell target, int? value)
			{
				cell = target;
				oldValue = target.Value;
				newValue = value;
			}
			public override string Description => string.Format("Set Cell[{0},{1}] Value: {2}",
				cell.Row.Index, cell.Column.Index, newValue != null ? newValue.ToString() : "(empty)");
			public override bool CanExecute(PuzzleViewModel target) => oldValue != newValue && (!target.IsReady || !target.IsCellFixed(cell));
			public override bool Execute(PuzzleViewModel context) { cell.Value = newValue; return true; }
			public override void Undo() => cell.Value = oldValue;
			public override void Redo() => cell.Value = newValue;
		}

		class SolveCommand : UndoableCommand
		{
			private readonly Puzzle grid;
			private readonly List<Cell> emptyCells;
			public SolveCommand(Puzzle target)
			{
				grid = target;
				emptyCells = target.EmptyCells.ToList();
			}
			public override string Description => "Solve Puzzle";
			public override bool CanExecute(PuzzleViewModel target) => emptyCells.Count > 0;
			public override bool Execute(PuzzleViewModel target)
			{
				if (grid.Solve()) return true;
				target.ShowInfoMessage("No Solution Found.");
				return false;
			}
			public override void Undo()
			{
				grid.BeginUpdate();
				foreach (var cell in emptyCells)
					cell.Value = null;
				grid.EndUpdate();
			}
			public override void Redo() => grid.Solve();
		}

		#endregion
	}
}
