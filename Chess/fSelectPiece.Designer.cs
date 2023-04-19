
namespace Chess
{
    partial class fSelectPiece
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectQueen = new System.Windows.Forms.Button();
            this.btnSelectBishop = new System.Windows.Forms.Button();
            this.btnSelectKnight = new System.Windows.Forms.Button();
            this.btnSelectRook = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectQueen
            // 
            this.btnSelectQueen.Location = new System.Drawing.Point(12, 24);
            this.btnSelectQueen.Name = "btnSelectQueen";
            this.btnSelectQueen.Size = new System.Drawing.Size(119, 109);
            this.btnSelectQueen.TabIndex = 0;
            this.btnSelectQueen.UseVisualStyleBackColor = true;
            this.btnSelectQueen.Click += new System.EventHandler(this.btnSelectQueen_Click);
            // 
            // btnSelectBishop
            // 
            this.btnSelectBishop.Location = new System.Drawing.Point(137, 24);
            this.btnSelectBishop.Name = "btnSelectBishop";
            this.btnSelectBishop.Size = new System.Drawing.Size(119, 109);
            this.btnSelectBishop.TabIndex = 1;
            this.btnSelectBishop.UseVisualStyleBackColor = true;
            this.btnSelectBishop.Click += new System.EventHandler(this.btnSelectBishop_Click);
            // 
            // btnSelectKnight
            // 
            this.btnSelectKnight.Location = new System.Drawing.Point(262, 24);
            this.btnSelectKnight.Name = "btnSelectKnight";
            this.btnSelectKnight.Size = new System.Drawing.Size(119, 109);
            this.btnSelectKnight.TabIndex = 2;
            this.btnSelectKnight.UseVisualStyleBackColor = true;
            this.btnSelectKnight.Click += new System.EventHandler(this.btnSelectKnight_Click);
            // 
            // btnSelectRook
            // 
            this.btnSelectRook.Location = new System.Drawing.Point(387, 24);
            this.btnSelectRook.Name = "btnSelectRook";
            this.btnSelectRook.Size = new System.Drawing.Size(119, 109);
            this.btnSelectRook.TabIndex = 3;
            this.btnSelectRook.UseVisualStyleBackColor = true;
            this.btnSelectRook.Click += new System.EventHandler(this.btnSelectRook_Click);
            // 
            // fSelectPiece
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 159);
            this.Controls.Add(this.btnSelectRook);
            this.Controls.Add(this.btnSelectKnight);
            this.Controls.Add(this.btnSelectBishop);
            this.Controls.Add(this.btnSelectQueen);
            this.Name = "fSelectPiece";
            this.Text = "fSelectPiece";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectQueen;
        private System.Windows.Forms.Button btnSelectBishop;
        private System.Windows.Forms.Button btnSelectKnight;
        private System.Windows.Forms.Button btnSelectRook;
    }
}