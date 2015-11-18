//-----------------------------------------------------------------------
// <copyright file="NFCGameToWorldModule.cs">
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
    class NFCGameToWorldModule : NFIGameToWorldModule
    {
        protected NFIKernelModule mxKernelModule;
        protected NFILogicClassModule mxLogicClassModule;
        protected NFIElementModule mxElementInfoModule;
        protected NFIEventModule mxEventProcessModule;

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
            mxElementInfoModule = GetMng().GetModule<NFIElementModule>();

            System.Diagnostics.Debug.Assert(null != mxEventProcessModule);
            System.Diagnostics.Debug.Assert(null != mxKernelModule);
            System.Diagnostics.Debug.Assert(null != mxLogicClassModule);
            System.Diagnostics.Debug.Assert(null != mxElementInfoModule);

            NFILogicClass xLogicClass = mxLogicClassModule.GetElement("Server");
            if (xLogicClass != null)
            {
                List<string> xNameList = xLogicClass.GetConfigNameList();

                foreach (string strConfigName in xNameList)
                {
                    long nServerID = mxElementInfoModule.QueryPropertyInt(strConfigName, "ServerID");
                    long nServerType = mxElementInfoModule.QueryPropertyInt(strConfigName, "Type");
                    if (nServerType == (long)NFServer_def.NF_SERVER_TYPES.NF_ST_WORLD)
                    {
                        Int64 nPort = mxElementInfoModule.QueryPropertyInt(strConfigName, "Port");
                        Int64 nMaxConnect = mxElementInfoModule.QueryPropertyInt(strConfigName, "MaxOnline");
                        Int64 nCpus = mxElementInfoModule.QueryPropertyInt(strConfigName, "CpuCount");
                        Int64 nAreaID = mxElementInfoModule.QueryPropertyInt(strConfigName, "AreaID");
                        string strName = mxElementInfoModule.QueryPropertyString(strConfigName, "Name");
                        string strIP = mxElementInfoModule.QueryPropertyString(strConfigName, "IP");

                        ConnectObjectData xConnectObjectData = new ConnectObjectData();

                        xConnectObjectData.fMaxReconnectTime = 10.0f;//重连时间
                        //xConnectObjectData.xNextTriggerTime = new DateTime();//下次重连时间

                        xConnectObjectData.nServerID = nServerID;
                        xConnectObjectData.meServerType = (NFServer_def.NF_SERVER_TYPES)nServerType;
                        xConnectObjectData.mstrIP = strIP;
                        xConnectObjectData.mstrDns = strIP;
                        xConnectObjectData.mstrAuth = "";

                        xConnectObjectData.mnPort = nPort;
                        xConnectObjectData.mnAreaID = nAreaID;
                        //public NFNetModule xNetModule;
                        xConnectObjectData.eState = NFClusterClientModule.ClusterClientState.ADDING;

                        //GetNetHandler().RegisterEventCallback(OnSocketEvent);
                        //GetNetHandler().RegisterPackCallback(-1, OnRecivePack);

                        AddNetInfo(xConnectObjectData);
                    }
                }
            }
        }
    }
}
