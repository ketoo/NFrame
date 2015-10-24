//-----------------------------------------------------------------------
// <copyright file="NFCActorMng.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace NFrame
{
    public class NFCActorMng : NFIActorMng
    {
        private static NFCActorMng _instance;
        private static readonly object _syncLock = new object();
        public static NFIActorMng Intance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new NFCActorMng();
                    }
                }
            }

            return _instance;
        }

        private NFCActorMng()
        {
            mxScheduler = new NFCScheduler(this);
        }

        public override NFIDENTID CreateActor()
        {
             return CreateActor(null);
        }

        public override NFIDENTID CreateActor(NFIActor.Handler handler)
        {
            NFIDENTID xID = new NFIDENTID(0, ++mnActorIndex);
            NFIActor xActor = new NFCActor(xID, this);

            //添加仍旧有问题，foreach中万一有其他线程添加
            bool bRet = mxActorDic.TryAdd(xID, xActor);
            if (bRet)
            {
                if (null != handler)
                {
                    RegisterHandler(xID, handler);

                    NFIActorMessage xMessage = new NFIActorMessage();
                    xMessage.bAsync = false;//同步消息
                    xMessage.bReturn = false;//无需返回

                    xMessage.eType = NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_INIT;
                    SendMsg(xActor.GetAddress(), null, xMessage);

                    xMessage.eType = NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_AFTER_INIT;
                    SendMsg(xActor.GetAddress(), null, xMessage);
                }
                
                return xID;
            }

            return null;
        }

        public override NFIScheduler GetScheduler()
        {
            return mxScheduler;
        }

        //运行过程中不能释放全部
        public override void ReleaseAllActor()
        {
            foreach (var kv in mxActorDic)
            {
                ReleaseActor(kv.Value);
            }
        }

        public override bool ReleaseActor(NFIDENTID xID)
        {
            if (null == xID)
            {
                return false; ;
            }

            NFIActor xActor = null;
            if (mxActorDic.TryRemove(xID, out xActor))
            {
                ReleaseActor(xActor);
            }

            return true;
        }

        public override bool RegisterHandler(NFIDENTID xID, NFIActor.Handler handler)
        {
            if (null == xID || null == handler)
            {
                return false;
            }

            NFIActor xActor = GetActor(xID);
            if (null != xActor)
            {
                xActor.RegisterHandler(handler);

                return true;
            }

            return false;
        }


        public override bool SendMsg(NFIDENTID address, NFIDENTID from, NFIActorMessage xMessage)
        {
            if (null == address || null == xMessage)
            {
                return false; ;
            }

            NFIActor xActor = GetActor(address);
            if (null != xActor)
            {
                return xActor.PushMessages(from, xMessage);
            }

            return false;
        }

        public override bool Execute()
        {
            //调度，send的时候记录哪些有(异步)消息，然后这里统一调度过去
            return false; 
        }

        public override NFIActor GetActor(NFIDENTID xID)
        {
            if (null == xID)
            {
                return null;
            }

            NFIActor xActor = null;
            if (mxActorDic.TryGetValue(xID, out xActor))
            {
                return xActor;
            }

            return null;
        }

        ///////////////////////////////////////////////////////

        private bool ReleaseActor(NFIActor xActor)
        {
            if (null == xActor)
            {
                return false;
            }

            NFIActorMessage xMessage = new NFIActorMessage();
            xMessage.bAsync = false;//同步消息

            xMessage.eType = NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_BEFORE_SHUT;
            xActor.PushMessages(null, xMessage);

            xMessage.eType = NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_SHUT;
            xActor.PushMessages(null, xMessage);

            return true;
        }
        ///////////////////////////////////////////////////////
        private readonly NFIScheduler mxScheduler;
        private readonly ConcurrentDictionary<NFIDENTID, NFIActor> mxActorDic = new ConcurrentDictionary<NFIDENTID, NFIActor>();
        private int mnActorIndex = 0;
    }
}
