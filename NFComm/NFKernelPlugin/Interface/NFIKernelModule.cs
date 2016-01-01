#if UNITY_EDITOR
#define NF_CLIENT_FRAME
#elif UNITY_IPHONE
#define NF_CLIENT_FRAME
#elif UNITY_ANDROID
#define NF_CLIENT_FRAME
#elif UNITY_STANDALONE_OSX
#define NF_CLIENT_FRAME
#elif UNITY_STANDALONE_WIN
#define NF_CLIENT_FRAME
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NFrame
{
    public abstract class NFIKernelModule : NFILogicModule
    {
#if NF_CLIENT_FRAME
        public abstract NFILogicClassModule GetLogicClassModule();
        public abstract NFIElementModule GetElementModule();
#endif

        public abstract bool AddHeartBeat(NFGUID self, string strHeartBeatName, NFIHeartBeat.HeartBeatEventHandler handler, float fTime, int nCount);

        public abstract bool FindHeartBeat(NFGUID self, string strHeartBeatName);

        public abstract bool RemoveHeartBeat(NFGUID self, string strHeartBeatName);

        public abstract bool UpDate(float fTime);
		
		public abstract NFIDataList GetObjectList();
			
        /////////////////////////////////////////////////////////////
		public abstract void RegisterPropertyCallback(NFGUID self, string strPropertyName, NFIProperty.PropertyEventHandler handler);
		
        public abstract void RegisterRecordCallback(NFGUID self, string strRecordName, NFIRecord.RecordEventHandler handler);

        public abstract void RegisterClassCallBack(string strClassName, NFIObject.ClassEventHandler handler);

		public abstract void RegisterEventCallBack(NFGUID self, int nEventID, NFIEvent.EventHandler handler);
        /////////////////////////////////////////////////////////////////

        public abstract NFIObject GetObject(NFGUID ident);

        public abstract NFIObject CreateObject(NFGUID self, int nContainerID, int nGroupID, string strClassName, string strConfigIndex, NFIDataList arg);

        public abstract bool DestroyObject(NFGUID self);

        public abstract bool FindProperty(NFGUID self, string strPropertyName);

        public abstract bool SetPropertyInt(NFGUID self, string strPropertyName, Int64 nValue);
        public abstract bool SetPropertyFloat(NFGUID self, string strPropertyName, float fValue);
        public abstract bool SetPropertyDouble(NFGUID self, string strPropertyName, double dValue);
        public abstract bool SetPropertyString(NFGUID self, string strPropertyName, string strValue);
        public abstract bool SetPropertyObject(NFGUID self, string strPropertyName, NFGUID objectValue);

        public abstract Int64 QueryPropertyInt(NFGUID self, string strPropertyName);
        public abstract float QueryPropertyFloat(NFGUID self, string strPropertyName);
        public abstract double QueryPropertyDouble(NFGUID self, string strPropertyName);
        public abstract string QueryPropertyString(NFGUID self, string strPropertyName);
        public abstract NFGUID QueryPropertyObject(NFGUID self, string strPropertyName);

        public abstract NFIRecord FindRecord(NFGUID self, string strRecordName);

        public abstract bool SetRecordInt(NFGUID self, string strRecordName, int nRow, int nCol, Int64 nValue);
        public abstract bool SetRecordFloat(NFGUID self, string strRecordName, int nRow, int nCol, float fValue);
        public abstract bool SetRecordDouble(NFGUID self, string strRecordName, int nRow, int nCol, double dwValue);
        public abstract bool SetRecordString(NFGUID self, string strRecordName, int nRow, int nCol, string strValue);
        public abstract bool SetRecordObject(NFGUID self, string strRecordName, int nRow, int nCol, NFGUID objectValue);

        public abstract Int64 QueryRecordInt(NFGUID self, string strRecordName, int nRow, int nCol);
        public abstract float QueryRecordFloat(NFGUID self, string strRecordName, int nRow, int nCol);
        public abstract double QueryRecordDouble(NFGUID self, string strRecordName, int nRow, int nCol);
        public abstract string QueryRecordString(NFGUID self, string strRecordName, int nRow, int nCol);
        public abstract NFGUID QueryRecordObject(NFGUID self, string strRecordName, int nRow, int nCol);

        public abstract int FindRecordRow(NFGUID self, string strRecordName, int nCol, int nValue);
        public abstract int FindRecordRow(NFGUID self, string strRecordName, int nCol, float fValue);
        public abstract int FindRecordRow(NFGUID self, string strRecordName, int nCol, double fValue);
        public abstract int FindRecordRow(NFGUID self, string strRecordName, int nCol, string strValue);
        public abstract int FindRecordRow(NFGUID self, string strRecordName, int nCol, NFGUID nValue);


    }
}