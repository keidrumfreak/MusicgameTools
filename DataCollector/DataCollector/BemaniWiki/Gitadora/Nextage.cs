using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataCollector.BemaniWiki.Gitadora
{
    public class Nextage : BemaniWikiBase
    {
        public Nextage() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE") { }
    }

    public class NewMusicList : BemaniWikiBase
    {
        public XElement Table { get; private set; }

        public IEnumerable<Data> Datas { get; private set; }

        public IEnumerable<XElement> InvalidDatas { get; private set; }

        public NewMusicList() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%B5%EC%B6%CA%A5%EA%A5%B9%A5%C8%28%A1%C1XG3%29") { }

        public override async Task LoadAsync()
        {
            await base.LoadAsync();

            var ns = Content.Root.Name.Namespace;

            Table = Body.Descendants(ns + "table").Where(elem => elem.Attribute("class")?.Value == "style_table").ElementAt(1);

            var datas = new List<Data>();
            var invalidDatas = new List<XElement>();

            var version = string.Empty;
            var artist = string.Empty;
            var movie = string.Empty;
            var bpm = string.Empty;
            var time = string.Empty;
            foreach (var elem in Table.Element(ns + "tbody").Elements(ns + "tr"))
            {
                try
                {
                    var tmp = elem.Elements(ns + "td").Select(e => e.Value).ToArray();
                    if (tmp.Length == 1)
                    {
                        version = tmp[0];
                        continue;
                    }

                    if (tmp.Length == 14)
                    {
                        // CLASSIC譜面
                        var d = new Data
                        {
                            Version = version,
                            MusicName = tmp[1],
                            Artist = artist,
                            Movie = movie,
                            Bpm = bpm,
                            Time = time,
                            DrumBsc = tmp[2],
                            DrumAdv = tmp[3],
                            DrumExt = tmp[4],
                            DrumMas = tmp[5],
                            GuitarBsc = tmp[6],
                            GuitarAdv = tmp[7],
                            GuitarExt = tmp[8],
                            GuitarMas = tmp[9],
                            BaseBsc = tmp[10],
                            BaseAdv = tmp[11],
                            BaseExt = tmp[12],
                            BaseMas = tmp[13],
                        };
                        datas.Add(d);
                        continue;
                    }

                    // TODO: 天体観測 (CLASSIC), Predator's Crypto Pt.2対応

                    if (tmp.Length != 18)
                    {
                        invalidDatas.Add(elem);
                        continue;
                    }

                    var data = new Data
                    {
                        Version = version,
                        MusicName = tmp[1],
                        Artist = tmp[2],
                        Movie = tmp[3],
                        Bpm = tmp[4],
                        Time = tmp[5],
                        DrumBsc = tmp[6],
                        DrumAdv = tmp[7],
                        DrumExt = tmp[8],
                        DrumMas = tmp[9],
                        GuitarBsc = tmp[10],
                        GuitarAdv = tmp[11],
                        GuitarExt = tmp[12],
                        GuitarMas = tmp[13],
                        BaseBsc = tmp[14],
                        BaseAdv = tmp[15],
                        BaseExt = tmp[16],
                        BaseMas = tmp[17],
                    };

                    artist = data.Artist;
                    movie = data.Movie;
                    bpm = data.Bpm;
                    time = data.Time;

                    datas.Add(data);
                }
                catch
                {
                    invalidDatas.Add(elem);
                }
            }

            Datas = datas;
            InvalidDatas = invalidDatas;
        }

        public class Data
        {
            public string Version { get; set; }
            public string MusicName { get; set; }
            public string Artist { get; set; }
            public string Movie { get; set; }
            public string Bpm { get; set; }
            public string Time { get; set; }
            public string DrumBsc { get; set; }
            public string DrumAdv { get; set; }
            public string DrumExt { get; set; }
            public string DrumMas { get; set; }
            public string GuitarBsc { get; set; }
            public string GuitarAdv { get; set; }
            public string GuitarExt { get; set; }
            public string GuitarMas { get; set; }
            public string BaseBsc { get; set; }
            public string BaseAdv { get; set; }
            public string BaseExt { get; set; }
            public string BaseMas { get; set; }
        }
    }

    public class ClassicMusicList : BemaniWikiBase
    {
        public ClassicMusicList() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%A5%CE%A1%BC%A5%C8%BF%F4%A5%EA%A5%B9%A5%C8%28%A1%C1XG3%29") { }
    }

    public class OldMusicList : BemaniWikiBase
    {
        public OldMusicList() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%A5%CE%A1%BC%A5%C8%BF%F4%A5%EA%A5%B9%A5%C8%28GITADORA%A1%C1%29") { }
    }
}
