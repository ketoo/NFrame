// -------------------------------------------------------------------------
//    @FileName         £º    NFCNet.h
//    @Author           £º    LvSheng.Huang
//    @Date             £º    2013-12-15
//    @Module           £º    NFIPacket
//    @Desc             :     CNet
// -------------------------------------------------------------------------

#ifndef __NFC_NET_H__
#define __NFC_NET_H__

#include "NFINet.h"
#include <chrono>
#include <thread>

#if NF_PLATFORM == NF_PLATFORM_WIN
#include <windows.h>
#else
#include <unistd.h>
#endif

#pragma pack(push, 1)


class NFCNet : public NFINet
{
public:
    NFCNet()
    {
        base = NULL;
        listener = NULL;

        mstrIP = "";
        mnPort = 0;
        mnCpuCount = 0;
        mbServer = false;
        ev = NULL;
    }

	virtual ~NFCNet(){};

public:
	virtual  void Execute();

	virtual  int Initialization(const char* strIP, const unsigned short nPort);
	virtual  int Initialization(const unsigned int nMaxClient, const unsigned short nPort, const int nCpuCount = 4);

	virtual  bool Final();

	virtual bool SendMsg(const char* msg, const uint32_t nLen, const int nSockIndex = 0);
	virtual bool SendMsgToAllClient(const char* msg, const uint32_t nLen);

    virtual bool CloseNetObject(const int nSockIndex);

	virtual int OnRecivePacket(const int nSockIndex, const char* msg, const uint32_t nLen);
	virtual int OnNetEvent(const int nSockIndex, const NF_NET_EVENT nEvent);
	virtual int Log(int severity, const char *msg);

private:

	virtual void ExecuteClose();

	virtual int InitClientNet();
	virtual int InitServerNet();

	static void listener_cb(struct evconnlistener *listener, evutil_socket_t fd,struct sockaddr *sa, int socklen, void *user_data);
	static void conn_readcb(struct bufferevent *bev, void *user_data);
	static void conn_writecb(struct bufferevent *bev, void *user_data);
	static void conn_eventcb(struct bufferevent *bev, short events, void *user_data);
	static void log_cb(int severity, const char *msg);

private:

	int mnMaxConnect;
	std::string mstrIP;
	int mnPort;
	int mnCpuCount;
	bool mbServer;

	std::vector<int> mvRemoveObject;

	struct event_base *base;
	struct evconnlistener *listener;
	//////////////////////////////////////////////////////////////////////////
	struct timeval tv;
	struct event* ev;
	//////////////////////////////////////////////////////////////////////////

};

#pragma pack(pop)

#endif
