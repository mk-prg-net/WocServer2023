using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DT = MKPRG.Tracing.DocuTerms;

namespace TestWebViewControl
{
    public partial class Form1 : Form
    {
        DT.IComposer pnL = new DT.Composer();

        public Form1()
        {
            InitializeComponent();
        }

        private void webView1_DOMContentLoaded(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlDOMContentLoadedEventArgs e)
        {
            MessageBox.Show("Inhalt geladen");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var htm = new MKPRG.HTML.HTMLDocument(pnL);

            htm.h1.txt("Hallo Welt ").html(MKPRG.Naming.Glyphs.Geographic.Globe).E
                .p.txt("Meine erste Testseite").E
                .build();

            webView1.NavigateToString(htm.CloseDoc());
        }
    }
}
