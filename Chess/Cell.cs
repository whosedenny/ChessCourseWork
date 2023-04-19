using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public class Cell : Button
    {
        public Piece piece;

        public Piece posPiece;

        public Cell(Piece piece = null)
        {
            this.piece = piece;
        }



        public static Cell Copy(ref Cell cell)
        {
            return cell;
        }

    }
}
