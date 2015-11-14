//-----------------------------------------------------------------------
// <copyright file="NFCMasterServerNetModule.cs">
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
    class NFCMasterServerNetModule : NFIMasterServerNetModule
    {
        protected NFIKernelModule mxKernelModule;
        protected NFILogicClassModule mxLogicClassModule;
        protected NFIElementModule mxElementInfoModule;
        protected NFIEventModule mxEventProcessModule;

        public override void Init()
        {

        }

        public override void AfterInit()
        {
            mxEventProcessModule = GetMng().GetModule<NFCEventModule>();
            mxKernelModule = GetMng().GetModule<NFIKernelModule>();
            mxLogicClassModule = GetMng().GetModule<NFILogicClassModule>();
            mxElementInfoModule = GetMng().GetModule<NFIElementModule>();

            NFILogicClass xLogicClass = mxLogicClassModule.GetElement("Server");
            if (xLogicClass != null)
            {
                List<string> xNameList = xLogicClass.GetConfigNameList();

                foreach (string strConfigName in xNameList)
                {
                    long nServerType = mxElementInfoModule.QueryPropertyInt(strConfigName, "Type");
                    long nServerID = mxElementInfoModule.QueryPropertyInt(strConfigName, "ServerID");
                    if (nServerType == (long)NFServer_def.NF_SERVER_TYPES.NF_ST_PROXY && GetMng().GetAPPID() == nServerID)
                    {
                        Int64 nPort = mxElementInfoModule.QueryPropertyInt(strConfigName, "Port");
                        Int64 nMaxConnect = mxElementInfoModule.QueryPropertyInt(strConfigName, "MaxOnline");
                        Int64 nCpus = mxElementInfoModule.QueryPropertyInt(strConfigName, "CpuCount");
                        string strName = mxElementInfoModule.QueryPropertyString(strConfigName, "Name");
                        string strIP = mxElementInfoModule.QueryPropertyString(strConfigName, "IP");

                        GetNetHandler().RegisterEventCallback(OnNetSocketEvent);
                        GetNetHandler().RegisterPackCallback(-1, OnNetRecivePack);

                        Initialization(NFIMsgHead.NF_Head.NF_HEAD_LENGTH, (UInt32)nMaxConnect, (UInt16)nPort);
                    }
                }
            }
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
        //////////////////////////////////////////////////////////////////////////

        protected void OnNetSocketEvent(UInt32 nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet)
        {
            switch(eEvent)
            {
                case NFINet.NF_NET_EVENT.NF_NET_EVENT_CONNECTED:
                    {
                        //OnClientDisconnect(nSockIndex);
                    }
                    break;
                default:
                    {
                        //OnClientConnected(nSockIndex);
                    }
                    break;
            }
        }
        protected void OnNetRecivePack(NFIPacket msg, NFINet pNet)
        {
            int nMsgID = msg.GetMsgID();
            switch ((NFMsg.EGameMsgID)nMsgID)
            {
                case NFMsg.EGameMsgID.EGMI_STS_HEART_BEAT:
                    break;
                case NFMsg.EGameMsgID.EGMI_MTL_WORLD_REGISTERED:
                    OnWorldRegisteredProcess(msg);
                    break;

                case NFMsg.EGameMsgID.EGMI_MTL_WORLD_UNREGISTERED:
                    OnWorldUnRegisteredProcess(msg);
                    break;

                case NFMsg.EGameMsgID.EGMI_MTL_WORLD_REFRESH:
                    OnRefreshWorldInfoProcess(msg);
                    break;

                case NFMsg.EGameMsgID.EGMI_LTM_LOGIN_REGISTERED:
                    OnLoginRegisteredProcess(msg);
                    break;

                case NFMsg.EGameMsgID.EGMI_LTM_LOGIN_UNREGISTERED:
                    OnLoginUnRegisteredProcess(msg);
                    break;

                case NFMsg.EGameMsgID.EGMI_LTM_LOGIN_REFRESH:
                    OnRefreshLoginInfoProcess(msg);
                    break;

                case NFMsg.EGameMsgID.EGMI_REQ_CONNECT_WORLD:
                    OnSelectWorldProcess(msg);
                    break;

                case NFMsg.EGameMsgID.EGMI_ACK_CONNECT_WORLD:
                    OnSelectServerResultProcess(msg);
                    break;

                default:
                    break;
            }
        }

        //世界服务器注册，刷新信息
        protected void OnWorldRegisteredProcess(NFIPacket msg)
        {
            //NFMsg.MsgBase xMsg = new NFMsg.MsgBase();
            //xMsg = Serializer.Deserialize<NFMsg.MsgBase>(stream);

            //NFMsg.ServerInfoReportList xMsg = new NFMsg.ServerInfoReportList();
            //msg.GetData

        }
	    protected void OnWorldUnRegisteredProcess(NFIPacket msg)
        {

        }
        protected void OnRefreshWorldInfoProcess(NFIPacket msg)
        {

        }

        //////////////////////////////////////////////////////////////////////////
        //登录服务器注册，刷新信息
        protected void OnLoginRegisteredProcess(NFIPacket msg)
        {

        }
        protected void OnLoginUnRegisteredProcess(NFIPacket msg)
        {

        }
        protected void OnRefreshLoginInfoProcess(NFIPacket msg)
        {

        }

        //选择世界服务器消息
        protected void OnSelectWorldProcess(NFIPacket msg)
        {

        }
        protected void OnSelectServerResultProcess(NFIPacket msg)
        {

        }

        //////////////////////////////////////////////////////////////////////////

        protected void SynWorldToLogin()
        { }
    }
}
