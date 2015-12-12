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
        public NFCNet(OnSocketEvent xEventHandler, OnRecivePack xPackHandler)
        {

        }

        public override void Initialization(string strIP, UInt16 nPort)
        {

        }

        public override int Initialization(UInt32 nMaxClient, UInt16 nPort)
        {
            return 0;
        }

        public override bool Final()
        {
            return true;
        }

        //无包头，内部组装
        public override bool SendMsgWithOutHead(UInt16 nMsgID, string msg, UInt32 nSockIndex)
        {
            return true;
        }

        //无包头，内部组装
        public override bool SendMsgWithOutHead(UInt16 nMsgID, string msg, List<UInt32> fdList)
        {
            return true;
        }

        //无包头，内部组装
        public override bool SendMsgToAllClientWithOutHead(UInt16 nMsgID, string msg)
        {
            return true;
        }

        public override int CloseNetObject(UInt32 nSockIndex)
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
