using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    class NFCProxyServerNet_ServerModule : NFIProxyServerNet_ServerModule
    {
        protected NFILogicClassModule m_pLogicClassModule;

        protected Dictionary<NFGUID, int> mxClientIdent;

        //NFIProxyServerToWorldModule* m_pProxyToWorldModule;
        //NFIProxyServerToGameModule* m_pProxyServerToGameModule;
        protected NFIKernelModule m_pKernelModule;
        //protected NFILogModule* m_pLogModule;
        protected NFCElementModule m_pElementInfoModule;
        protected NFCEventModule m_pEventProcessModule;
        protected NFGUID m_pUUIDModule;

        public NFCProxyServerNet_ServerModule(NFIPluginManager p)
        {
            SetMng(p);
        }

        public override int Initialization(NFIMsgHead.NF_Head nHeadLength, NFINet.OnSocketEvent xEventHandler, NFINet.OnRecivePack xPackHandler, string strIP, ushort nPort)
        {
            throw new NotImplementedException();
        }

        public override int Initialization(NFIMsgHead.NF_Head nHeadLength, NFINet.OnSocketEvent xEventHandler, NFINet.OnRecivePack xPackHandler, uint nMaxClient, ushort nPort)
        {
            throw new NotImplementedException();
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
                m_pEventProcessModule = GetMng().GetModule<NFCEventModule>();
                m_pKernelModule = GetMng().GetModule<NFIKernelModule>();

                m_pLogicClassModule = GetMng().GetModule<NFILogicClassModule>();
                //m_pProxyServerToGameModule = dynamic_cast<NFIProxyServerToGameModule*>(pPluginManager->FindModule("NFCProxyServerToGameModule"));
                //m_pProxyToWorldModule = dynamic_cast<NFIProxyServerToWorldModule*>(pPluginManager->FindModule("NFCProxyServerToWorldModule"));
                //m_pLogModule = dynamic_cast<NFILogModule*>(pPluginManager->FindModule("NFCLogModule"));
                m_pElementInfoModule = GetMng().GetModule<NFCElementModule>();
                //m_pUUIDModule = GetMng().GetModule<NFGUID>();

                NFILogicClass xLogicClass = m_pLogicClassModule.GetElement("Server");
                if (xLogicClass != null)
                {
                    List<string> xNameList = xLogicClass.GetConfigNameList();
                    
                    foreach(string strConfigName in xNameList)
                    {
                        Int64 nServerType = m_pElementInfoModule.QueryPropertyInt(strConfigName, "Type");
                        Int64 nServerID = m_pElementInfoModule.QueryPropertyInt(strConfigName, "ServerID");
                        if (nServerType == (Int64)NFrame.NFServer_def.NF_SERVER_TYPES.NF_ST_PROXY && GetMng().GetAPPID() == nServerID)
                        {
                            Int64 nPort = m_pElementInfoModule.QueryPropertyInt(strConfigName, "Port");
                            Int64 nMaxConnect = m_pElementInfoModule.QueryPropertyInt(strConfigName, "MaxOnline");
                            Int64 nCpus = m_pElementInfoModule.QueryPropertyInt(strConfigName, "CpuCount");
                            string strName = m_pElementInfoModule.QueryPropertyString(strConfigName, "Name");
                            string strIP = m_pElementInfoModule.QueryPropertyString(strConfigName, "IP");

                            //m_pUUIDModule = nServerID;

                            //Initialization(NFIMsgHead::NF_Head::NF_HEAD_LENGTH, this, &NFCProxyServerNet_ServerModule::OnReciveClientPack, &NFCProxyServerNet_ServerModule::OnSocketClientEvent, nMaxConnect, nPort, nCpus);
                        }
                    }
                }
            
        }

        //public override LogRecive(string str){}

       // public override void LogSend(string str) {}

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
    }
}
