using ERS.Aprendendo.TestesFuncionais.Dominio.Entidades;
using ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Contexto;

namespace ERS.Aprendendo.TestesFuncionais.Infra.BancoDeDados.Seed
{
    public class DataSeeder
    {
        private readonly ColecaoCarrinhosContexto _context;

        public DataSeeder(ColecaoCarrinhosContexto context)
        {
            _context = context;
        }

        public void Seed()
        { 
            Guid colecaoId = Guid.NewGuid();
            _context.Colecoes.Add(new Colecao(colecaoId, "Lengendary Ride"));
            _context.Carrinhos.AddRange(
                new Carrinho("'65 Pontiac GTO", DateTime.Parse("2000-01-01"), colecaoId),
                new Carrinho("'68 Cougar", DateTime.Parse("2000-01-01"), colecaoId)
            );

            colecaoId = Guid.NewGuid();
            _context.Colecoes.Add(new Colecao(colecaoId, "Car Culture Race Day"));
            _context.Carrinhos.AddRange(
                new Carrinho("'71 Plymouth GTX", DateTime.Parse("2000-01-01"), colecaoId),
                new Carrinho("1967 Camaro", DateTime.Parse("2000-01-01"), colecaoId)
            );

            colecaoId = Guid.NewGuid();
            _context.Colecoes.Add(new Colecao(colecaoId, "Entertainment Character Car"));
            _context.Carrinhos.AddRange(
                new Carrinho("1967 Camaro", DateTime.Parse("2000-01-01"), colecaoId),
                new Carrinho("Corvette Stingray", DateTime.Parse("2000-01-01"), colecaoId),
                new Carrinho("Corvette Stingray", DateTime.Parse("2000-01-01"), colecaoId),
                new Carrinho("T-Bucket", DateTime.Parse("2000-01-01"), colecaoId)
            );

            colecaoId = Guid.NewGuid();
            _context.Colecoes.Add(new Colecao(colecaoId, "Car Culture Q Case"));
            _context.Carrinhos.Add(new Carrinho("1957 Chevy Bel Air Convertible", DateTime.Parse("2000-01-01"), colecaoId));

            _context.SaveChanges();
            
        }
    }
}
