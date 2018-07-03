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

        int ticks = 0;
        Panel Jim = new Panel();
        Panel[] Shortcuts = new Panel[]
        {
                new Panel(),
                new Panel(),
                new Panel(),
                new Panel()
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

            for(int i = 0; i < Shortcuts.Length; i++)
            {
                this.Controls.Add(Shortcuts[i]);
                Shortcuts[i].Size = new Size(100, 100);
                Shortcuts[i].BackColor = Color.Black;
            }


            Jim.Location = new Point(300, 300);
            Jim.Size = new Size(100, 100);
            Jim.BackColor = Color.Black;
        }

        private void Update(object sender, EventArgs e)
        {
            ticks++;
            for(int i = 0; i < Shortcuts.Length; i++)
            {
                Shortcuts[i].Location = new Point(150 * i, ticks);
            }
        }

        private void Input(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) { this.BackColor = Color.Green; }
        }
    }
}
