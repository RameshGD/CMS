﻿using Kooboo.CMS.Sites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.CMS.Sites.Persistence.Couchbase
{
    public class SiteInitializer
    {
        public virtual void Initialize(Site site)
        {
            var bucketName = site.GetBucketName();
            if (!DatabaseHelper.ExistBucket(bucketName))
            {
                DatabaseHelper.CreateBucket(bucketName);
                System.Threading.Thread.Sleep(3000);
                //此处需暂停几秒钟，否则，通过选择模板创建站点的方式，在导入数据时，会出现数据未导入的情况
                //大致原因在于，Couchbae在数据库创建之后，需要几秒钟的初始化过程，在这个过程中插入任何数据都将失败                
            }
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.PageDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.PageDataType), ModelExtensions.PageDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.PageDraftDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.PageDraftDataType), ModelExtensions.PageDraftDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.HtmlBlockDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.HtmlBlockDataType), ModelExtensions.HtmlBlockDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.LabelDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.LabelDataType), ModelExtensions.LabelDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.LabelCategoryDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.LabelCategoryDataType), ModelExtensions.LabelCategoryDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.UserDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.UserDataType), ModelExtensions.UserDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.CustomErrorDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.CustomErrorDataType), ModelExtensions.CustomErrorDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.UrlRedirectDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.UrlRedirectDataType), ModelExtensions.UrlRedirectDataType));

            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.ABPageSettingDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.ABPageSettingDataType), ModelExtensions.ABPageSettingDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.ABPageTestResultDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.ABPageTestResultDataType), ModelExtensions.ABPageTestResultDataType));
            DatabaseHelper.CreateDesignDocument(bucketName, ModelExtensions.GetQueryView(ModelExtensions.ABRuleSettingDataType), string.Format(DataHelper.viewTemplate, ModelExtensions.GetQueryView(ModelExtensions.ABRuleSettingDataType), ModelExtensions.ABRuleSettingDataType));
            
            System.Threading.Thread.Sleep(3000);
        }
    }
}
