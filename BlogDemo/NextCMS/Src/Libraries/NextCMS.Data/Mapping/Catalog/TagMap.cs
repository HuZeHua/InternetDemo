using System.Data.Entity.ModelConfiguration;
using XCode.Core.Domain.Catalog;

namespace XCode.Data.Mapping.Catalog
{
    public partial class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            this.ToTable("Catalog_Tag");
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).IsRequired().HasMaxLength(100);
            this.Property(t => t.Description).HasMaxLength(200);
        }
    }
}
