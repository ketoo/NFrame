//-----------------------------------------------------------------------
// <copyright file="NFCPlugin.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public class NFCPlugin : NFIPlugin
    {
        Assembly xAssembly;
        Type xType;
        Object xPlugin;

        public NFCPlugin(string strLibName)
        {
            mstrLibName = strLibName;
            mstrPluginName = "NFrame." + mstrLibName.Substring(0, mstrLibName.LastIndexOf("."));

            xAssembly = Assembly.LoadFrom(mstrLibName);
            xType = xAssembly.GetType(mstrPluginName);
            xPlugin = Activator.CreateInstance(xType);

        }

        public override void Init()
        {
            MethodInfo xMethod = xType.GetMethod("Init");
            if (xMethod != null)  
            {  
                //xMethod.Invoke(xPlugin, null);  
            }
        }

        public override void AfterInit()
        {
        }

        public override void BeforeShut()
        {
        }

        public override void Shut()
        {
        }

        public override void Execute()
        {
        }

        private string mstrLibName = "";
        private string mstrPluginName = "";
    }
}
