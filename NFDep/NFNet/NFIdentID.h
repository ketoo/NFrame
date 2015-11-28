// -------------------------------------------------------------------------
//    @FileName         :    NFIDENTID.h
//    @Author           :    LvSheng.Huang
//    @Date             :    2012-07-11
//    @Module           :    NFIDENTID
//
// -------------------------------------------------------------------------

#ifndef _NF_IDENTID_H_
#define _NF_IDENTID_H_

#include <iostream>
#include <stdio.h>
#include <stdlib.h>

struct NFIDENTID
{
    int64_t nData64;
    int64_t nHead64;

    NFIDENTID()
    {
        nData64 = 0;
        nHead64 = 0;
    }

    NFIDENTID(int64_t nHeadData, int64_t nData)
    {
        nHead64 = nHeadData;
        nData64 = nData;
    }

    NFIDENTID(const NFIDENTID& xData)
    {
        nHead64 = xData.nHead64;
        nData64 = xData.nData64;
    }

    NFIDENTID& operator=(const NFIDENTID& xData)
    {
        nHead64 = xData.nHead64;
        nData64 = xData.nData64;

        return *this;
    }

    int64_t GetData()
    {
        return nData64;
    }

    int64_t GetHead()
    {
        return nHead64;
    }

    void SetData(const int64_t nData)
    {
        nData64 = nData;
    }

    void SetHead(const int64_t nData)
    {
        nHead64 = nData;
    }

    bool IsNull() const
    {
        return 0 == nData64 && 0 == nHead64;
    }

    bool operator == (const NFIDENTID& id) const
    {
        return this->nData64 == id.nData64 && this->nHead64 == id.nHead64;
    }

    bool operator != (const NFIDENTID& id) const
    {
        return this->nData64 != id.nData64 || this->nHead64 != id.nHead64;
    }

    bool operator < (const NFIDENTID& id) const
    {
        if (this->nHead64 == id.nHead64)
        {
            return this->nData64 < id.nData64;
        }

        return this->nHead64 < id.nHead64;
    }
};

#endif
