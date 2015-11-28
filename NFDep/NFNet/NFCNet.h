// -------------------------------------------------------------------------
//    @FileName         ：    NFCNet.h
//    @Author           ：    LvSheng.Huang
//    @Date             ：    2013-12-15
//    @Module           ：    NFIPacket
//    @Desc             :     CNet
// -------------------------------------------------------------------------

#ifndef __NFC_NET_H__
#define __NFC_NET_H__

#include "NFINet.h"
#include <chrono>
#include <thread>

#ifdef _MSC_VER
#include <windows.h>
#else
#include <unistd.h>
#endif

#pragma pack(push, 1)


class NFCNet : public NFINet
{
public:
    template<typename BaseType>
    NFCNet(BaseType* pBaseType, int (BaseType::*handleRecieve)(const int nSockIndex, const char* msg, const uint32_t nLen), int (BaseType::*handleEvent)(const int, const NF_NET_EVENT, NFINet*))
    {
		mnIndex = 0;
        base = NULL;
        listener = NULL;

        mRecvCB = std::bind(handleRecieve, pBaseType, std::placeholders::_1);
        mEventCB = std::bind(handleEvent, pBaseType, std::placeholders::_1, std::placeholders::_2, std::placeholders::_3);
        mstrIP = "";
        mnPort = 0;
        mnCpuCount = 0;
        mbServer = false;
        ev = NULL;
    }

	virtual ~NFCNet(){};

public:
	virtual bool Execute(const float fLasFrametime, const float fStartedTime);

	virtual void Initialization(const char* strIP, const unsigned short nPort);
	virtual int Initialization(const unsigned int nMaxClient, const unsigned short nPort, const int nCpuCount = 4);

	virtual bool Final();

	//已带上包头
	virtual bool SendMsg(const char* msg, const uint32_t nLen, const int nSockIndex = 0);

	//无包头，内部组装
	virtual bool SendMsgWithOutHead(const int16_t nMsgID, const char* msg, const uint32_t nLen, const int nSockIndex = 0);

	//已带上包头
	virtual bool SendMsgToAllClient(const char* msg, const uint32_t nLen);

	//无包头，内部组装
	virtual bool SendMsgToAllClientWithOutHead(const int16_t nMsgID, const char* msg, const uint32_t nLen);

    virtual bool CloseNetObject(const int nSockIndex);

	virtual bool Log(int severity, const char *msg);

private:
	virtual void ExecuteClose();
    virtual bool CloseSocketAll();

	virtual bool Dismantle(NetObject* pObject);
	virtual bool AddNetObject(const int nSockIndex, NetObject* pObject);
	virtual NetObject* GetNetObject(const int nSockIndex);

	virtual int InitClientNet();
	virtual int InitServerNet();
	virtual void CloseObject(const int nSockIndex);

	static void listener_cb(struct evconnlistener *listener, evutil_socket_t fd,struct sockaddr *sa, int socklen, void *user_data);
	static void conn_readcb(struct bufferevent *bev, void *user_data);
	static void conn_writecb(struct bufferevent *bev, void *user_data);
	static void conn_eventcb(struct bufferevent *bev, short events, void *user_data);
	static void time_cb(evutil_socket_t fd, short _event, void *argc);
	static void log_cb(int severity, const char *msg);

protected:
	int DeCode( const char* strData, const uint32_t unLen/*, std::string& strOutData */);
	int EnCode( const uint16_t unMsgID, const char* strData, const uint32_t unLen, std::string& strOutData );

private:
	//<fd,object>
	std::map<int, NetObject*> mmObject;
	std::vector<int> mvRemoveObject;

	int mnMaxConnect;
	std::string mstrIP;
	int mnPort;
	int mnCpuCount;
	bool mbServer;

	struct event_base *base;
	struct evconnlistener *listener;
	//////////////////////////////////////////////////////////////////////////
	struct event* ev;
	//////////////////////////////////////////////////////////////////////////

    NET_RECIEVE_FUNCTOR mRecvCB;
    NET_EVENT_FUNCTOR mEventCB;

	//////////////////////////////////////////////////////////////////////////
};

#pragma pack(pop)

#endif
