using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace NFrame
{
    public abstract class NFILogicClassModule : NFILogicModule
    {
        public abstract bool ExistElement(string strClassName);
        public abstract bool AddElement(string strClassName);

        public abstract NFILogicClass GetElement(string strClassName);
        public abstract Dictionary<string, NFILogicClass> GetElementList();

    }
}