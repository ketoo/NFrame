//-----------------------------------------------------------------------
// <copyright file="NFNetHandler.cs">
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
    public class NFNetHandler : NFILogicModule
    {
        public void RegisterEventCallback(NFINet.OnSocketEvent xEventHandler)
        {
            mxEventHandlerList.Add(xEventHandler);
        }
        public void RegisterPackCallback(int nMsgdID, NFINet.OnRecivePack xEventHandler)
        {
            List<NFINet.OnRecivePack> xList;
            if(!mxPackHandlerDic.TryGetValue(nMsgdID, out xList))
            {
                xList = new List<NFINet.OnRecivePack>();
            }

            xList.Add(xEventHandler);
        }

        public void OnSocketEvent(UInt32 nSockIndex, NFINet.NF_NET_EVENT eEvent, NFINet pNet)
        {
            foreach(NFINet.OnSocketEvent handler in mxEventHandlerList)
            {
                handler(nSockIndex, eEvent, pNet);
            }
        }

        public void OnRecivePack(UInt32 nSockIndex, UInt16 nMsgID, string msg, NFINet pNet)
        {
            List<NFINet.OnRecivePack> xList;
            if(mxPackHandlerDic.TryGetValue(nMsgID, out xList))
            {
                foreach (NFINet.OnRecivePack handler in xList)
                {
                    handler(nSockIndex, nMsgID, msg, pNet);
                }
            }
            else if(mxPackHandlerDic.TryGetValue(-1, out xList))
            {
                foreach (NFINet.OnRecivePack handler in xList)
                {
                    handler(nSockIndex, nMsgID, msg, pNet);
                }
            }
        }

        private readonly Dictionary<int, List<NFINet.OnRecivePack>> mxPackHandlerDic = new Dictionary<int, List<NFINet.OnRecivePack>>();
        private readonly List<NFINet.OnSocketEvent> mxEventHandlerList = new List<NFINet.OnSocketEvent>();
    }
}
