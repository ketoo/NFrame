//-----------------------------------------------------------------------
// <copyright file="NFCHeartBeatManager.cs">
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
	public class NFCHeartBeatManager : NFIHeartBeatManager
    {
		public NFCHeartBeatManager(NFGUID self)
		{
			mSelf = self;
            mhtHeartBeat = new Dictionary<string, NFIHeartBeat>();
		}

        public override void AddHeartBeat(string strHeartBeatName, float fTime, NFIHeartBeat.HeartBeatEventHandler handler, int nCount)
		{
			if (!mhtHeartBeat.ContainsKey(strHeartBeatName))
			{
                NFIHeartBeat xHeartBeat = new NFCHeartBeat(mSelf, strHeartBeatName, fTime);
                mhtHeartBeat.Add(strHeartBeatName, xHeartBeat);
                xHeartBeat.RegisterCallback(handler);
			}
		}

        public override bool FindHeartBeat(string strHeartBeatName)
        {
            if (!mhtHeartBeat.ContainsKey(strHeartBeatName))
            {
                return true;
            }

            return false;
        }
        
        public override void RemoveHeartBeat(string strHeartBeatName)
        {
            if (!mhtHeartBeat.ContainsKey(strHeartBeatName))
            {
                mhtHeartBeat.Remove(strHeartBeatName);
            }
        }

		public override void Update(float fPassTime)
		{

            NFIDataList keyList = null;

            foreach (KeyValuePair<string, NFIHeartBeat> kv in mhtHeartBeat)
            {
                NFIHeartBeat heartBeat = (NFIHeartBeat)kv.Value;
                if (heartBeat.Update(fPassTime))
                {
                    if (null == keyList)
                    {
                        keyList = new NFCDataList();
                    }

                    keyList.AddString((string)kv.Key);
                }
            }

            if (null != keyList)
            {
                for (int i = 0; i < keyList.Count(); i++)
                {
                    mhtHeartBeat.Remove(keyList.StringVal(i));
                }
            }
		}

		NFGUID mSelf;
        Dictionary<string, NFIHeartBeat> mhtHeartBeat;
    }
}