using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    class App
    {
        private static bool bAppRuning = true;

        static void Main(string[] args)
        {
            NFCPluginManager.Intance().Init();
            NFCPluginManager.Intance().AfterInit();

            while (bAppRuning)
            {
                NFCPluginManager.Intance().Execute();
            }

            NFCPluginManager.Intance().BeforeShut();
            NFCPluginManager.Intance().Shut();
        }
    }
}
