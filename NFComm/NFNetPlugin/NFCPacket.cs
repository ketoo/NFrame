using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFrame
{
    
//     class NFCMsgHead : public NFIMsgHead
//     {
//     public:
//         NFCMsgHead()
//         {
//             munSize = 0;
//             munMsgID = 0;
//         }
// 
//         virtual uint32_t GetHeadLength() const { return NF_HEAD_LENGTH; }
// 
// 	    // 内存结构[ MsgID(2) | MsgSize(4) ]
//         virtual int EnCode(char* strData)
//         {
//             uint32_t nOffset = 0;
// 
//             uint16_t nMsgID = NF_HTONS(munMsgID);
//             memcpy(strData + nOffset, (void*)(&nMsgID), sizeof(munMsgID));
//             nOffset += sizeof(munMsgID);
// 
//             uint32_t nSize = NF_HTONL(munSize);
//             memcpy(strData + nOffset, (void*)(&nSize), sizeof(munSize));
//             nOffset += sizeof(munSize);
// 
//             if (nOffset != GetHeadLength())
//             {
//                 assert(0);
//             }
// 
//             return nOffset;
//         }
// 
//         virtual int DeCode(const char* strData)
//         {
//             uint32_t nOffset = 0;
// 
//             uint16_t nMsgID = 0;
//             memcpy(&nMsgID, strData + nOffset, sizeof(munMsgID));
//             munMsgID = NF_NTOHS(nMsgID);
//             nOffset += sizeof(munMsgID);
// 
//             uint32_t nSize = 0;
//             memcpy(&nSize, strData + nOffset, sizeof(munSize));
//             munSize = NF_NTOHL(nSize);
//             nOffset += sizeof(munSize);
// 
//             if (nOffset != GetHeadLength())
//             {
//                 assert(0);
//             }
// 
//             return nOffset;
//         }
// 
//         virtual uint16_t GetMsgID() const { return munMsgID; }
//         virtual void SetMsgID(uint16_t nMsgID) { munMsgID = nMsgID; }
// 
//         virtual uint32_t GetMsgLength() const { return munSize; }
//         virtual void SetMsgLength(uint32_t nLength){ munSize = nLength; }
// 
//     protected:
//         uint32_t munSize;
//         uint16_t munMsgID;
//     }


    class NFCPacket
    {
    }
}
