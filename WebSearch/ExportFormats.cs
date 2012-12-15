using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSearchBase;

namespace WebSearch
{

    class CSV : IExportFormat
    {
        public string Name
        {
            get { return "CSV"; }
        }
        char sep;
        public CSV()
        {
            sep = ',';
        }
        public CSV(char separator)
        {
            this.sep = separator;
        }
        public void Write(string fileName, SearchResult res)
        {
                System.IO.File.WriteAllText(fileName + ".csv", Convert(res));
        //        System.Windows.Forms.MessageBox.Show("Eroare la scriere in fisierul: "+fileName + ".csv");
        }

        public string Convert(SearchResult res)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<res.Length; i++)
            {

                sb.Append(res[i].Label);
                sb.Append(sep);
                sb.Append(res[i].Url);
                sb.Append(sep);
                sb.Append(res[i].Description);
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
    
    class Screen : IExportFormat
    {
        public string Name
        {
            get { return "Screen"; }
        }
        public void Write(string fileName, SearchResult res)
        {
               System.Windows.Forms.MessageBox.Show(Convert(res));
        //        System.Windows.Forms.MessageBox.Show("Eroare la scriere in fisierul: "+fileName + ".csv");
        }

        public string Convert(SearchResult res)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<res.Length; i++)
            {

                sb.Append(res[i].Label);
                sb.Append("\r\n");
                sb.Append(res[i].Url);
                sb.Append("\r\n");
                sb.Append(res[i].Description);
                sb.Append("\r\n------------\r\n");
            }
            return sb.ToString();
        }
    }


    class XML : IExportFormat
    {
        public string Name
        {
            get { return "XML"; }
        }
        public XML()
        {
        }
        public void Write(string fileName, SearchResult res)
        {
                System.IO.File.WriteAllText(fileName + ".xml", Convert(res));
        //        System.Windows.Forms.MessageBox.Show("Eroare la scriere in fisierul: "+fileName + ".csv");
        }

        public string Convert(SearchResult res)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<search>");
            for(int i=0;i<res.Length; i++)
            {
                sb.Append("<result>");
                sb.Append("<label>");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Label));
                sb.Append("</label> \n<url>");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Url));
                sb.Append("</url>\n<description>");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Description));
                sb.Append("</description>");
                sb.Append("</result>\n");
            }
            sb.Append("</search>");
            return sb.ToString();
        }
    }
    class HTML : IExportFormat
    {
        public string Name
        {
            get { return "HTML"; }
        }
        public HTML()
        {
        }
        public void Write(string fileName, SearchResult res)
        {
                System.IO.File.WriteAllText(fileName + ".html", Convert(res));
        //        System.Windows.Forms.MessageBox.Show("Eroare la scriere in fisierul: "+fileName + ".csv");
        }

        public string Convert(SearchResult res)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>\n<head></head>\n<body>");
            for(int i=0;i<res.Length; i++)
            {
                sb.Append("<p>");
                sb.Append("<b>");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Label));
                sb.Append("</b></br> \n<a href=\"http://");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Url));
                sb.Append("\">");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Url));
                sb.Append("</a></br>\n<span>");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Description));
                sb.Append("</span>");
                sb.Append("</p>\n");
            }
            sb.Append("<body></html>");
            return sb.ToString();
        }
    }
}
