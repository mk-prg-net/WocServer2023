using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MKPRG.Tracing;
using MKPRG.Naming;

namespace LngUIDGenerator
{
    public partial class LngUidGeneratorFrm : Form
    {
        public LngUidGeneratorFrm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SessionIdGenerator generator;

        private void LngUidGeneratorFrm_Load(object sender, EventArgs e)
        {
            Text = $"{Glyphs.toStr(Glyphs.DataAndDocuments.Key)} Long Uinique Identifier Generator ";

            generator = new SessionIdGenerator();

            GenNextUID();

        }

        private void GenNextUID()
        {
            var uid = generator.Get(Environment.UserName);
            tbxUIDasDEC.Text = uid.ToString();
            tbxUIDasHEX.Text = uid.ToString("X");
            tbxUIDasOct.Text = Convert.ToString(uid, 8);
            tbxUIDasBin.Text = Convert.ToString(uid, 2);
        }

        private void btnNextUID_Click(object sender, EventArgs e)
        {
            GenNextUID();
        }

        private void btnCopyDec_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbxUIDasDEC.Text);
        }

        private void btnCopyHex_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbxUIDasHEX.Text);
        }

        private void btnCopyOct_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbxUIDasOct.Text);
        }

        private void btnCopyBin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbxUIDasBin.Text);
        }
    }
}
