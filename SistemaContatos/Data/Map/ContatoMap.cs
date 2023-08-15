using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaContatos.Models;

namespace SistemaContatos.Data.Map;

public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
{
    public void Configure(EntityTypeBuilder<ContatoModel> builder)
    {
        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Nome).HasMaxLength(100).IsRequired();
        builder.HasOne(x => x.Usuario);

    }
}
