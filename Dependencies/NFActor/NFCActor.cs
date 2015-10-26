//-----------------------------------------------------------------------
// <copyright file="NFCActor.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NFrame
{
    public class NFCActor : NFIActor
    {
        public NFCActor(NFIDENTID xID, NFIActorMng xActorMng)
        {
            mxID = xID;
            mxActorMng = xActorMng;
        }

        public override NFIDENTID GetAddress()
        {
            return mxID;
        }

        public override int GetNumQueuedMessages()
        {
            return mxMessageQueue.Count;
        }

        public static void ExecuteAsync(object x)
        {
            NFIActor xActor = (NFIActor)x;
            if (null != xActor)
            {
                xActor.Execute();
            }
        }

        public override void Execute()
        {
            NFIActorMessage xMsg;
            while (mxMessageQueue.TryDequeue(out xMsg) && null != xMsg)
            {
                if (xMsg.nMasterActor != GetAddress())
                {
                    return;
                }

                if (null == xMsg.xMasterHandler)
                {
                    return;
                }

                if (!xMsg.bAsync)
                {
                    return;
                }

                if (null != xMsg.xMasterHandler)
                {
                    foreach (Handler xHandler in xMsg.xMasterHandler)
                    {
                        xHandler(xMsg);
                    }
                }
            }
        }

        public override bool RegisterHandler(Handler handler)
        {
            mxMessageHandler.Enqueue(handler);
            return true;
        }

        public override bool PushMessages(NFIDENTID from, NFIActorMessage xMessage)
        {
            xMessage.nMasterActor = mxID;
            xMessage.nFromActor = from;

            if (null != mxMessageHandler)
            {
                xMessage.xMasterHandler = new ConcurrentQueue<NFIActor.Handler>(mxMessageHandler);
            }

            if (!xMessage.bAsync)
            {
                //同步消息，也不用排队，就等吧
                ProcessMessageSyns(xMessage);
            }
            else 
            {
                //异步消息，需要new新的msg，否则担心masteractor还需使用它
                NFIActorMessage xMsg = new NFIActorMessage(xMessage);
                mxMessageQueue.Enqueue(xMsg);
                NFIScheduler xScheduler = mxActorMng.GetScheduler();
                if(null != xScheduler)
                {
                    xScheduler.AddToScheduler(mxID);
                }
            }

            return true;
        }

        public override bool AddComponent(Type xType)
        {
            NFBehaviour xCom = Activator.CreateInstance(xType) as NFBehaviour;
            return mxComponentDic.TryAdd(xType, xCom);
        }

        public override NFBehaviour GetComponent(Type xType)
        {
            NFBehaviour xCom;
            if (mxComponentDic.TryGetValue(xType, out xCom))
            {
                return xCom;
            }

            return null;
        }
        /////////////////////////////////////////////////////////////

        private static void TaskMethodSync(object param)
        {
            NFIActorMessage xMsg = (NFIActorMessage)param;

            if (null != xMsg.xMasterHandler)
            {
                foreach (Handler xHandler in xMsg.xMasterHandler)
                {
                    xHandler(xMsg);
                }
            }
        }

        private void ProcessMessageSyns(NFIActorMessage xMessage)
        {
            if (xMessage.nMasterActor != GetAddress())
            {
                return;
            }

            if (null == xMessage.xMasterHandler)
            {
                return;
            }
            if (xMessage.bAsync)
            {
                return;
            }

            Task xTask = Task.Factory.StartNew(TaskMethodSync, xMessage);
            if (null != xTask)
            {
                //同步消息需要wait
                xTask.Wait();
            }
        }

        private NFIActorMng GetActorMng()
        {
            return mxActorMng;
        }


        /////////////////////////////////////////////////////////////
        private readonly NFIDENTID mxID;
        private readonly NFIActorMng mxActorMng;

        private readonly ConcurrentDictionary<Type, NFBehaviour> mxComponentDic = new ConcurrentDictionary<Type, NFBehaviour>();
        private readonly ConcurrentQueue<NFIActorMessage> mxMessageQueue = new ConcurrentQueue<NFIActorMessage>();
        private readonly ConcurrentQueue<Handler> mxMessageHandler = new ConcurrentQueue<Handler>();
    }
}
