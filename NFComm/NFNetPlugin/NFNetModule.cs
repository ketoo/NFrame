//-----------------------------------------------------------------------
// <copyright file="NFNetModule.cs">
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
    public class NFNetModule : NFILogicModule
    {
        public NFNetHandler GetNetHandler()
        {
            return mxNetHandler;
        }
        public int Initialization(NFIMsgHead.NF_Head nHeadLength, string strIP, UInt16 nPort)
        {
            return 0;
        }
        public int Initialization(NFIMsgHead.NF_Head nHeadLength, UInt32 nMaxClient, UInt16 nPort)
        {
            return 0;
        }

        protected void OnSocketEvent(UInt32 nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet)
        {
            GetNetHandler().OnSocketEvent(nSockIndex, eEvent, pNet);
        }
        protected void OnRecivePack(NFIPacket msg, NFINet pNet)
        {
            GetNetHandler().OnRecivePack(msg, pNet);
        }

        private readonly NFNetHandler mxNetHandler = new NFNetHandler();
    }
}
