// -------------------------------------------------------------------------
//    @FileName         ：    NFCNet.cpp
//    @Author           ：    LvSheng.Huang
//    @Date             ：    2013-12-15
//    @Module           ：    NFIPacket
//    @Desc             :     CNet
// -------------------------------------------------------------------------
#pragma  comment(lib,"libevent.lib")
#pragma  comment(lib,"libevent_core.lib")
#pragma  comment(lib,"libevent_extras.lib")

#include "NFCNet.h"
#include <string.h>

#ifdef _MSC_VER
#include <WS2tcpip.h>
#include <winsock2.h>
#pragma  comment(lib,"Ws2_32.lib")
#endif

#include "event2/bufferevent_struct.h"
#include "event2/event.h"

void NFCNet::conn_writecb(struct bufferevent *bev, void *user_data)
{
    //每次收到发送消息的时候事件
}

void NFCNet::conn_eventcb(struct bufferevent *bev, short events, void *user_data)
{
	evutil_socket_t sockfd = bufferevent_getfd(bev);

	NFINet* pNet = (NFINet*)user_data;
	if (!pNet)
	{
		return;
	}

	pNet->OnNetEvent(sockfd, NF_NET_EVENT(events));
}

void NFCNet::listener_cb(struct evconnlistener *listener, evutil_socket_t fd, struct sockaddr *sa, int socklen, void *user_data)
{
    NFCNet* pNet = (NFCNet*)user_data;
	if (!pNet)
	{
		return;
	}

    struct event_base *base = pNet->base;
    //创建一个基于socket的bufferevent
    struct bufferevent *bev = bufferevent_socket_new(base, fd, BEV_OPT_CLOSE_ON_FREE);
    if (!bev)
    {
        //应该T掉，拒绝
        fprintf(stderr, "Error constructing bufferevent!");
        //event_base_loopbreak(base);
        return;
    }

    //我获得一个新连接。为其创建一个bufferevent--FD需要管理
    struct sockaddr_in* pSin = (sockaddr_in*)sa;

    //为bufferevent设置各种回调
    bufferevent_setcb(bev, conn_readcb, conn_writecb, conn_eventcb, user_data);

    //开启bufferevent的读写
    bufferevent_enable(bev, EV_READ|EV_WRITE);

    //模拟客户端已连接事件
    conn_eventcb(bev, BEV_EVENT_CONNECTED, user_data);
}


void NFCNet::conn_readcb(struct bufferevent *bev, void *user_data)
{
	NFINet* pNet = (NFINet*)user_data;
	if (!pNet)
	{
		return;
	}

    struct evbuffer *input = bufferevent_get_input(bev);
    if (!input)
    {
        return;
    }

	evutil_socket_t sockfd = bufferevent_getfd(bev);
    size_t len = evbuffer_get_length(input);

	char* strMsg = new char[len];
	if(evbuffer_remove(input, strMsg, len) > 0)
	{
		pNet->OnRecivePacket(sockfd, strMsg, len);
	}

	delete[] strMsg;
}

//////////////////////////////////////////////////////////////////////////

void NFCNet::Execute()
{
	ExecuteClose();

    if (base)
    {
        event_base_loop(base, EVLOOP_ONCE|EVLOOP_NONBLOCK);
    }

}


int NFCNet::Initialization( const char* strIP, const unsigned short nPort)
{
    mstrIP = strIP;
    mnPort = nPort;

    return InitClientNet();
}

int NFCNet::Initialization( const unsigned int nMaxClient, const unsigned short nPort, const int nCpuCount)
{
    mnMaxConnect = nMaxClient;
    mnPort = nPort;
    mnCpuCount = nCpuCount;

    return InitServerNet();

}

bool NFCNet::Final()
{
    //CloseSocketAll();

    if (listener)
    {
        evconnlistener_free(listener);
        listener = NULL;
    }

	if (!mbServer)
	{
		if (base)
		{
			event_base_free(base);
			base = NULL;
		}
	}

    return true;
}

bool NFCNet::SendMsg(const char* msg, const uint32_t nLen, const int nSockIndex)
{
    if (nLen <= 0)
    {
        return false;
    }

// 	std::map<int, NetObject*>::iterator it = mmObject.find(nSockIndex);
// 	if (it != mmObject.end())
// 	{
// 		NetObject* pNetObject = (NetObject*)it->second;
// 		if (pNetObject)
// 		{
// 			bufferevent* bev = pNetObject->GetBuffEvent();
// 			if (NULL != bev)
// 			{
// 				bufferevent_write(bev, msg, nLen);
// 
// 				return true;
// 			}
// 		}
// 	}

    return false;
}

bool NFCNet::CloseNetObject( const int nSockIndex )
{
    mvRemoveObject.push_back(nSockIndex);

    return false;
}

