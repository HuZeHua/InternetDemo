﻿
namespace Anycmd.Ac.Web.Mvc.Controllers
{
    using Anycmd.Web.Mvc;
    using Engine.Ac;
    using Engine.Ac.Catalogs;
    using Engine.Ac.Privileges;
    using Engine.Host.Ac;
    using Exceptions;
    using MiniUI;
    using Repositories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web.Mvc;
    using Util;
    using ViewModel;
    using ViewModels.AppSystemViewModels;
    using ViewModels.CatalogViewModels;
    using ViewModels.FunctionViewModels;
    using ViewModels.PrivilegeViewModels;

    /// <summary>
    /// 目录模型视图控制器
    /// </summary>
    [Guid("9EC361D7-0D7B-4295-BB79-21D800298157")]
    public class CatalogController : AnycmdController
    {
        #region 视图
        [By("xuexs")]
        [Description("目录管理")]
        [Guid("3A593EA4-9159-4B12-92FD-9F5D3DD5EB68")]
        public ViewResultBase Index()
        {
            return ViewResult();
        }

        [By("xuexs")]
        [Description("目录详细信息")]
        [Guid("870D7CFB-D5BB-45EE-B8B5-CDFC362A59FE")]
        public ViewResultBase Details()
        {
            if (!string.IsNullOrEmpty(Request["isTooltip"]))
            {
                Guid id;
                if (Guid.TryParse(Request["id"], out id))
                {
                    var data = CatalogInfo.Create(base.EntityType.GetData(id));
                    return new PartialViewResult { ViewName = "Partials/Details", ViewData = new ViewDataDictionary(data) };
                }
                else
                {
                    throw new ValidationException("非法的Guid标识" + Request["id"]);
                }
            }
            else if (!string.IsNullOrEmpty(Request["isInner"]))
            {
                return new PartialViewResult { ViewName = "Partials/Details" };
            }
            else
            {
                return this.View();
            }
        }

        [By("xuexs")]
        [Description("岗位（工作组）列表")]
        [Guid("E63A2FDF-B4A0-4076-B313-145BB27BEF1A")]
        public ViewResultBase Groups()
        {
            return ViewResult();
        }

        [By("xuexs")]
        [Description("下级目录列表")]
        [Guid("5FA956FA-CFE6-4265-8019-D7123A01E988")]
        public ViewResultBase Children()
        {
            return ViewResult();
        }

        [By("xuexs")]
        [Description("目录账户")]
        [Guid("AA7FFFD5-21EC-43CF-BA2F-E6A6D07B8C3F")]
        public ViewResultBase Accounts()
        {
            return ViewResult();
        }

        #endregion

        [By("xuexs")]
        [Description("根据ID获取目录")]
        [Guid("530AD37E-E162-4BA5-BAE8-C21D34311CB3")]
        public ActionResult Get(Guid? id)
        {
            if (!id.HasValue)
            {
                throw new ValidationException("未传入标识");
            }
            return this.JsonResult(base.EntityType.GetData(id.Value));
        }

        [By("xuexs")]
        [Description("根据ID获取目录详细信息")]
        [Guid("347E1C8E-66D9-4CC1-B4F5-6B76F2B4131F")]
        public ActionResult GetInfo(Guid? id)
        {
            if (id.HasValue)
            {
                FunctionState function;
                if (AcDomain.FunctionSet.TryGetFunction(id.Value, out function))
                {
                    EntityTypeState functionEntityType;
                    if (!AcDomain.EntityTypeSet.TryGetEntityType(new Coder("Ac", "Function"), out functionEntityType))
                    {
                        throw new GeneralException();
                    }
                    var functionInfo = FunctionInfo.Create(functionEntityType.GetData(id.Value));
                    functionInfo.Add("Name", functionInfo["Description"]);
                    CatalogState parentCatalog;
                    if (!AcDomain.CatalogSet.TryGetCatalog((Guid)functionInfo["ResourceTypeId"], out parentCatalog))
                    {
                        throw new GeneralException();
                    }
                    functionInfo.Add("ParentCode", parentCatalog.Code);
                    functionInfo.Add("ParentName", parentCatalog.Name);
                    functionInfo.Add("CategoryCode", "operation");
                    functionInfo.Add("CategoryName", "绑定在资源类型上的操作");
                    return this.JsonResult(functionInfo);
                }
                var recordInfo = CatalogInfo.Create(base.EntityType.GetData(id.Value));
                if (recordInfo == null)
                {
                    return this.JsonResult(GetEmptyCatalogInfo());
                }
                AppSystemState appSystem;
                if (AcDomain.AppSystemSet.TryGetAppSystem(id.Value, out appSystem))
                {
                    EntityTypeState appSystemEntityType;
                    if (!AcDomain.EntityTypeSet.TryGetEntityType(new Coder("Ac", "AppSystem"), out appSystemEntityType))
                    {
                        throw new GeneralException();
                    }
                    recordInfo.Add("data", AppSystemInfo.Create(appSystemEntityType.GetData(id.Value)));
                }
                return this.JsonResult(recordInfo);
            }
            else
            {
                return this.JsonResult(GetEmptyCatalogInfo());
            }
        }

