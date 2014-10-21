using CRModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditOnline
{
    public class DicHelper : Controller
    {
        private List<DicModel> EnumDictionary = new List<DicModel>();

        private List<DicModel> mLanguageType;
        public List<DicModel> LanguageType
        {
            get
            {
                if (mLanguageType == null)
                {
                    mLanguageType = this.EnumDictionary.FindAll(m => m.Type == "LanguageType");
                    if (mLanguageType == null)
                    {
                        mLanguageType = new List<DicModel>();
                    }
                }
                return mLanguageType;
            }
        }

        private List<DicModel> mReportType;
        public List<DicModel> ReportType
        {
            get
            {
                if (mReportType == null)
                {
                    mReportType = this.EnumDictionary.FindAll(m => m.Type == "ReportType");
                    if (mReportType == null)
                    {
                        mReportType = new List<DicModel>();
                    }
                }
                return mReportType;
            }
        }

        private List<DicModel> mNationalReportType;
        public List<DicModel> NationalReportType
        {
            get
            {
                if (mNationalReportType == null)
                {
                    mNationalReportType = this.EnumDictionary.FindAll(m => m.Type == "NationalReportType");
                    if (mNationalReportType == null)
                    {
                        mNationalReportType = new List<DicModel>();
                    }
                }
                return mNationalReportType;
            }
        }

        private List<DicModel> mExpressType;
        public List<DicModel> ExpressType
        {
            get
            {
                if (mExpressType == null)
                {
                    mExpressType = this.EnumDictionary.FindAll(m => m.Type == "ExpressType");
                    if (mExpressType == null)
                    {
                        mExpressType = new List<DicModel>();
                    }
                }
                return mExpressType;
            }
        }

        public DicHelper()
        {
            using (CRServiceProxy.CRServiceClient client = new CRServiceProxy.CRServiceClient())
            {
                EnumDictionary = client.GetDicModel();
            }
        }
    }
}
