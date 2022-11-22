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
        public bool AreMainAndChildOnSameScreen(ChildForm child)
        {
            var screenBounds = Screen.FromControl(mainForm).Bounds;

            return child.Location.X >= screenBounds.Location.X && child.Location.X < screenBounds.Location.X + screenBounds.Size.Width;
        }

        /// <summary>
        /// Returns true, if on screen where mainWindow is places also a client window is placed.
        /// </summary>
        /// <returns></returns>
        public bool AnyChildOnMainScreen()
            => ChildWindows.Values.Any(child => AreMainAndChildOnSameScreen(child.window));

        /// <summary>
        /// returns alls childs, placed on screen where frm is placed.
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        public (bool childFound, IEnumerable<(int order, ChildForm window)> childs) AllChildsOnScreenWhereThisWindowIsPlaced(Form frm)
        {
            (bool childFound, IEnumerable<(int order, ChildForm window)> childs) ret = (false, new (int order, ChildForm window)[] { });

            var screenBounds = Screen.FromControl(frm).Bounds;

            var _childs = ChildWindows.Values.Where(child => child.window.Location.X >= screenBounds.Location.X
                                                            && child.window.Location.X < screenBounds.Location.X + screenBounds.Size.Width);

            if (_childs.Any())
            {
                ret = (true, _childs);
            }

            return ret;
        }

        public (bool childFound, IEnumerable<(int order, ChildForm window)> childs) AllChildsNotOnScreenWhereThisWindowIsPlaced(Form frm)
        {
            (bool childFound, IEnumerable<(int order, ChildForm window)> childs) ret = (false, new (int order, ChildForm window)[] { });

            var screenBounds = Screen.FromControl(frm).Bounds;

            var _childs = ChildWindows.Values.Where(child => child.window.Location.X < screenBounds.Location.X
                                                            || child.window.Location.X >= screenBounds.Location.X + screenBounds.Size.Width);

            if (_childs.Any())
            {
                ret = (true, _childs);
            }

            return ret;
        }



        /// <summary>
        /// If on screen where given Form is placed also a childWindow is placed, then it returns true + child with 
        /// highest order on this screen.
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        public (bool childFound, (int order, ChildForm window) child) LastChildOnScreenWhereThisWindowIsPlaced(Form frm)
        {
            (bool childFound, (int order, ChildForm window)) ret = (false, (-1, null));

            var getChilds = AllChildsOnScreenWhereThisWindowIsPlaced(frm);

            if (getChilds.childFound)
            {
                ret = (true, getChilds.childs.OrderByDescending(c => c.order).First());
            }

            return ret;
        }

        /// <summary>
        ///  Add a child window and places it below main Window
        /// </summary>
        /// <param name="frm"></param>
        public void AddChildWindow(ChildForm frm)
        {
            if (ChildWindows.Any())
            {
                var getLastChild = LastChildOnScreenWhereThisWindowIsPlaced(mainForm);

                var maxOrder = ChildWindows.Values.Max(r => r.order);
                ChildWindows[(int)frm.Handle] = (maxOrder + 1, frm);

                if (!getLastChild.childFound)
                {
                    // new child is the first below main Window
                    PlaceChildWindowsBelowMainWindow(frm, WindowPlacement.Full);
                }
                else
                {
                    var lastChild = getLastChild.child.window;

                    if (lastChild.MyWindowPlacement == WindowPlacement.Full)
                    {
                        PlaceChildWindowsBelowMainWindow(lastChild, WindowPlacement.Left);
                        PlaceChildWindowsBelowMainWindow(frm, WindowPlacement.Right);
                    }
                    else if (lastChild.MyWindowPlacement == WindowPlacement.Left)
                    {
                        PlaceChildWindowsBelowMainWindow(frm, WindowPlacement.Right);
                    }
                    else if (lastChild.MyWindowPlacement == WindowPlacement.Right)
                    {
                        PlaceChildWindowsBelowMainWindow(frm, WindowPlacement.Left);
                    }
                }
            }
            else
            {
                ChildWindows[(int)frm.Handle] = (1, frm);
                PlaceChildWindowsBelowMainWindow(frm, WindowPlacement.Full);
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

            // Alle Kindfenster auf diesem Bildschirm unterhalb des Hauptfensters anordnen

            var getChilds = AllChildsOnScreenWhereThisWindowIsPlaced(mainForm);

            if (getChilds.childFound)
            {
                if (getChilds.childs.Count() == 1)
                {
                    PlaceChildWindowsBelowMainWindow(getChilds.childs.First().window, WindowPlacement.Full, screenBounds);
                }
                else
                {
                    bool left = true;
                    foreach (var child in getChilds.childs.OrderBy(c => c.order).Select(c => c.window))
                    {
                        if (left)
                        {
                            PlaceChildWindowsBelowMainWindow(child, WindowPlacement.Left, screenBounds);
                            left = false;
                        }
                        else
                        {
                            PlaceChildWindowsBelowMainWindow(child, WindowPlacement.Right, screenBounds);
                            left = true;
                        }

                    }
                }
            }


            var getOtherChilds = AllChildsNotOnScreenWhereThisWindowIsPlaced(mainForm);

            if (getOtherChilds.childFound)
            {
                foreach (var child in getOtherChilds.childs.OrderBy(c => c.order).Select(c => c.window))
                {
                    PlaceChildWindows(child, child.MyWindowPlacement);
                }
            }
        }

        public void PlaceChildWindowsBelowMainWindow(ChildForm childForm, WindowPlacement placement)
        {
            var screenBounds = Screen.FromControl(mainForm).Bounds;
            PlaceChildWindowsBelowMainWindow(childForm, placement, screenBounds);
        }

        /// <summary>
        /// Place a childwindow below a main window.
        /// </summary>
        /// <param name="childForm"></param>
        /// <param name="placement"></param>
        /// <param name="screenBounds"></param>
        void PlaceChildWindowsBelowMainWindow(ChildForm childForm, WindowPlacement placement, Rectangle screenBounds)
        {
            childForm.MyWindowPlacement = placement;

            if (placement == WindowPlacement.Full)
            {
                childForm.Location = new Point(screenBounds.Location.X, screenBounds.Location.Y + screenBounds.Height / 4);
                childForm.Size = new Size(screenBounds.Width, 3 * screenBounds.Height / 4);
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


        public void PlaceChildWindows(ChildForm childForm, WindowPlacement placement)
        {
            var screenBounds = Screen.FromControl(childForm).Bounds;
            PlaceChildWindows(childForm, placement, screenBounds);
        }


        void PlaceChildWindows(ChildForm childForm, WindowPlacement placement, Rectangle screenBounds)
        {
            childForm.MyWindowPlacement = placement;

            if (placement == WindowPlacement.Full)
            {
                childForm.Location = new Point(screenBounds.Location.X, screenBounds.Location.Y);
                childForm.Size = new Size(screenBounds.Width, screenBounds.Height);
            }
            else if (placement == WindowPlacement.Left)
            {
                childForm.Location = new Point(screenBounds.Location.X, screenBounds.Location.Y);
                childForm.Size = new Size(screenBounds.Width / 2, screenBounds.Height);
            }
            else if (placement == WindowPlacement.Right)
            {
                childForm.Location = new Point(screenBounds.Location.X + screenBounds.Width / 2, screenBounds.Location.Y);
                childForm.Size = new Size(screenBounds.Width / 2, screenBounds.Height);
            }
        }
    }
}
