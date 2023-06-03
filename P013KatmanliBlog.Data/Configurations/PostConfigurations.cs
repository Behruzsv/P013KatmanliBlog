using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.Data.Configurations
{
    internal class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post>builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Author).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ImageUrl).HasMaxLength(100);
            builder.Property(x => x.Comments).HasMaxLength(500);
            builder.HasOne(x => x.Category).WithMany(x => x.Posts).HasForeignKey(c => c.CategoryId);

        }
    }
}
