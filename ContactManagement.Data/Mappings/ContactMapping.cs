using ContactManagement.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManagement.Data.Mappings;

public class ContactMapping : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(p => p.ID);

        builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.EntityContact).HasColumnType("INT").IsRequired();
        builder.Property(p => p.Email).HasColumnType("VARCHAR").IsRequired();

    }
}
