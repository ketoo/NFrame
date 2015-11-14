//-----------------------------------------------------------------------
// <copyright file="NFCPlugin.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
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
    public class NFCDynLib : NFBehaviour
    {
        Assembly xAssembly;
        Type xType;
        NFIPlugin xPlugin;

        public NFCDynLib(string strLibName, NFIPluginManager xMng)
        {
            mstrLibName = strLibName;
            mstrPluginName = "NFrame." + mstrLibName.Substring(0, mstrLibName.LastIndexOf("."));

            xAssembly = Assembly.LoadFrom(mstrLibName);
            xType = xAssembly.GetType(mstrPluginName);

            xPlugin = Activator.CreateInstance(xType) as NFIPlugin;
            xPlugin.SetMng(xMng);

            //xPlugin.Install();
            xPlugin.Init();
            xPlugin.AfterInit();
        }

        ~NFCDynLib()
        {
            //xPlugin.UnInstall();
            xPlugin.BeforeShut();
            xPlugin.Shut();
        }

        public override void Init()
        {
            xPlugin.Init();
            //MethodInfo xMethod = xType.GetMethod("Init");
            //if (xMethod != null)  
            //{
            //    xMethod.Invoke(xPlugin, null);  
            //}
        }

        public override void AfterInit()
        {
            xPlugin.AfterInit();

            //MethodInfo xMethod = xType.GetMethod("AfterInit");
            //if (xMethod != null)
            //{
            //    xMethod.Invoke(xPlugin, null);
            //}
        }

        public override void BeforeShut()
        {
            xPlugin.BeforeShut();

            //MethodInfo xMethod = xType.GetMethod("BeforeShut");
            //if (xMethod != null)
            //{
            //    xMethod.Invoke(xPlugin, null);
            //}
        }

        public override void Shut()
        {
            xPlugin.Shut();

            //MethodInfo xMethod = xType.GetMethod("Shut");
            //if (xMethod != null)
            //{
            //    xMethod.Invoke(xPlugin, null);
            //}
        }

        public override void Execute()
        {
            xPlugin.Execute();

            //MethodInfo xMethod = xType.GetMethod("Execute");
            //if (xMethod != null)
            //{
            //    xMethod.Invoke(xPlugin, null);
            //}
        }

        private string mstrLibName = "";
        private string mstrPluginName = "";
    }
}
