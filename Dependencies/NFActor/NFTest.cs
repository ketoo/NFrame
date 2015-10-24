//-----------------------------------------------------------------------
// <copyright file="NFTest.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFActor>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace NFrame
{
    public class NFTest
    {
        class TestHandler1 : NFBehaviour
        {
            public static int i = 0;

            public void Handler(NFIActorMessage xMessage)
            {
                ++i;

                //Console.WriteLine("handler11 ThreadID: " + Thread.CurrentThread.ManagedThreadId + " " + i);

                switch (xMessage.eType)
                {
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_INIT:
                        Init();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_AFTER_INIT:
                        AfterInit();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_EXCUTE:
                        Execute();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_BEFORE_SHUT:
                        BeforeShut();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_SHUT:
                        Shut();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_TEST_MSG:
                        {
                            NFIActorMessage xMsgData = new NFIActorMessage();
                            xMsgData.data = "test1";
                            //xMsgData.bAsync = false;
                            xMsgData.eType = NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_DATA_MSG;

                           // Console.WriteLine("handler11 ThreadID: " + Thread.CurrentThread.ManagedThreadId + " " + xMsgData.data);

                            NFCActorMng.Intance().SendMsg(xMessage.nFromActor, xMessage.nMasterActor, xMsgData);

                           // Console.WriteLine("handler11 ThreadID: " + Thread.CurrentThread.ManagedThreadId + " " + xMsgData.data);

                        }
                        break;
                    default:
                        break;
                }
            }

            public override void Init() 
            { 
            }

            public override void AfterInit()
            {
            }

            public override void BeforeShut()
            {
            }

            public override void Shut() 
            { 
            }

            public override void Execute() 
            { 
            }

            public override NFIDENTID Self() 
            { 
                return new NFIDENTID();
            }
        }

        class TestHandler2 : NFBehaviour
        {
            public static int i = 0;
            public void Handler(NFIActorMessage xMessage)
            {
                ++i;

                //Console.WriteLine("handler222222 ThreadID: " + Thread.CurrentThread.ManagedThreadId + " " + i);
                switch (xMessage.eType)
                {
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_INIT:
                        Init();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_AFTER_INIT:
                        AfterInit();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_EXCUTE:
                        Execute();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_BEFORE_SHUT:
                        BeforeShut();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_SHUT:
                        Shut();
                        break;
                    case NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_DATA_MSG:
                        {
                            xMessage.data = "1111111111111111111";
                            xMessage.eType = NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_TEST_MSG;

                            NFCActorMng.Intance().SendMsg(xMessage.nFromActor, xMessage.nMasterActor, xMessage);

                        }
                        break;
                    default:
                        break;
                }
            }

            public override void Init() 
            {
            }

            public override void AfterInit() 
            {
            }

            public override void BeforeShut() 
            { 
            }

            public override void Shut() 
            { 
            }

            public override void Execute() 
            { 
            }

            public override NFIDENTID Self()
            { 
                return new NFIDENTID(); 
            }
        }

        static void Main()
        {
            TestHandler1 xTestHandler1 = new TestHandler1();
            TestHandler2 xTestHandler2 = new TestHandler2();

            Console.WriteLine("start run... ThreadID: " + Thread.CurrentThread.ManagedThreadId);

            NFIDENTID xID1 = NFCActorMng.Intance().CreateActor(xTestHandler1.Handler);
            NFIDENTID xID2 = NFCActorMng.Intance().CreateActor(xTestHandler2.Handler);

             NFIActorMessage xMsgData = new NFIActorMessage();
             xMsgData.data = "test";
             //xMsgData.bAsync = false;//控制为同步消息还是异步消息
             xMsgData.eType = NFIActorMessage.EACTOR_MESSAGE_ID.EACTOR_TEST_MSG;

             System.DateTime currentTime = new System.DateTime();
             currentTime = System.DateTime.Now;

             NFCActorMng.Intance().SendMsg(xID1, xID2, xMsgData);

             Console.WriteLine("start loop... ThreadID: " + Thread.CurrentThread.ManagedThreadId);

            while(true)
            {
                Thread.Sleep(1);

                System.TimeSpan ts = System.DateTime.Now - currentTime;
                if (ts.TotalMilliseconds > 1000)
                {
                    int nCount = TestHandler1.i + TestHandler2.i;
                    Console.WriteLine("Count : " + nCount);

                    break;
                }
            }

            while (true)
            {
                Thread.Sleep(1);
            }

            NFCActorMng.Intance().ReleaseAllActor();
        }
    }
}
