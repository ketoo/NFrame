//-----------------------------------------------------------------------
// <copyright file="NFIPluginManager.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFrame;

namespace NFrame
{
    public abstract class NFIPluginManager : NFBehaviour
    {
        public abstract void Install();
        public abstract void UnInstall();

        public abstract string GetClassPath();
        public abstract int GetAPPID();
        public abstract int GetAPPType();

        public abstract NFILogicModule GetModule(string strClassName);
        public abstract bool AddModule(string strClassName, NFILogicModule xModule);

        //////////////////////////////////////////////////////////////////////

        public T GetModule<T>()
        {
            Type xType = typeof(T);
            if (xType.ToString().Contains("NFC"))
            {
                xType = xType.BaseType;
            }

            object xModule = GetModule(xType.ToString());
            if(null != xModule)
            {
                return (T)(xModule);
            }

            return default(T);
        }

        public bool AddModule(Type xType, NFILogicModule xModule)
        {
            xModule.SetMng(this);

            AddModule(xType.BaseType.ToString(), xModule);

            return false;
        }

        

    }
}
