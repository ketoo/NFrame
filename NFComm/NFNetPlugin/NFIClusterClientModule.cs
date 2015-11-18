//-----------------------------------------------------------------------
// <copyright file="NFClusterClientModule.cs">
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
    public class NFIClusterClientModule : NFILogicModule
    {
        public class ConnectObjectData
        {
            public ConnectObjectData()
            {
            }

            public float fMaxReconnectTime;//重连时间
            public DateTime xNextTriggerTime = new DateTime();//下次重连时间

            public long nServerID;
            public NFServer_def.NF_SERVER_TYPES meServerType;
            public string mstrIP;
            public string mstrDns;
            public string mstrAuth;

            public long mnPort;
            public long mnAreaID;
            public NFNetModule xNetModule;
            public NFClusterClientModule.ClusterClientState eState;
        }

        class NFCMachineNode : NFIVirtualNode
        {
            public enum EConstDefine
            {
                EConstDefine_DefaultWeith = 500,//权重
            }

            public NFCMachineNode(int nVirID) : base(nVirID)
            {
                strIP = "";
                nPort = 0;
                nWeight = 0;//总共多少权重即是多少虚拟节点
                nMachineID = 0;
            }

            public NFCMachineNode()
            {
                strIP = "";
                nPort = 0;
                nWeight = 0;//总共多少权重即是多少虚拟节点
                nMachineID = 0;
            }

            public override string GetDataStr()
            {
                return strIP;
            }

            public override long GetDataID()
            {
                return nMachineID;
            }

            public string strIP;
            public long nPort;
            public long nWeight;
            public long nMachineID;
        };

         public NFIVirtualNode GetServerMachineData(int nServeID)
        {
            int nHashKey = nServeID.GetHashCode();
            return mxConsistentHash.GetSuitNodeHashKey(nHashKey);
        }

        void AddServerWeightData(ConnectObjectData xInfo)
        {
            //根据权重创建节点
            for (int j = 0; j < (int)NFCMachineNode.EConstDefine.EConstDefine_DefaultWeith; ++j)
            {
                NFCMachineNode vNode = new NFCMachineNode(j);

                vNode.nMachineID = xInfo.nServerID;
                vNode.strIP = xInfo.mstrIP;
                vNode.nPort = xInfo.mnPort;
                vNode.nWeight = (long)NFCMachineNode.EConstDefine.EConstDefine_DefaultWeith;
                mxConsistentHash.Insert(vNode);
            }
        }

        void RemoveServerWeightData(ConnectObjectData xInfo)
        {
            for (int j = 0; j < (int)NFCMachineNode.EConstDefine.EConstDefine_DefaultWeith; ++j)
            {
                NFCMachineNode vNode = new NFCMachineNode(j);

                vNode.nMachineID = xInfo.nServerID;
                vNode.strIP = xInfo.mstrIP;
                vNode.nPort = xInfo.mnPort;
                vNode.nWeight = (long)NFCMachineNode.EConstDefine.EConstDefine_DefaultWeith;
                mxConsistentHash.Erase(vNode);
            }
        }

        NFIConsistentHash mxConsistentHash = new NFCConsistentHash();
    }
}
