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

### Docker

# Redis Docker comando [ASBOLETO]:
docker pull redis:alpine
docker run --name regis-carrinhos -p 5002:6372 -d redis:alpine

# Redis Commander para acesso ao servidor [OBSOLETO]:
docker pull rediscommander/redis-commander

docker run --rm --name redis-commander -d \
  -p 8081:8081 \
  rediscommander/redis-commander:latest

# Docker compose

Ver o arquivo \Docker\redis-compose.yml

# Subir o arquivo:

docker compose -f redis-compose.yml up -d

# Acessando o Redis Commander:

http://127.0.0.1:8081/

