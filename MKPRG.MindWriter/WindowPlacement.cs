using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace MKPRG.MindWriter
{
    /// <summary>
    /// mko, 15.11.2022
    /// Definiert die möglichen Fensterplatzierungen
    /// </summary>
    public enum WindowPlacement
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


    public class  WindowPlacementManager
    {
        Form mainForm;       


        Dictionary<int, (int order, ChildForm window)> OpenForms = new Dictionary<int, (int order, ChildForm window)>();

        /// <summary>
        /// List of all Windows, managed by this App.
        /// </summary>
        public IReadOnlyDictionary<int, (int order, ChildForm window)> WindowsList => OpenForms;


        public WindowPlacementManager(MainFrm mainForm)
        {
            this.mainForm = mainForm;                       
        }

        public void AddChildWindow(ChildForm frm)
        {
            if (OpenForms.Any())
            {
                var maxOrder = OpenForms.Values.Max(r => r.order);
                OpenForms[(int)frm.Handle] = (maxOrder + 1, frm);

                var lastChild = OpenForms.First(r => r.Value.order == maxOrder).Value;

                if (lastChild.window.MyWindowPlacement == WindowPlacement.Left)
                {
                    frm.MyWindowPlacement = WindowPlacement.Right;
                }
                else if (lastChild.window.MyWindowPlacement == WindowPlacement.Right)
                {
                    frm.MyWindowPlacement = WindowPlacement.Left;
                }
                else 
                {
                    frm.MyWindowPlacement = WindowPlacement.Full;
                }
            }
            else
            {                
                OpenForms[(int)frm.Handle] = (1, frm);
                frm.MyWindowPlacement = WindowPlacement.Full;
            }
            




        }

        public void RemoveChildWindow(ChildForm frm)
        {
            if (!OpenForms.ContainsKey((int)frm.Handle))
            {
                throw new ArgumentException("Child Window not managed by WindowPlacementMananger.");
            }
            else
            {
                var entry = OpenForms[(int)frm.Handle];
                OpenForms.Remove((int)frm.Handle);               
            }
        }

        /// <summary>
        /// mko, 19.11.2022
        /// Teilt Bildschirm zwischen Haupt und Kindfenster auf.
        /// 
        /// Das Hauptfenster wird am oberen Rand angedockt und nimmt zunächst 25% der Bildschirmhöhe ein.
        /// 
        /// Das erste Kindfenster belegt die restliche Fläche.
        /// Das zweite Kindfenster teilt sich mit dem ersten die Fläche horizontal. Das erste Kindfenster ist dann links, und das zweite rechts angedockt.
        /// 
        /// Weitere Kindfenster werden zunächst Bildschirmfüllend unter dem Hauptfenster angedockt. Wenn im Haupffenster in der Fensterliste zwei Fenster ausgewählt
        /// werden, dann teilen diese sich wieder die Bildschirmfläche.
        /// 
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="placement"></param>

        public void PlaceMainWindow()
        {
            mainForm.WindowState = FormWindowState.Normal;
            var screenBounds = Screen.FromControl(mainForm).Bounds;

            mainForm.Location = screenBounds.Location;
            mainForm.Size = new Size(screenBounds.Width, screenBounds.Height / 4);
        }


        public void PlaceChildWindow(ChildForm childForm, WindowPlacement placement)
        {
            if (!OpenForms.ContainsKey((int)childForm.Handle))
            {
                throw new ArgumentException("Child Window not managed by WindowPlacementMananger.");
            }
            else
            {
                childForm.WindowState = FormWindowState.Normal;
                var screenBounds = Screen.FromControl(mainForm).Bounds;                

                if (placement == WindowPlacement.Full)
                {
                    childForm.Location = new Point(screenBounds.Location.X, screenBounds.Location.Y + screenBounds.Height / 4);
                    childForm.Size = new Size(screenBounds.Width, 3 * screenBounds.Height/4);
                }
                else if (placement == WindowPlacement.Left)
                {
                    childForm.Location = new Point(screenBounds.Location.X, screenBounds.Location.Y + screenBounds.Height / 4);
                    childForm.Size = new Size(screenBounds.Width / 2, 3 * screenBounds.Height / 4);
                }
                else if (placement == WindowPlacement.Right)
                {
                    childForm.Location = new Point(screenBounds.Location.X + screenBounds.Width / 2, screenBounds.Location.Y + screenBounds.Height / 4);
                    childForm.Size = new Size(screenBounds.Width / 2, 3 * screenBounds.Height / 4);
                }
            }
        }

        /// <summary>
        /// mko, 19.11.2022
        /// Teilt Bildschirm zwischen Haupt und Kindfenster auf.
        /// 
        /// Das Hauptfenster wird am oberen Rand angedockt und nimmt zunächst 25% der Bildschirmhöhe ein.
        /// 
        /// Das erste Kindfenster belegt die restliche Fläche.
        /// Das zweite Kindfenster teilt sich mit dem ersten die Fläche horizontal. Das erste Kindfenster ist dann links, und das zweite rechts angedockt.
        /// 
        /// Weitere Kindfenster werden zunächst Bildschirmfüllend unter dem Hauptfenster angedockt. Wenn im Haupffenster in der Fensterliste zwei Fenster ausgewählt
        /// werden, dann teilen diese sich wieder die Bildschirmfläche.
        /// 
        /// 
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="placement"></param>
        public void PlaceWindow(int windowHandle, WindowPlacement placement)
        {

            var (order, childWindow) = OpenForms[windowHandle];

            childWindow.WindowState = FormWindowState.Normal;
            var screenBounds = Screen.FromControl(childWindow).Bounds;

            if (placement == WindowPlacement.Full)
            {
                mainForm.TopMost = true;
                mainForm.WindowState = FormWindowState.Maximized;
            }
            else if (placement == WindowPlacement.Left)
            {
                mainForm.Location = screenBounds.Location;
                mainForm.Size = new Size(screenBounds.Width / 2, screenBounds.Height);
            }
            else if (placement == WindowPlacement.Right)
            {
                mainForm.Location = new Point(screenBounds.Location.X + screenBounds.Width / 2, screenBounds.Location.Y);
                mainForm.Size = new Size(screenBounds.Width / 2, screenBounds.Height);
            }

        }
    }
}
