// -------------------------------------------------------------------------
//    @FileName         £º    NFIPacket.h
//    @Author           £º    LvSheng.Huang
//    @Date             £º    2013-12-15
//    @Module           £º    NFIPacket
//    @Desc             :     Net Packet
// -------------------------------------------------------------------------

#ifndef __NFI_PACKET_H__
#define __NFI_PACKET_H__

#include <cstring>
#include <errno.h>
#include <stdio.h>
#include <signal.h>
#include <stdint.h>
#include <assert.h>

#ifdef _MSC_VER

#include <WinSock2.h>
#else

#include <arpa/inet.h>
#include <netinet/in.h>

#endif

#include <string>

#pragma pack(push, 1)




class NFIPacket
{
public:
	void operator = (const NFIPacket& packet)
	{
		this->Construction(packet);
	}

    virtual int EnCode(const uint16_t unMsgID, const char* strData, const uint32_t unLen) = 0;
	virtual int DeCode(const char* strData, const uint32_t unLen) = 0;

	virtual void Construction(const NFIPacket& packet) = 0;
	virtual const NFIMsgHead* GetMsgHead() const = 0;
	virtual const char* GetPacketData() const = 0;
    virtual const std::string& GetPacketString() const = 0;
    virtual const uint32_t GetPacketLen() const = 0;
    virtual const uint32_t GetDataLen() const = 0;
	virtual const char* GetData() const = 0;
	virtual const int GetFd() const = 0;
	virtual void SetFd(const int nFd) = 0;
};

#pragma pack(pop)

#endif
