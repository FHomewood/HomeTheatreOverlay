using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HTOInstaller_V1._0
{
    public partial class Installer : Form
    {
        int screen = 0;
        bool allowedTransition = true;
        public Installer()
        {
            InitializeComponent();
        }

        private void Initialise(object sender, EventArgs e)
        {
                string ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                DirectoryTbx.Text = Path.Combine(ProgramFiles, "Home Theatre Overlay");
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (allowedTransition) screen++;

            if (screen == 1)
            {
                DirectoryLabel.Visible = true;
                DirectoryTbx.Visible = true;
                NextButton.Text = "Next";
            }

            if (screen == 2)
            {
                DirectoryLabel.Visible = true;
                DirectoryTbx.Visible = true;
                Directory.CreateDirectory(DirectoryTbx.Text);
                Shell32.ShellClass sc = new Shell32.ShellClass();
                Shell32.Folder SrcFlder = sc.NameSpace(Properties.Resources.HomeTheatreOverlay);
                Shell32.Folder DestFlder = sc.NameSpace(DirectoryTbx.Text);
                Shell32.FolderItems items = SrcFlder.Items();
                DestFlder.CopyHere(items, 20);
            }
        }
    }
}
