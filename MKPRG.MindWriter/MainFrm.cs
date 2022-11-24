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

namespace MKPRG.MindWriter
{
    /// <summary>
    /// mko, 20.11.2022
    /// Main Window, servers Commandlines, Windows- Managment etc.
    /// 
    /// Below Main Window can be created Child Windows. Main Window arranges Child Windows aoutomatically. 
    /// If Main Window is moved to an other screen, then all child windows are created on Screen to wich Main Windows was moved.
    /// </summary>
    public partial class MainFrm : Form
    {
        WindowPlacementManager plcMgr;

        DT.Composer pnL;

        public MainFrm()
        {
            InitializeComponent();

            plcMgr = new WindowPlacementManager(this);
        }

        private async void MainFrm_Load(object sender, EventArgs e)
        {
            TopMost = true;            
            WindowState = FormWindowState.Maximized;           

            plcMgr.PlaceMainWindow();
            pnL = new DT.Composer();

            await mainWindowWebView2.EnsureCoreWebView2Async();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            childWnd.Text = $"🜶 {plcMgr.ChildWindowCount}";
            childWnd.Show();
            childWnd.FormClosed += SecondForm_FormClosed;
            
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

        private void mainWindowWebView2_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {

                var htm = new MKPRG.HTML.HTMLDocument(pnL);

                htm.h1.txt("Hallo Welt ").html(MKPRG.Naming.Glyphs.Geographic.Globe).E
                    .p.txt("Meine erste Testseite").E
                    .build();


                mainWindowWebView2.NavigateToString(htm.CloseDoc());
            }
            else
            {
                MessageBox.Show($"Laden der WevView2 ist fehlgeschlagen: {e.InitializationException.Message}");
            }
        }
    }
}
