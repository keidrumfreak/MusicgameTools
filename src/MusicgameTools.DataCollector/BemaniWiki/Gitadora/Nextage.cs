using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.Net.Http;

namespace MusicgameTools.DataCollector.BemaniWiki.Gitadora
{
    public class Nextage
    {
        IHtmlDocument classicMusicList;
        IHtmlDocument oldMusicList;
        IHtmlDocument newMusicList;

        public Nextage()
            : this(new HtmlDocument("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%B5%EC%B6%CA%A5%EA%A5%B9%A5%C8%28%A1%C1XG3%29"),
                  new HtmlDocument("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%B5%EC%B6%CA%A5%EA%A5%B9%A5%C8%28GITADORA%A1%C1%29"),
                  new HtmlDocument("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%BF%B7%B6%CA%A5%EA%A5%B9%A5%C8"))
        { }

        public Nextage(IHtmlDocument classicMusicList, IHtmlDocument oldMusicList, IHtmlDocument newMusicList)
        {
            this.classicMusicList = classicMusicList;
            this.oldMusicList = oldMusicList;
            this.newMusicList = newMusicList;
        }

        public async Task<IEnumerable<MusicData>> LoadMusicDataAsync()
        {
            var t1 = Task.Run(async () =>
            {
                await newMusicList.LoadAsync();
                return loadNewMusicData();
            });
            var t2 = Task.Run(async () =>
            {
                await oldMusicList.LoadAsync();
                return loadOldMusicData();
            });
            var t3 = Task.Run(async () =>
            {
                await classicMusicList.LoadAsync();
                return loadClassicMusicData();
            });

            return (await t3).Concat(await t2).Concat(await t1);
        }

        private IEnumerable<MusicData> loadNewMusicData()
        {
            var table = newMusicList.Content
                .Descendants(newMusicList.Namespace + "div").FirstOrDefault(elem => elem.Attribute("id")?.Value == "body")
                .Descendants(newMusicList.Namespace + "table").Where(elem => elem.Attribute("class")?.Value == "style_table").ElementAt(0);

            var eventName = string.Empty;
            foreach (var elem in table.Element(newMusicList.Namespace + "tbody").Elements(newMusicList.Namespace + "tr"))
            {
                var rawData = elem.Elements(newMusicList.Namespace + "td").Select(e => e.Value).ToArray();
                if (rawData.Length == 1)
                {
                    eventName = rawData[0];
                    continue;
                }

                var data = parseData(rawData);
                if (data == null)
                    throw new Exception($"find invalid data. {string.Join(",", rawData)}");

                data.Event = eventName;
                data.Version = "GITADORA NEX+AGE";
                yield return data;
            }
        }

        private IEnumerable<MusicData> loadOldMusicData()
        {
            var table = oldMusicList.Content
                .Descendants(oldMusicList.Namespace + "div").FirstOrDefault(elem => elem.Attribute("id")?.Value == "body")
                .Descendants(oldMusicList.Namespace + "table").Where(elem => elem.Attribute("class")?.Value == "style_table").ElementAt(1);

            var version = string.Empty;

            foreach (var elem in table.Element(oldMusicList.Namespace + "tbody").Elements(oldMusicList.Namespace + "tr"))
            {
                var rawData = elem.Elements(oldMusicList.Namespace + "td").Select(e => e.Value).ToArray();
                if (rawData.Length == 1)
                {
                    version = rawData[0];
                    continue;
                }

                var data = parseData(rawData);
                if (data == null)
                    throw new Exception($"find invalid data. {string.Join(",", rawData)}");

                data.Version = version;
                yield return data;
            }
        }

