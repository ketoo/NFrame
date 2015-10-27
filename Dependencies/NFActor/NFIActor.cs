//-----------------------------------------------------------------------
// <copyright file="NFIActor.cs">
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
    public abstract class NFIActor : NFBehaviour
    {
        public delegate void Handler(NFIActorMessage xMessage);

        public abstract bool RegisterHandler(Handler handler);

        public abstract NFIDENTID GetAddress();
        public abstract int GetNumQueuedMessages();
        public abstract bool PushMessages(NFIDENTID from, NFIActorMessage xMessage);

        public abstract bool AddComponent(Type xType);
        public abstract NFBehaviour GetComponent(Type xType);

        public T GetComponent<T>()
        {
            Type xType = typeof(T);
            NFBehaviour xBehaviour = GetComponent(xType);
            object xBehaviour = GetComponent(xType);
            if (null != xBehaviour)
            {
                return (T)xBehaviour;
            }

            return default(T);
        }

        public bool AddComponent<T>()
        {
            Type xType = typeof(T);
            if (xType.IsSubclassOf(typeof(NFBehaviour)))
            {
                return AddComponent(xType);
            }

            return false;
        }
    }
}
