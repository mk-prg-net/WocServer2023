using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MKPRG.MindWriter
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            TopMost = true;            
            WindowState = FormWindowState.Maximized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fullSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = true;            
            WindowState = FormWindowState.Maximized;

        }

        private void leftHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TopMost = true;            
            WindowState = FormWindowState.Normal;

            var screenBounds = Screen.FromControl(this).Bounds;
            Location = screenBounds.Location;
            Size = new Size(screenBounds.Width / 2, screenBounds.Height);


        }

        private void rightHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;

            var screenBounds = Screen.FromControl(this).Bounds;
            Location = new Point(screenBounds.Location.X + screenBounds.Width / 2, screenBounds.Location.Y);
            Size = new Size(screenBounds.Width / 2, screenBounds.Height);

        }
    }
}
