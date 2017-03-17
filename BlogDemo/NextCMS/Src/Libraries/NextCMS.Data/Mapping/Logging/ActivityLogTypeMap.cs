using System.Data.Entity.ModelConfiguration;
using XCode.Core.Domain.Logging;

namespace XCode.Data.Mapping.Logging
{
    public partial class ActivityLogTypeMap : EntityTypeConfiguration<ActivityLogType>
    {
        public ActivityLogTypeMap()
        {
            this.ToTable("Common_ActivityLogType");
            this.HasKey(t => t.Id);

            this.Property(t => t.SystemKeyword).IsRequired().HasMaxLength(100);
            this.Property(t => t.Name).IsRequired().HasMaxLength(200);
        }
    }
}
