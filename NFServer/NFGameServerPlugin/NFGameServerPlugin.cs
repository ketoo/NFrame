//-----------------------------------------------------------------------
// <copyright file="NFGameServerPlugin.cs">
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
    public class NFGameServerPlugin : NFIPlugin
    {
        public NFGameServerPlugin()
        {

        }

        public override void Init()
        {
            CreateModule<NFCGameToWorldModule>();
            CreateModule<NFCGameServerNetModule>();
        }
    }
}
