# ERS.Aprendendo.TestesFuncionais
WEB API para testes variados de implementações de testes de unidade.

## Padrão de nomenclatura

### CQRS / Mediator

Command Handler: `[Armazenar/Editar/Inserir/Alterar/Validar]...CommandHandler`
Command Request: `[Armazenar/Editar/Inserir/Alterar/Validar]...Command`
Command Response: `[Armazenar/Editar/Inserir/Alterar/Validar]...CommandResponse`

Query Handler: `...[Filtro|Paginacao]QueryHandler`
Query Request: `...[Filtro|Paginacao]Query`
Command Response: `...QueryResponse`

Exemplo Command

```cs

public class ArmazenarFooCommmand : IRequest<Guid>
{
  public Guid Id { get; set; }
  public string Nome { get; set; }
}

public class ArmazenarFooCommmandHandler : IRequestHandler<ArmazenarFooCommmand, Guid>
{
  public ArmazenarFooCommmandHandler() {}

  public async Task<Guid> Handle(
    ArmazenarFooCommmand request,
    CancellationToken cancellationToken
  )
  {
    ...
    return <o id (Guid) do objeto armazenado>;
  }
}

```
