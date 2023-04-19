using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public int color;
        
        public Pawn(int color) : base(color)
        {
            this.color = color;
            Image part = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(part);
            switch (color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (6 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (6 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }
            

            pieceImage = part;
        }
        public override List<Point> PossibleMoves(Cell[,] board)
        {
            List<Point> validSteps = new List<Point>();

            if (board[position.Y - color, position.X].piece == null)
                validSteps.Add(new Point(position.Y - color, position.X));
                if (isFirstStep && board[position.Y - 2*color, position.X].piece == null)
                    validSteps.Add(new Point(position.Y - (2 * color), position.X ));

            if (InRange(position.Y - color, position.X + 1) && board[position.Y - color, position.X + 1].piece != null && board[position.Y - color, position.X + 1].piece.color != color)
                validSteps.Add(new Point(position.Y - color, position.X + 1));
            if (InRange(position.Y - color, position.X - 1) && board[position.Y - color, position.X - 1].piece != null && board[position.Y - color, position.X - 1].piece.color != color)
                validSteps.Add(new Point(position.Y - color, position.X - 1));
            this.validSteps = validSteps;


            return this.validSteps;
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
                if (current.piece.position.Y == 0 || current.piece.position.Y == 7)
                {
                    Application.Run(new fSelectPiece(current));
                    if (color == 1)
                    {
                        Desk.whitePiece.Remove(this);
                        Desk.whitePiece.Add(current.piece);
                    }
                    else if (color == -1)
                    {
                        Desk.blackPiece.Remove(this);
                        Desk.blackPiece.Add(current.piece);
                    }
                }

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
            if (current.piece.position.Y == 0 || current.piece.position.Y == 7)
            {

                fSelectPiece selectPiece = new fSelectPiece(current);
                if(selectPiece.ShowDialog() == DialogResult.OK)
                {
                    current.Image = current.piece.pieceImage;
                    if (color == 1)
                    {
                        Desk.whitePiece.Remove(this);
                        Desk.whitePiece.Add(current.piece);
                    }
                    else if (color == -1)
                    {
                        Desk.blackPiece.Remove(this);
                        Desk.blackPiece.Add(current.piece);
                    }
                }
            }
            isFirstStep = false;
        }
    }
}
