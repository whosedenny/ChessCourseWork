using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;

namespace Chess.Pieces
{
    public class Bishop : Piece
    {

        

 
        

        public Bishop(int color):base(color)
        {
            Image part = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(part);
            switch (color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (3 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (3 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }


            pieceImage = part;
        }

        

        public override List<Point> PossibleMoves(Cell[,] board)
        {
            
            this.validSteps = Diagonal(board);
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
