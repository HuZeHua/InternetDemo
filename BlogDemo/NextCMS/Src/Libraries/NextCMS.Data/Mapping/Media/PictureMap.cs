using System.Data.Entity.ModelConfiguration;
using XCode.Core.Domain.Media;

namespace XCode.Data.Mapping.Media
{
    public partial class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            this.ToTable("Media_Picture");
            this.HasKey(p => p.Id);

            this.Property(p => p.MimeType).IsRequired().HasMaxLength(40);
            this.Property(p => p.SeoFilename).HasMaxLength(300);
        }
    }
}