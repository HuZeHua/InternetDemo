using System.Data.Entity.ModelConfiguration;
using XCode.Core.Domain.Configuration;

namespace XCode.Data.Mapping.Configuration
{
    public partial class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            this.ToTable("Configuration_Setting");
            this.HasKey(s => s.Id);

            this.Property(s => s.Name).IsRequired().HasMaxLength(200);
            this.Property(s => s.Value).IsRequired().HasMaxLength(2000);
        }
    }
}