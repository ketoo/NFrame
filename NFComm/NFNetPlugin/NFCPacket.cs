using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    class NFCMsgHead : NFIMsgHead
    {
        enum NFHead
        {
            NF_HEAD_LENGTH = 6,
            NF_MSG_BUFF_LENGTH = 1024 * 1024,
        }

        public NFCMsgHead()
        {
            mnMsgID = 0;
            mnMsgLength = 0;
        }

        public override UInt32 GetHeadLength()
        {
            return (UInt32)NFHead.NF_HEAD_LENGTH;
        }

        public override UInt32 GetMaxMsgLength()
        {
            return (UInt32)NFHead.NF_MSG_BUFF_LENGTH;
        }

        public override UInt16 GetMsgID()
        {
            return mnMsgID;
        }

        public override void SetMsgID(UInt16 value)
        {
            mnMsgID = value;
        }

        public override UInt32 GetMsgLength()
        {
            return mnMsgLength;
        }

        public override void SetMsgLength(UInt32 value)
        {
            mnMsgLength = value;
        }

        public override UInt32 EnCode(byte[] strData)
        {
            UInt16 nNetMsgID = (UInt16)System.Net.IPAddress.HostToNetworkOrder(mnMsgID);
            strData.Concat(BitConverter.GetBytes(mnMsgID));

            UInt32 nNetMsgLength = (UInt32)System.Net.IPAddress.HostToNetworkOrder(mnMsgLength);
            strData.Concat(BitConverter.GetBytes(nNetMsgLength));

            return (UInt32)strData.Length;
        }

        public override UInt32 DeCode(byte[] strData)
        {
            UInt16 nOffset = 0;
            UInt16 nNetMsgID = BitConverter.ToUInt16(strData, nOffset);
            mnMsgID = (UInt16)System.Net.IPAddress.NetworkToHostOrder(nNetMsgID);
            nOffset += sizeof(UInt16);

            UInt32 nNetMsgLength = BitConverter.ToUInt32(strData, nOffset);
            mnMsgLength = (UInt32)System.Net.IPAddress.NetworkToHostOrder(nNetMsgLength);
            nOffset += sizeof(UInt32);

            return nOffset;
        }
        ///////////////////////////
        UInt16 mnMsgID;
        UInt32 mnMsgLength;
    }

    class NFCPacket
    {
    }
}
