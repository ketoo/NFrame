//-----------------------------------------------------------------------
// <copyright file="NFIPlugin.cs">
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
    public class NFIPlugin : NFILogicModule
    {
        public bool CreateModule<T>()
        {
            Type xType = typeof(T);
            if (xType.IsSubclassOf(typeof(NFILogicModule)))
            {
                NFILogicModule xModule = Activator.CreateInstance(xType) as NFILogicModule;


                GetMng().AddModule(xType, xModule);

                return true;
            }

            return false;
        }
    }
}