        private Dictionary<string, object> GetEmptyCatalogInfo()
        {
            return new Dictionary<string, object>
            {
                {"CategoryName", string.Empty},
                {"Code", string.Empty},
                {"CreateBy", string.Empty},
                {"CreateOn", DateTime.MinValue},
                {"CreateUserId", Guid.Empty},
                {"Description", string.Empty},
                {"ModifiedBy", string.Empty},
                {"ModifiedOn", DateTime.MinValue},
                {"ModifiedUserId", Guid.Empty},
                {"Name", string.Empty},
                {"ParentId", Guid.Empty},
                {"ParentName", string.Empty},
                {"Postalcode", string.Empty}
            };
        }

        // TODO:重构 因为方法太长
        [By("xuexs")]
        [Description("根据父节点获取子节点")]
        [Guid("CD5F67AB-A415-46E7-B0D8-0A2D6F44E43E")]
        public ActionResult GetNodesByParentId(Guid? parentId)
        {
            if (parentId == Guid.Empty)
            {
                parentId = null;
            }
            if (!parentId.HasValue)
            {
                if (AcSession.IsDeveloper())
                {
                    var nodeList = new List<CatalogMiniNode>();
                    nodeList.AddRange(AcDomain.CatalogSet.Where(a => a != CatalogState.VirtualRoot && a.ParentCode == null).OrderBy(a => a.SortCode + a.CategoryCode.GetHashCode()).Select(a => new CatalogMiniNode
                    {
                        CategoryCode = a.CategoryCode,
                        Code = a.Code,
                        expanded = false,
                        Id = a.Id.ToString(),
                        isLeaf = !AcDomain.CatalogSet.Any(o => a.Code.Equals(o.ParentCode, StringComparison.OrdinalIgnoreCase)),
                        Name = a.Name,
                        ParentCode = a.ParentCode,
                        ParentId = a.Parent.Id.ToString(),
                        SortCode = a.SortCode.ToString()
                    }));
                    return this.JsonResult(nodeList);
                }
                else
                {
                    var orgs = AcSession.AccountPrivilege.Catalogs;
                    if (orgs != null && orgs.Count > 0)
                    {
                        return this.JsonResult(orgs.Select(org => new CatalogMiniNode
                        {
                            Code = org.Code ?? string.Empty,
                            Id = org.Id.ToString(),
                            Name = org.Name,
                            isLeaf = AcDomain.CatalogSet.All(o => !org.Code.Equals(o.ParentCode, StringComparison.OrdinalIgnoreCase))
                        }));
                    }
                    return this.JsonResult(new List<CatalogMiniNode>());
                }
            }
            else
            {
                var pid = parentId.Value;
                CatalogState parentCatalog;
                if (!AcDomain.CatalogSet.TryGetCatalog(pid, out parentCatalog))
                {
                    throw new ValidationException("意外的目录标识" + pid);
                }
                var nodeList = new List<CatalogMiniNode>();
                nodeList.AddRange(AcDomain.CatalogSet.Where(a => parentCatalog.Code.Equals(a.ParentCode, StringComparison.OrdinalIgnoreCase)).OrderBy(a => a.SortCode + a.CategoryCode.GetHashCode()).Select(a => new CatalogMiniNode
                {
                    CategoryCode = a.CategoryCode,
                    Code = a.Code,
                    expanded = false,
                    Id = a.Id.ToString(),
                    isLeaf = !AcDomain.CatalogSet.Any(o => a.Code.Equals(o.ParentCode, StringComparison.OrdinalIgnoreCase)),
                    Name = a.Name,
                    ParentCode = a.ParentCode,
                    ParentId = a.Parent.Id.ToString(),
                    SortCode = a.SortCode.ToString()
                }));
                nodeList.AddRange(AcDomain.FunctionSet.Where(a => a.ResourceTypeId == pid).OrderBy(a => a.SortCode).Select(a => new CatalogMiniNode
                {
                    CategoryCode = "operation",
                    Code = a.Code,
                    expanded = false,
                    Id = a.Id.ToString(),
                    isLeaf = true,
                    Name = string.Format("<span style='color:red;'>{0}</span>", a.Description),
                    ParentCode = parentCatalog.Code,
                    ParentId = pid.ToString(),
                    SortCode = a.SortCode.ToString()
                }));
                return this.JsonResult(nodeList);
            }
        }

