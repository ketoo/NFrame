//-----------------------------------------------------------------------
// <copyright file="NFCHeartBeat.cs">
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
	public class NFCHeartBeat : NFIHeartBeat
	{

		public NFCHeartBeat(NFGUID self, string strHeartBeatName, float fTime)
		{
			mSelf = self;
			mstrHeartBeatName = strHeartBeatName;
			mfTime = fTime;
			mfOldTime = fTime;
		}

		public override void RegisterCallback(NFIHeartBeat.HeartBeatEventHandler handler)
		{
			doHandlerDel += handler;
		}

		public override bool Update(float fPassTime)
		{
			mfTime -= fPassTime;
			if (mfTime < 0.0f)
			{
				if (null != doHandlerDel)
				{
					doHandlerDel(mSelf, mstrHeartBeatName, mfOldTime);
				}
				return true;
			}

			return false;
		}

		NFGUID mSelf;
		string mstrHeartBeatName;
		float mfTime;
		float mfOldTime;

		HeartBeatEventHandler doHandlerDel;
    }
}