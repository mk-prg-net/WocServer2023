
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
            this.fullSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRemoveWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.add2WindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowPlacementToolStripMenuItem,
            this.addRemoveWindowToolStripMenuItem});
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
            this.fullSizeToolStripMenuItem,
            this.leftHalfToolStripMenuItem,
            this.rightHalfToolStripMenuItem});
            this.windowPlacementToolStripMenuItem.Name = "windowPlacementToolStripMenuItem";
            this.windowPlacementToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.windowPlacementToolStripMenuItem.Text = "&Window Size";
            // 
            // fullSizeToolStripMenuItem
            // 
            this.fullSizeToolStripMenuItem.Name = "fullSizeToolStripMenuItem";
            this.fullSizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fullSizeToolStripMenuItem.Text = "&Full Size";
            this.fullSizeToolStripMenuItem.Click += new System.EventHandler(this.fullSizeToolStripMenuItem_Click);
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
            // addRemoveWindowToolStripMenuItem
            // 
            this.addRemoveWindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.add2WindowToolStripMenuItem});
            this.addRemoveWindowToolStripMenuItem.Name = "addRemoveWindowToolStripMenuItem";
            this.addRemoveWindowToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
            this.addRemoveWindowToolStripMenuItem.Text = "Add/Remove Window";
            // 
            // add2WindowToolStripMenuItem
            // 
            this.add2WindowToolStripMenuItem.Name = "add2WindowToolStripMenuItem";
            this.add2WindowToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.add2WindowToolStripMenuItem.Text = "Add 2. Window";
            this.add2WindowToolStripMenuItem.Click += new System.EventHandler(this.add2WindowToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 502);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowPlacementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftHalfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightHalfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRemoveWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem add2WindowToolStripMenuItem;
    }
}

