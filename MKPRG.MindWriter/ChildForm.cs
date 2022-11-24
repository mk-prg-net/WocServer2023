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
    public partial class ChildForm : Form
    {
        WindowPlacementManager plcMgr;

        public ChildForm(WindowPlacementManager plcMgr)
        {
            InitializeComponent();

            this.plcMgr = plcMgr;
        }

        private void ChildForm_Load(object sender, EventArgs e)
        {
            plcMgr.AddChildWindow(this);
        }


        /// <summary>
        /// Extends Window to Full Size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fullSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Full;
            PlaceMeNow();
        }

        /// <summary>
        /// Stores current valid placement
        /// </summary>
        public WindowPlacement MyWindowPlacement { get; set; }



        private void leftHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Left;

            PlaceMeNow();
        }

        /// <summary>
        /// Resizes and moves to requested size and position.
        /// </summary>
        private void PlaceMeNow()
        {
            if (plcMgr.AreMainAndChildOnSameScreen(this))
            {
                plcMgr.PlaceChildWindowsBelowMainWindow(this, MyWindowPlacement);
            }
            else
            {
                plcMgr.PlaceChildWindows(this, MyWindowPlacement);
            }
        }

        private void rightHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Right;

            PlaceMeNow();
        }
    }
}
