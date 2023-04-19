using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Chess;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        

        public Knight(int color) : base(color)
        {
            Image part = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(part);
            switch (color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (4 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (4 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }


            pieceImage = part;
        }
        public override List<Point> PossibleMoves(Cell[,] board)
        {
            List<Point> validSteps = new List<Point>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (CheckKnightStep(Math.Abs(i-position.Y),Math.Abs(j-position.X))&& (board[i,j].piece == null || board[i, j].piece.color != color))
                    {
                        validSteps.Add(new Point(i, j));
                    }
                }
            }
            this.validSteps = validSteps;

            return this.validSteps;
        }


        private bool CheckKnightStep(int i, int j)
        {
            if ((i == 2 && j == 1) || (i == 1 && j == 2))
                return true;
            return false;
        }

        public override void ChangePiece(Cell pressed, Cell current, Cell[,] board)
        {

            if (pressed.piece.CanAttack(current))
            {
                if (pressed.piece.color == 1)
                    Desk.blackPiece.Remove(current.piece);
                else if (pressed.piece.color == -1)
                    Desk.whitePiece.Remove(current.piece);
                pressed.piece.Attack(pressed, current);
            }
            else
            {
                Piece temp = pressed.piece;
                pressed.piece = current.piece;
                current.piece = temp;

                pressed.Image = pressed.piece != null ? pressed.piece.pieceImage : null;
                current.Image = current.piece != null ? current.piece.pieceImage : null;
            }
            isFirstStep = false;
        }

        public override void Attack(Cell pressed, Cell current)
        {
            Point attactPiecePosition = current.piece.position;
            current.piece = pressed.piece;
            current.piece.position = attactPiecePosition;
            pressed.piece = null;
            current.Image = current.piece.pieceImage;
            pressed.Image = null;
            isFirstStep = false;
        }
    }
}
