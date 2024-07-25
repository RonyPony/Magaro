using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace mangaroUI
{
    public partial class getusername : Form
    {
        public getusername()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                Form1 ff = new Form1(textBox1.Text);
                this.Hide();
                ff.ShowDialog();
            }
            else
            {
                label2.Visible = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(textBox1.Text))
            {
                if (!String.IsNullOrEmpty(textBox1.Text))
                {
                    Form1 ff = new Form1(textBox1.Text);
                    this.Hide();
                    ff.ShowDialog();
                }
                else
                {
                    label2.Visible = true;
                }
            }
        }
    }
}
