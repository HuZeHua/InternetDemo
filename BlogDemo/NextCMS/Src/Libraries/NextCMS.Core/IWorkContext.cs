using XCode.Core.Domain.Authen;
using XCode.Core.Domain.Settings;

namespace XCode.Core
{
    /// <summary>
    /// 网站上下文接口
    /// </summary>
    public interface IWorkContext
    {
        string UserName { get; set; }

        User CurrentUser { get; set; }

        GeneralSettings GeneralSettings { get; }
    }
}