int NFCNet::InitClientNet()
{
    std::string strIP = mstrIP;
    int nPort = mnPort;

    struct sockaddr_in addr;
    struct bufferevent *bev = NULL;

#if NF_PLATFORM == NF_PLATFORM_WIN
    WSADATA wsa_data;
    WSAStartup(0x0201, &wsa_data);
#endif

    memset(&addr, 0, sizeof(addr));
    addr.sin_family = AF_INET;
    addr.sin_port = htons(nPort);

    if (inet_pton(AF_INET, strIP.c_str(), &addr.sin_addr) <= 0)
    {
        printf("inet_pton");
        return -1;
    }

    base = event_base_new();
    if (base == NULL)
    {
        printf("event_base_new ");
        return -1;
    }

    bev = bufferevent_socket_new(base, -1, BEV_OPT_CLOSE_ON_FREE);
    if (bev == NULL)
    {
        printf("bufferevent_socket_new ");
        return -1;
    }

    int bRet = bufferevent_socket_connect(bev, (struct sockaddr *)&addr, sizeof(addr));
    if (0 != bRet)
    {
        //int nError = GetLastError();
        printf("bufferevent_socket_connect error");
        return -1;
    }

	evutil_socket_t sockfd = bufferevent_getfd(bev);
    mbServer = false;

    bufferevent_setcb(bev, conn_readcb, conn_writecb, conn_eventcb, (void*)this);
    bufferevent_enable(bev, EV_READ|EV_WRITE);

	event_set_log_callback(&NFCNet::log_cb);

    return sockfd;
}

int NFCNet::InitServerNet()
{
    int nMaxClient = mnMaxConnect;
    int nCpuCount = mnCpuCount;
    int nPort = mnPort;

    struct sockaddr_in sin;

#if NF_PLATFORM == NF_PLATFORM_WIN
    WSADATA wsa_data;
    WSAStartup(0x0201, &wsa_data);

#endif
    //////////////////////////////////////////////////////////////////////////

    struct event_config *cfg = event_config_new();

#if NF_PLATFORM == NF_PLATFORM_WIN

    //event_config_avoid_method(cfg, "iocp");
    //event_config_require_features(cfg, event_method_feature.EV_FEATURE_ET);//触发方式
    evthread_use_windows_threads();
    if(event_config_set_flag(cfg, EVENT_BASE_FLAG_STARTUP_IOCP) < 0)
    {
        //使用IOCP
        return -1;
    }

    if(event_config_set_num_cpus_hint(cfg, nCpuCount) < 0)
    {
        return -1;
    }

    base = event_base_new_with_config(cfg);

#else

    //event_config_avoid_method(cfg, "epoll");
    if(event_config_set_flag(cfg, EVENT_BASE_FLAG_EPOLL_USE_CHANGELIST) < 0)
    {
        //使用EPOLL
        return -1;
    }

    if(event_config_set_num_cpus_hint(cfg, nCpuCount) < 0)
    {
        return -1;
    }

    base = event_base_new_with_config(cfg);//event_base_new()

#endif
    event_config_free(cfg);

    //////////////////////////////////////////////////////////////////////////

    if (!base)
    {
        fprintf(stderr, "Could not initialize libevent!\n");
        Final();

        return -1;
    }

    //初始化时间
    //gettime(base, &base->event_tv);

    memset(&sin, 0, sizeof(sin));
    sin.sin_family = AF_INET;
    sin.sin_port = htons(nPort);

    printf("server started with %d\n", nPort);

    listener = evconnlistener_new_bind(base, listener_cb, (void *)this,
        LEV_OPT_REUSEABLE|LEV_OPT_CLOSE_ON_FREE, -1,
        (struct sockaddr*)&sin,
        sizeof(sin));

    if (!listener)
    {
        fprintf(stderr, "Could not create a listener!\n");
        Final();

        return -1;
    }

    mbServer = true;

	event_set_log_callback(&NFCNet::log_cb);

    return mnMaxConnect;
}

void NFCNet::ExecuteClose()
{
	for (int i = 0; i < mvRemoveObject.size(); ++i)
	{
		int nSocketIndex = mvRemoveObject[i];
		//bufferevent_free(bev);
	}

	mvRemoveObject.clear();
}

int NFCNet::Log( int severity, const char *msg )
{

	return 0;
}

int NFCNet::OnNetEvent( const int nSockIndex, const NF_NET_EVENT nEvent )
{
	return 0;
}

int NFCNet::OnRecivePacket( const int nSockIndex, const char* msg, const uint32_t nLen )
{
	return 0;
}

void NFCNet::log_cb( int severity, const char *msg )
{
	//return Log(severity, msg);
}
