namespace SistemaContatos.Helper;

public interface IEmail
{
    bool Enviar(string email, string assuntoEmail, string mensagem);
}
