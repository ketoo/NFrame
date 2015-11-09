//-----------------------------------------------------------------------
// <copyright file="NFIActorMng.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public abstract class NFIActorMng : NFBehaviour
    {
        public abstract NFGUID CreateActor();
        public abstract NFGUID CreateActor(NFIActor.Handler handler);

        public abstract NFIScheduler GetScheduler();

        public abstract NFIActor GetActor(NFGUID xID);
        public abstract bool ReleaseActor(NFGUID xID);

        public abstract void ReleaseAllActor();
        
        public abstract bool RegisterHandler(NFGUID xID, NFIActor.Handler handler); 
        public abstract bool SendMsg(NFGUID address, NFGUID from, NFIActorMessage xMessage);

    }
}
