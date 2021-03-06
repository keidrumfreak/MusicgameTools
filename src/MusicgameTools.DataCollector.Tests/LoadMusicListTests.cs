using System;
using System.Linq;
using System.Threading.Tasks;
using CommonLib.TestHelper.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicgameTools.DataCollector.BemaniWiki.Gitadora;

namespace MusicgameTools.DataCollector.Tests
{
    [TestClass]
    public class LoadMusicListTests
    {
        [TestMethod("LoadNextage 実際に接続して走ることを確認")]
        public async Task LoadNextage()
        {
            var page = new Nextage();
            var datas = await page.LoadMusicDataAsync();
            datas.Any().IsTrue();
        }
    }
}
