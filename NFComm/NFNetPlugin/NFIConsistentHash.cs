//-----------------------------------------------------------------------
// <copyright file="NFIConsistentHash.cs">
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
        //虚拟节点
    public abstract class NFIVirtualNode 
    {
        private int nVirtualIndex;//虚拟节点序号

        //主机IP，此主机的第几个虚节点序号
        public NFIVirtualNode( int nVirID)
        {
            nVirtualIndex = nVirID;
        }

	    public NFIVirtualNode()
	    {
		    nVirtualIndex = 0;
	    }

	    public abstract string GetDataStr();
	    public abstract long GetDataID();

        public string ToStr()  
        {
            return GetDataID().ToString() + GetDataStr() + nVirtualIndex.ToString(); 
        }

    };

    public abstract class NFIHasher
    {
        public abstract int GetHashValue(NFIVirtualNode vNode);
    };

    public abstract class NFIConsistentHash : NFBehaviour
    {
        public abstract int Size();
        public abstract bool Empty();
        public abstract void Insert(NFIVirtualNode xNode);
        public abstract bool Exist(NFIVirtualNode xNode);
        public abstract bool Erase(NFIVirtualNode xNode);
        public abstract NFIVirtualNode GetSuitNode();
        public abstract NFIVirtualNode GetSuitNodeHashKey(int nHashValue);

        public NFIHasher mxHasher = null;

    }
}
