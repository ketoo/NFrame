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

//         public abstract bool AddModule(Type xType);
//         public abstract NFBehaviour GetModule(Type xType);
        Dictionary<string, NFILogicModule>  mhtObject = new Dictionary<string, NFILogicModule>();

        public NFILogicModule GetModule(string strClassName)
        {
            return null;
        }

        public T GetModule<T>()
        {
            return default(T);
        }

        public bool AddModule(Type xType, NFILogicModule xModule)
        {
            AddModule(xType.ToString(), xModule);

            return false;
        }

        public bool AddModule(string strClassName, NFILogicModule xModule)
        {
            

            return false;
        }
    }
}
