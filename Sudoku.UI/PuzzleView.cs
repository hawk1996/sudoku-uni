using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.UI
{
	class PuzzleView : Control
	{
		const int CellSize = 32;

		private Color CellBackColor = Color.White;
		private Color CellBorderColor = Color.Black;
		private Color FixedCellForeColor = Color.Black;
		private Color UserCellForeColor = Color.Blue;
		private Color SelectedCellBorderColor = Color.Red;

		private PuzzleViewModel source;

		public PuzzleView()
		{
			DoubleBuffered = true;
			ResizeRedraw = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				UnsubscribeFromSource();
			base.Dispose(disposing);
		}

		private void SubscribeToSource()
		{
			if (source != null)
				source.ContentChanged += OnSourceContentChanged;
		}

		private void UnsubscribeFromSource()
		{
			if (source != null)
				source.ContentChanged -= OnSourceContentChanged;
		}

		private void OnSourceContentChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		internal void SetSource(PuzzleViewModel value)
		{
			if (source == value) return;
			UnsubscribeFromSource();
			source = value;
			SubscribeToSource();
			Invalidate();
		}

		private int selectedRow, selectedCol;

		private bool SelectCell(int row, int col)
		{
			if (selectedRow == row && selectedCol == col) return false;
			selectedRow = row;
			selectedCol = col;
			Invalidate();
			return true;
		}

		private bool SelectNextCell(int rowAdvance, int colAdvance)
		{
			int row = selectedRow + rowAdvance;
			int col = selectedCol + colAdvance;
			if (col < 0) { col = 8; row--; }
			else if (col > 8) { col = 0; row++; }
			if (row < 0) row = 8;
			else if (row > 8) row = 0;
			return SelectCell(row, col);
		}

		private bool SetSelectedValue(int? value) => source.SetCellValue(selectedRow, selectedCol, value);

		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Left: return SelectNextCell(0, -1);
				case Keys.Right: return SelectNextCell(0, 1);
				case Keys.Up: return SelectNextCell(-1, 0);
				case Keys.Down: return SelectNextCell(1, 0);
				case Keys.Home: return SelectCell(selectedRow, 0);
				case Keys.End: return SelectCell(selectedRow, 8);
				case Keys.Control | Keys.Down: return SelectCell(8, selectedCol);
				case Keys.Control | Keys.End: return SelectCell(8, 8);
				case Keys.Control | Keys.Home: return SelectCell(0, 0);
				case Keys.Control | Keys.Up: return SelectCell(0, selectedCol);
			}
			return base.ProcessDialogKey(keyData);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if ((e.KeyData == Keys.Delete || e.KeyData == Keys.Back) && SetSelectedValue(null))
			{
				e.Handled = e.SuppressKeyPress = true;
				return;
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if ((e.KeyChar >= '1' && e.KeyChar <= '9') && SetSelectedValue(e.KeyChar - '0'))
			{
				e.Handled = true;
				return;
			}
			base.OnKeyPress(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				int row, col;
				if (!HitTest(e.Location, out row, out col)) return;
				if (!Focused && !Focus()) return;
				SelectCell(row, col);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			// Setup drawing area
			var viewRect = GetViewRect();
			if (viewRect.X != 0 || viewRect.Y != 0)
			{
				// Set the graphics transform and reset the view rectangle,
				// so the whole drawing is based on (0,0) point
				e.Graphics.TranslateTransform(viewRect.X, viewRect.Y);
				viewRect.X = viewRect.Y = 0;
			}
			// Draw background
			using (var brush = new SolidBrush(CellBackColor))
				e.Graphics.FillRectangle(brush, viewRect);
			// Draw cells
			if (source != null)
			{
				using (var userBrush = new SolidBrush(UserCellForeColor))
				using (var fixedBrush = new SolidBrush(FixedCellForeColor))
				using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
				{
					var font = this.Font;
					for (int row = 0; row < 9; row++)
					{
						for (int col = 0; col < 9; col++)
						{
							var value = source.GetCellValue(row, col);
							if (value == null) continue;
							var text = value.ToString();
							var brush = !source.IsReady || source.IsCellFixed(row, col) ? fixedBrush : userBrush;
							var rect = GetBaseCellRect(row, col);
							e.Graphics.DrawString(text, font, brush, rect, format);
						}
					}
				}
			}
			// Draw grid
			using (var thinPen = new Pen(CellBorderColor, 1))
			using (var thickPen = new Pen(CellBorderColor, 3))
			{
				// Draw grid lines
				for (int i = 1; i < 9; i++)
				{
					var pen = (i % 3) == 0 ? thickPen : thinPen;
					int pos = i * CellSize;
					// Draw horizontal line at x = pos
					e.Graphics.DrawLine(pen, 0, pos, viewRect.Right, pos);
					// Draw vertical line at y = pos
					e.Graphics.DrawLine(pen, pos, 0, pos, viewRect.Bottom);
				}
				// Draw grid border
				e.Graphics.DrawRectangle(thickPen, viewRect);
			}
			// Draw selection rectangle
			using (var pen = new Pen(SelectedCellBorderColor, 2))
			{
				var cellRect = GetBaseCellRect(selectedRow, selectedCol);
				cellRect.Inflate(-1, -1);
				e.Graphics.DrawRectangle(pen, cellRect);
			}
		}

		private bool HitTest(Point pt, out int row, out int col)
		{
			row = col = -1;
			var viewRect = GetViewRect();
			if (!viewRect.Contains(pt)) return false;
			row = (pt.Y - viewRect.Y) / CellSize;
			col = (pt.X - viewRect.X) / CellSize;
			return true;
		}

		private Rectangle GetViewRect()
		{
			var viewRect = GetBaseViewRect();
			var displayRect = ClientRectangle;
			// Center the view rectangle inside the display rectangle
			viewRect.X = Math.Max((displayRect.Width - viewRect.Width) / 2, 0);
			viewRect.Y = Math.Max((displayRect.Height - viewRect.Height) / 2, 0);
			return viewRect;
		}

		static Rectangle GetBaseViewRect() => new Rectangle(0, 0, 9 * CellSize, 9 * CellSize);

		static Rectangle GetBaseCellRect(int row, int col) => new Rectangle(col * CellSize, row * CellSize, CellSize, CellSize);
	}
}
