//-----------------------------------------------------------------------
// <copyright file="NFCPluginManager.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFrame;
using System.Collections.Concurrent;

namespace NFrame
{
    public class NFCPluginManager : NFIPluginManager
    {
        private static NFCPluginManager _instance;
        private static readonly object _syncLock = new object();
        public static NFCPluginManager Intance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new NFCPluginManager();
                    }
                }
            }

            return _instance;
        }

        public override void Init() 
        {
            string strLibName = "NFKernelPlugin.dll";
            NFIPlugin xPlugin = new NFCPlugin(strLibName);

            mxPluginDic.TryAdd(strLibName, xPlugin);

            foreach(var x in mxPluginDic)
            {
                x.Value.Init();
            }
        }

        public override void AfterInit() 
        {
            foreach (var x in mxPluginDic)
            {
                x.Value.AfterInit();
            }
        }

        public override void BeforeShut() 
        {
            foreach (var x in mxPluginDic)
            {
                x.Value.BeforeShut();
            }
        }

        public override void Shut() 
        {
            foreach (var x in mxPluginDic)
            {
                x.Value.Shut();
            }
        }

        public override void Execute() 
        {
            foreach (var x in mxPluginDic)
            {
                x.Value.Execute();
            }
        }

        /////////////////////////////////////////////////////////        
        private readonly ConcurrentDictionary<string, NFIPlugin> mxPluginDic = new ConcurrentDictionary<string, NFIPlugin>();


    }
}
