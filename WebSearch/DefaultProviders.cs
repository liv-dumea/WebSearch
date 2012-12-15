using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Windows.Forms;
using WebSearchBase;
namespace WebSearch
{
    class Google : ISearchProvider
    {
        public string Name
        {
            get { return "Google"; }
        }

        public string GetRequestUrl(string searchedWord, int nr=25)
        {
            return @"http://www.google.com/search?q=" + WebUtility.UrlEncode(searchedWord) + "&num=" + nr.ToString();
        }

        public SearchResult ParseDocument(HtmlAgilityPack.HtmlDocument document)
        {

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.
            SearchResult rez = new SearchResult();
            var node = document.GetElementbyId("ires");
            bool notFound = false;
            if (node == null)
            {
                notFound = true;
            }
             var list = node.SelectSingleNode("ol");
            if (list == null)
            {
                notFound = true;
            }
            if (notFound)
            {
                MessageBox.Show("word not found");
                return rez;
            }
           // StringBuilder sb = new StringBuilder("");
            var list2 = list.SelectNodes("//li[@class='g']");
          
            foreach (var el in list2)
            {
                SearchResultItem item = new SearchResultItem();
                foreach (var dsc in el.Descendants("h3"))
                {
                      item.Label = WebUtility.HtmlDecode(dsc.InnerText);
                }
                foreach (var dsc in el.Descendants("cite"))
                {
                    item.Url = dsc.InnerText;
                }
                foreach (var dsc in el.Descendants("span"))
                {
                    if (dsc != null && dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value == "st")
                    {
                        
                        item.Description=WebUtility.HtmlDecode(dsc.InnerText);
                    }
                }
                rez.Add(item);
            }
            return rez;
        }
    }

    class Bing : ISearchProvider
    {
        public string Name
        {
            get { return "Bing"; }
        }

        public string GetRequestUrl(string searchedWord, int nr=25)
        {
            return @"http://www.bing.com/search?q=" + WebUtility.UrlEncode(searchedWord) + "&num=" + nr.ToString();
        }

        public SearchResult ParseDocument(HtmlAgilityPack.HtmlDocument document)
        {

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.
            SearchResult rez = new SearchResult();
            var node = document.GetElementbyId("results");
            bool notFound = false;
            if (node == null)
            {
                notFound = true;
            }
             var list = node.SelectSingleNode("ul");
            if (list == null)
            {
                notFound = true;
            }
            if (notFound)
            {
                MessageBox.Show("word not found");
                return rez;
            }
           // StringBuilder sb = new StringBuilder("");
            var list2 = list.SelectNodes("//li[@class='sa_wr']");
          
            foreach (var el in list2)
            {
                SearchResultItem item = new SearchResultItem();
                foreach (var dsc in el.Descendants("h3"))
                {
                      item.Label = WebUtility.HtmlDecode(dsc.InnerText);
                }
                foreach (var dsc in el.Descendants("cite"))
                {
                    item.Url = dsc.InnerText;
                }
              /*  foreach (var dsc in el.Descendants("span"))
                {
                    if (dsc != null && dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value == "st")
                    {
                        
                        item.Description=WebUtility.HtmlDecode(dsc.InnerText);
                    }
                }
               */
               foreach (var dsc in el.Descendants("p"))
               {
                   item.Description=WebUtility.HtmlDecode(dsc.InnerText);
               }
                rez.Add(item);
            }
            return rez;
        }
    }

    class Yahoo : ISearchProvider
    {
        public string Name
        {
            get { return "Yahoo"; }
        }

        public string GetRequestUrl(string searchedWord, int nr=25)
        {
            return @"http://search.yahoo.com/search?n="+ nr.ToString( )  + "&p="+ WebUtility.UrlEncode(searchedWord)+"&vs=";
        }

        public SearchResult ParseDocument(HtmlAgilityPack.HtmlDocument document)
        {

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.
            SearchResult rez = new SearchResult();
            var node = document.GetElementbyId("web");
            bool notFound = false;
            if (node == null)
            {
                notFound = true;
            }
             var list = node.SelectSingleNode("ol");
            if (list == null)
            {
                notFound = true;
            }
            if (notFound)
            {
                MessageBox.Show("word not found");
                return rez;
            }
           // StringBuilder sb = new StringBuilder("");
            var list2 = list.SelectNodes("//div[@class='res']");
          
            foreach (var el in list2)
            {
                SearchResultItem item = new SearchResultItem();
                foreach (var dsc in el.Descendants("h3"))
                {
                      item.Label = WebUtility.HtmlDecode(dsc.InnerText);
                }
                foreach (var dsc in el.Descendants("span"))
                {
                    if(dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value=="url")
                            item.Url = dsc.InnerText;
                }
              /*  foreach (var dsc in el.Descendants("span"))
                {
                    if (dsc != null && dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value == "st")
                    {
                        
                        item.Description=WebUtility.HtmlDecode(dsc.InnerText);
                    }
                }
               */
               foreach (var dsc in el.Descendants("div"))
               {
                    if(dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value=="abstr")
                         item.Description=WebUtility.HtmlDecode(dsc.InnerText);
               }
                rez.Add(item);
            }
            return rez;
        }
    }

    
}