using Microsoft.EntityFrameworkCore;
using SistemaContatos.Models;

namespace SistemaContatos.Data;

public class BancoContext : DbContext
{
	public BancoContext(DbContextOptions<BancoContext> options) : base(options)
	{
		
	}
	public DbSet<ContatoModel> Contatos { get; set; }
	public DbSet<UsuarioModel> Usuarios { get; set; }
}
