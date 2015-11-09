//-----------------------------------------------------------------------
// <copyright file="NFCProperty.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace NFrame
{
    public class NFCProperty : NFIProperty
    {
        public NFCProperty( NFIDENTID self, string strPropertyName, NFIDataList varData)
        {
            mSelf = self;
            msPropertyName = strPropertyName;
            mxData = new NFIDataList.TData();
            mxData.nType = varData.GetType(0);
            switch (varData.GetType(0))
            {
                case NFIDataList.VARIANT_TYPE.VTYPE_INT:
                    mxData.mData = varData.IntVal(0);
                    break;
                case NFIDataList.VARIANT_TYPE.VTYPE_FLOAT:
                    mxData.mData = varData.FloatVal(0);
                    break;
                case NFIDataList.VARIANT_TYPE.VTYPE_DOUBLE:
                    mxData.mData = varData.DoubleVal(0);
                    break;
                case NFIDataList.VARIANT_TYPE.VTYPE_OBJECT:
                    mxData.mData = varData.ObjectVal(0);
                    break;
                case NFIDataList.VARIANT_TYPE.VTYPE_STRING:
                    mxData.mData = varData.StringVal(0);
                    break;
                default:
                    break;
            }
        }

        public NFCProperty(NFIDENTID self, string strPropertyName, NFIDataList.TData varData)
        {
            mSelf = self;
            msPropertyName = strPropertyName;
            mxData = new NFIDataList.TData(varData);
        }

        public override string GetKey()
        {
            return msPropertyName;
        }
		
		public override NFIDataList.VARIANT_TYPE GetType()
		{
            return mxData.nType;
		}

        public override NFIDataList.TData GetData()
        {
            return mxData;
        }

        public override Int64 QueryInt()
        {
            if (NFIDataList.VARIANT_TYPE.VTYPE_INT == mxData.nType)
            {
                return (Int64)mxData.mData;
            }

            return 0;
        }

        public override float QueryFloat()
        {
            if (NFIDataList.VARIANT_TYPE.VTYPE_FLOAT == mxData.nType)
            {
                return (float)mxData.mData;
            }

            return 0.0f;
        }

        public override double QueryDouble()
        {
            if (NFIDataList.VARIANT_TYPE.VTYPE_DOUBLE == mxData.nType)
            {
                return (double)mxData.mData;
            }

            return 0.0;
        }

        public override string QueryString()
        {
            if (NFIDataList.VARIANT_TYPE.VTYPE_STRING == mxData.nType)
            {
                return (string)mxData.mData;
            }

            return "";
        }

        public override NFIDENTID QueryObject()
        {
            if(NFIDataList.VARIANT_TYPE.VTYPE_INT == mxData.nType)
            {
                return (NFIDENTID)mxData.mData;
            }

            return new NFIDENTID();
        }

        public override bool SetInt(Int64 value)
		{
            Int64 nData = (Int64)mxData.mData;

            if (nData != value)
            {
                NFCDataList oldValue = new NFCDataList();
                NFCDataList newValue = new NFCDataList();

                oldValue.SetInt(0, nData);
                newValue.SetInt(0, value);

                mxData.mData = value;

                if (null != doHandleDel)
                {
                    doHandleDel(mSelf, msPropertyName, oldValue, newValue);
                }
				
			}

			return true;
		}

		public override bool SetFloat(float value)
		{
            double dwData = (double)mxData.mData;

            if (dwData - value > 0.001f
                || dwData - value < -0.001f)
            {
                NFCDataList oldValue = new NFCDataList();
                NFCDataList newValue = new NFCDataList();

                oldValue.SetDouble(0, dwData);
                newValue.SetDouble(0, value);

                mxData.mData = value;

                if (null != doHandleDel)
                {
                    doHandleDel(mSelf, msPropertyName, oldValue, newValue);
                }
			}

			return true;
		}

		public override bool SetDouble(double value)
		{
            double dwData = (double)mxData.mData;

            if (dwData - value > 0.001f
                || dwData - value < -0.001f)
            {
                NFCDataList oldValue = new NFCDataList();
                NFCDataList newValue = new NFCDataList();

                oldValue.SetDouble(0, dwData);
                newValue.SetDouble(0, value);

                mxData.mData = value;

                if (null != doHandleDel)
                {
                    doHandleDel(mSelf, msPropertyName, oldValue, newValue);
                }
            }

			return true;
		}

		public override bool SetString(string value)
		{
            string strData = (string)mxData.mData;

            if (strData != value)
            {
                NFCDataList oldValue = new NFCDataList();
                NFCDataList newValue = new NFCDataList();

                oldValue.SetString(0, strData);
                newValue.SetString(0, value);

                mxData.mData = value;

                if (null != doHandleDel)
                {
                    doHandleDel(mSelf, msPropertyName, oldValue, newValue);
                }
            }

			return true;
		}

		public override bool SetObject(NFIDENTID value)
		{
            NFIDENTID xData = (NFIDENTID)mxData.mData;

            if (xData != value)
            {
                NFCDataList oldValue = new NFCDataList();
                NFCDataList newValue = new NFCDataList();

                oldValue.SetObject(0, xData);
                newValue.SetObject(0, value);

                mxData.mData = value;
                if (null != doHandleDel)
                {
                    doHandleDel(mSelf, msPropertyName, oldValue, newValue);
                }
            }

			return true;
		}

        public override bool SetData(NFIDataList.TData x)
        {
            if (NFIDataList.VARIANT_TYPE.VTYPE_UNKNOWN == mxData.nType
                || x.nType == mxData.nType)
            {
                mxData.nType = x.nType;
                mxData.mData = x.mData;

                return true;
            }

            return false;
        }

		public override void RegisterCallback(PropertyEventHandler handler)
		{
			doHandleDel += handler;
		}

		PropertyEventHandler doHandleDel;

		NFIDENTID mSelf;
		string msPropertyName;
        NFIDataList.TData mxData;
    }
}