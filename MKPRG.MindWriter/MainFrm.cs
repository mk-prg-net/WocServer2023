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
        WindowPlacementManager plcTools;

        public MainFrm()
        {
            InitializeComponent();

            plcTools = new WindowPlacementManager(this);
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            TopMost = true;            
            WindowState = FormWindowState.Maximized;
            Text = "Main Window";            
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

                plcTools.PlaceWindow(value);
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

        ChildForm secondForm;

        private void add2WindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (secondForm == null)
            {
                secondForm = new ChildForm();
                secondForm.Text = "2. Fenster";
                secondForm.Show();
                secondForm.FormClosed += SecondForm_FormClosed;
                
                if(MyWindowPlacement == WindowPlacement.Left)
                {
                    secondForm.MyWindowPlacement = WindowPlacement.Right;
                }
                else if(MyWindowPlacement == WindowPlacement.Right)
                {
                    secondForm.MyWindowPlacement = WindowPlacement.Left;
                }
                else
                {
                    secondForm.MyWindowPlacement = WindowPlacement.Full;
                }
            }
        }

        private void SecondForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            secondForm = null;
        }
    }
}
