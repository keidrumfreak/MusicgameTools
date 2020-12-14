using System;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Net;
using CommonLib.Net.Http;
using MusicgameTools.DataCollector.BemaniWiki;
using MusicgameTools.DataCollector.BemaniWiki.Gitadora;

namespace DataCollector.Jig
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var page = new NewMusicList();
            await page.LoadAsync();

            Console.WriteLine(page.ToString());
        }
    }
}
