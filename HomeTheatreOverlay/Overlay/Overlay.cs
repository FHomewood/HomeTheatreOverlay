using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Overlay
{
    public partial class Overlay : Form
    {
        //APIs
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nInde, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nInde);

        bool selected = false;
        int ticks = 0;
        Charm[] Charms = new Charm[]
        {
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","",""),
                new Charm("","","")
        };
        public Overlay()
        {
            InitializeComponent();
        }

        private void Initialize(object sender, EventArgs e)
        {
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;

            this.TopMost = true;
            this.Location = new Point(0, 0);
            this.Size = new Size(1920,1080);

            //Makes the overlay clickthrough
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            for(int i = 0; i < Charms.Length; i++) this.Controls.Add(Charms[i]._panel);
        }

        private void Update(object sender, EventArgs e)
        {
            ticks++;
            if (selected)
                for (int i = 0; i < Charms.Length; i++)
                {
                    Charms[i].x = 550 * i;
                    Charms[i].y = (Charms[i].y -
                        (this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ) / 1.07f +
                        (this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ;
                    //opacity breaks it since it tries to show the Color.Wheat background
                    //if (Charms[i].opacity <255) Charms[i].opacity++;
                }
            if (!selected)
                for (int i = 0; i < Charms.Length; i++)
                {
                    Charms[i].x = 550 * i;
                    Charms[i].y = (Charms[i].y -
                        ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ) / 1.07f +
                        ((Charms[i].x - this.Width / 2f + Charms[i]._panel.Size.Width / 2f) * 3 + this.Height / 2f - Charms[i]._panel.Height / 2f)
                        ;
                    //opacity breaks it since it tries to show the Color.Wheat background
                    //if (Charms[i].opacity > 0) Charms[i].opacity--;
                }

            for (int i = 0; i < Charms.Length; i++)
            {
                Charms[i]._panel.Location = new Point((int)Charms[i].x, (int)Charms[i].y);
                Charms[i]._panel.BackColor = Color.FromArgb((int)Charms[i].opacity, 0, 0, 0);
            }
        }
        

        private void MouseInput(object sender, MouseEventArgs e)
        {
        }

        private void KeyInput(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) selected = !selected;
            if (e.KeyData == Keys.Escape) this.Close();
        }
    }
}
