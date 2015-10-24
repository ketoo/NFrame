using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFrame;

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

        public override bool Init() 
        {
            return false; 
        }

        public override bool AfterInit() 
        { 
            return false;
        }

        public override bool BeforeShut() 
        { 
            return false;
        }

        public override bool Shut() 
        {
            return false;
        }

        public override bool Execute() 
        { 
            return false; 
        }
        /////////////////////////////////////////////////////////

    }
}
