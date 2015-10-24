//-----------------------------------------------------------------------
// <copyright file="NFIActorMessage.cs">
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
    public class NFIActorMessage
    {
        public NFIActorMessage()
        {
            eType = EACTOR_MESSAGE_ID.EACTOR_UNKNOW;
            bAsync = true;
            bReturn = true;
            nSubMsgID = 0;

            t = new System.DateTime();
            t = System.DateTime.Now;
        }

        public NFIActorMessage(NFIActorMessage x)
        {
            eType = x.eType;
            bAsync = x.bAsync;
            bReturn = x.bReturn;
	        nSubMsgID = x.nSubMsgID;
            nFromActor = x.nFromActor;
            nMasterActor = x.nMasterActor;
            if (null != x.xMasterHandler)
            {
                xMasterHandler = new ConcurrentQueue<NFIActor.Handler>(x.xMasterHandler);
            }

            if (null != x.data)
            {
                data = (string)x.data.Clone();
            }
        }

	    public enum EACTOR_MESSAGE_ID
	    {
            EACTOR_UNKNOW,

		    EACTOR_INIT,
		    EACTOR_AFTER_INIT,
		    EACTOR_EXCUTE,
		    EACTOR_BEFORE_SHUT,
		    EACTOR_SHUT,

		    EACTOR_NET_MSG,
            EACTOR_TRANS_MSG,
		    EACTOR_LOG_MSG,
            EACTOR_EVENT_MSG,
            EACTOR_DATA_MSG,

            EACTOR_TEST_MSG,
	    }

        public EACTOR_MESSAGE_ID eType;
        public bool bAsync;//true=异步,false=同步,默认异步
        public bool bReturn;
	    public int nSubMsgID;
        public NFIDENTID nFromActor;
        public NFIDENTID nMasterActor;
        public ConcurrentQueue<NFIActor.Handler> xMasterHandler;
	    public string data;
        public DateTime t;
    }
}
