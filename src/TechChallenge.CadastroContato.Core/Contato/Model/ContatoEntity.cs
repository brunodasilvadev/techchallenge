using System.Diagnostics.CodeAnalysis;

namespace TechChallenge.CadastroContato.Core.Contato.Model
{
    public class ContatoEntity
    {
        public int Id { get; }
        public string Nome { get; }
        public string Email { get; }
        public string TelefoneCompleto { get; }
        public short DddTelefone { get; private set; }
        public int NumeroTelefone { get; private set; }

        public ContatoEntity([NotNull] string nome, [NotNull] string email, [NotNull] string telefoneCompleto)
        {
            Nome = nome;
            Email = email;
            TelefoneCompleto = telefoneCompleto;
            DddTelefone = Convert.ToInt16(TelefoneCompleto[..2]);
            NumeroTelefone = Convert.ToInt32(TelefoneCompleto[2..]);
        }

        public ContatoEntity([NotNull] int id, [NotNull] string nome, [NotNull] string email, [NotNull] string telefoneCompleto)
            : this(nome, email, telefoneCompleto)
        {
            Id = id;
        }
    }
}