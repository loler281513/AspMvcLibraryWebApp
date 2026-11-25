using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspMvcLibraryWebApp.Models.ModelsConfigurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Genre)
                   .WithMany()
                   .HasForeignKey(a => a.GenreId);
        }
    }
}
