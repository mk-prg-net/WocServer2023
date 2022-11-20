
namespace MKPRG.MindWriter
{
    partial class MainFrm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowPlacementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addChildWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainFormTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.cmdLabel = new System.Windows.Forms.Label();
            this.tbxCmd = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.mainFormTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowPlacementToolStripMenuItem,
            this.addChildWindowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // windowPlacementToolStripMenuItem
            // 
            this.windowPlacementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.placeOnTopToolStripMenuItem,
            this.leftHalfToolStripMenuItem,
            this.rightHalfToolStripMenuItem});
            this.windowPlacementToolStripMenuItem.Name = "windowPlacementToolStripMenuItem";
            this.windowPlacementToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
            this.windowPlacementToolStripMenuItem.Text = "&Placement of Window";
            // 
            // placeOnTopToolStripMenuItem
            // 
            this.placeOnTopToolStripMenuItem.Name = "placeOnTopToolStripMenuItem";
            this.placeOnTopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.placeOnTopToolStripMenuItem.Text = "&Top";
            this.placeOnTopToolStripMenuItem.Click += new System.EventHandler(this.placeTopToolStripMenuItem_Click);
            // 
            // leftHalfToolStripMenuItem
            // 
            this.leftHalfToolStripMenuItem.Name = "leftHalfToolStripMenuItem";
            this.leftHalfToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.leftHalfToolStripMenuItem.Text = "&Left Half";
            this.leftHalfToolStripMenuItem.Click += new System.EventHandler(this.leftHalfToolStripMenuItem_Click);
            // 
            // rightHalfToolStripMenuItem
            // 
            this.rightHalfToolStripMenuItem.Name = "rightHalfToolStripMenuItem";
            this.rightHalfToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rightHalfToolStripMenuItem.Text = "&Right Half";
            this.rightHalfToolStripMenuItem.Click += new System.EventHandler(this.rightHalfToolStripMenuItem_Click);
            // 
            // addChildWindowToolStripMenuItem
            // 
            this.addChildWindowToolStripMenuItem.Name = "addChildWindowToolStripMenuItem";
            this.addChildWindowToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.addChildWindowToolStripMenuItem.Text = "&Add Child Window";
            this.addChildWindowToolStripMenuItem.Click += new System.EventHandler(this.addChildWindowToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainFormTableLayoutPanel
            // 
            this.mainFormTableLayoutPanel.ColumnCount = 3;
            this.mainFormTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.mainFormTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainFormTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainFormTableLayoutPanel.Controls.Add(this.cmdLabel, 0, 0);
            this.mainFormTableLayoutPanel.Controls.Add(this.tbxCmd, 1, 0);
            this.mainFormTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainFormTableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.mainFormTableLayoutPanel.Name = "mainFormTableLayoutPanel";
            this.mainFormTableLayoutPanel.RowCount = 4;
            this.mainFormTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainFormTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainFormTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainFormTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainFormTableLayoutPanel.Size = new System.Drawing.Size(800, 456);
            this.mainFormTableLayoutPanel.TabIndex = 2;
            // 
            // cmdLabel
            // 
            this.cmdLabel.AutoSize = true;
            this.cmdLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdLabel.Location = new System.Drawing.Point(1, 14);
            this.cmdLabel.Name = "cmdLabel";
            this.cmdLabel.Size = new System.Drawing.Size(35, 13);
            this.cmdLabel.TabIndex = 0;
            this.cmdLabel.Text = "CMD";
            this.cmdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxCmd
            // 
            this.tbxCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxCmd.Location = new System.Drawing.Point(55, 16);
            this.tbxCmd.Name = "tbxCmd";
            this.tbxCmd.Size = new System.Drawing.Size(719, 22);
            this.tbxCmd.TabIndex = 1;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 502);
            this.Controls.Add(this.mainFormTableLayoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFrm";
            this.ShowIcon = false;
            this.Text = "Mind Writer ";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainFormTableLayoutPanel.ResumeLayout(false);
            this.mainFormTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowPlacementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem placeOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftHalfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightHalfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addChildWindowToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel mainFormTableLayoutPanel;
        private System.Windows.Forms.Label cmdLabel;
        private System.Windows.Forms.TextBox tbxCmd;
    }
}

