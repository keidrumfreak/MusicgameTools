using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollector.BemaniWiki.Gitadora
{
    public class Nextage : BemaniWikiBase
    {
        public Nextage() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE") { }
    }

    public class NewMusicList : BemaniWikiBase
    {
        public NewMusicList() : base("https://bemaniwiki.com:443/index.php?GITADORA%20NEX%2BAGE/%B5%EC%B6%CA%A5%EA%A5%B9%A5%C8%28%A1%C1XG3%29") { }
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
