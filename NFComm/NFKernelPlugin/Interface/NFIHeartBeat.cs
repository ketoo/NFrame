using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace NFrame
{
	public abstract class NFIHeartBeat
	{
        public delegate void HeartBeatEventHandler(NFGUID self, string strHeartBeat, float fTime, int nCount);

		public abstract void RegisterCallback(NFIHeartBeat.HeartBeatEventHandler handler);
		public abstract bool Update(float fPassTime);
	}
}