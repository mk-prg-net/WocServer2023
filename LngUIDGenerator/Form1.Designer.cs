namespace LngUIDGenerator
{
    partial class LngUidGeneratorFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LngUidGeneratorFrm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLngUIDasDec = new System.Windows.Forms.Label();
            this.lblUIDasHEX = new System.Windows.Forms.Label();
            this.tbxUIDasDEC = new System.Windows.Forms.TextBox();
            this.tbxUIDasHEX = new System.Windows.Forms.TextBox();
            this.btnNextUID = new System.Windows.Forms.Button();
            this.lblUIDasOctal = new System.Windows.Forms.Label();
            this.lblUIDasBin = new System.Windows.Forms.Label();
            this.tbxUIDasOct = new System.Windows.Forms.TextBox();
            this.tbxUIDasBin = new System.Windows.Forms.TextBox();
            this.btnCopyDec = new System.Windows.Forms.Button();
            this.btnCopyHex = new System.Windows.Forms.Button();
            this.btnCopyOct = new System.Windows.Forms.Button();
            this.btnCopyBin = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(822, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblLngUIDasDec, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblUIDasHEX, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbxUIDasDEC, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbxUIDasHEX, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnNextUID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblUIDasOctal, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblUIDasBin, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbxUIDasOct, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbxUIDasBin, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnCopyDec, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCopyHex, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCopyOct, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnCopyBin, 2, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(822, 263);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblLngUIDasDec
            // 
            this.lblLngUIDasDec.AutoSize = true;
            this.lblLngUIDasDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLngUIDasDec.Location = new System.Drawing.Point(3, 50);
            this.lblLngUIDasDec.Name = "lblLngUIDasDec";
            this.lblLngUIDasDec.Size = new System.Drawing.Size(108, 24);
            this.lblLngUIDasDec.TabIndex = 0;
            this.lblLngUIDasDec.Text = "UID as DEC";
            // 
            // lblUIDasHEX
            // 
            this.lblUIDasHEX.AutoSize = true;
            this.lblUIDasHEX.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUIDasHEX.Location = new System.Drawing.Point(3, 90);
            this.lblUIDasHEX.Name = "lblUIDasHEX";
            this.lblUIDasHEX.Size = new System.Drawing.Size(110, 24);
            this.lblUIDasHEX.TabIndex = 1;
            this.lblUIDasHEX.Text = "UID as HEX";
            // 
            // tbxUIDasDEC
            // 
            this.tbxUIDasDEC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tbxUIDasDEC.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUIDasDEC.Location = new System.Drawing.Point(203, 53);
            this.tbxUIDasDEC.Name = "tbxUIDasDEC";
            this.tbxUIDasDEC.Size = new System.Drawing.Size(450, 32);
            this.tbxUIDasDEC.TabIndex = 2;
            this.tbxUIDasDEC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxUIDasHEX
            // 
            this.tbxUIDasHEX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tbxUIDasHEX.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUIDasHEX.Location = new System.Drawing.Point(203, 93);
            this.tbxUIDasHEX.Name = "tbxUIDasHEX";
            this.tbxUIDasHEX.Size = new System.Drawing.Size(450, 32);
            this.tbxUIDasHEX.TabIndex = 3;
            this.tbxUIDasHEX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnNextUID
            // 
            this.btnNextUID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnNextUID.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextUID.Location = new System.Drawing.Point(3, 3);
            this.btnNextUID.Name = "btnNextUID";
            this.btnNextUID.Size = new System.Drawing.Size(157, 32);
            this.btnNextUID.TabIndex = 4;
            this.btnNextUID.Text = "Next >>";
            this.btnNextUID.UseVisualStyleBackColor = false;
            this.btnNextUID.Click += new System.EventHandler(this.btnNextUID_Click);
            // 
            // lblUIDasOctal
            // 
            this.lblUIDasOctal.AutoSize = true;
            this.lblUIDasOctal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUIDasOctal.Location = new System.Drawing.Point(3, 130);
            this.lblUIDasOctal.Name = "lblUIDasOctal";
            this.lblUIDasOctal.Size = new System.Drawing.Size(112, 24);
            this.lblUIDasOctal.TabIndex = 5;
            this.lblUIDasOctal.Text = "UID as Octal";
            // 
            // lblUIDasBin
            // 
            this.lblUIDasBin.AutoSize = true;
            this.lblUIDasBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUIDasBin.Location = new System.Drawing.Point(3, 170);
            this.lblUIDasBin.Name = "lblUIDasBin";
            this.lblUIDasBin.Size = new System.Drawing.Size(121, 24);
            this.lblUIDasBin.TabIndex = 6;
            this.lblUIDasBin.Text = "UID as Binary";
            // 
            // tbxUIDasOct
            // 
            this.tbxUIDasOct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tbxUIDasOct.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUIDasOct.Location = new System.Drawing.Point(203, 133);
            this.tbxUIDasOct.Name = "tbxUIDasOct";
            this.tbxUIDasOct.Size = new System.Drawing.Size(450, 32);
            this.tbxUIDasOct.TabIndex = 7;
            this.tbxUIDasOct.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxUIDasBin
            // 
            this.tbxUIDasBin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tbxUIDasBin.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUIDasBin.Location = new System.Drawing.Point(203, 173);
            this.tbxUIDasBin.Name = "tbxUIDasBin";
            this.tbxUIDasBin.Size = new System.Drawing.Size(450, 32);
            this.tbxUIDasBin.TabIndex = 8;
            this.tbxUIDasBin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCopyDec
            // 
            this.btnCopyDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyDec.Location = new System.Drawing.Point(703, 53);
            this.btnCopyDec.Name = "btnCopyDec";
            this.btnCopyDec.Size = new System.Drawing.Size(75, 32);
            this.btnCopyDec.TabIndex = 9;
            this.btnCopyDec.Text = "Copy";
            this.btnCopyDec.UseVisualStyleBackColor = true;
            this.btnCopyDec.Click += new System.EventHandler(this.btnCopyDec_Click);
            // 
            // btnCopyHex
            // 
            this.btnCopyHex.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyHex.Location = new System.Drawing.Point(703, 93);
            this.btnCopyHex.Name = "btnCopyHex";
            this.btnCopyHex.Size = new System.Drawing.Size(75, 32);
            this.btnCopyHex.TabIndex = 10;
            this.btnCopyHex.Text = "Copy";
            this.btnCopyHex.UseVisualStyleBackColor = true;
            this.btnCopyHex.Click += new System.EventHandler(this.btnCopyHex_Click);
            // 
            // btnCopyOct
            // 
            this.btnCopyOct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyOct.Location = new System.Drawing.Point(703, 133);
            this.btnCopyOct.Name = "btnCopyOct";
            this.btnCopyOct.Size = new System.Drawing.Size(75, 32);
            this.btnCopyOct.TabIndex = 11;
            this.btnCopyOct.Text = "Copy";
            this.btnCopyOct.UseVisualStyleBackColor = true;
            this.btnCopyOct.Click += new System.EventHandler(this.btnCopyOct_Click);
            // 
            // btnCopyBin
            // 
            this.btnCopyBin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyBin.Location = new System.Drawing.Point(703, 173);
            this.btnCopyBin.Name = "btnCopyBin";
            this.btnCopyBin.Size = new System.Drawing.Size(75, 32);
            this.btnCopyBin.TabIndex = 12;
            this.btnCopyBin.Text = "Copy";
            this.btnCopyBin.UseVisualStyleBackColor = true;
            this.btnCopyBin.Click += new System.EventHandler(this.btnCopyBin_Click);
            // 
            // LngUidGeneratorFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 287);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LngUidGeneratorFrm";
            this.Text = "Long UID Generatorn (Martin Korneffel Feb. 2020)";
            this.Load += new System.EventHandler(this.LngUidGeneratorFrm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblLngUIDasDec;
        private System.Windows.Forms.Label lblUIDasHEX;
        private System.Windows.Forms.TextBox tbxUIDasDEC;
        private System.Windows.Forms.TextBox tbxUIDasHEX;
        private System.Windows.Forms.Button btnNextUID;
        private System.Windows.Forms.Label lblUIDasOctal;
        private System.Windows.Forms.Label lblUIDasBin;
        private System.Windows.Forms.TextBox tbxUIDasOct;
        private System.Windows.Forms.TextBox tbxUIDasBin;
        private System.Windows.Forms.Button btnCopyDec;
        private System.Windows.Forms.Button btnCopyHex;
        private System.Windows.Forms.Button btnCopyOct;
        private System.Windows.Forms.Button btnCopyBin;
    }
}

