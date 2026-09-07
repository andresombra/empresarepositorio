# Instruções para agentes — Empresa

## Escopo e comunicação

- Estas instruções se aplicam ao repositório inteiro. Leia também eventuais AGENTS.md mais específicos na pasta da tarefa.
- Responda em português do Brasil, com explicações diretas e exemplos C# quando úteis.
- Execute somente o escopo solicitado. Pedidos de análise não autorizam alterações; pedidos de implementação autorizam os ajustes necessários e sua verificação.
- Não peça confirmação para etapas rotineiras já autorizadas. Peça esclarecimento quando uma regra de negócio desconhecida alterar o resultado.
- Informe arquivos alterados, motivo, validação realizada e impedimentos reais. Nunca declare build ou testes aprovados sem executá-los.

## Projeto e localização

- Repositório: andresombra/empresarepositorio.
- Caminho Windows informado pelo proprietário: C:\Users\User\source\repos\empresarepositorio.
- Trabalhe no checkout efetivamente disponível; não presuma acesso ao disco Windows em ambientes remotos.
- Solução principal: Empresa/GerEmpresas.sln.
- Backend: .NET 8, ASP.NET Core Web API, EF Core com Pomelo/MySQL, Mapster, FluentValidation e autenticação JWT.
- Testes: Empresa/Empresa.Test, xUnit e Moq.
- Frontend separado: Empresa/empresa.app, React, TypeScript e Vite. Não faz parte da solução .NET consultada.
- Empresa/csharp Empresa.WebApp contém um esboço; não trate essa pasta como um frontend Blazor completo.
- Confira global.json, projetos e configuração atuais antes de executar: estas notas não substituem o código.

## Arquitetura e responsabilidades

Referências de projeto esperadas:

| Camada | Referências permitidas entre os projetos principais |
| --- | --- |
| Empresa.Domain | Nenhuma |
| Empresa.Application | Empresa.Domain |
| Empresa.Infrastructure | Empresa.Domain |
| Empresa.Api | Empresa.Application e Empresa.Infrastructure |
| Empresa.Test | Projetos necessários ao tipo de teste; testes unitários da Application usam contratos e mocks |

- Domain concentra entidades, invariantes e contratos de repositórios e IUnitOfWork. Não introduza dependências de ASP.NET Core, EF Core ou Infrastructure no domínio.
- Application coordena casos de uso, DTOs, validação de entrada e mapeamento. Não acesse DbContext, DbSet ou implementações concretas de repositórios nela.
- Infrastructure implementa acesso ao banco, mapeamentos EF e persistência.
- API é a composição da aplicação: chama AddApplication e AddInfrastructure, configura autenticação, integração HTTP da validação e endpoints.
- Controllers cuidam da entrada HTTP, autorização e tradução do resultado para códigos HTTP; regras de negócio não pertencem a controllers.
- Application registra seus serviços e validadores; Infrastructure registra suas implementações. AddFluentValidationAutoValidation pertence à API.
- Preserve os namespaces existentes, inclusive GerEmpresa.Domain.Entities. Não renomeie o projeto inteiro durante uma tarefa pontual.
- Não adicione CQRS, MediatR, novos projetos ou camadas apenas por preferência arquitetural.

## Fluxo de trabalho

1. Verifique branch, git status e instruções locais antes de editar. Preserve alterações do usuário.
2. Leia o fluxo relevante: controller, contrato de aplicação, serviço, DTO/validador, entidade, contrato de repositório, implementação, mapeamento e testes.
3. Identifique comportamento esperado, consumidores e contratos públicos afetados.
4. Faça a menor mudança coerente. Atualize interfaces, injeção de dependências e testes quando necessário.
5. Revise o diff e execute as verificações pertinentes.
- Em análises, separe defeitos demonstráveis, riscos condicionais e melhorias opcionais; cite arquivos e métodos.
- Não deduza regras de negócio apenas pelo nome de campos. Verifique código, documentação e testes; declare o que não está confirmado.

## Persistência e Unit of Work

- Para consulta seguida de atualização no mesmo DbContext, preserve o tracking e altere a entidade carregada.
- Nesse fluxo, não chame Update novamente apenas para informar mudanças já rastreadas. Persista pelo IUnitOfWork.SaveChangesAsync.
- Repositório e Unit of Work devem compartilhar o mesmo DbContext scoped. Não crie um contexto separado para salvar a entidade consultada.
- Para entidades desconectadas, Update pode marcar todas as propriedades atualizáveis e entidades relacionadas. Avalie o contrato antes de usá-lo; para edição parcial, prefira carregar a entidade e atribuir somente campos permitidos.
- Não use Entry(item).CurrentValues.SetValues(item): copiar a própria instância para ela mesma é redundante.
- SaveChangesAsync salva todas as alterações pendentes do contexto, não somente uma entidade.
- O código ainda contém métodos de repositório que salvam diretamente. Não retire esses salvamentos globalmente sem revisar todos os consumidores.
- IUnitOfWork.SaveChangesAsync não exige uma transação explícita. Commit e Rollback existentes pressupõem BeginTransaction; não os use como substitutos diretos de SaveChangesAsync.
- Use transações explícitas quando a operação realmente exigir coordenação de múltiplas etapas. Não acrescente BeginTransaction em todo CRUD por padrão.
- Em consultas somente leitura, considere AsNoTracking, projeções e paginação conforme necessidade. Não aplique AsNoTracking indiscriminadamente em fluxos de edição.
- Não bloqueie código assíncrono com .Result ou .Wait(). Ao adicionar CancellationToken, propague-o por todo o fluxo afetado.
- Avalie concorrência otimista quando atualizações simultâneas puderem causar perda de dados; não presuma que Update resolve conflitos.

