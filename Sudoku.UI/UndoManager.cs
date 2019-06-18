using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UI
{
	abstract class UndoCommand
	{
		public abstract string Description { get; }
		public abstract void Undo();
		public abstract void Redo();
	}

	class UndoManager
	{
		private Stack<UndoCommand> undoCommands = new Stack<UndoCommand>();
		private Stack<UndoCommand> redoCommands = new Stack<UndoCommand>();

		public bool Clear()
		{
			if (!CanUndo && !CanRedo) return false;
			undoCommands.Clear();
			redoCommands.Clear();
			return true;
		}

		public void Add(UndoCommand command)
		{
			undoCommands.Push(command);
			redoCommands.Clear();
		}

		public bool CanUndo => undoCommands.Count > 0;

		public bool Undo() => Do(undoCommands, redoCommands, command => command.Undo());

		public bool CanRedo => redoCommands.Count > 0;

		public bool Redo() => Do(redoCommands, undoCommands, command => command.Redo());

		private bool Do(Stack<UndoCommand> from, Stack<UndoCommand> to, Action<UndoCommand> action)
		{
			if (from.Count == 0) return false;
			var command = from.Pop();
			to.Push(command);
			action(command);
			return true;
		}
	}
}
