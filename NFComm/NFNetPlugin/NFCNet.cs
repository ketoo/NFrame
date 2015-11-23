//-----------------------------------------------------------------------
// <copyright file="NFINetModule.cs">
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
    public class NFCNet : NFINet
    {
        public NFCNet(NFIMsgHead.NF_Head nHeadLength, OnSocketEvent xEventHandler, OnRecivePack xPackHandler)
        {

        }

        public override int Initialization(string strIP, UInt16 nPort)
        {
            return 0;
        }

        public override int Initialization(UInt32 nMaxClient, UInt16 nPort)
        {
            return 0;
        }

        public override int Final()
        {
            return 0;
        }

        public override int SendMsg(NFIPacket msg, UInt32 nSockIndex = 0)
        {
            return 0;
        }

        public override int SendMsg(string msg, UInt32 nLen, UInt32 nSockIndex = 0)
        {
            return 0;
        }

        public override int SendMsgToAllClient(NFIPacket msg)
        {
            return 0;
        }

        public override int SendMsgToAllClient(string msg, UInt32 nLen)
        {
            return 0;
        }

        public override int AddBan(UInt32 nSockIndex, UInt32 nTime = 0)
        {
            return 0;
        }

        public override int RemoveBan(UInt32 nSockIndex)
        {
            return 0;
        }

        public override int CloseNetObject(UInt32 nSockIndex)
        {
            return 0;
        }

        public override NFIMsgHead.NF_Head GetHeadLen()
        {
            return 0;
        }

        public override int IsServer()
        {
            return 0;
        }

        public override int Log(int severity, string msg)
        {
            return 0;
        }
    }
}
