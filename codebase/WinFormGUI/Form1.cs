using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using q = QueryParserLogic;

namespace WinFormGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            ParseQuery((TextBox)sender);
        }

        private void ParseQuery(TextBox textBox)
        {
            string input = textBox.Text;
            string query = q.QueryParserLogic.ParseQuery(input);

            txtOutput.Text = query;
        }
    }
}
