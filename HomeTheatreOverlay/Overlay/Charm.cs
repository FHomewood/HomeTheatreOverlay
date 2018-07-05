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
        public Image _background, _icon;
        public string _launch;
        public float x, y;


        public Charm(string _title, string _launch, Image _background, Image _icon)
        {
            this._panel = new Panel();
            this._panel.BackColor = Color.FromArgb(40, 40, 40);
            this._panel.BackgroundImageLayout = ImageLayout.Stretch;
            this._panel.Size = new Size(225, 338);

            this._title = new Label();
            this._title.Text = _title;
            this._title.Size = new Size(this._panel.Size.Width-1, 50);
            this._title.TextAlign = ContentAlignment.MiddleCenter;
            this._title.Font = new Font("Century Gothic", 15, FontStyle.Regular);
            this._title.ForeColor = Color.White;

            this._launch = _launch;
            this._background = _background;
            this._icon = _icon;

        }
    }
}
