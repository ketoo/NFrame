// -------------------------------------------------------------------------
//    @FileName         ：    NFINet.h
//    @Author           ：    LvSheng.Huang
//    @Date             ：    2013-12-15
//    @Module           ：    NFINet
//    @Desc             :     INet
// -------------------------------------------------------------------------

#ifndef __NFI_NET_H__
#define __NFI_NET_H__

#include <cstring>
#include <errno.h>
#include <stdio.h>
#include <signal.h>
#include <stdint.h>
#include <iostream>
#include <map>

#if NF_PLATFORM != NF_PLATFORM_WIN
#include <netinet/in.h>
# ifdef _XOPEN_SOURCE_EXTENDED
#  include <arpa/inet.h>
# endif
#include <sys/socket.h>
#endif

#include <vector>
#include <functional>
#include <memory>
#include <list>
#include <vector>
#include <event2/bufferevent.h>
#include <event2/buffer.h>
#include <event2/listener.h>
#include <event2/util.h>
#include <event2/thread.h>
#include <event2/event_compat.h>

#pragma pack(push, 1)

enum NF_NET_EVENT
{
    NF_NET_EVENT_EOF = 0x10,        //掉线
    NF_NET_EVENT_ERROR = 0x20,      //未知错误
    NF_NET_EVENT_TIMEOUT = 0x40,    //连接超时
    NF_NET_EVENT_CONNECTED = 0x80,  //连接成功(作为客户端)
};

class NFINet
{
public:
	virtual  void Execute() = 0;
	virtual  int Initialization(const char* strIP, const unsigned short nPort) = 0;
	virtual  int Initialization(const unsigned int nMaxClient, const unsigned short nPort, const int nCpuCount = 4) = 0;

	virtual  bool Final() = 0;

	//数据裸发
	virtual bool SendMsg(const char* msg, const uint32_t nLen, const int nSockIndex = 0) = 0;
	virtual bool SendMsgToAllClient(const char* msg, const uint32_t nLen) = 0;

	virtual int OnRecivePacket(const int nSockIndex, const char* msg, const uint32_t nLen) = 0;
	virtual int OnNetEvent(const int nSockIndex, const NF_NET_EVENT nEvent) = 0;

	virtual bool CloseNetObject(const int nSockIndex) = 0;
};

#pragma pack(pop)

#endif
