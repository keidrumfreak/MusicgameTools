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
        [TestMethod("LoadNextage ���ۂɐڑ����đ��邱�Ƃ��m�F")]
        public async Task LoadNextage()
        {
            var page = new Nextage();
            var datas = await page.LoadMusicDataAsync();
            datas.Any().IsTrue();
        }
    }
}
