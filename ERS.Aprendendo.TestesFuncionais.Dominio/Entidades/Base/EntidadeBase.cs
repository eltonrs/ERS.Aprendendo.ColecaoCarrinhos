namespace ERS.Aprendendo.TestesFuncionais.Dominio.Entidades.Base
{
    public class EntidadeBase
    {
        public Guid Id { get; set; }

        public EntidadeBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
