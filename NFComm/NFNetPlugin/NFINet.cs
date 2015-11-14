//-----------------------------------------------------------------------
// <copyright file="NFINet.cs">
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
    public abstract class NFINet : NFBehaviour
    {
        public enum NF_NET_EVENT
        {
            NF_NET_EVENT_EOF = 0x10,        //掉线
            NF_NET_EVENT_ERROR = 0x20,      //未知错误
            NF_NET_EVENT_TIMEOUT = 0x40,    //连接超时
            NF_NET_EVENT_CONNECTED = 0x80,  //连接成功(作为客户端)
        }

        public delegate void OnSocketEvent(UInt32 nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet);
        public delegate void OnRecivePack(NFIPacket msg, NFINet pNet);

        public abstract int Initialization(string strIP, UInt16 nPort);
        public abstract int Initialization(UInt32 nMaxClient, UInt16 nPort);

        public abstract int Final();
        public abstract int Reset();

        public abstract int SendMsg(NFIPacket msg, UInt32 nSockIndex = 0);
        public abstract int SendMsg(string msg, UInt32 nLen, UInt32 nSockIndex = 0);

        public abstract int SendMsgToAllClient(NFIPacket msg);
        public abstract int SendMsgToAllClient(string msg, UInt32 nLen);

        public abstract int AddBan(UInt32 nSockIndex, UInt32 nTime = 0);
        public abstract int RemoveBan(UInt32 nSockIndex);
        public abstract int CloseNetObject(UInt32 nSockIndex);

        public abstract NFIMsgHead.NF_Head GetHeadLen();
        public abstract int IsServer();
        public abstract int Log(int severity, string msg);
    }
}
