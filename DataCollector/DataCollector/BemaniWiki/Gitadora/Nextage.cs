using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataCollector.BemaniWiki.Gitadora
{
    public class Nextage : BemaniWikiPage
    {
        public Nextage() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE") { }
    }

    public abstract class NextageMusicList : MusicListPage<NextageMusicList.Data>
    {
        public NextageMusicList(string uri, int tableIndex) : base(uri, tableIndex) { }

        string version;

        protected override void AddData(string[] rawData)
        {
            if (rawData.Length == 1)
            {
                version = rawData[0];
                return;
            }

            var data = ParseData(rawData);
            if (data == null)
            {
                invalidDatas.Add(rawData);
                return;
            }

            // 新曲リスト以外の場合バージョンをここで入れる
            if (string.IsNullOrEmpty(data.Version))
                data.Version = version;

            datas.Add(data);
        }

        protected virtual Data ParseData(string[] rawData)
        {
            if (rawData.Length != 18)
                return null;

            return new Data
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
        
        public class Data
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

    public class ClassicMusicList : NextageMusicList
    {
        public ClassicMusicList() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%B5%EC%B6%CA%A5%EA%A5%B9%A5%C8%28%A1%C1XG3%29", 1) { }

        protected override Data ParseData(string[] rawData)
        {
            var data = base.ParseData(rawData);
            if (data != null)
                return data;

            var last = datas.Last();

            if (rawData.Length == 14)
            {
                // CLASSIC譜面
                return new Data
                {
                    MusicName = rawData[1],
                    Artist = last.Artist,
                    Movie = last.Movie,
                    Bpm = last.Bpm,
                    Time = last.Time,
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
                return new Data
                {
                    MusicName = rawData[0],
                    Artist = last.Artist,
                    Movie = last.Movie,
                    Bpm = last.Bpm,
                    Time = last.Time,
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
                return new Data
                {
                    MusicName = rawData[1],
                    Artist = last.Artist,
                    Movie = last.Movie,
                    Bpm = last.Bpm,
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

            return null;
        }
    }

    public class NewMusicList : NextageMusicList
    {
        public NewMusicList() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%BF%B7%B6%CA%A5%EA%A5%B9%A5%C8", 0) { }

        protected override Data ParseData(string[] rawData)
        {
            var data = base.ParseData(rawData);
            data.Event = data.Version;
            data.Version = "GITADORA NEX+AGE";
            return data;
        }
    }

    public class OldMusicList : NextageMusicList
    {
        public OldMusicList() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%B5%EC%B6%CA%A5%EA%A5%B9%A5%C8%28GITADORA%A1%C1%29", 1) { }
    }
}
