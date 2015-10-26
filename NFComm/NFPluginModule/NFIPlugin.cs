//-----------------------------------------------------------------------
// <copyright file="NFIPlugin.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public class NFIPlugin : NFBehaviour
    {
        public NFIPlugin()
        {

        }

        public virtual void Init()
        {

        }

        public virtual void AfterInit()
        {

        }

        public virtual void BeforeShut()
        {

        }

        public virtual void Shut()
        {

        }

        public virtual void Execute() 
        {
        }


        private Dictionary<string, NFBehaviour> mxPluginModule;
    }
}
