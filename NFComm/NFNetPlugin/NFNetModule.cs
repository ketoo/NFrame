//-----------------------------------------------------------------------
// <copyright file="NFNetModule.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;
using ProtoBuf;
using NFMsg;
using NFrame;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public class NFNetModule : NFILogicModule
    {
        static public NFMsg.Ident NFToPB(NFrame.NFGUID xID)
        {
            NFMsg.Ident xIdent = new NFMsg.Ident();
            xIdent.svrid = xID.nHead64;
            xIdent.index = xID.nData64;

            return xIdent;
        }
        static public NFrame.NFGUID PBToNF(NFMsg.Ident xID)
        {
            NFrame.NFGUID xIdent = new NFrame.NFGUID();
            xIdent.nHead64 = xID.svrid;
            xIdent.nData64 = xID.index;

            return xIdent;
        }

        public NFNetHandler GetNetHandler()
        {
            return mxNetHandler;
        }
        public int Initialization(string strIP, UInt16 nPort)
        {

            return 0;
        }
        public int Initialization(UInt32 nMaxClient, UInt16 nPort)
        {

            return 0;
        }


        public int SendMsg(int nMsgID, int nSockIndex, string msg)
        {
            return 0;
        }


        public int SendMsgToAllClient(int nMsgID, string msg)
        {
            return 0;
        }

        public int SendMsgPB(int nMsgID, int nSockIndex, NFMsg.MsgBase xData)
	    {
            MemoryStream body = new MemoryStream();
            Serializer.Serialize<NFMsg.MsgBase>(body, xData);

            SendMsg(nMsgID, nSockIndex, body.ToString());

            return 0;
        }

        private void OnSocketEvent(UInt32 nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet)
        {
            GetNetHandler().OnSocketEvent(nSockIndex, eEvent, pNet);
        }
        private void OnRecivePack(UInt32 nSockIndex, UInt16 nMsgID, string msg, NFINet pNet)
        {
            GetNetHandler().OnRecivePack(nSockIndex, nMsgID, msg, pNet);
        }

        private readonly NFNetHandler mxNetHandler = new NFNetHandler();
    }
}
