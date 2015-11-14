using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    class NFCProxyServerNet_ServerModule : NFIProxyServerNet_ServerModule
    {

        protected readonly Dictionary<NFGUID, int> mxClientIdent = new Dictionary<NFGUID, int>();

        protected NFIProxyToGameModule mxProxyToGameModule;
        protected NFIProxyToWorldModule mxProxyToWorldModule;
        protected NFIKernelModule mxKernelModule;
        protected NFILogicClassModule mxLogicClassModule;
        protected NFIElementModule mxElementInfoModule;
        protected NFIEventModule mxEventProcessModule;

        public NFCProxyServerNet_ServerModule(NFIPluginManager p)
        {
            SetMng(p);
        }

        public override void Init()
        {
        }

        public override void Shut()
        {
        }

        public override void Execute()
        {

        }

        public override void AfterInit()
        {
            mxEventProcessModule = GetMng().GetModule<NFCEventModule>();
            mxKernelModule = GetMng().GetModule<NFIKernelModule>();
            mxLogicClassModule = GetMng().GetModule<NFILogicClassModule>();
            mxProxyToWorldModule = GetMng().GetModule<NFIProxyToWorldModule>();
            mxProxyToGameModule = GetMng().GetModule<NFIProxyToGameModule>();
            mxElementInfoModule = GetMng().GetModule<NFIElementModule>();

            NFILogicClass xLogicClass = mxLogicClassModule.GetElement("Server");
            if (xLogicClass != null)
            {
                List<string> xNameList = xLogicClass.GetConfigNameList();
                
                foreach(string strConfigName in xNameList)
                {
                    Int64 nServerType = mxElementInfoModule.QueryPropertyInt(strConfigName, "Type");
                    Int64 nServerID = mxElementInfoModule.QueryPropertyInt(strConfigName, "ServerID");
                    if (nServerType == (long)NFServer_def.NF_SERVER_TYPES.NF_ST_PROXY && GetMng().GetAPPID() == nServerID)
                    {
                        Int64 nPort = mxElementInfoModule.QueryPropertyInt(strConfigName, "Port");
                        Int64 nMaxConnect = mxElementInfoModule.QueryPropertyInt(strConfigName, "MaxOnline");
                        Int64 nCpus = mxElementInfoModule.QueryPropertyInt(strConfigName, "CpuCount");
                        string strName = mxElementInfoModule.QueryPropertyString(strConfigName, "Name");
                        string strIP = mxElementInfoModule.QueryPropertyString(strConfigName, "IP");

                        GetNetHandler().RegisterEventCallback(OnSocketEvent);
                        GetNetHandler().RegisterPackCallback(-1, OnRecivePack);

                        Initialization(NFIMsgHead.NF_Head.NF_HEAD_LENGTH, (UInt32)nMaxConnect, (UInt16)nPort);
                    }
                }
            }
        }

        public override int Transpond(NFIPacket msg)
        {
            return 0;
        }

        public override int EnterGameSuccessEvent(NFGUID xClientID, NFGUID xPlayerID)
        {
            return 0;
        }

        protected int OnReciveClientPack(NFIPacket msg)
        {
            return 0;
        }

        protected int OnSocketClientEvent(int nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet) 
        {
            return 0;
        }

        protected void OnClientDissconnect(int nAddress)
        { }

        protected void OnClientConnected(int nAddress) 
        { }

        protected int OnConnectKeyProcess(NFIPacket msg)
        {
            return 0;
        }

        protected int OnReqServerListProcess(NFIPacket msg)
        {
            return 0;
        }

        protected int OnSelectServerProcess(NFIPacket msg)
        {
            return 0;
        }

        protected int OnReqRoleListProcess(NFIPacket msg)
        {
            return 0;
        }

        protected int OnReqCreateRoleProcess(NFIPacket msg)
        {
            return 0;
        }

        protected int OnReqDelRoleProcess(NFIPacket msg)
        {
            return 0;
        }

        protected int OnReqEnterGameServer(NFIPacket msg)
        {
            return 0;
        }

        protected int HB_OnConnectCheckTime(NFGUID self, string strHeartBeat, float fTime, int nCount, NFIDataList var)
        {
            return 0;
        }

        protected void OnSocketEvent(UInt32 nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet)
        {

        }
        protected void OnRecivePack(NFIPacket msg)
        {

        }
    }
}
