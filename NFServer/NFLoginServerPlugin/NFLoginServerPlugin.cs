//-----------------------------------------------------------------------
// <copyright file="NFLoginServerPlugin.cs">
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
    public class NFLoginServerPlugin : NFIPlugin
    {
        public NFLoginServerPlugin()
        {
        }
        public override void Init()
        {
            CreateModule<NFCLoginServerNetModule>();

        }
    }
}
