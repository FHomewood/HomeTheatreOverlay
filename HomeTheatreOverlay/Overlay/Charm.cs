using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Overlay
{
    class Charm
    {
        public Panel _panel;
        public Label _title;
        public string
                _icon,
                _background,
                _shortcut;
        public float x, y;


        public Charm(string _title, string _icon, string _background, string _shortcut)
        {
            this._panel = new Panel();
            this._panel.BackColor = Color.FromArgb(255,255,255);
            this._panel.Size = new Size(300, 450);
            this._title = new Label();
            this._title.Text = _title;
        }
    }
}
