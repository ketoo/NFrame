//-----------------------------------------------------------------------
// <copyright file="App.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NFrame
{
    class App
    {
        private static bool bAppRuning = true;

        static void Main(string[] args)
        {
            NFCPluginManager.Intance().Install();

            NFCPluginManager.Intance().Init();
            NFCPluginManager.Intance().AfterInit();

            while (bAppRuning)
            {
                Thread.Sleep(1);

                NFCPluginManager.Intance().Execute();
            }

            NFCPluginManager.Intance().BeforeShut();
            NFCPluginManager.Intance().Shut();

            NFCPluginManager.Intance().UnInstall();
        }
    }
}
