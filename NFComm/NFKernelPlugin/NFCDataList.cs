//-----------------------------------------------------------------------
// <copyright file="NFCDataList.cs">
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
    public class NFCDataList : NFIDataList
    {
        private Dictionary<int, TData> mValueObject = new Dictionary<int, TData>();

        //==============================================

        public NFCDataList(string str, char c)
        {
            string[] strSub = str.Split(c);
            foreach (string strDest in strSub)
            {
                AddString(strDest);
            }
        }

        public NFCDataList(NFIDataList src)
        {
			for (int i = 0; i < src.Count(); i++ )
			{
				switch (src.GetType(i))
				{
					case VARIANT_TYPE.VTYPE_INT:
						AddInt(src.IntVal(i));
					break;
					case VARIANT_TYPE.VTYPE_FLOAT:
						AddFloat(src.FloatVal(i));
					break;
					case VARIANT_TYPE.VTYPE_DOUBLE:
						AddDouble(src.DoubleVal(i));
					break;
					case VARIANT_TYPE.VTYPE_STRING:
						AddString(src.StringVal(i));
					break;
					case VARIANT_TYPE.VTYPE_OBJECT:
						AddObject(src.ObjectVal(i));
					break;
						default:
					break;
				}
			}

        }

        public NFCDataList()
        {
        }

        public override bool AddInt(Int64 value)
        {
            TData data = new TData();
            data.nType = VARIANT_TYPE.VTYPE_INT;
            data.mData = value;

			return AddDataObject(ref data);
        }

        public override bool AddFloat(float value)
        {
            TData data = new TData();
            data.nType = VARIANT_TYPE.VTYPE_FLOAT;
            data.mData = value;

			return AddDataObject(ref data);
        }

        public override bool AddDouble(double value)
        {
            TData data = new TData();
            data.nType = VARIANT_TYPE.VTYPE_DOUBLE;
            data.mData = value;

            return AddDataObject(ref data);
        }

        public override bool AddString(string value)
        {
            TData data = new TData();
            data.nType = VARIANT_TYPE.VTYPE_STRING;
            data.mData = value;

            return AddDataObject(ref data);
        }

        public override bool AddObject(NFIDENTID value)
        {
            TData data = new TData();
            data.nType = VARIANT_TYPE.VTYPE_OBJECT;
            data.mData = value;

			return AddDataObject(ref data);
        }

        public override bool SetInt(int index, Int64 value)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_INT)
            {
                data.mData = value;

                return true;
            }

            return false;
        }

        public override bool SetFloat(int index, float value)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_FLOAT)
            {
                data.mData = value;

                return true;
            }

            return false;
        }

        public override bool SetDouble(int index, double value)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_DOUBLE)
            {
                data.mData = value;

                return true;
            }

            return false;
        }

        public override bool SetString(int index, string value)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_STRING)
            {
                data.mData = value;

                return true;
            }

            return false;
        }

        public override bool SetObject(int index, NFIDENTID value)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_OBJECT)
            {
                data.mData = value;

                return true;
            }

            return false;
        }

        public override Int64 IntVal(int index)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_INT)
            {
                return (Int64)data.mData;
            }

            return 0;
        }

        public override float FloatVal(int index)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_FLOAT)
            {
                return (float)data.mData;
            }

            return 0.0f;
        }

        public override double DoubleVal(int index)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_DOUBLE)
            {
                return (double)data.mData;
            }

            return 0.0;
        }

        public override string StringVal(int index)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_STRING)
            {
                return (string)data.mData;
            }

            return "";
        }

        public override NFIDENTID ObjectVal(int index)
        {
            TData data = GetData(index);
            if (data != null && data.nType == VARIANT_TYPE.VTYPE_OBJECT)
            {
                return (NFIDENTID)data.mData;
            }

            return new NFIDENTID();
        }

		public override int Count()
		{
			return mValueObject.Count;
		}

		public override void Clear()
		{
			mValueObject.Clear();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
        protected bool AddDataObject(ref TData data)
        {
            int nCount = mValueObject.Count;
            mValueObject.Add(nCount, data);

            return true;
        }

		public override VARIANT_TYPE GetType(int index)
        {
			if (mValueObject.Count > index)
			{
				TData data = (TData)mValueObject[index];

				return data.nType;
			}

			return VARIANT_TYPE.VTYPE_UNKNOWN;
        }

        public override TData GetData(int index)
        {
            if (mValueObject.ContainsKey(index))
            {
                return (TData)mValueObject[index];
            }

            return null;
        }
    }
}
