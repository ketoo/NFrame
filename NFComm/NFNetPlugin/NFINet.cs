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
        public delegate void OnRecivePack(UInt32 nSockIndex, UInt16 nMsgID, string msg, NFINet pNet);
        
	    public abstract void Initialization(string strIP, UInt16 nPort);
	    public abstract int Initialization(UInt32 nMaxClient, UInt16 nPort);

	    public abstract bool Final();

	    //无包头，内部组装
        public abstract bool SendMsgWithOutHead(UInt16 nMsgID, string msg, UInt32 nSockIndex);

	    //无包头，内部组装
	    public abstract bool SendMsgWithOutHead(UInt16 nMsgID, string msg, List<UInt32> fdList);

	    //无包头，内部组装
	    public abstract bool SendMsgToAllClientWithOutHead(UInt16 nMsgID, string msg);

        public abstract int CloseNetObject(UInt32 nSockIndex);

        public abstract int IsServer();
        public abstract int Log(int severity, string msg);
    }
}
