using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace Overlay
{
    public partial class Overlay : Form
    {
        //APIs
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nInde, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nInde);

        bool selected = true;
        int lastSelected = -1;
        Point oldmousePos = MousePosition;
        int oldlastSelected = -1;
        
        float scrollVal = -3;
        float scroller = -3;
        List<Charm> Charms = new List<Charm>();
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Menu2);

        public Overlay()
        {
            InitializeComponent();
            this.MouseWheel += Overlay_MouseWheel;
        }

        private void Initialize(object sender, EventArgs e)
        {
            this.Opacity = 0.1f;
            this.BackColor = Color.FromArgb(255,1, 1, 1);
            this.TransparencyKey = Color.FromArgb(255, 1, 1, 1);

            this.TopMost = true;
            this.Location = Screen.PrimaryScreen.Bounds.Location;
            this.Size = Screen.PrimaryScreen.Bounds.Size;


            foreach (string folder in Directory.GetDirectories(@"Charms"))
            {
                Charms.Add(new Charm(
                    folder.Substring(9).ToUpper(),
                    folder + @"\\Shortcut",
                    new Bitmap(folder + @"\\Background.png"),
                    new Bitmap(folder + @"\\Foreground.png")
                    ));
            }


            for (int i = 0; i < Charms.Count; i++)
            {
                this.Controls.Add(Charms[i]._title);
                this.Controls.Add(Charms[i]._panel);
                Charms[i].x = (float)Math.Sinh((i + scroller + 0.5) / 4) * 950 + this.Width / 2f - Charms[i]._panel.Width / 2;
                Charms[i].y = ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f);
            }
        }

        private void Update(object sender, EventArgs e)
        {
            oldmousePos = MousePosition;
            if (selected) this.Opacity += (0.8f - this.Opacity) / 15f; else this.Opacity += (0.0f - this.Opacity) / 4f;
            if (this.Opacity < 0.05f)
            {
                player.Dispose(); this.Close();
            }
                
            if (this.Focused == false) selected = false;
            scrollVal += ((float)Math.Round(scrollVal,0) - scrollVal)/2f;
            if (scrollVal > -3) scrollVal = -3;
            if (scrollVal < 3-Charms.Count) scrollVal = 3-Charms.Count;
            scroller += (scrollVal - scroller) / 4f;

            for (int i = 0; i < Charms.Count; i++)
            {
                if (selected)
                {
                    Charms[i].x = (float)Math.Sinh((i + scroller + 0.5) / 4) * 950 + this.Width / 2f - Charms[i]._panel.Width / 2;
                    Charms[i].y = lastSelected == i ?
                        
                        (Charms[i].y -
                        (this.Height / 2.08f - Charms[i]._panel.Height / 2f)
                        ) / 1.1f +
                        (this.Height / 2.08f - Charms[i]._panel.Height / 2f)
                        :

                        (Charms[i].y -
                        (this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ) / 1.2f +
                        (this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ;
                }

                if (!selected)
                {
                    Charms[i].x = (float)Math.Sinh((i + scroller + 0.5) / 4) * 950 + this.Width / 2f - Charms[i]._panel.Width / 2;
                    Charms[i].y = (Charms[i].y -
                        ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ) / 1.04f +
                        ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ;
                }

                Charms[i]._panel.Location = new Point((int)Charms[i].x, (int)Charms[i].y);
                Charms[i]._title.Location = new Point(
                    (int)(Charms[i]._panel.Location.X + Charms[i]._panel.Size.Width/2 - Charms[i]._title.Size.Width / 2f)+1,
                    (int)(Charms[i]._panel.Location.Y + Charms[i]._panel.Size.Height - Charms[i]._title.Size.Height));

                if (i == lastSelected || new Rectangle(Charms[i]._panel.Location.X,
                                    Charms[i]._panel.Location.Y,
                                    Charms[i]._panel.Size.Width,
                                    Charms[i]._panel.Size.Height).Contains(MousePosition))
                {
                    //on Hover
                    if (new Rectangle(Charms[i]._panel.Location.X,
                                    Charms[i]._panel.Location.Y,
                                    Charms[i]._panel.Size.Width,
                                    Charms[i]._panel.Size.Height).Contains(oldmousePos))
                        lastSelected = i;
                    Charms[i]._panel.BackgroundImage = Charms[i]._background;
                    Charms[i]._title.BackColor = Color.FromArgb(60, 60, 60);
                    Charms[i]._panel.MouseUp += _panel_MouseUp;
                }
                else
                {
                    //no Hover
                    Charms[i]._panel.BackgroundImage = Charms[i]._icon;
                    Charms[i]._title.BackColor = Color.FromArgb(40, 40, 40);
                }
                if (oldlastSelected != lastSelected && lastSelected != -1) player.Play();
                oldlastSelected = lastSelected;
            }
        }

        private void _panel_MouseUp(object sender, MouseEventArgs e)
        {
                int i = 0;
                while (i < Charms.Count)
                {
                    if (selected && new Rectangle(Charms[i]._panel.Location.X,
                                        Charms[i]._panel.Location.Y,
                                        Charms[i]._panel.Size.Width,
                                        Charms[i]._panel.Size.Height).Contains(MousePosition))
                    {
                    try { Process.Start(Charms[i]._launch); }
                    catch { }
                    selected = false;
                    i = Charms.Count;
                    }
                    i++;
                }
        }

        private void KeyInput(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up && lastSelected < Charms.Count - 1) { lastSelected++; player.Play(); }
            if (e.KeyData == Keys.Down && lastSelected > 0) { lastSelected--; player.Play(); }
            if (e.KeyData == Keys.Enter) try { Process.Start(Charms[lastSelected]._launch); } catch { }
            if (lastSelected > -scrollVal + 2) scrollVal--;
            if (lastSelected < -scrollVal - 3) scrollVal++;

        }

        private void Overlay_MouseWheel(object sender, MouseEventArgs e)
        {
            scrollVal -= e.Delta/120f;
            if (e.Delta != 0 && scrollVal < -3 &&scrollVal > 3-Charms.Count) player.Play();
        }

    }
}
