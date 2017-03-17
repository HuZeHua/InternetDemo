using System.Data.Entity.ModelConfiguration;
using XCode.Core.Domain.Configuration;

namespace XCode.Data.Mapping.Configuration
{
    public partial class ScheduleTaskMap : EntityTypeConfiguration<ScheduleTask>
    {
        public ScheduleTaskMap()
        {
            this.ToTable("Configuration_ScheduleTask");
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).IsRequired().HasMaxLength(200);
            this.Property(t => t.Type).IsRequired().HasMaxLength(200);
        }
    }
}