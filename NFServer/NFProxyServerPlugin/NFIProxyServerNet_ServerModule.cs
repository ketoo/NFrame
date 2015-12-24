using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public abstract class NFIProxyServerNet_ServerModule : NFNetModule
    {

        public abstract int Transpond(UInt32 nSockIndex, UInt16 nMsgID, string msg);

        public abstract int EnterGameSuccessEvent(NFGUID xClientID, NFGUID xPlayerID);
    }
}
