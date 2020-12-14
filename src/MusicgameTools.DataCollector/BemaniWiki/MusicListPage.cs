using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicgameTools.DataCollector.BemaniWiki
{
    public abstract class MusicListPage<T> : BemaniWikiPage where T : class
    {
        protected MusicListPage(string uri, int tableIndex) : base(uri)
        {
            this.tableIndex = tableIndex;
        }

        int tableIndex;

        public XElement Table { get; private set; }

        protected List<T> datas = new List<T>();
        public IEnumerable<T> Datas => datas;

        protected List<string[]> invalidDatas = new List<string[]>();
        public IEnumerable<string[]> InvalidDatas => invalidDatas;

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            Table = Body.Descendants(Namespace + "table").Where(elem => elem.Attribute("class")?.Value == "style_table").ElementAt(tableIndex);

            foreach (var elem in Table.Element(Namespace + "tbody").Elements(Namespace + "tr"))
            {
                var rawData = elem.Elements(Namespace + "td").Select(e => e.Value).ToArray();
                try
                {
                    AddData(rawData);
                }
                catch
                {
                    invalidDatas.Add(rawData);
                }
            }
        }

        protected abstract void AddData(string[] rawData);
    }
}
