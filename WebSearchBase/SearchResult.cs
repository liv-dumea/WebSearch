using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSearchBase
{
    public class SearchResult
    {
        List<SearchResultItem> items;
        int len;
        string searchedWord;
        public string Word
        {
            get
            {
                return searchedWord;
            }
        }
        public SearchResult()
        {
            searchedWord = "";
            items = new List<SearchResultItem>();
            len = 0;
        }
        public SearchResult(string word)
        {
            searchedWord = word;
            items = new List<SearchResultItem>();
            len = 0;
        }
        public int Length
        {
            get
            {
                return len;
            }
            //set;
        }
        public SearchResultItem this[int index] {
            get {
                if (index < Length && index >= 0)
                {
                    return items[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                    //return null;
                }
            }
        }
        public void Add(SearchResultItem rez)
        {
            len++;
            items.Add(rez);
        }
    }
}
