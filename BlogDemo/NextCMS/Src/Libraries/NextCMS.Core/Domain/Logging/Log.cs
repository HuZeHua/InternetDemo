using System;
using XCode.Core.Domain.Authen;

namespace XCode.Core.Domain.Logging
{
    /// <summary>
    /// ��־��¼
    /// </summary>
    public partial class Log : BaseEntity
    {
        /// <summary>
        /// ��־����
        /// </summary>
        public int LogLevelId { get; set; }

        /// <summary>
        /// �����Ϣ
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        /// ��ϸ��Ϣ
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// �û�
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// ����ҳ��
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// �����ַ
        /// </summary>
        public string ReferrerUrl { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// ��־����
        /// </summary>
        public LogLevel LogLevel
        {
            get
            {
                return (LogLevel)this.LogLevelId;
            }
            set
            {
                this.LogLevelId = (int)value;
            }
        }

        /// <summary>
        /// �û�
        /// </summary>
        public virtual User User { get; set; }
    }
}
