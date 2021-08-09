using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Installer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            SystemSounds.Asterisk.Play();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }
    }
}
