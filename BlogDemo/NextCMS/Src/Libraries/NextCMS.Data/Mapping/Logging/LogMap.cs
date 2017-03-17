using System.Data.Entity.ModelConfiguration;
using XCode.Core.Domain.Logging;

namespace XCode.Data.Mapping.Logging
{
    public partial class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("Common_Log");
            this.HasKey(t => t.Id);
            this.Property(t => t.ShortMessage).IsRequired();
            this.Property(t => t.IpAddress).HasMaxLength(200);

            this.Ignore(t => t.LogLevel);

            this.HasOptional(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
            .WillCascadeOnDelete(true);

        }
    }
}