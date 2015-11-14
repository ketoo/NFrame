//-----------------------------------------------------------------------
// <copyright file="NFMasterServerPlugin.cs">
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
    public class NFMasterServerPlugin : NFIPlugin
    {
        public override void Init()
        {
            CreateModule<NFCMasterServerNetModule>();
        }

        public override void AfterInit() { }

        public override void BeforeShut() { }

        public override void Shut() { }

        public override void Execute() { }
    }
}
