using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public abstract class Piece
    {



        public bool KingIsAttacked;
        public bool isFirstStep;

        public Point position;
        public Point nextPosition;

        public int color;

        public List<Point> validSteps { get; protected set; }

        public Image pieceImage { get; set; }

        public Piece()
        {

        }
        public Piece(int color)
        {
            KingIsAttacked = false;
            isFirstStep = true;
            this.color = color;
            nextPosition = new Point();
            nextPosition.X = position.X;
            nextPosition.Y = position.Y;
            validSteps = new List<Point>();
        }


        public abstract List<Point> PossibleMoves(Cell[,] board);


        public void ShowMoves(Cell pressed,Cell[,] board)
        {
            DeleteDangareMoves(pressed, board);

            foreach (var item in pressed.piece.validSteps)
            {
                board[item.X, item.Y].BackColor = Color.Yellow;
            }

        }


        public void ChangePosition(Cell currentCell)
        {
            this.position.Y = currentCell.Location.Y/50;
            this.position.X = currentCell.Location.X/50;
        }


        public bool IsValidStep(Cell current)
        {
            foreach (var item in validSteps)
            {
                if (item.X == current.Location.Y / 50 && item.Y == current.Location.X / 50)
                {
                    return true;
                }
            }

            return false;
        }

        protected bool InRange(int i, int j)
        {
            if (i >= 0 && i < 8 && j < 8 && j >= 0)
                return true;
            return false;
        }


        protected bool Condition(int i, int j, Cell[,] board)// Check cell contain oponent piece
        {
            return InRange(i, j) && (board[i, j].piece == null || board[i, j].piece.color != color);
        }


        public List<Point> Diagonal(Cell[,] board)
        {
            List<Point> validSteps = new List<Point>();

            int i = position.Y + 1;
            int j = position.X + 1;

            while (Condition(i, j, board))
            {
                if (board[i, j].piece != null && board[i, j].piece.color == color * -1)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));


                i++;
                j++;

            }
            i = position.Y - 1;
            j = position.X + 1;
            while (Condition(i, j, board))
            {

                if (board[i, j].piece != null && board[i, j].piece.color != color)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));
                i--;
                j++;

            }
            i = position.Y + 1;
            j = position.X - 1;
            while (Condition(i, j, board))
            {
                if (board[i, j].piece != null && board[i, j].piece.color == color * -1)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));
                i++;
                j--;

            }
            i = position.Y - 1;
            j = position.X - 1;
            while (Condition(i, j, board))
            {
                if (board[i, j].piece != null && board[i, j].piece.color == color * -1)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));
                i--;
                j--;

            }

            return validSteps;
        }

        public List<Point> HorizontalVertikal(Cell[,] board)
        {
            List<Point> validSteps = new List<Point>();

            int i = position.Y;
            int j = position.X + 1;

            while (Condition(i, j, board))
            {
                if (board[i, j].piece != null && board[i, j].piece.color == color * -1)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));
                j++;

            }
            i = position.Y;
            j = position.X - 1;
            while (Condition(i, j, board))
            {
                if (board[i, j].piece != null && board[i, j].piece.color == color * -1)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));
                j--;

            }
            i = position.Y + 1;
            j = position.X;
            while (Condition(i, j, board))
            {
                if (board[i, j].piece != null && board[i, j].piece.color == color * -1)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));
                i++;

            }
            i = position.Y - 1;
            j = position.X;
            while (Condition(i, j, board))
            {
                if (board[i, j].piece != null && board[i, j].piece.color == color * -1)
                {
                    validSteps.Add(new Point(i, j));
                    break;
                }
                else
                    validSteps.Add(new Point(i, j));
                i--;

            }
            return validSteps;
        }



        public bool CanAttack(Cell currentCell)
        {

            if (currentCell.piece != null && currentCell.piece.color != color)
            {
                foreach (var item in validSteps)
                {
                    if (currentCell.piece.position.Y == item.X && currentCell.piece.position.X == item.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        abstract public void ChangePiece(Cell pressed, Cell current, Cell[,] board);

        abstract public void Attack(Cell pressed, Cell current);


        protected bool KingWillBeAttack(List<Piece> opponentPiece, int i, int j)
        {

            foreach (var item in opponentPiece)
            {
                foreach (var step in item.validSteps)
                {
                    if (step.X == i && step.Y == j)
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        public void DeleteDangareMoves(Cell pressed, Cell[,]board)
        {
            Point[] moves = new Point[pressed.piece.PossibleMoves(board).Count];

            pressed.piece.validSteps.CopyTo(moves);

            foreach (var item in moves)
            {
                Piece[] twoPiece = new Piece[] { pressed.piece, board[item.X, item.Y].piece };
                pressed.piece = null;
                board[item.X, item.Y].piece = twoPiece[0];

                if (color == 1)
                {
                    foreach (var piece in Desk.blackPiece)
                    {
                        if (twoPiece[1] != piece)
                        {
                            foreach (var step in piece.PossibleMoves(board))
                            {
                                if (board[step.X, step.Y].piece == Desk.whiteKing)
                                {
                                    twoPiece[0].validSteps.Remove(item);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (color == -1)
                {
                    foreach (var piece in Desk.whitePiece)
                    {
                        if (twoPiece[1] != piece)
                        {
                            foreach (var step in piece.PossibleMoves(board))
                            {
                                if (board[step.X, step.Y].piece == Desk.blackKing)
                                {
                                    twoPiece[0].validSteps.Remove(item);
                                    break;
                                }
                            }
                        }
                    }
                }

                pressed.piece = twoPiece[0];
                board[item.X, item.Y].piece = twoPiece[1];

            }
        }

    }
}
