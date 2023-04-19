using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class fMain : Form
    {
        public Label lbCurrentPlayer = new Label();
        public Label lbGameContinues = new Label();
        public void Start()
        {
            if (board != null)
            {
                foreach (var item in board._desk)
                {
                    this.Controls.Remove(item);
                }
            }
            board = new Desk();
            board.DrawBoard(this);
        }

        public Desk board;
        public fMain()
        {
            InitializeComponent();
            this.gbState.Controls.Add(this.lbCurrentPlayer);
            this.lbCurrentPlayer.AutoSize = true;
            this.lbCurrentPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCurrentPlayer.Location = new System.Drawing.Point(25, 65);
            this.lbCurrentPlayer.Name = "lbCurrentPlayer";
            this.lbCurrentPlayer.Size = new System.Drawing.Size(96, 25);
            this.lbCurrentPlayer.TabIndex = 0;
            this.lbCurrentPlayer.Text = "Хід Білих";


            this.gbState.Controls.Add(this.lbGameContinues);
            this.lbGameContinues.AutoSize = true;
            this.lbGameContinues.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbGameContinues.Location = new System.Drawing.Point(25, 130);
            this.lbGameContinues.Name = "lbGemeContinues";
            this.lbGameContinues.Size = new System.Drawing.Size(114, 25);
            this.lbGameContinues.TabIndex = 1;
            this.lbGameContinues.Text = "Гра триває";

            Start();

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Start();
        }
    }
}
