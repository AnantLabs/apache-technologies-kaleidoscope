// Image Processing Prototyper
// Apache.Iplib framework

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kaleidoscope
{
    internal partial class CAboutUs : Form
    {
        public CAboutUs()
        {
            InitializeComponent();

            lblProductName.Text = Program.cAPP_NAME;

            // initialize links
            lblEmail.Links.Add(0, lblEmail.Text.Length, "mailto:" + lblEmail.Text);
            lblWebsite.Links.Add(0, lblWebsite.Text.Length, lblWebsite.Text);
        }

        // Link clicked
        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}
