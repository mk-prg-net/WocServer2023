
namespace MKPRG.MindWriter
{
    partial class ChildForm
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
            this.menuStripOfChildWindow = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowPlacementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightHalfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripOfChildWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripOfChildWindow
            // 
            this.menuStripOfChildWindow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowPlacementToolStripMenuItem});
            this.menuStripOfChildWindow.Location = new System.Drawing.Point(0, 0);
            this.menuStripOfChildWindow.Name = "menuStripOfChildWindow";
            this.menuStripOfChildWindow.Size = new System.Drawing.Size(742, 24);
            this.menuStripOfChildWindow.TabIndex = 0;
            this.menuStripOfChildWindow.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(742, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStripOfChildWindow";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // windowPlacementToolStripMenuItem
            // 
            this.windowPlacementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullSizeToolStripMenuItem,
            this.leftHalfToolStripMenuItem,
            this.rightHalfToolStripMenuItem});
            this.windowPlacementToolStripMenuItem.Name = "windowPlacementToolStripMenuItem";
            this.windowPlacementToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.windowPlacementToolStripMenuItem.Text = "Window Placement";
            // 
            // leftHalfToolStripMenuItem
            // 
            this.leftHalfToolStripMenuItem.Name = "leftHalfToolStripMenuItem";
            this.leftHalfToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.leftHalfToolStripMenuItem.Text = "&Left Hlaf";
            this.leftHalfToolStripMenuItem.Click += new System.EventHandler(this.leftHalfToolStripMenuItem_Click);
            // 
            // rightHalfToolStripMenuItem
            // 
            this.rightHalfToolStripMenuItem.Name = "rightHalfToolStripMenuItem";
            this.rightHalfToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rightHalfToolStripMenuItem.Text = "&Right Half";
            this.rightHalfToolStripMenuItem.Click += new System.EventHandler(this.rightHalfToolStripMenuItem_Click);
            // 
            // fullSizeToolStripMenuItem
            // 
            this.fullSizeToolStripMenuItem.Name = "fullSizeToolStripMenuItem";
            this.fullSizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fullSizeToolStripMenuItem.Text = "&Full Size";
            this.fullSizeToolStripMenuItem.Click += new System.EventHandler(this.fullSizeToolStripMenuItem_Click);
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 658);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStripOfChildWindow);
            this.MainMenuStrip = this.menuStripOfChildWindow;
            this.Name = "ChildForm";
            this.Text = "ChildForm";
            this.menuStripOfChildWindow.ResumeLayout(false);
            this.menuStripOfChildWindow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripOfChildWindow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowPlacementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftHalfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightHalfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullSizeToolStripMenuItem;
    }
}