## DTOs, validação e domínio

- Prefira DTOs específicos para criação, atualização e resposta quando os campos permitidos diferirem.
- Evite mapear indiscriminadamente DTOs sobre entidades: ID, empresa proprietária, permissões e dados de auditoria exigem controle explícito.
- DataCadastro deve ser preservada em edições comuns, salvo requisito explícito de correção. Há código legado que a recebe na atualização; não altere esse contrato silenciosamente em uma refatoração de infraestrutura.
- Validações HTTP não garantem validação quando um serviço é chamado diretamente. Defina a proteção necessária no caso de uso e no domínio.
- Encapsule invariantes reais em métodos da entidade quando apropriado. Não mova atribuições para métodos vazios de significado apenas para aparentar DDD.
- Atribuição manual de poucos campos é aceitável; Mapster não é obrigatório.
- Diferencie não encontrado, entrada inválida, acesso negado e conflito. Preserve contratos existentes salvo mudança necessária e explicitada.
- Não capture Exception apenas para retornar false ou esconder falhas de banco. Não exponha detalhes internos ao cliente.

## Segurança e isolamento entre empresas

- O objetivo do sistema é multiempresas. Confira se o usuário pode consultar ou alterar a empresa solicitada; autenticação não equivale a autorização por empresa.
- Não confie somente em EmpresaId recebido do cliente para conceder acesso. Verifique os mecanismos existentes antes de implementar uma regra nova.
- Não imprima, copie para exemplos, testes ou logs, nem use credenciais encontradas no repositório para acessar serviços externos.
- Há histórico de segredos versionados. Não repita seus valores. Para novas configurações, use placeholders e mecanismos próprios como User Secrets, variáveis de ambiente ou cofre de segredos.
- Não acesse banco real, execute migrações, altere esquema ou faça publicação sem autorização correspondente.

## Testes e verificação

- Para mudanças comportamentais, adicione ou ajuste testes que detectem regressões reais, incluindo cenários negativos relevantes.
- Em atualizações rastreadas, teste que a entidade é alterada, o Unit of Work salva e empresa inexistente não provoca persistência.
- Mocks não comprovam SQL gerado, tracking, mapeamento nem rollback. Use testes de integração isolados quando essas propriedades precisarem de comprovação.
- Não utilize o appsettings real para testes de integração; use um banco de teste isolado e configuração própria.
- Não altere testes para simplesmente aceitar um comportamento incorreto, nem desative verificações para obter sucesso.

Comandos a partir da raiz do repositório:

```sh
dotnet --info
dotnet restore Empresa/GerEmpresas.sln
dotnet build Empresa/GerEmpresas.sln --no-restore --configuration Release
dotnet test Empresa/Empresa.Test/Empresa.Test.csproj --no-restore --configuration Release
git diff --check
```

- Confira o SDK de Empresa/global.json; na versão consultada ele é 8.0.419. Não altere o SDK fixado apenas para acomodar o ambiente do agente.
- Se o SDK ou dependências estiverem indisponíveis, reporte a limitação e faça revisão estática; não apresente isso como aprovação de build/testes.
- Há limitações preexistentes de pacotes, caminho absoluto de documentação XML e pipeline com etapas que toleram falhas. Investigue erros e diferencie os preexistentes dos introduzidos pela tarefa.
- Um pipeline verde não substitui conferir o resultado efetivo dos testes.

## Git e entrega

- Preserve a main. Use a branch de trabalho autorizada e informe qual foi usada; não fixe permanentemente o nome de uma branch neste arquivo.
- Não faça merge, force-push, reescrita de histórico ou deploy sem pedido correspondente.
- Nunca sobrescreva alterações alheias com reset --hard ou checkout de descarte.
- Não adicione node_modules, bin, obj, dist, publish, resultados de testes, arquivos locais ou segredos aos commits. Alguns desses itens já existem no histórico; não promova limpeza ampla sem escopo.
- Entregue um resumo do comportamento alterado, links dos arquivos ou commit e resultado real das verificações, com pendências relevantes.
