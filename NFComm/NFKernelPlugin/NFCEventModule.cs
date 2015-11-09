//-----------------------------------------------------------------------
// <copyright file="NFCEventModule.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace NFrame
{
	public class NFCEventModule : NFIEventModule
    {
		public NFCEventModule(NFGUID self)
		{
			mSelf = self;
            mhtEvent = new Dictionary<int, NFIEvent>();
		}

		public override void RegisterCallback(int nEventID, NFIEvent.EventHandler handler, NFIDataList valueList)
		{
			if (!mhtEvent.ContainsKey(nEventID))
			{
				mhtEvent.Add(nEventID, new NFCEvent(mSelf, nEventID, valueList));
			}

			NFIEvent identEvent = (NFIEvent)mhtEvent[nEventID];
			identEvent.RegisterCallback(handler);

		}

		public override void DoEvent(int nEventID, NFIDataList valueList)
		{
			if (mhtEvent.ContainsKey(nEventID))
			{
				NFIEvent identEvent = (NFIEvent)mhtEvent[nEventID];
				identEvent.DoEvent(valueList);
			}
		}

		NFGUID mSelf;
        Dictionary<int, NFIEvent> mhtEvent;
    }
}