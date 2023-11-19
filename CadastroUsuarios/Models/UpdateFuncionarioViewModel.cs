namespace CadastroUsuarios.Models
{
    public class UpdateFuncionarioViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public long Salario { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Departamento { get; set; }
    }
}
