//-----------------------------------------------------------------------
// <copyright file="NFBehaviour.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;

namespace NFrame
{
    public class NFBehaviour : IDisposable
    {
        public virtual void Dispose()
        {
        }

        public virtual bool Init(){return false;}

        public virtual bool AfterInit() { return false; }

        public virtual bool BeforeShut() { return false; }

        public virtual bool Shut() { return false; }

        public virtual bool Execute() { return false; }

        public virtual NFIDENTID Self() { return new NFIDENTID(); }

        //event
        //ThreadPool
    }
}