        [By("xuexs")]
        [Description("分页获取目录")]
        [Guid("088D19ED-0C1E-4C1A-A721-962F5E424964")]
        public ActionResult GetPlistChildren(GetPlistChildren requestModel)
        {
            if (requestModel.ParentId == Guid.Empty)
            {
                requestModel.ParentId = null;
            }
            if (!ModelState.IsValid)
            {
                return ModelState.ToJsonResult();
            }
            foreach (var filter in requestModel.Filters)
            {
                PropertyState property;
                if (!base.EntityType.TryGetProperty(filter.field, out property))
                {
                    throw new ValidationException("意外的Catalog实体类型属性" + filter.field);
                }
            }
            int pageIndex = requestModel.PageIndex;
            int pageSize = requestModel.PageSize;
            IQueryable<CatalogTr> queryable = null;
            if (requestModel.IncludeDescendants.HasValue && requestModel.IncludeDescendants.Value)
            {
                queryable = AcDomain.CatalogSet.Where(a => a != CatalogState.VirtualRoot).Select(a => CatalogTr.Create(a)).AsQueryable().Where(a => a.Code.Contains(requestModel.ParentCode));
            }
            else
            {
                if (requestModel.ParentCode == string.Empty)
                {
                    requestModel.ParentCode = null;
                }
                queryable = AcDomain.CatalogSet.Where(a => a != CatalogState.VirtualRoot && string.Equals(a.ParentCode, requestModel.ParentCode, StringComparison.OrdinalIgnoreCase)).Select(a => CatalogTr.Create(a)).AsQueryable();
            }
            foreach (var filter in requestModel.Filters)
            {
                queryable = queryable.Where(filter.ToPredicate(), filter.value);
            }
            var list = queryable.OrderBy(requestModel.SortField + " " + requestModel.SortOrder).Skip(pageIndex * pageSize).Take(pageSize);

            return this.JsonResult(new MiniGrid<CatalogTr> { total = queryable.Count(), data = list });
        }

        [By("xuexs")]
        [Description("添加数据集管理员")]
        [HttpPost]
        [Guid("EB100BDE-9919-4CB1-8C88-C7DF42B7D1E8")]
        public ActionResult AddAccountCatalogs(string accountIDs, Guid catalogId)
        {
            if (string.IsNullOrEmpty(accountIDs))
            {
                throw new ValidationException("accountIDs不能为空");
            }
            if (catalogId == Guid.Empty)
            {
                throw new ValidationException("catalogID是必须的");
            }
            CatalogState catalog;
            if (!AcDomain.CatalogSet.TryGetCatalog(catalogId, out catalog))
            {
                throw new ValidationException("意外的目录标识" + catalogId);
            }
            string[] aIds = accountIDs.Split(',');
            foreach (var item in aIds)
            {
                var accountId = new Guid(item);
                AcDomain.Handle(new PrivilegeCreateIo
                {
                    SubjectInstanceId = accountId,
                    SubjectType = UserAcSubjectType.Account.ToName(),
                    Id = Guid.NewGuid(),
                    ObjectInstanceId = catalogId,
                    ObjectType = AcElementType.Catalog.ToName()
                }.ToCommand(AcSession));
            }

            return this.JsonResult(new ResponseData { success = true, id = accountIDs });
        }

