//-----------------------------------------------------------------------
// <copyright file="NFCHeartBeat.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace NFrame
{
	public class NFKernelPlugin : NFBehaviour
	{
        public NFKernelPlugin()
        {
            int i = 5;
            ++i;
        }

        public override void Init()
        {
            mxLogicModule = null;
        }

        public override void AfterInit()
        {
        }

        public override void BeforeShut()
        {

        }

        public override void Shut()
        {

        }

        public override void Execute()
        { 
        }

        private NFILogicClassModule mxLogicModule;
        private NFIElementModule mxElementModule;
    }
}