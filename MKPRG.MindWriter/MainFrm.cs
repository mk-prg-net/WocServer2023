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

        private void fullSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Full;
        }


        WindowPlacement MyWindowPlacement 
        { 
            get => _myWindowPlacement; 
            set
            {
                _myWindowPlacement = value;

                plcMgr.PlaceWindow((int)this.Handle, value);
            } 
        }

        WindowPlacement _myWindowPlacement = WindowPlacement.Full;

        private void leftHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Left;
        }

        private void rightHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Right;
        }



        private void add2WindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
                var childWnd = new ChildForm(plcMgr);
                childWnd.Text = "2. Fenster";
                childWnd.Show();
                childWnd.FormClosed += SecondForm_FormClosed;
        }

        private void SecondForm_FormClosed(object sender, FormClosedEventArgs e)
        {            
        }
    }
}
