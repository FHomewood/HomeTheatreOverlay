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
        public string _icon,
               _background,
               _shortcut;
        public float x, y, opacity=255;


        public Charm(string _icon, string _background, string _shortcut)
        {
            _panel = new Panel();
            _panel.BackColor = Color.Black;
            _panel.Size = new Size(200, 300);
        }
    }
}
