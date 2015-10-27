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
        public virtual void Install() 
        {
            mxLogicClassModule = new NFCLogicClassModule();
            mxElementModule = new NFCElementModule();
        }

        public virtual void UnInstall()
        {

        }

        public override void Init()
        {
            mxLogicClassModule.Init();
            mxElementModule.Init();
        }

        public override void AfterInit()
        {
            mxLogicClassModule.AfterInit();
            mxElementModule.AfterInit();
        }

        public override void BeforeShut()
        {
            mxElementModule.BeforeShut();
            mxLogicClassModule.BeforeShut();
        }

        public override void Shut()
        {
            mxElementModule.Shut();
            mxLogicClassModule.Shut();
        }

        public override void Execute()
        { 
        }

        private NFILogicClassModule mxLogicClassModule;
        private NFIElementModule mxElementModule;
    }
}