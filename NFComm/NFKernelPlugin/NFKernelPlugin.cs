//-----------------------------------------------------------------------
// <copyright file="NFCHeartBeat.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace NFrame
{
	public class NFKernelPlugin : NFIPlugin
	{
        public override void Init()
        {
            CreateModule<NFCLogicClassModule>();
            CreateModule<NFCElementModule>();
            CreateModule<NFCKernelModule>();
        }

        public override void AfterInit() { }

        public override void BeforeShut() { }

        public override void Shut() { }

        public override void Execute() { }

    }
}