using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSearch
{
    public partial class ScreenResults : Form
    {
        public ScreenResults()
        {
            InitializeComponent();
        }
        public ScreenResults(string results,string description ="...")
        {
            InitializeComponent();
            this.desciptionLabel.Text = description;
            this.resultsTextBox.Text = results;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}