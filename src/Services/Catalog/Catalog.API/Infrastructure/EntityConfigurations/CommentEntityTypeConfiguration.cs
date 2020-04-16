using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopOnContainers.Services.Catalog.API.Model;

namespace Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure.EntityConfigurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.Property(ci => ci.Id)
                .UseHiLo("comment_hilo")
                .IsRequired();

            builder.Property(ci => ci.Text)
                .IsRequired(true)
                .HasMaxLength(2000);

            builder.Property(ci => ci.UserId)
                .IsRequired(true)
                .HasMaxLength(400);

            builder.HasOne(ci => ci.CatalogItem)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogItemId);
        }
    }
}