        [By("xuexs")]
        [Description("移除数据集管理员")]
        [HttpPost]
        [Guid("2A387394-B364-453D-A8FA-DC2E4DD3A02C")]
        public ActionResult RemoveAccountCatalogs(string id)
        {
            string[] ids = id.Split(',');
            foreach (var item in ids)
            {
                AcDomain.Handle(new RemovePrivilegeCommand(AcSession, new Guid(item)));
            }

            return this.JsonResult(new ResponseData { success = true, id = id });
        }

        [By("xuexs")]
        [Description("添加目录")]
        [HttpPost]
        [Guid("296BDC47-026C-4E67-BFFA-5278D8EC6431")]
        public ActionResult Create(CatalogCreateInput input)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.ToJsonResult();
            }
            AcDomain.Handle(input.ToCommand(AcSession));

            return this.JsonResult(new ResponseData { id = input.Id, success = true });
        }

        [By("xuexs")]
        [Description("更新目录")]
        [HttpPost]
        [Guid("7A720313-3611-4A72-ADE6-F5369E2F1CFC")]
        public ActionResult Update(CatalogUpdateInput input)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.ToJsonResult();
            }
            AcDomain.Handle(input.ToCommand(AcSession));

            return this.JsonResult(new ResponseData { id = input.Id, success = true });
        }

        [By("xuexs")]
        [Description("删除目录")]
        [HttpPost]
        [Guid("AE84D20A-D754-4C0B-895B-2478421D99B1")]
        public ActionResult Delete(string id)
        {
            string[] ids = id.Split(',');
            var idArray = new Guid[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                Guid tmp;
                if (Guid.TryParse(ids[i], out tmp))
                {
                    idArray[i] = tmp;
                }
                else
                {
                    throw new ValidationException("意外的目录标识" + ids[i]);
                }
            }
            foreach (var item in idArray)
            {
                AcDomain.Handle(new RemoveCatalogCommand(AcSession, item));
            }

            return this.JsonResult(new ResponseData { id = id, success = true });
        }

        #region GrantOrDenyRoles
        [By("xuexs")]
        [Description("为指定目录下的全部账户逻辑授予或收回角色")]
        [HttpPost]
        [Guid("7EC5DE38-DC10-4264-89B2-94007803C7D2")]
        public ActionResult GrantOrDenyRoles()
        {
            String json = Request["data"];
            var rows = (ArrayList)MiniJSON.Decode(json);
            foreach (Hashtable row in rows)
            {
                var id = new Guid(row["Id"].ToString());
                //根据记录状态，进行不同的增加、删除、修改操作
                String state = row["_state"] != null ? row["_state"].ToString() : "";

                //更新：_state为空或modified
                if (state == "modified" || state == "")
                {
                    bool isAssigned = bool.Parse(row["IsAssigned"].ToString());
                    var entity = GetRequiredService<IRepository<Privilege, Guid>>().GetByKey(id);
                    if (entity != null)
                    {
                        if (!isAssigned)
                        {
                            AcDomain.Handle(new RemovePrivilegeCommand(AcSession, id));
                        }
                    }
                    else if (isAssigned)
                    {
                        AcDomain.Handle(new PrivilegeCreateIo
                        {
                            Id = new Guid(row["Id"].ToString()),
                            ObjectType = AcElementType.Role.ToName(),
                            SubjectType = UserAcSubjectType.Catalog.ToName(),
                            ObjectInstanceId = new Guid(row["RoleId"].ToString()),
                            SubjectInstanceId = new Guid(row["CatalogId"].ToString())
                        }.ToCommand(AcSession));
                    }
                }
            }

            return this.JsonResult(new ResponseData { success = true });
        }
        #endregion
    }
}
