# PoupeMoney Core Web API

Backend do PoupeMoney para gerenciamento de assinaturas, contas bancárias e bancos. A solução usa ASP.NET Core, autenticação JWT integrada ao Firebase e persistência em SQL Server com Entity Framework Core.

## Estado atual

- .NET 10, fixado pelo `global.json` no SDK `10.0.302`.
- ASP.NET Core e Entity Framework Core `10.0.10`.
- Arquitetura separada em Domain, Application, Infrastructure e WebApi.
- API versionada com rotas no formato `/v1/{controller}`.
- Swagger disponível somente no ambiente `Development`.
- Testes em xUnit v3, com cobertura de código e análise de vulnerabilidades de dependências.
- Build configurado com analyzers, nullable e warnings tratados como erro.
- Logs estruturados com Serilog.

## Pré-requisitos

- SDK .NET `10.0.302` ou patch compatível com a política do `global.json`.
- SQL Server para os fluxos que acessam dados.
- Docker, Podman ou outro runtime OCI para construir a imagem.
- `dotnet-ef` 10 para trabalhar com migrations.

Execute os comandos deste documento a partir da pasta `backend`.

## Configuração local

Defina o ambiente de desenvolvimento no shell atual:

```bash
export ASPNETCORE_ENVIRONMENT=Development
```

A aplicação exige uma connection string chamada `DefaultConnection` e uma chave do Firebase. Para desenvolvimento, armazene esses valores com User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=core;User Id=sa;Password=change-me;TrustServerCertificate=True;" --project src/PoupeMoney.Core.WebApi/PoupeMoney.Core.WebApi.csproj
dotnet user-secrets set "Firebase:ApiKey" "replace-me" --project src/PoupeMoney.Core.WebApi/PoupeMoney.Core.WebApi.csproj
```

Não use os valores fictícios acima fora do desenvolvimento. O contrato definitivo de `.env` e a remoção das credenciais atualmente versionadas ainda estão acompanhados no plano de ação.

O warm-up da aplicação está habilitado por padrão e tenta acessar o banco. Para desabilitá-lo em uma execução isolada:

```bash
export WarmUp=false
```

## Restore e build

Restaure exatamente as versões registradas nos lock files e compile em Release:

```bash
dotnet restore PoupeMoney.Core.sln --locked-mode
dotnet build PoupeMoney.Core.sln -c Release --no-restore
```

## Execução local

```bash
dotnet run --project src/PoupeMoney.Core.WebApi/PoupeMoney.Core.WebApi.csproj --launch-profile Development
```

Com o perfil padrão, a API fica disponível em:

- `https://localhost:7021`
- `http://localhost:5262`
- Swagger: `https://localhost:7021/swagger`
- Health/warm-up: `https://localhost:7021/v1/application`

Os endpoints de negócio exigem um token JWT válido do projeto Firebase configurado.

## Testes

Execute toda a regressão, incluindo os testes de integração HTTP:

```bash
dotnet test PoupeMoney.Core.sln -c Release --no-restore
```

Para executar as suítes separadamente:

```bash
dotnet test test/PoupeMoney.Core.UnitTests/PoupeMoney.Core.UnitTests.csproj -c Release --no-restore
dotnet test test/PoupeMoney.Core.IntegrationTests/PoupeMoney.Core.IntegrationTests.csproj -c Release --no-restore
```

Os testes de integração atuais validam o startup e health check, a rejeição de acesso sem token e a geração do documento Swagger. A integração completa com SQL Server ainda está pendente.

## Auditoria de dependências

```bash
dotnet package list --project PoupeMoney.Core.sln --vulnerable --include-transitive
```

## Publicação

```bash
dotnet publish src/PoupeMoney.Core.WebApi/PoupeMoney.Core.WebApi.csproj -c Release --no-restore -o ./artifacts/publish
```

## Banco de dados e migrations

Ainda não existe uma baseline de migrations versionada. Para criar a migration inicial quando o modelo estiver revisado:

```bash
dotnet tool install --global dotnet-ef --version "10.*"
dotnet ef migrations add InitialCreate --startup-project src/PoupeMoney.Core.WebApi/PoupeMoney.Core.WebApi.csproj --project src/PoupeMoney.Core.Infrastructure.SqlServer/PoupeMoney.Core.Infrastructure.SqlServer.csproj
```

Para aplicar migrations existentes:

```bash
dotnet ef database update --startup-project src/PoupeMoney.Core.WebApi/PoupeMoney.Core.WebApi.csproj --project src/PoupeMoney.Core.Infrastructure.SqlServer/PoupeMoney.Core.Infrastructure.SqlServer.csproj
```

Revise a migration inicial antes de aplicá-la. Precisão monetária, índices, restrições únicas e comportamento de cascade ainda fazem parte do trabalho planejado.

## Container

O Dockerfile usa imagens SDK e runtime .NET 10. Construa a imagem a partir da pasta `backend`:

```bash
docker build --file src/PoupeMoney.Core.WebApi/Dockerfile --tag poupemoney-webapi:net10 .
```

O mesmo comando pode ser executado com `podman build`.

O `docker-compose.yml` ainda não representa o setup seguro e reproduzível desejado: contém credencial literal, não usa o contrato de `.env` e precisa de revisão dos health checks. Consulte o plano de ação antes de utilizá-lo como referência de ambiente.

## Estrutura da solução

```text
src/
  PoupeMoney.Core.Domain/
  PoupeMoney.Core.Application/
  PoupeMoney.Core.Infrastructure.SqlServer/
  PoupeMoney.Core.WebApi/
test/
  PoupeMoney.Core.Commons/
  PoupeMoney.Core.UnitTests/
  PoupeMoney.Core.IntegrationTests/
```

## Tecnologias

- ASP.NET Core 10
- Entity Framework Core 10 e SQL Server
- Firebase JWT
- CQS (Command Query Separation)
- xUnit v3, FluentAssertions, Moq e Bogus
- Coverlet
- Serilog
- Swashbuckle/OpenAPI
- Microsoft.CodeAnalysis.NetAnalyzers
