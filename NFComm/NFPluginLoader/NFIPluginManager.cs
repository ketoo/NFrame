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
    }
}
