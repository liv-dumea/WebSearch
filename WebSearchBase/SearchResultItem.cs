using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSearchBase
{
    public class SearchResultItem
    {
        Dictionary<string, string> items;

        public SearchResultItem()
        {
            items = new Dictionary<string, string>(3);
        }
        public string this[string key]
        {
            get
            {
                if (items.ContainsKey(key))
                    return items[key];
                else
                    return "";
            }
        }
        public string[] GetKeys()
        {
            int i=0;
            string[] rez= new string[items.Count()];

            foreach (var k in items.Keys)
            {
                k.ToString();
                rez[i++] = k;
            }
            return rez;
        }
        public void Add(string key, string value)
        {
            if (key.Trim() != "")
            {
                items.Add(key, value);
            }
        }

        public string Url
        {
            get
            {
                return this["url"];
            }
            set
            {
                if (items.ContainsKey("url"))
                    return;
                items.Add("url", value);
            }
        }
        public string Label
        {
            get
            {
                return this["label"];
            }
            set
            {
                if (items.ContainsKey("label"))
                    return;
                items.Add("label", value);
            }

        }
        public string Description
        {
            get
            {
                return this["description"];
            }
            set
            {
                if (items.ContainsKey("description"))
                    return;
                items.Add("description", value);
            }
        }
    }
}
