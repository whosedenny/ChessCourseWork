using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Pieces;

namespace Chess
{
    public partial class fSelectPiece : Form
    {

        Cell selectCell;
        int color;
        Point position;

        public fSelectPiece(Cell cell)
        {
            selectCell = cell;
            this.color = selectCell.piece.color;
            this.position = selectCell.piece.position;
            InitializeComponent();
            Image part = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(part);
            switch (selectCell.piece.color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (2 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (2 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }
            btnSelectQueen.Image = part;
            part = new Bitmap(50, 50);
            g = Graphics.FromImage(part);
            switch (selectCell.piece.color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (3 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (3 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }
            btnSelectBishop.Image = part;
            part = new Bitmap(50, 50);
            g = Graphics.FromImage(part);
            switch (selectCell.piece.color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (5 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (5 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }
            btnSelectRook.Image = part;
            part = new Bitmap(50, 50);
            g = Graphics.FromImage(part);
            switch (selectCell.piece.color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (4 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (4 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }
            btnSelectKnight.Image = part;
        }

        private void btnSelectQueen_Click(object sender, EventArgs e)
        {
            selectCell.piece = new Queen(color);
            selectCell.piece.position = position;
            DialogResult = DialogResult.OK;
        }

        private void btnSelectBishop_Click(object sender, EventArgs e)
        {
            selectCell.piece = new Bishop(color);
            selectCell.piece.position = position;
            DialogResult = DialogResult.OK;
        }

        private void btnSelectKnight_Click(object sender, EventArgs e)
        {
            selectCell.piece = new Knight(color);
            selectCell.piece.position = position;
            DialogResult = DialogResult.OK;
        }

        private void btnSelectRook_Click(object sender, EventArgs e)
        {
            selectCell.piece = new Rook(color);
            selectCell.piece.position = position;
            DialogResult = DialogResult.OK;
        }
    }
}
