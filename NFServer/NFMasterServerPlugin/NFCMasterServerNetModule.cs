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


                        Initialization(NFIMsgHead.NF_Head.NF_HEAD_LENGTH, OnSocketEvent, OnRecivePack, (UInt32)nMaxConnect, (UInt16)nPort);
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
        protected void OnSocketEvent(UInt32 nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet)
        {

        }
        protected void OnRecivePack(NFIPacket msg)
        {

        }
    }
}
