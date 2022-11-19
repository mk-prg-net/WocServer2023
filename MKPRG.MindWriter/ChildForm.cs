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
        WindowPlacementManager plcTools;

        public ChildForm()
        {
            InitializeComponent();

            plcTools = new WindowPlacementManager(this);
        }

        private void fullSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Full;
        }

        public WindowPlacement MyWindowPlacement
        {
            get => _myWindowPlacement;
            set
            {
                _myWindowPlacement = value;

                plcTools.PlaceWindow(value);
            }
        }

        WindowPlacement _myWindowPlacement;

        private void leftHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Left;
        }

        private void rightHalfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyWindowPlacement = WindowPlacement.Right;
        }
    }
}
