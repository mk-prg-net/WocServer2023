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
    /// Defines all possible windows placements.
    /// </summary>
    public enum WindowPlacementMgr
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

    /// <summary>
    /// mko, 19.11.2022
    /// Manages placements of child windows in an Application.
    /// </summary>
    public class WindowPlacementManager
    {
        Form mainForm;

        /// <summary>
        /// Stores all Child Forms to manage palcements.
        /// </summary>
        Dictionary<int, (int order, ChildForm window)> ChildWindows = new Dictionary<int, (int order, ChildForm window)>();

        /// <summary>
        /// List of all Windows, managed by this App.
        /// </summary>
        public IReadOnlyDictionary<int, (int order, ChildForm window)> WindowsList => ChildWindows;

        public int ChildWindowCount => ChildWindows.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainForm">Main Form of App with Commandline (e.g. for Search Commands) etc.</param>
        public WindowPlacementManager(MainFrm mainForm)
        {
            this.mainForm = mainForm;
        }

        /// <summary>
        /// Returns true, if MainWindow and Child Window on the same Screen.
        /// </summary>
        /// <param name="childFrm"></param>
        /// <returns></returns>
        public bool AreMainAndChildOnSameScreen(ChildForm childFrm)
                => mainForm.Location.X == childFrm.Location.X;

        /// <summary>
        /// Returns true, if on screen where mainWindow is places too a client window is placed.
        /// </summary>
        /// <returns></returns>
        public bool AnyChildOnMainScreen()
            => ChildWindows.Values.Any(r => r.window.Location.X == mainForm.Location.X);

        public (int order, ChildForm window) LastChildOnScreenWhereThisWindowIsPlaced(Form frm)
        {
            (int order, ChildForm window) ret = (-1, null);

            var screenBounds = Screen.FromControl(frm).Bounds;

            var childs = ChildWindows.Values.Where(child => child.window.Location.X >= screenBounds.Location.X 
                                                            && child.window.Location.X < screenBounds.Location.X + screenBounds.Size.Width);

            if (childs.Any())
            {
                ret = childs.OrderByDescending(c => c.order).First();
            }

            return ret;
        }


        public void AddChildWindow(ChildForm frm)
        {
            if (ChildWindows.Any())
            {
                var maxOrder = ChildWindows.Values.Max(r => r.order);

                // var lastChild = ChildWindows.First(r => r.Value.order == maxOrder).Value;
                var lastChild = LastChildOnScreenWhereThisWindowIsPlaced(mainForm);

                ChildWindows[(int)frm.Handle] = (maxOrder + 1, frm);


                if (lastChild.order == -1)
                {
                    // No child found on screen, where frm is placed: frm will be set to fullSize

                    frm.MyWindowPlacement = WindowPlacementMgr.Full;
                }
                else
                {
                    if (lastChild.window.MyWindowPlacement == WindowPlacementMgr.Left)
                    {
                        frm.MyWindowPlacement = WindowPlacementMgr.Right;
                    }
                    else if (lastChild.window.MyWindowPlacement == WindowPlacementMgr.Right)
                    {
                        frm.MyWindowPlacement = WindowPlacementMgr.Left;
                    }
                    else if (lastChild.window.MyWindowPlacement == WindowPlacementMgr.Full && ChildWindows.Count == 2)
                    {
                        lastChild.window.MyWindowPlacement = WindowPlacementMgr.Left;
                        frm.MyWindowPlacement = WindowPlacementMgr.Right;
                    }
                    else
                    {
                        frm.MyWindowPlacement = WindowPlacementMgr.Full;
                    }
                }
            }
            else
            {
                ChildWindows[(int)frm.Handle] = (1, frm);
                frm.MyWindowPlacement = WindowPlacementMgr.Full;
            }

        }

        public void RemoveChildWindow(ChildForm frm)
        {
            if (!ChildWindows.ContainsKey((int)frm.Handle))
            {
                throw new ArgumentException("Child Window not managed by WindowPlacementMananger.");
            }
            else
            {
                var entry = ChildWindows[(int)frm.Handle];
                ChildWindows.Remove((int)frm.Handle);
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


        public void PlaceChildWindow(ChildForm childForm, WindowPlacementMgr placement)
        {
            if (!ChildWindows.ContainsKey((int)childForm.Handle))
            {
                throw new ArgumentException("Child Window not managed by WindowPlacementMananger.");
            }
            else
            {
                childForm.WindowState = FormWindowState.Normal;
                var screenBounds = Screen.FromControl(childForm).Bounds;

                bool fullHight = screenBounds.Location.X != mainForm.Location.X;

                (int F, int N, int D) = fullHight ? (0, 1, 1) : (1, 3, 4);

                if (placement == WindowPlacementMgr.Full)
                {
                    childForm.Location = new Point(screenBounds.Location.X, screenBounds.Location.Y + F * screenBounds.Height / 4);
                    childForm.Size = new Size(screenBounds.Width, N * screenBounds.Height / D);
                }
                else if (placement == WindowPlacementMgr.Left)
                {
                    childForm.Location = new Point(screenBounds.Location.X, screenBounds.Location.Y + F * screenBounds.Height / D);
                    childForm.Size = new Size(screenBounds.Width / 2, N * screenBounds.Height / D);
                }
                else if (placement == WindowPlacementMgr.Right)
                {
                    childForm.Location = new Point(screenBounds.Location.X + screenBounds.Width / 2, screenBounds.Location.Y + F * screenBounds.Height / D);
                    childForm.Size = new Size(screenBounds.Width / 2, N * screenBounds.Height / D);
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
        public void PlaceWindow(int windowHandle, WindowPlacementMgr placement)
        {

            var (order, childWindow) = ChildWindows[windowHandle];

            childWindow.WindowState = FormWindowState.Normal;
            var screenBounds = Screen.FromControl(childWindow).Bounds;

            if (placement == WindowPlacementMgr.Full)
            {
                mainForm.TopMost = true;
                mainForm.WindowState = FormWindowState.Maximized;
            }
            else if (placement == WindowPlacementMgr.Left)
            {
                mainForm.Location = screenBounds.Location;
                mainForm.Size = new Size(screenBounds.Width / 2, screenBounds.Height);
            }
            else if (placement == WindowPlacementMgr.Right)
            {
                mainForm.Location = new Point(screenBounds.Location.X + screenBounds.Width / 2, screenBounds.Location.Y);
                mainForm.Size = new Size(screenBounds.Width / 2, screenBounds.Height);
            }

        }
    }
}
