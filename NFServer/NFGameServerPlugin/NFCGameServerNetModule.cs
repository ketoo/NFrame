//-----------------------------------------------------------------------
// <copyright file="NFCGameServerNetModule.cs">
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
    class NFCGameServerNetModule : NFIGameServerNetModule
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
                    Int64 nServerType = mxElementInfoModule.QueryPropertyInt(strConfigName, "Type");
                    Int64 nServerID = mxElementInfoModule.QueryPropertyInt(strConfigName, "ServerID");
                    if (nServerType == (long)NFServer_def.NF_SERVER_TYPES.NF_ST_GAME && GetMng().GetAPPID() == nServerID)
                    {
                        Int64 nPort = mxElementInfoModule.QueryPropertyInt(strConfigName, "Port");
                        Int64 nMaxConnect = mxElementInfoModule.QueryPropertyInt(strConfigName, "MaxOnline");
                        Int64 nCpus = mxElementInfoModule.QueryPropertyInt(strConfigName, "CpuCount");
                        string strName = mxElementInfoModule.QueryPropertyString(strConfigName, "Name");
                        string strIP = mxElementInfoModule.QueryPropertyString(strConfigName, "IP");

                        //GetNetHandler().RegisterEventCallback(OnSocketEvent);
                        //GetNetHandler().RegisterPackCallback(-1, OnRecivePack);

                        Initialization((UInt32)nMaxConnect, (UInt16)nPort);
                    }
                }
            }
        }
    }
}
