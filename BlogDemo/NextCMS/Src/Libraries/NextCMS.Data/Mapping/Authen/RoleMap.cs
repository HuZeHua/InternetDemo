using System.Data.Entity.ModelConfiguration;
using XCode.Core.Domain.Authen;

namespace XCode.Data.Mapping.Authen
{
    public partial class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.ToTable("Authen_Role");
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).IsRequired().HasMaxLength(100);
            this.Property(t => t.SystemName).IsRequired().HasMaxLength(100);
        }
    }
}
