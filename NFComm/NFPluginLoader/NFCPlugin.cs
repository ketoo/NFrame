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
        public NFCPlugin(string strLibName)
        {
            mstrLibName = strLibName;
        }

        public override bool Init()
        {
            Assembly xAssembly = Assembly.LoadFrom(mstrLibName);

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

        private string mstrLibName = "";
    }
}
