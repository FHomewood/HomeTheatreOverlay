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
        float scrollVal = -2;
        float scroller = -2;
        Charm[] Charms = new Charm[]
        {
            //  new Charm( Name , Launch File Path, Background Resource, Icon Resource )
                new Charm("Chrome","",Resources.Template,Resources.Chrome),
                new Charm("TWO","",Resources.Template,Resources.Template),
                new Charm("THREE","",Resources.Template,Resources.Template),
                new Charm("FOUR","",Resources.Template,Resources.Template),
                new Charm("FIVE","",Resources.Template,Resources.Template),
                new Charm("SIX","",Resources.Template,Resources.Template),
                new Charm("SEVEN","",Resources.Template,Resources.Template),
                new Charm("EIGHT","",Resources.Template,Resources.Template),
                new Charm("NINE","",Resources.Template,Resources.Template),
                new Charm("TEN","",Resources.Template,Resources.Template),
                new Charm("ELEVEN","",Resources.Template,Resources.Template),
                new Charm("TWELVE","",Resources.Template,Resources.Template)
        };
        public Overlay()
        {
            InitializeComponent();
        }

        private void Initialize(object sender, EventArgs e)
        {
            this.Opacity = 0.9f;
            this.BackColor = Color.FromArgb(255,1, 1, 1);
            this.TransparencyKey = Color.FromArgb(255, 1, 1, 1);

            this.TopMost = true;
            this.Location = new Point(0, 0);
            this.Size = new Size(1920,1080);


            for (int i = 0; i < Charms.Length; i++)
            {
                this.Controls.Add(Charms[i]._title);
                this.Controls.Add(Charms[i]._panel);
                Charms[i].x = 550 * i + scrollVal;
                Charms[i].y = ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f);
            }
        }

        private void Update(object sender, EventArgs e)
        {
            if (this.Focused == false) selected = false;
            scroller += (scrollVal - scroller) / 4f;

            for (int i = 0; i < Charms.Length; i++)
            {
                if (selected)
                {
                    Charms[i].x = (float)Math.Sinh(i + scroller + 0.5)*300 + this.Width/2f - 150;
                    Charms[i].y = (Charms[i].y -
                        (this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ) / 1.07f +
                        (this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ;
                }

                if (!selected)
                {
                    Charms[i].x = (float)Math.Sinh(i + scroller + 0.5) * 300 + this.Width / 2f - 150;
                    Charms[i].y = (Charms[i].y -
                        ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ) / 1.07f +
                        ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ;
                }

                Charms[i]._panel.Location = new Point((int)Charms[i].x, (int)Charms[i].y);
                Charms[i]._title.Location = new Point(
                    (int)(Charms[i]._panel.Location.X + 150 - Charms[i]._title.Size.Width / 2f),
                    (int)(Charms[i]._panel.Location.Y + 425 - Charms[i]._title.Size.Height / 2f));

                if (new Rectangle(Charms[i]._panel.Location.X,
                                    Charms[i]._panel.Location.Y,
                                    Charms[i]._panel.Size.Width,
                                    Charms[i]._panel.Size.Height).Contains(MousePosition))
                {
                    //on Hover
                    Charms[i]._panel.BackgroundImage = Charms[i]._background;
                    Charms[i]._title.BackColor = Color.FromArgb(60, 60, 60);
                    Charms[i]._panel.Click += _panel_Click;
                }
                else
                {
                    //no Hover
                    Charms[i]._panel.BackgroundImage = Charms[i]._icon;
                    Charms[i]._title.BackColor = Color.FromArgb(40, 40, 40);
                }
            }
        }

        private void _panel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Charms.Length; i++)
            {
                if (new Rectangle(Charms[i]._panel.Location.X,
                                    Charms[i]._panel.Location.Y,
                                    Charms[i]._panel.Size.Width,
                                    Charms[i]._panel.Size.Height).Contains(MousePosition))
                {
                    try
                    {
                        Process.Start(Charms[i]._launch);
                    }
                    catch { }
                }
            }
        }

        private void KeyInput(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) selected = !selected;
            if (e.KeyData == Keys.Escape) this.Close();
            if (e.KeyData == Keys.Up) scrollVal++;
            if (e.KeyData == Keys.Down) scrollVal--;
        }
    }
}
