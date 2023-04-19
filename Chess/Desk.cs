using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess.Pieces;
using System.Drawing;

namespace Chess
{
    public class Desk
    {

        public Cell[,] _desk;  //Масив клітинок в яких буде міститися фігура

        private const int cellSize = 50;

        public int PlayerColor;
        static public Piece attackedPiece;
        public fMain form;
        static public Piece blackKing;
        static public Piece whiteKing;
        static public List<Piece> whitePiece;
        static public List<Piece> blackPiece;

        private Cell pressedCell;

        public Desk()
        {
            _desk = new Cell[,]{
        { new Cell(new Rook(-1)),new Cell(new Knight(-1)),new Cell(new Bishop(-1)),new Cell(new Queen(-1)),new Cell(new King(-1)),new Cell(new Bishop(-1)),new Cell(new Knight(-1)),new Cell(new Rook(-1))},
        { new Cell(new Pawn(-1)),new Cell(new Pawn(-1)),new Cell(new Pawn(-1)),new Cell(new Pawn(-1)),new Cell(new Pawn(-1)),new Cell(new Pawn(-1)),new Cell(new Pawn(-1)),new Cell(new Pawn(-1))},
        {new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell() },
        { new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell()},
        {new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell() },
        { new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell(),new Cell()},
        { new Cell(new Pawn(1)),new Cell(new Pawn(1)),new Cell(new Pawn(1)),new Cell(new Pawn(1)),new Cell(new Pawn(1)),new Cell(new Pawn(1)),new Cell(new Pawn(1)),new Cell(new Pawn(1))},
        {new Cell(new Rook(1)),new Cell(new Knight(1)), new Cell(new Bishop(1)),new Cell(new Queen(1)),new Cell(new King(1)),new Cell(new Bishop(1)),new Cell(new Knight(1)), new Cell(new Rook(1))}

        };
            PlayerColor = 1;

            blackPiece = new List<Piece>();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < _desk.GetLength(1); j++)
                {
                    blackPiece.Add(_desk[i, j].piece);
                    if (i == 0 && j == 4)
                        blackKing = _desk[i, j].piece;
                }
            }
            whitePiece = new List<Piece>();
            for (int i = 6; i < 8; i++)
            {
                for (int j = 0; j < _desk.GetLength(1); j++)
                {
                    whitePiece.Add(_desk[i, j].piece);
                    if (i == 7 && j == 4)
                        whiteKing = _desk[i, j].piece;
                }
            }

        }


        public void DrawBoard(fMain form)
        {
            this.form = form;
            for (int i = 0; i < _desk.GetLength(0); i++)
            {
                for (int j = 0; j < _desk.GetLength(1); j++)
                {
                    _desk[i, j].Location = new Point(j * cellSize, i * cellSize);
                    _desk[i, j].Size = new Size(cellSize, cellSize);
                    _desk[i, j].Image = _desk[i, j].piece != null ? _desk[i, j].piece.pieceImage : null;
                    _desk[i, j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.SandyBrown;
                    _desk[i, j].Click += new EventHandler(DoStep);
                    if (_desk[i, j].piece != null)
                        _desk[i, j].piece.position = new Point(j, i);
                    form.Controls.Add(_desk[i, j]);
                }
            }
        }

        private void DoStep(object sender, EventArgs e)
        {
            Cell clickCell = sender as Cell;
            if ((clickCell.piece == null || PlayerColor != clickCell.piece.color) && pressedCell == null)
                return;
            if (pressedCell == null)
            {
                pressedCell = clickCell;
                pressedCell.piece.ShowMoves(pressedCell, _desk);
            }
            else
            {

                foreach (var item in pressedCell.piece.validSteps)
                {
                    _desk[item.X, item.Y].BackColor = (item.X + item.Y) % 2 == 0 ? Color.White : Color.SandyBrown;
                }
                
                if (pressedCell.piece.IsValidStep(clickCell))
                {

                    pressedCell.piece.ChangePosition(clickCell);
                    pressedCell.piece.ChangePiece(pressedCell, clickCell, _desk);
                    form.lbGameContinues.Text = "Гра продовжується";

                    if (PlayerColor == 1)
                        Check(blackKing, whitePiece, _desk, PlayerColor);
                    else if (PlayerColor == -1)
                        Check(whiteKing, blackPiece, _desk, PlayerColor);

                    if (clickCell.piece.color == 1 && blackKing.KingIsAttacked)
                    {
                        form.lbGameContinues.Text = "Check White";
                        if (CheckMate(blackPiece, _desk))
                        {
                            form.lbGameContinues.Text = "Mat White";
                        }
                    }
                    else if (clickCell.piece.color == -1 && whiteKing.KingIsAttacked)
                    {
                        form.lbGameContinues.Text = "Check Black";
                        if (CheckMate(blackPiece, _desk))
                        {
                            form.lbGameContinues.Text = "Mat Black";

                        }
                    }
                    else
                    {
                        if (PlayerColor == 1)
                        {
                            Pat(blackPiece, _desk);

                        }
                        if (PlayerColor == -1)
                        {
                            Pat(whitePiece, _desk);
                            
                        }
                    }
                    PlayerColor *= -1;
                    if (PlayerColor == -1)
                        form.lbCurrentPlayer.Text = "Хід чорних";
                    
                    else if(PlayerColor == 1)
                        form.lbCurrentPlayer.Text = "Хід білих";
                }


                pressedCell = null;
                
            }
            
        }

        public void Check(Piece oponentKing, List<Piece> myPieces, Cell[,] board, int color)
        {
            foreach (var item in myPieces)
            {
                foreach (var step in item.PossibleMoves(board))
                {
                    if (color == 1 && step.Y == oponentKing.position.X && step.X == oponentKing.position.Y)
                    {
                        attackedPiece = item;
                        oponentKing.KingIsAttacked = true;
                        return;
                    }
                    if (color == -1 && step.Y == oponentKing.position.X && step.X == oponentKing.position.Y)
                    {
                        attackedPiece = item;
                        oponentKing.KingIsAttacked = true;
                        return;
                    }
                }
            }
            attackedPiece = null;
            oponentKing.KingIsAttacked = false;
            oponentKing.KingIsAttacked = false;
        }

        public bool CheckMate(List<Piece> pieces, Cell[,] board)
        {
            foreach (var item in pieces)
            {
                if (item.PossibleMoves(board).Count > 0)
                {
                    return false;
                }
            }
            return true;
        }


        public void Pat(List<Piece> pieces, Cell[,] board)
        {
            foreach (var item in pieces)
            {
                if (item.PossibleMoves(board).Count > 0)
                {
                    return;
                }
            }
            MessageBox.Show("На дошці пат тому гра перезапуститься");
            form.Start();
        }



    }
}
