//-----------------------------------------------------------------------
// <copyright file="NFBehaviour.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;

namespace NFrame
{
    public class NFILogicModule : NFBehaviour
    {
        public NFIPluginManager GetMng()
        {
            return mxMng;
        }
        public void SetMng(NFIPluginManager xMng)
        {
            mxMng = xMng;
        }
        private NFIPluginManager mxMng;
    }
}
