using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.Net.Http;

namespace DataCollector.BemaniWiki
{
    public class BemaniWikiBase : WebPage
    {
        public XElement Body { get; private set; }

        public BemaniWikiBase() : base("https://bemaniwiki.com") { }

        protected BemaniWikiBase(string uri) : base(uri) { }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            var ns = Content.Root.Name.Namespace;

            Body = Content.Descendants(ns + "div").FirstOrDefault(elem => elem.Attribute("id")?.Value == "body");
        }
    }
}
