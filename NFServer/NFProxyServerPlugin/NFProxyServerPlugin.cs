//-----------------------------------------------------------------------
// <copyright file="NFProxyServerPlugin.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public class NFProxyServerPlugin : NFIPlugin
    {
        public NFProxyServerPlugin()
        {
        }

        public override void Init()
        {
            CreateModule<NFCProxyServerNet_ServerModule>();
            CreateModule<NFCProxyToGameModule>();
            CreateModule<NFCProxyToWorldModule>();
        }
    }
}
