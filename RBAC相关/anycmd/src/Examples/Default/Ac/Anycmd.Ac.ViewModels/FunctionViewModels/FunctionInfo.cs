﻿
namespace Anycmd.Ac.ViewModels.FunctionViewModels
{
    using Engine.Ac;
    using Exceptions;
    using Model;
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public class FunctionInfo : Dictionary<string, object>
    {
        private FunctionInfo() { }

        public static FunctionInfo Create(DicReader dic)
        {
            if (dic == null)
            {
                return null;
            }
            var data = new FunctionInfo();
            foreach (var item in dic)
            {
                data.Add(item.Key, item.Value);
            }
            CatalogState resource;
            if (!dic.AcDomain.CatalogSet.TryGetCatalog((Guid)data["ResourceTypeId"], out resource))
            {
                throw new GeneralException("意外的资源标识" + data["ResourceTypeId"]);
            }
            AppSystemState appSystem;
            if (!dic.AcDomain.AppSystemSet.TryGetAppSystem(resource.Code.Substring(0, resource.Code.IndexOf('.')), out appSystem))
            {
                throw new GeneralException("意外的区域应用系统标识");
            }
            if (!data.ContainsKey("AppSystemCode"))
            {
                data.Add("AppSystemCode", appSystem.Code);
            }
            if (!data.ContainsKey("AppSystemName"))
            {
                data.Add("AppSystemName", appSystem.Name);
            }

            if (!data.ContainsKey("ResourceCode"))
            {
                data.Add("ResourceCode", resource.Code);
            }
            if (!data.ContainsKey("ResourceName"))
            {
                data.Add("ResourceName", resource.Name);
            }
            if (!data.ContainsKey("IsManagedName"))
            {
                data.Add("IsManagedName", dic.AcDomain.Translate("Ac", "Function", "IsManagedName", (bool)data["IsManaged"]));
            }
            if (!data.ContainsKey("IsEnabledName"))
            {
                data.Add("IsEnabledName", dic.AcDomain.Translate("Ac", "Function", "IsEnabledName", (int)data["IsEnabled"]));
            }
            if (!data.ContainsKey("IsUiView"))
            {
                UiViewState view;
                data.Add("IsUiView", dic.AcDomain.UiViewSet.TryGetUiView((Guid)data["Id"], out view));
            }
            if (!data.ContainsKey("DeveloperCode"))
            {
                AccountState developer;
                if (dic.AcDomain.SysUserSet.TryGetDevAccount((Guid)data["DeveloperId"], out developer))
                {
                    data.Add("DeveloperCode", developer.LoginName);
                }
                else
                {
                    data.Add("DeveloperCode", "无效的值");
                }
            }

            return data;
        }
    }
}
