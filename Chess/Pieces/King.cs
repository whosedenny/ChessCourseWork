using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Chess.Pieces
{
    class King : Piece
    {

        private bool _canLongCasting;
        private bool _canShortCasting;

        public King(int color) : base(color)
        {
            Image part = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(part);
            switch (color)
            {
                case -1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (1 % 10 - 1), 150, 150, 150, GraphicsUnit.Pixel);
                    break;
                case 1:
                    g.DrawImage(Properties.Resources.chess, new Rectangle(0, 0, 50, 50), 0 + 150 * (1 % 10 - 1), 0, 150, 150, GraphicsUnit.Pixel);
                    break;

            }


            pieceImage = part;
        }
        public override List<Point> PossibleMoves(Cell[,] board)
        {
            List<Point> validSteps = new List<Point>();

            for (int i = position.Y - 1; i < position.Y + 2; i++)
            {
                for (int j = position.X - 1; j < position.X + 2; j++)
                {
                    if (InRange(i, j) && (board[i, j].piece == null || board[i, j].piece.color != color))
                    {

                        validSteps.Add(new Point(i, j));

                    }
                }
            }
            if (CanLongCasting(board))
            {
                if (color == 1)
                    validSteps.Add(new Point(7, 2));
                if (color == -1)
                    validSteps.Add(new Point(0, 2));
            }
            if (CanShortCasting(board))
            {
                if (color == 1)
                    validSteps.Add(new Point(7, 6));
                if (color == -1)
                    validSteps.Add(new Point(0, 6));
            }

            this.validSteps = validSteps;
            return this.validSteps;
        }

        public void LongCasting(Cell king, Cell rook, Cell[,] board)
        {
            if (color == 1)
            {
                board[7, 2].piece = king.piece;
                king.piece = null;
                board[7, 2].piece.position = new Point(2, 7);
                board[7, 3].piece = rook.piece;
                rook.piece = null;
                board[7, 3].piece.position = new Point(3, 7);
                board[7, 3].Image = board[7, 3].piece.pieceImage;
                board[7, 2].Image = board[7, 2].piece.pieceImage;
                king.Image = null;
                rook.Image = null;

            }
            else if (color == -1)
            {
                board[0, 2].piece = king.piece;
                king.piece = null;
                board[0, 2].piece.position = new Point(2, 0);
                board[0, 3].piece = rook.piece;
                rook.piece = null;
                board[0, 3].piece.position = new Point(3, 0);
                board[0, 3].Image = board[0, 3].piece.pieceImage;
                board[0, 2].Image = board[0, 2].piece.pieceImage;
                king.Image = null;
                rook.Image = null;

            }

        }



        public void ShortCasting(Cell king, Cell rook, Cell[,] board)
        {
            if (color == 1)
            {
                board[7, 6].piece = king.piece;
                king.piece = null;
                board[7, 6].piece.position = new Point(6, 7);
                board[7, 5].piece = rook.piece;
                rook.piece = null;
                board[7, 5].piece.position = new Point(5, 7);
                board[7, 5].Image = board[7, 5].piece.pieceImage;
                board[7, 6].Image = board[7, 6].piece.pieceImage;
                king.Image = null;
                rook.Image = null;

            }
            else if (color == -1)
            {
                board[0, 6].piece = king.piece;
                king.piece = null;
                board[0, 6].piece.position = new Point(6, 0);
                board[0, 5].piece = rook.piece;
                rook.piece = null;
                board[0, 5].piece.position = new Point(5, 0);
                board[0, 5].Image = board[0, 5].piece.pieceImage;
                board[0, 6].Image = board[0, 6].piece.pieceImage;
                king.Image = null;
                rook.Image = null;

            }

        }

        public bool CanLongCasting(Cell[,] board)
        {

            if (isFirstStep && color == 1 && board[7, 7].piece != null && board[7, 7].piece.isFirstStep)
            {
                int i = position.X-1;
                while ( InRange(position.Y,i) && board[position.Y, i].piece == null)
                    i--;
                _canLongCasting = i == 0;
            }
            else if(isFirstStep && color == -1 && board[0, 0].piece != null && board[0, 0].piece.isFirstStep)
            {
                int i = position.X - 1;
                while (board[position.Y, i].piece == null)
                    i--;
                _canLongCasting = i == 0;
            }
            return _canLongCasting;
        }


        public bool CanShortCasting(Cell[,] board)
        {

            if (isFirstStep && color == 1 && board[7, 7].piece != null && board[7, 7].piece.isFirstStep)
            {
                int i = position.X +1;
                while (board[position.Y, i].piece == null)
                    i++;
                _canShortCasting = i == 7;
            }
            else if (isFirstStep && color == -1 && board[0, 7].piece  != null && board[0, 7].piece.isFirstStep)
            {
                int i = position.X + 1;
                while (board[position.Y, i].piece == null)
                    i++;
                _canShortCasting = i == 7;
            }
            return _canShortCasting;
        }


        public override void ChangePiece(Cell pressed, Cell current,Cell[,] board)
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

                if (_canLongCasting && (current == board[0, 2] || current == board[7, 2]))
                {
                    this.LongCasting(pressed, board[current.Location.Y / 50, current.Location.X / 50 - 2], board);
                    _canLongCasting = false;
                }
                else if (_canShortCasting && (current == board[0, 6] || current == board[7, 6]))
                {
                    this.ShortCasting(pressed, board[current.Location.Y / 50, current.Location.X / 50 + 1], board);
                    _canShortCasting = false;
                }
                else
                {
                    Piece temp = pressed.piece;
                    pressed.piece = current.piece;
                    current.piece = temp;

                    pressed.Image = pressed.piece != null ? pressed.piece.pieceImage : null;
                    current.Image = current.piece != null ? current.piece.pieceImage : null;

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
            isFirstStep = false;
        }




    }
}
