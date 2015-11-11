//-----------------------------------------------------------------------
// <copyright file="NFCPluginManager.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFrame;
using System.Collections.Concurrent;
using System.Xml;

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
       
        public override string GetClassPath()
        {
            return mstrClassPath;
        }

        public override int GetAPPID()
        {
            return mnAPPID;
        }

        public override int GetAPPType()
        {
            return mnAPPType;
        }

        public override void Install()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("Plugin.xml");
            XmlNode xRoot = xmldoc.SelectSingleNode("XML");

            XmlNodeList xPluginNodeList = xRoot.SelectNodes("Plugin");
            for (int i = 0; i < xPluginNodeList.Count; ++i)
            {
                XmlNode xNodeClass = xPluginNodeList.Item(i);
                XmlAttribute strID = xNodeClass.Attributes["Name"];
                string strLibName = strID.Value;

                NFCDynLib xDynLib = new NFCDynLib(strLibName, this);
                mxDynLibDic.TryAdd(strLibName, xDynLib);
            }

            ///////////////////////////////////////////////////////////

            XmlNode xAPPIDNode = xRoot.SelectSingleNode("APPID");
            XmlAttribute strAppID = xAPPIDNode.Attributes["Name"];
            mnAPPID = int.Parse(strAppID.Value);
            SetSelf(new NFGUID(mnAPPID, 0));

            XmlNode xClassPathNode = xRoot.SelectSingleNode("ClassPath");
            XmlAttribute strClassPathID = xClassPathNode.Attributes["Name"];
            mstrClassPath = strClassPathID.Value;

            XmlNode xAPPTypeNode = xRoot.SelectSingleNode("APPType");
            XmlAttribute strAPPType = xAPPTypeNode.Attributes["Name"];
            mnAPPType = int.Parse(strAPPType.Value);
        }

        public override void UnInstall()
        {

        }

        public override void Init() 
        {
            foreach (var x in mhtObject)
            {
                x.Value.Init();
            }
        }

        public override void AfterInit() 
        {
            foreach (var x in mhtObject)
            {
                x.Value.AfterInit();
            }
        }

        public override void BeforeShut() 
        {
            foreach (var x in mhtObject)
            {
                x.Value.BeforeShut();
            }
        }

        public override void Shut() 
        {
            foreach (var x in mhtObject)
            {
                x.Value.Shut();
            }
        }

        public override void Execute() 
        {
            foreach (var x in mhtObject)
            {
                x.Value.Execute();
            }
        }


        /////////////////////////////////////////////////////////

        public override NFILogicModule GetModule(string strClassName)
        {
            NFILogicModule xModule;
            if (mhtObject.TryGetValue(strClassName, out xModule))
            {
                return xModule;
            }

            return null;
        }

        public override bool AddModule(string strClassName, NFILogicModule xModule)
        {
            return mhtObject.TryAdd(strClassName, xModule);
        }
        /////////////////////////////////////////////////////////        
        private readonly ConcurrentDictionary<string, NFCDynLib> mxDynLibDic = new ConcurrentDictionary<string, NFCDynLib>();
        private readonly ConcurrentDictionary<string, NFBehaviour> mxPluginModule = new ConcurrentDictionary<string, NFBehaviour>();
        private readonly ConcurrentDictionary<string, NFILogicModule> mhtObject = new ConcurrentDictionary<string, NFILogicModule>();

        private int mnAPPID;
        private int mnAPPType;
        private string mstrClassPath;
    }
}
