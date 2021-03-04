using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.Net.Http;

namespace MusicgameTools.DataCollector.BemaniWiki
{
    public class BemaniWikiPage : HtmlDocument
    {
        public XElement Body { get; private set; }

        public BemaniWikiPage() : base("https://bemaniwiki.com") { }

        protected BemaniWikiPage(string uri) : base(uri) { }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            Body = Content.Descendants(Namespace + "div").FirstOrDefault(elem => elem.Attribute("id")?.Value == "body");
        }
    }
}
