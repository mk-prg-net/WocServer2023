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

        enum WindowPlacement
        {
            Full,
            Top,
            TopLeft,
            TopRight,
            Bottom,
            BottomLeft,
            BottomRight,
            Left,
            Right           
        }

        WindowPlacement MyWindowPlacement 
        { 
            get => _myWindowPlacement; 
            set
            {
                _myWindowPlacement = value;

                WindowState = FormWindowState.Normal;
                var screenBounds = Screen.FromControl(this).Bounds;
                
                if(_myWindowPlacement == WindowPlacement.Full)
                {
                    TopMost = true;
                    WindowState = FormWindowState.Maximized;
                }
                else if(_myWindowPlacement == WindowPlacement.Left)
                {
                    Location = screenBounds.Location;
                    Size = new Size(screenBounds.Width / 2, screenBounds.Height);
                }
                else if(_myWindowPlacement == WindowPlacement.Right)
                {
                    Location = new Point(screenBounds.Location.X + screenBounds.Width / 2, screenBounds.Location.Y);
                    Size = new Size(screenBounds.Width / 2, screenBounds.Height);
                }
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

        MainFrm secondForm;

        private void add2WindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (secondForm == null)
            {
                secondForm = new MainFrm();
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
