//-----------------------------------------------------------------------
// <copyright file="NFCScheduler.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NFrame
{
    public class NFCScheduler : NFIScheduler
    {
        public NFCScheduler(NFIActorMng xMng)
        {
            ThreadPool.SetMaxThreads(1024, 1024);
            ThreadPool.SetMinThreads(1024, 1024);

            mxActorMng = xMng;
            mxTask = Task.Factory.StartNew(ExecuteScheduler, this);
        }

        private static void ExecuteScheduler(object x)
        {
            NFIScheduler xScheduler = (NFIScheduler)x;
            while (true && null != xScheduler)
            {
                //Thread.Sleep(5);

                xScheduler.Execute();
            }
        }
        /////////////////////////////////////////////////////////////
        public override void AddToScheduler(NFIDENTID xID)
        {
            mxWaitScheduler.Enqueue(xID);
        }

        public override bool Execute() 
        {
            NFIDENTID xID = null;
            while (mxWaitScheduler.TryDequeue(out xID))
            {
                NFIActor xActor = mxActorMng.GetActor(xID);
                if (null != xActor && xActor.GetNumQueuedMessages() > 0)
                {
                    //分配一个空闲的thread去隔天actor执行任务
                    Task.Factory.StartNew(NFCActor.ExecuteAsync, xActor);
                }
            }

            return false; 
        }

        /////////////////////////////////////////////////////////////

        private readonly NFIActorMng mxActorMng;
        private readonly Task mxTask;
        private readonly ConcurrentQueue<NFIDENTID> mxWaitScheduler = new ConcurrentQueue<NFIDENTID>();
    }
}
