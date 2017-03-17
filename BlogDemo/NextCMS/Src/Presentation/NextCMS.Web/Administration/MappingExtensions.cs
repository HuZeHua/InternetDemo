using AutoMapper;
using XCode.Admin.Models.Authen;
using XCode.Admin.Models.Catalog;
using XCode.Admin.Models.Settings;
using XCode.Core.Domain.Authen;
using XCode.Core.Domain.Catalog;
using XCode.Core.Domain.Settings;

namespace XCode.Admin
{
    public static class MappingExtensions
    {
        #region 公用方法

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        #endregion

        #region 用户

        public static UserModel ToModel(this User entity)
        {
            return entity.MapTo<User, UserModel>();
        }

        public static User ToEntity(this UserModel model)
        {
            return model.MapTo<UserModel, User>();
        }

        public static User ToEntity(this UserModel model, User destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region 角色

        public static RoleModel ToModel(this Role entity)
        {
            return entity.MapTo<Role, RoleModel>();
        }

        public static Role ToEntity(this RoleModel model)
        {
            return model.MapTo<RoleModel, Role>();
        }

        public static Role ToEntity(this RoleModel model, Role destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region 权限

        public static PermissionModel ToModel(this Permission entity)
        {
            return entity.MapTo<Permission, PermissionModel>();
        }

        public static Permission ToEntity(this PermissionModel model)
        {
            return model.MapTo<PermissionModel, Permission>();
        }

        public static Permission ToEntity(this PermissionModel model, Permission destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region 文章

        public static ArticleModel ToModel(this Article entity)
        {
            return entity.MapTo<Article, ArticleModel>();
        }

        public static Article ToEntity(this ArticleModel model)
        {
            return model.MapTo<ArticleModel, Article>();
        }

        public static Article ToEntity(this ArticleModel model, Article destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region 分类

        public static CategoryModel ToModel(this Category entity)
        {
            return entity.MapTo<Category, CategoryModel>();
        }

        public static Category ToEntity(this CategoryModel model)
        {
            return model.MapTo<CategoryModel, Category>();
        }

        public static Category ToEntity(this CategoryModel model, Category destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region 标签

        public static TagModel ToModel(this Tag entity)
        {
            return entity.MapTo<Tag, TagModel>();
        }

        public static Tag ToEntity(this TagModel model)
        {
            return model.MapTo<TagModel, Tag>();
        }

        public static Tag ToEntity(this TagModel model, Tag destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region 设置

        public static ArticleSettingsModel ToModel(this ArticleSettings entity)
        {
            return entity.MapTo<ArticleSettings, ArticleSettingsModel>();
        }
        public static ArticleSettings ToEntity(this ArticleSettingsModel model, ArticleSettings destination)
        {
            return model.MapTo(destination);
        }

        public static GeneralSettingsModel ToModel(this GeneralSettings entity)
        {
            return entity.MapTo<GeneralSettings, GeneralSettingsModel>();
        }
        public static GeneralSettings ToEntity(this GeneralSettingsModel model, GeneralSettings destination)
        {
            return model.MapTo(destination);
        }
        #endregion
    }
}