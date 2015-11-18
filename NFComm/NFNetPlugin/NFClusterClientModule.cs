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
    public class NFClusterClientModule : NFIClusterClientModule
    {
        public enum ClusterClientState
        {
            ADDING,
            CONNECTING,
            CONNECTED,
            DISCONNECT,
            RECONNECT,
        }

        public NFNetHandler GetNetHandler()
        {
            return mxNetHandler;
        }
        public override void Execute()
        {
            foreach(var kv in mxNetDic)
            {
                switch(kv.Value.eState)
                {
                    case ClusterClientState.ADDING:
                        {
                            //kv.Value.xNetModule
                        }
                        break;
                    case ClusterClientState.CONNECTING:
                        {
                            kv.Value.xNetModule.Execute();
                        }
                        break;
                    case ClusterClientState.CONNECTED:
                        {
                            kv.Value.xNetModule.Execute();
                        }
                        break;
                    case ClusterClientState.DISCONNECT:
                        {
                            kv.Value.eState = ClusterClientState.RECONNECT;
                        }
                        break;
                    case ClusterClientState.RECONNECT:
                        {
                            //kv.Value.xNetModule
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void AddNetInfo(ConnectObjectData xData)
        {
            if(null == xData)
            {
                return;
            }

            if (mxNetDic.ContainsKey(xData.nServerID))
            {
                //或者，更新ip等
                return;
            }

            xData.eState = ClusterClientState.ADDING;
            mxNetDic.Add(xData.nServerID, xData);
        }

        private readonly NFNetHandler mxNetHandler = new NFNetHandler();
        private readonly Dictionary<long, ConnectObjectData> mxNetDic = new Dictionary<long, ConnectObjectData>();
    }
}
