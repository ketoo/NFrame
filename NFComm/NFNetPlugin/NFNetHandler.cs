//-----------------------------------------------------------------------
// <copyright file="NFNetHandler.cs">
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
    public class NFNetHandler : NFILogicModule
    {
        public void RegisterEventCallback(NFINet.OnSocketEvent xEventHandler)
        {

        }
        public void RegisterPackCallback(int nMsgdID, NFINet.OnRecivePack xEventHandler)
        {

        }
    }
}
