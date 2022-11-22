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
    /// <summary>
    /// mko, 20.11.2022
    /// Main Window, servers Commandlines, Windows- Managment etc.
    /// </summary>
    public partial class MainFrm : Form
    {
        WindowPlacementManager plcMgr;

        public MainFrm()
        {
            InitializeComponent();

            plcMgr = new WindowPlacementManager(this);
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            TopMost = true;            
            WindowState = FormWindowState.Maximized;
            Text = "Main Window";

            plcMgr.PlaceMainWindow();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void placeTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plcMgr.PlaceMainWindow();
        }      

        private void SecondForm_FormClosed(object sender, FormClosedEventArgs e)
        {      
            if(sender is ChildForm child)
            {
                plcMgr.RemoveChildWindow(child);
            }

        }

        private void addChildWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var childWnd = new ChildForm(plcMgr);
            childWnd.Location = this.Location;
            childWnd.Text = $"{plcMgr.ChildWindowCount} Fenster";
            childWnd.Show();
            childWnd.FormClosed += SecondForm_FormClosed;
            
        }

        private void MainFrm_LocationChanged(object sender, EventArgs e)
        {
            //if (Location.Y != 0 && !moves)
            //{
            //    plcMgr.PlaceMainWindow();
            //}

            moves = false;
        }

        bool moves = false;

        private void MainFrm_Move(object sender, EventArgs e)
        {
            moves = true;
        }

        /// <summary>
        /// Ensures, that mainForm ist anytime placed well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_ResizeEnd(object sender, EventArgs e)
        {
            plcMgr.PlaceMainWindow();
        }
    }
}
