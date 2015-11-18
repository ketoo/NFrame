//-----------------------------------------------------------------------
// <copyright file="NFWorldServerPlugin.cs">
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
    public class NFWorldServerPlugin : NFIPlugin
    {
        public NFWorldServerPlugin()
        {

        }

        public override void Init()
        {
            CreateModule<NFCWorldToMasterModule>();
            CreateModule<NFCWorldServerNetModule>();
        }
    }
}
