using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Overlay.Properties;

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
        float scrollVal = -3;
        float scroller = -3;
        Charm[] Charms = new Charm[]
        {
            //  new Charm( Name , Launch File Path, Background Resource, Icon Resource )
                new Charm("START", "",Resources.BGStart,Resources.Start),
                new Charm("CHROME","C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe",Resources.BGChrome,Resources.Chrome),
                new Charm("STEAM","",Resources.BGSteam,Resources.Steam),
                new Charm("EXPLORER","",Resources.BGFolder,Resources.Folder),
                new Charm("SPOTIFY","",Resources.BGSpotify,Resources.Spotify),
                new Charm("PAINT","",Resources.BGPaintDotNet,Resources.PaintDotNet),
                new Charm("GITHUB","",Resources.BGGitHub,Resources.GitHub),
                new Charm("VISUAL STUDIO","",Resources.BGVisualStudio,Resources.VisualStudio),
                new Charm("SETTINGS","",Resources.BGSetting,Resources.Setting)
        };
        public Overlay()
        {
            InitializeComponent();
        }

        private void Initialize(object sender, EventArgs e)
        {
            this.Opacity = 0.1f;
            this.BackColor = Color.FromArgb(255,1, 1, 1);
            this.TransparencyKey = Color.FromArgb(255, 1, 1, 1);

            this.TopMost = true;
            this.Location = new Point(0, 0);
            this.Size = new Size(1920,1080);


            for (int i = 0; i < Charms.Length; i++)
            {
                this.Controls.Add(Charms[i]._title);
                this.Controls.Add(Charms[i]._panel);
                Charms[i].x = (float)Math.Sinh((i + scroller + 0.5) / 4) * 950 + this.Width / 2f - Charms[i]._panel.Width / 2;
                Charms[i].y = ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f);
            }
        }

        private void Update(object sender, EventArgs e)
        {
            if (selected) this.Opacity += (0.8f - this.Opacity) / 15f; else this.Opacity += (0.0f - this.Opacity) / 4f;
            if (this.Opacity < 0.05f) this.Close();
                
            if (this.Focused == false) selected = false;
            scroller += (scrollVal - scroller) / 4f;

            for (int i = 0; i < Charms.Length; i++)
            {
                if (selected)
                {
                    Charms[i].x = (float)Math.Sinh((i + scroller + 0.5) / 4) * 950 + this.Width / 2f - Charms[i]._panel.Width / 2;
                    Charms[i].y = (Charms[i].y -
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
                    (int)(Charms[i]._panel.Location.X + Charms[i]._panel.Size.Width/2 - Charms[i]._title.Size.Width / 2f),
                    (int)(Charms[i]._panel.Location.Y + Charms[i]._panel.Size.Height - Charms[i]._title.Size.Height));

                if (new Rectangle(Charms[i]._panel.Location.X,
                                    Charms[i]._panel.Location.Y,
                                    Charms[i]._panel.Size.Width,
                                    Charms[i]._panel.Size.Height).Contains(MousePosition))
                {
                    //on Hover
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
            }
        }

        private void _panel_MouseUp(object sender, MouseEventArgs e)
        {
                int i = 0;
                while (i < Charms.Length)
                {
                    if (selected && new Rectangle(Charms[i]._panel.Location.X,
                                        Charms[i]._panel.Location.Y,
                                        Charms[i]._panel.Size.Width,
                                        Charms[i]._panel.Size.Height).Contains(MousePosition))
                    {
                    try { Process.Start(Charms[i]._launch); }
                    catch { }
                    selected = false;
                    i = Charms.Length;
                    }
                    i++;
                }
        }
        

        private void KeyInput(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) selected = !selected;
            if (e.KeyData == Keys.Escape) this.Close();
            if (e.KeyData == Keys.Up) scrollVal++;
            if (e.KeyData == Keys.Down) scrollVal--;
        }

        private void RunFine(object sender, MouseEventArgs e)
        {
        }
    }
}
