using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using WebSearchBase;

namespace WebSearch
{
    public partial class Form1 : Form
    {
        List<ISearchProvider> providers;
        List<IExportFormat> formats;
        private System.Windows.Forms.CheckBox[] provCheckBox;
        private System.Windows.Forms.CheckBox[] formatsCheckBox;
        private int nrProviders;
        private int nrFormats;
        public Form1()
        {

            providers = new List<ISearchProvider>();
            formats = new List<IExportFormat>();
            loadDefaultProvides();
            loadDefaultFormats();
            if (System.IO.Directory.Exists("plugins"))
                loadPlugins();

            if (!System.IO.Directory.Exists("searches"))
            {
                System.IO.Directory.CreateDirectory("searches");
            }
            InitializeComponent();
        }
        void initializeCheckBoxesProviders()
        {
            int nr = providers.Count();
            this.nrProviders = nr;
            provCheckBox = new CheckBox[nr];
            for (int i = 0; i < nr; i++)
            {
                provCheckBox[i] = new CheckBox();
                provCheckBox[i].Text = providers[i].Name;
                provCheckBox[i].AutoSize = true;
                provCheckBox[i].Size = new System.Drawing.Size(80, 17);
                provCheckBox[i].Location = new System.Drawing.Point(10, 20+i*20);
                provCheckBox[i].UseVisualStyleBackColor = true;
                provCheckBox[i].Name = "checkBox__" + i.ToString();
                this.providersBox.Controls.Add(this.provCheckBox[i]);
            }
        }

        void initializeCheckBoxesFormats()
        {
            int nr = formats.Count();
            this.nrFormats = nr;
            formatsCheckBox = new CheckBox[nr];
            for (int i = 0; i < nr; i++)
            {
                formatsCheckBox[i] = new CheckBox();
                formatsCheckBox[i].Text = formats[i].Name;
                formatsCheckBox[i].AutoSize = true;
                formatsCheckBox[i].Size = new System.Drawing.Size(80, 17);
                formatsCheckBox[i].Location = new System.Drawing.Point(10, 20 + i * 20);
                formatsCheckBox[i].UseVisualStyleBackColor = true;
                formatsCheckBox[i].Name = "checkBox__" + i.ToString();
                this.exportBox.Controls.Add(this.formatsCheckBox[i]);
            }
        }

        //TODO apelat in InitializeComponent(); (inainte de inializaril pentru Form1)
        // visual studio sterge aplelul
        void dinamicLoadComponents()
        {
            this.providersBox.SuspendLayout();
            this.initializeCheckBoxesProviders();
            this.providersBox.ResumeLayout(true);
            this.providersBox.PerformLayout();

            this.exportBox.SuspendLayout();
            this.initializeCheckBoxesFormats();
            this.exportBox.ResumeLayout();
            this.exportBox.PerformLayout();
        }
      

        private void loadDefaultFormats()
        {
            formats.Add(new Screen());
            formats.Add(new CSV('\t'));
            formats.Add(new XML());
            formats.Add(new HTML());
        }

        private void loadDefaultProvides()
        {
            providers.Add(new Google());
            providers.Add(new Bing());
            providers.Add(new Yahoo());
        }
        void loadPlugins()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo("plugins");

                foreach (FileInfo file in dir.GetFiles("*.dll"))
                {
                    Assembly assembly = Assembly.LoadFrom(file.FullName);
                    foreach (Type type in assembly.GetTypes())
                    {
                            ISearchProvider p = type.InvokeMember(null,
                                BindingFlags.CreateInstance, null, null, null) as ISearchProvider;
                        if(p!=null)  
                            providers.Add(p);
                            IExportFormat f = type.InvokeMember(null,
                                BindingFlags.CreateInstance, null, null, null) as IExportFormat;
                        if(f!=null)
                            formats.Add(f);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string s = this.searchWord.Text.Trim();
            int nr;
            if (s=="" || !int.TryParse(this.resultNumber.Text, out nr))
            {
                toolStripStatusLabel1.Text = "Wrong parameters";
                return;
            }
            else
                toolStripStatusLabel1.Text = "Searching for: " + s;
            
            this.Enabled = false;
            for (int i = 0; i < nrProviders; i++)
            {
                if (provCheckBox[i].Checked)
                {
                    
                    var res = providers[i].ParseDocument(s,nr);
                    for (int j = 0; j < nrFormats; j++)
                    {
                        if (formatsCheckBox[j].Checked)
                        {

                            formats[j].Write("searches/"+System.Net.WebUtility.UrlEncode(s)+"_"+providers[i].Name, res); 
                        }
                    }
                }
            }
          //  MessageBox.Show("Finished");
            toolStripStatusLabel1.Text = "Finished";
            this.Enabled = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", ".\\searches");
        }
    }
}
