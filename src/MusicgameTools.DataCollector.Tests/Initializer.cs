using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MusicgameTools.DataCollector.Tests
{
    [TestClass]
    class Initializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}
