using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace WebSearchBase
{
    public interface IExportFormat
    {
        string Name { get; }
        void Write(string fileName, SearchResult res);
        string Convert(SearchResult res);
    }
}