        private IEnumerable<MusicData> loadClassicMusicData()
        {
            var table = classicMusicList.Content
                .Descendants(classicMusicList.Namespace + "div").FirstOrDefault(elem => elem.Attribute("id")?.Value == "body")
                .Descendants(classicMusicList.Namespace + "table").Where(elem => elem.Attribute("class")?.Value == "style_table").ElementAt(1);

            var version = string.Empty;
            var lastData = default(MusicData);

            foreach (var elem in table.Element(classicMusicList.Namespace + "tbody").Elements(classicMusicList.Namespace + "tr"))
            {
                var rawData = elem.Elements(classicMusicList.Namespace + "td").Select(e => e.Value).ToArray();
                if (rawData.Length == 1)
                {
                    version = rawData[0];
                    continue;
                }

                var data = parseData(rawData);
                if (data != null)
                {
                    lastData = data;
                }

                if (rawData.Length == 14)
                {
                    // CLASSIC譜面
                    data = new MusicData
                    {
                        MusicName = rawData[1],
                        Artist = lastData.Artist,
                        Movie = lastData.Movie,
                        Bpm = lastData.Bpm,
                        Time = lastData.Time,
                        DrumBsc = rawData[2],
                        DrumAdv = rawData[3],
                        DrumExt = rawData[4],
                        DrumMas = rawData[5],
                        GuitarBsc = rawData[6],
                        GuitarAdv = rawData[7],
                        GuitarExt = rawData[8],
                        GuitarMas = rawData[9],
                        BaseBsc = rawData[10],
                        BaseAdv = rawData[11],
                        BaseExt = rawData[12],
                        BaseMas = rawData[13],
                    };
                }

                if (rawData.Length == 13)
                {
                    // 天体観測 (CLASSIC)
                    data = new MusicData
                    {
                        MusicName = rawData[0],
                        Artist = lastData.Artist,
                        Movie = lastData.Movie,
                        Bpm = lastData.Bpm,
                        Time = lastData.Time,
                        DrumBsc = rawData[1],
                        DrumAdv = rawData[2],
                        DrumExt = rawData[3],
                        DrumMas = rawData[4],
                        GuitarBsc = rawData[5],
                        GuitarAdv = rawData[6],
                        GuitarExt = rawData[7],
                        GuitarMas = rawData[8],
                        BaseBsc = rawData[9],
                        BaseAdv = rawData[10],
                        BaseExt = rawData[11],
                        BaseMas = rawData[12],
                    };
                }

                if (rawData.Length == 15)
                {
                    // Predator's Crypto Pt.2
                    data = new MusicData
                    {
                        MusicName = rawData[1],
                        Artist = lastData.Artist,
                        Movie = lastData.Movie,
                        Bpm = lastData.Bpm,
                        Time = rawData[2],
                        DrumBsc = rawData[3],
                        DrumAdv = rawData[4],
                        DrumExt = rawData[5],
                        DrumMas = rawData[6],
                        GuitarBsc = rawData[7],
                        GuitarAdv = rawData[8],
                        GuitarExt = rawData[9],
                        GuitarMas = rawData[10],
                        BaseBsc = rawData[11],
                        BaseAdv = rawData[12],
                        BaseExt = rawData[13],
                        BaseMas = rawData[14],
                    };
                }

                if (data == null)
                    throw new Exception($"find invalid data. {string.Join(",", rawData)}");

                data.Version = version;
                yield return data;
            }
        }

        private MusicData parseData(string[] rawData)
        {
            if (rawData.Length != 18)
                return null;

            return new MusicData
            {
                MusicName = rawData[1],
                Artist = rawData[2],
                Movie = rawData[3],
                Bpm = rawData[4],
                Time = rawData[5],
                DrumBsc = rawData[6],
                DrumAdv = rawData[7],
                DrumExt = rawData[8],
                DrumMas = rawData[9],
                GuitarBsc = rawData[10],
                GuitarAdv = rawData[11],
                GuitarExt = rawData[12],
                GuitarMas = rawData[13],
                BaseBsc = rawData[14],
                BaseAdv = rawData[15],
                BaseExt = rawData[16],
                BaseMas = rawData[17],
            };
        }

        public class MusicData
        {
            public string Version { get; set; }
            public string Event { get; set; }
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
}
