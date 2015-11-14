using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public abstract class NFIProxyServerNet_ServerModule : NFNetModule
    {

        public abstract int Transpond(NFIPacket msg);

        public abstract int EnterGameSuccessEvent(NFGUID xClientID, NFGUID xPlayerID);
    }
}
