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

        bool selected = true;
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
            this.Location = new Point(200, 200);
            this.Size = new Size(700,700);

            //Makes the overlay clickthrough
            //int initialStyle = GetWindowLong(this.Handle, -20);
            //SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            for(int i = 0; i < Charms.Length; i++) this.Controls.Add(Charms[i]._panel);
        }

        private void Update(object sender, EventArgs e)
        {
            ticks++;
            if (selected)
                for (int i = 0; i < Charms.Length; i++)
                {
                    Charms[i]._panel.Location = new Point(250 * i, (int)((Charms[i]._panel.Location.Y - this.Height / 2f + Charms[i]._panel.Height / 2f) / 1.07f + this.Height / 2f - Charms[i]._panel.Height/2f));

                }
        }

        private void Input(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) selected = !selected;
        }
    }
}
