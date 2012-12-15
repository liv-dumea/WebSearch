using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using WebSearchBase;
using System.Net;

namespace DuckDuckGo
{
    public class DuckDuckGo : ISearchProvider
    {
        public string Name
        {
            get { return "DuckDuckGo(plugin)"; }
        }


        public string GetRequestUrl(string searchedWord, int nr = 25)
        {
            return @"http://duckduckgo.com/html/?q=" + WebUtility.UrlEncode(searchedWord) + "&num=" + nr.ToString();
        }

        public SearchResult ParseDocument(HtmlAgilityPack.HtmlDocument document)
        {

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.
            SearchResult rez = new SearchResult();
            var node = document.GetElementbyId("links");
            bool notFound = false;
            if (node == null)
            {
                notFound = true;
            }
            var list = node.SelectSingleNode("div");
            if (notFound)
            {
                MessageBox.Show("word not found");
                return rez;
            }
            // StringBuilder sb = new StringBuilder("");
            var list2 = list.SelectNodes("//div[@class='results_links results_links_deep web-result']");


            foreach (var el in list2)
            {
                SearchResultItem item = new SearchResultItem();
                foreach (var dsc in el.Descendants("a"))
                {
                    if (dsc != null && dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value == "large")
                    {
                        item.Label = WebUtility.HtmlDecode(dsc.InnerText);
                    }
                }
                foreach (var dsc in el.Descendants("div"))
                {
                    if (dsc != null && dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value == "url")
                    {
                        item.Url = dsc.InnerText.Trim();
                    }
                }
                foreach (var dsc in el.Descendants("div"))
                {
                    if (dsc != null && dsc.Attributes.Contains("class") && dsc.Attributes["class"].Value == "snippet")
                    {

                        item.Description = WebUtility.HtmlDecode(dsc.InnerText);
                    }
                }
                rez.Add(item);
            }

            return rez;
        }
    }



}
