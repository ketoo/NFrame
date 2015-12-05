//#include "NFINet.h"
//
//NFINet* 
//
////event call back event func define
//typedef int( * INIT_NET )( int num );
//
//INIT_NET p;
//
//__declspec(dllexport) int InitNet(INIT_NET p,char * str);
//__declspec(dllexport) int FinalNet(INIT_NET p,char * str);
//
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//C++定义：
//    ////////////////////////////////////////////////////////
//    //自定义类别的头文件 WebICAdapter.h
//#ifdef DLL_API
//#else
//#define DLL_API extern "C" __declspec(dllexport)
//#endif
//
//class WebICAdapter
//{
//public:
//    WebICAdapter(void);
//    ~WebICAdapter(void);
//    // 测试add函数
//    int add(int p1, int p2);
//};
//
//// 返回类别的实例指针
//DLL_API void* classInit(void **clsp);
//DLL_API int add(WebICAdapter* p, int p1, int p2);
//
//////////////////////////////////////////////////////////
////自定义类别的头文件 WebICAdapter.cpp
////=========导出函数============
//// 返回类别的实例指针
//void* classInit(void **clsp)
//{
//    WebICAdapter* p = new WebICAdapter();
//    *clsp = p;
//    return clsp;
//}
//int add(WebICAdapter* p, int p1, int p2)
//{
//    return p->add(p1,p2);
//}
////==========类别实现===========
//WebICAdapter::WebICAdapter(void)
//{
//}
//WebICAdapter::~WebICAdapter(void)
//{
//}
//// 测试add函数
//int WebICAdapter::add(int p1, int p2)
//{
//    return p1+p2;
//}
//
//C#定义和调用：
//    ////////////////////////////////////////////////////////
//    using System.Runtime.InteropServices;
//......
//    //--------------DLL接口定义-----------
//    [DllImport("SWWebICAdapter.dll", EntryPoint = "classInit", CharSet = 
//
//    CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
//public static extern int classInit(ref int clsPoint);
//
//[DllImport("SWWebICAdapter.dll", EntryPoint = "add", CharSet = CharSet.Auto, 
//
//           CallingConvention = CallingConvention.StdCall)]
//public static extern int add(ref int clsPoint, int p1, int p2);
//// DLL中的类实例指针
//private int _clsPoint = 0;
//
//// -----------------------------------
//public SWWebIC()
//{
//    InitializeComponent();
//    // 初始化DLL类实例
//    _clsPoint = classInit(ref _clsPoint);
//}
//......
//    private void buttonDevice_Click(object sender, EventArgs e)
//{
//    int n = add(ref _clsPoint, 11, 12);
//    MessageBox.Show("计算结果：" + n);
//}