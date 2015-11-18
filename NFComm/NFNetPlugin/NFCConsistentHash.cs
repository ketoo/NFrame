//-----------------------------------------------------------------------
// <copyright file="NFINetModule.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{   

    public class NFCHasher : NFIHasher
    {
        public override int GetHashValue(NFIVirtualNode vNode)
        {
            return vNode.ToStr().GetHashCode();
        }
    };

    public class NFCConsistentHash : NFIConsistentHash
    {
        public NFCConsistentHash()
        {
            mxHasher = new NFCHasher();
        }

        public override int Size()
        {
            return mxNetDic.Count;
        }

        public override bool Empty()
        {
            if (mxNetDic.Count <= 0)
            {
                return true;
            }
            
            return false;
        }

        public override void Insert(NFIVirtualNode xNode)
        {
            int nHashKey = mxHasher.GetHashValue(xNode);
            mxNetDic.Add(nHashKey, xNode);
        }

        public override bool Exist(NFIVirtualNode xNode)
        {
            int nHashKey = mxHasher.GetHashValue(xNode);
            return mxNetDic.ContainsKey(nHashKey);
        }

        public override bool Erase(NFIVirtualNode xNode)
        {
            int nHashKey = mxHasher.GetHashValue(xNode);
            return mxNetDic.Remove(nHashKey);
        }

        public override NFIVirtualNode GetSuitNode()
        {
            return GetSuitNodeHashKey(0);
        }

        public override NFIVirtualNode GetSuitNodeHashKey(int nHashValue)
        {
            if (!mxNetDic.ContainsKey(nHashValue))
            {
                var tailMap = from coll in mxNetDic
                              where coll.Key > nHashValue
                              select new { coll.Key };
                int key;
                if (tailMap == null || tailMap.Count() == 0)
                    key = mxNetDic.FirstOrDefault().Key;
                else
                    key = tailMap.FirstOrDefault().Key;
            }

            return mxNetDic[nHashValue];
        }

        private SortedList<int, NFIVirtualNode> mxNetDic = new SortedList<int, NFIVirtualNode>();
    }

}
