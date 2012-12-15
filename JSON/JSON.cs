using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSearchBase;

namespace JSON
{
    public class JSON : IExportFormat
    {
        public string Name
        {
            get { return "JSON(plugin)"; }
        }
        public JSON()
        {
        }
        public void Write(string fileName, SearchResult res)
        {
            System.IO.File.WriteAllText(fileName + ".json", Convert(res));
            //        System.Windows.Forms.MessageBox.Show("Eroare la scriere in fisierul: "+fileName + ".csv");
        }

        public string Convert(SearchResult res)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"result:\"[");
            for (int i = 0; i < res.Length; i++)
            {
                sb.Append("{");
                sb.Append("\"label\":\"\n");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Label));
                sb.Append("\",\n\"url\":\"\n");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Url));
                sb.Append("\",\n\"description\":\"\n");
                sb.Append(System.Net.WebUtility.HtmlEncode(res[i].Description));
                sb.Append("\n\"}");
                sb.Append(",\n");
            }
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
