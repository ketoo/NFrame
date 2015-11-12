using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    class NFCProxyServerNet_ServerModule : NFIProxyServerNet_ServerModule
    {
        protected NFILogicModule m_pLogicClassModule;

        protected Dictionary<NFGUID, int> mxClientIdent;

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

        public override void AfterInit() { }

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
