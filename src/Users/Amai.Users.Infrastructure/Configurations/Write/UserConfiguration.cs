using Amai.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects.Ids;

namespace Amai.Users.Infrastructure.Configurations.Write;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
        
        builder.ComplexProperty(u => u.Email, tb =>
        {
            tb.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(Constants.EMAIL_MAX_LENGTH)
                .HasColumnName("email");
        });
        
        builder.Property(u => u.CreatedDate)
            .IsRequired()
            .HasColumnName("created_date");
        
        builder.ComplexProperty(u => u.Sub, tb =>
        {
            tb.Property(a => a.Value)
                .IsRequired()
                .HasMaxLength(Constants.SUB_MAX_LENGTH)
                .HasColumnName("sub");
        });
    }
}