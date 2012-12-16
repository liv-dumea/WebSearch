using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace WebSearchBase
{
    public interface ISearchProvider
    {
         string Name { get; }
         string GetRequestUrl(string searchedWord, int nr);
         SearchResult ParseDocument(string searchedWord, int nr);
    }
}
