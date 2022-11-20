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


        private void fullSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacementMgr.Full;
        }

        public WindowPlacementMgr MyWindowPlacement
        {
            get => _myWindowPlacement;
            set
            {
                _myWindowPlacement = value;

                plcMgr.PlaceChildWindow(this, value);
                //plcMgr.PlaceWindow((int)this.Handle, value);
            }
        }

        WindowPlacementMgr _myWindowPlacement;

        private void leftHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacementMgr.Left;
        }

        private void rightHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacementMgr.Right;
        }

    }
}
