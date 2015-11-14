using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    public abstract class NFIMsgHead
    {
        public enum NF_Head
        {
		    NF_HEAD_LENGTH = 6,
		    NF_MSGBUFF_LENGTH = 1024 * 1024, //1M,引擎累计消息最大
        };

        abstract public UInt32 EnCode(byte[] strData);
        abstract public UInt32 DeCode(byte[] strData);

        abstract public UInt32 GetHeadLength();

        abstract public UInt16 GetMsgID();
        abstract public void SetMsgID(UInt16 nMsgID);

        abstract public UInt32 GetMsgLength();
        abstract public void SetMsgLength(UInt32 nLength);
    }

    public abstract class NFIPacket
    {
        abstract public int EnCode(UInt16 unMsgID, byte[] strData, UInt32 unLen);
        abstract public int DeCode(byte[] data, UInt32 unLen);

	    abstract public void Construction(NFIPacket packet);
        abstract public NFIMsgHead.NF_Head GetMsgHead();
        abstract public byte[] GetPacketData();
        abstract public string GetPacketString();
        abstract public UInt32 GetPacketLen();
        abstract public UInt32 GetDataLen();
        abstract public byte[] GetData();
        abstract public Int64 GetFd();
        abstract public void SetFd(Int64 nFd);
        abstract public int GetMsgID();
        abstract public void SetMsgID(Int64 nFd);
    }

}
