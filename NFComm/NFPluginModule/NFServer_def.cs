using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public class NFServer_def
    {
        public enum NF_SERVER_TYPES
        {
            NF_ST_CLIENT = 0,    // client
            NF_ST_REDIS = 1,    //
            NF_ST_MYSQL = 2,    //
            NF_ST_MASTER = 3,    //
            NF_ST_LOGIN = 4,    //
            NF_ST_PROXY = 5,    //
            NF_ST_GAME = 6,    //
            NF_ST_WORLD = 7,    //

        };
    }
}
